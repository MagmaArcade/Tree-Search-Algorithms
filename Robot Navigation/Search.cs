using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;

namespace Robot_Navigation
{
    class Search
    {
        private Node pos, goalPos;
        private Map robotMap;
        private DrawUI UI = new DrawUI();
        private List<Node> listGoalPos = new List<Node>();
        private string fileName;

        public Node Pos { get { return pos; } }


        public Search(string initialState, List<string> goalStates, Map map, string filename)
        {
            // initaial State
            ParseString ifs = new ParseString(initialState);
            List<int> coordinate = ifs.getIntFromString();
            pos = new Node(coordinate[0], coordinate[1]);

            // Goal States
            foreach (string goalState in goalStates)
            {
                ifs = new ParseString(goalState);
                coordinate = ifs.getIntFromString();
                goalPos = new Node(coordinate[0], coordinate[1]);
                listGoalPos.Add(goalPos);
            }

            fileName = filename;

            robotMap = map;
        }




        // checks to see if the intial locaion is the final location
        public string IntialIsGoal()
        {
            foreach (Node goalPos in listGoalPos)
            {
                if ((pos.X == goalPos.X) && (pos.Y == goalPos.Y))
                {
                    return "The solution is the initial positition, no movement required";
                }
            }
            return null;
        }


        




        //Depth-First Search Function
        public string DfsSearch()
        {
            List<Node> frontier = new List<Node>();
            List<Node> exploredNode = new List<Node>();

            Node currentNode;

            frontier.Add(pos);

            while (frontier.Count != 0)
            {
                currentNode = frontier.Last();
                frontier.Remove(currentNode);
                exploredNode.Add(currentNode);

                Debug.WriteLine("Expanding: " + currentNode.Coordinate);
                UI.Draw(pos, listGoalPos, robotMap.WallsList, currentNode, robotMap.Width, robotMap.Length);
                Thread.Sleep(200);

                foreach (Grid grid in robotMap.Grids)
                {
                    //Verify the expanding grid is within the map
                    if ((currentNode.X == grid.Pos.X) && (currentNode.Y == grid.Pos.Y)) 
                    {
                        //Verify if adjacent nodes are available
                        if (grid.Paths.Count != 0) 
                        {
                            foreach (Path path in grid.Paths)     
                            {
                                if (!exploredNode.Any(x => x.X == path.Location.Pos.X && x.Y == path.Location.Pos.Y))
                                {
                                    path.Location.Pos.ParentNode = new Node(currentNode); // used to draw the path in map
                                    Debug.WriteLine(path.Location.Pos.Coordinate);
                                    if (!frontier.Any(x => x.X == path.Location.Pos.X && x.Y == path.Location.Pos.Y))
                                    {
                                        frontier.Add(path.Location.Pos); // add path to next available node list
                                    }
                                    else
                                    {
                                        frontier.RemoveAll(x => x.X == path.Location.Pos.X && x.Y == path.Location.Pos.Y); // delete repeate paths from frontier
                                        frontier.Add(path.Location.Pos); // add path to next available node list
                                    }
                                }

                            }

                        }



                            foreach (Node goalPos in listGoalPos)
                        {
                            if ((currentNode.X == goalPos.X) && (currentNode.Y == goalPos.Y))
                            {
                                return produceSolution("DFS", Pos, goalPos, exploredNode);
                            }
                        }
                    }
                   
                }
            }
            return "No solution";
        }







        //Breadth-First Search
        public string BfsSearch()
        {
            //Initialize data structure for frontier nodes and exploredNode nodes
            Queue<Node> frontier = new Queue<Node>();
            List<Node> exploredNode = new List<Node>();

            //Initialize expanding node
            Node currentNode;

            frontier.Enqueue(pos);

            while (frontier.Count != 0)
            {
                //Expand the first node of the queue
                currentNode = frontier.Dequeue();
                exploredNode.Add(currentNode);

                //Initialize UI
                UI.Draw(pos, listGoalPos, robotMap.WallsList, currentNode, robotMap.Width, robotMap.Length);
                Thread.Sleep(100);

                foreach (Grid grid in robotMap.Grids)
                {
                    //Verify the expanding grid is within the map
                    if ((currentNode.X == grid.Pos.X) && (currentNode.Y == grid.Pos.Y))
                    {
                        //Verify if adjacent nodes are available
                        if (grid.Paths.Count != 0)
                        {
                            grid.Paths.Reverse();
                            foreach (Path path in grid.Paths)
                            {
                                //Repeated state checking
                                if ((!exploredNode.Any(x => x.X == path.Location.Pos.X && x.Y == path.Location.Pos.Y)) && !frontier.Any(x => x.X == path.Location.Pos.X && x.Y == path.Location.Pos.Y))
                                {
                                    path.Location.Pos.ParentNode = new Node(currentNode);
                                    frontier.Enqueue(path.Location.Pos);
                                }
                            }
                        }
                        foreach (Node goalPos in listGoalPos)
                        {
                            //If solution is found
                            if ((currentNode.X == goalPos.X) && (currentNode.Y == goalPos.Y))
                            {
                                return produceSolution("BFS", Pos, goalPos, exploredNode);
                            }
                        }
                    }
                }
            }

            //If no solution is found
            return "No solution";
        }

        //Greedy Best First Search
        public string GbfsSearch()
        {
            //Initialize data structure for frontier nodes and exploredNode nodes
            List<Node> frontier = new List<Node>();
            List<Node> exploredNode = new List<Node>();

            Node currentNode;
            frontier.Add(pos);
            currentNode = frontier.First();

            while (frontier.Count != 0)
            {

                currentNode.DistanceToGoal = int.MaxValue; // initilizes the feild with the highest posible value
                foreach (Node goalPos in listGoalPos)
                {
                    foreach (Node path in frontier)    // calulates the H score for all frontiers
                    {
                        //Calculate heuristic value h(n)
                        int distanceToGoal = Math.Abs(path.X - goalPos.X) + Math.Abs(goalPos.Y - path.Y);

                       /* if (path.Y == 1)   //this is what im using to artificially change distance to goal
                        {
                            distanceToGoal = distanceToGoal + path.Y - 1;
                        }
                        // when this is set to 1 it changes the value for both (1,1) and (0,2)
                        // when this is set to 2 neither value changes */

                        // Find the lowest distance to any goal
                        if (distanceToGoal < path.DistanceToGoal)
                        {
                            path.DistanceToGoal = distanceToGoal;
                        }

          //this can be used for bug testing
                       //Console.WriteLine("position: " + path.X + ", " + path.Y + ".    distance to goal: " + distanceToGoal + " goal position:" + goalPos.X + ", " + goalPos.Y + ".       stored Distance field:" + path.DistanceToGoal);

                    }
                    //Console.ReadLine();


                    //If solution is found
                    if ((currentNode.X == goalPos.X) && (currentNode.Y == goalPos.Y))
                    {
                        return produceSolution("GBFS", Pos, goalPos, exploredNode);
                    }
                }

                //Sort the frontier list order by distance of the grid to goal
                frontier = frontier.OrderBy(s => s.DistanceToGoal).ToList();

                //Expand the first node of the priority list
                currentNode = frontier.First();
                frontier.Remove(frontier.First());
                exploredNode.Add(currentNode);

                //Initialize UI
                UI.Draw(pos, listGoalPos, robotMap.WallsList, currentNode, robotMap.Width, robotMap.Length);
                Thread.Sleep(200);


                foreach (Grid grid in robotMap.Grids)
                {
                    //Verify the expanding grid is within the map
                    if ((currentNode.X == grid.Pos.X) && (currentNode.Y == grid.Pos.Y))
                    {
                        //Verify if adjacent nodes are available
                        if (grid.Paths.Count != 0)
                        {
                            foreach (Path path in grid.Paths)
                            {
                                //Repeated state checking
                                if ((!exploredNode.Any(x => x.X == path.Location.Pos.X && x.Y == path.Location.Pos.Y)) && !frontier.Any(x => x.X == path.Location.Pos.X && x.Y == path.Location.Pos.Y))
                                {
                                    path.Location.Pos.ParentNode = new Node(currentNode);
                                    path.Location.Pos.DistanceToGoal = currentNode.DistanceToGoal;
                                    frontier.Insert(0, path.Location.Pos);
                                }
                            }
                        }
                    }
                }
            }
            return "No solution";
        }

        //A* Search
        public string AStarSearch()
        {
            //Initialize data structure for frontier nodes and exploredNode nodes
            List<Node> frontier = new List<Node>();
            List<Node> exploredNode = new List<Node>();

            //Initialize expanding node
            Node currentNode;
            frontier.Add(pos);

            //Initial stationary cost
            pos.GScore = 0;

            while (frontier.Count != 0)
            {
                //Sort the frontier list order by f(n)
                frontier = frontier.OrderBy(s => s.FScore).ToList();

                //Expand the first node of the priority list
                currentNode = frontier.First();
                frontier.Remove(frontier.First());

                //Add the expanded node to the visisted list
                exploredNode.Add(currentNode);

                //Initialize UI
                UI.Draw(pos, listGoalPos, robotMap.WallsList, currentNode, robotMap.Width, robotMap.Length);
                Thread.Sleep(100);

                foreach (Grid grid in robotMap.Grids)
                {
                    //Verify the expanding grid is within the map
                    if ((currentNode.X == grid.Pos.X) && (currentNode.Y == grid.Pos.Y))
                    {
                        //Verify if adjacent nodes are available
                        if (grid.Paths.Count != 0)
                        {
                            //grid.Paths.Reverse();
                            foreach (Path path in grid.Paths)
                            {
                                foreach (Node goalPos in listGoalPos)
                                {
                                    //Repeated state checking
                                    if ((!exploredNode.Any(x => x.X == path.Location.Pos.X && x.Y == path.Location.Pos.Y)) && !frontier.Any(x => x.X == path.Location.Pos.X && x.Y == path.Location.Pos.Y))
                                    {
                                        path.Location.Pos.ParentNode = new Node(currentNode);
                                        //Calculate g(n) as the cost so far from the start to the current node
                                        path.Location.Pos.GScore = currentNode.GScore + 1;

                                        //Calculate f(n) value
                                        path.Location.Pos.FScore = path.Location.Pos.GScore + Math.Abs(goalPos.X - path.Location.Pos.X) + Math.Abs(goalPos.Y - path.Location.Pos.Y);

                                        //Add adjacent nodes to the frontier list
                                        frontier.Insert(0, path.Location.Pos);
                                    }
                                }
                            }
                        }
                        //If solution is found
                        foreach (Node goalPos in listGoalPos)
                        {
                            if ((currentNode.X == goalPos.X) && (currentNode.Y == goalPos.Y))
                            {
                                return produceSolution("A*", Pos, goalPos, exploredNode);
                            }
                        }
                    }
                }
            }
            //If no solution is found
            return "No solution";
        }

        //Random Search
        public string RandomSearch()
        {
            //Initialize data structure for frontier nodes and exploredNode nodes
            List<Node> exploredNode = new List<Node>();

            Node currentNode = pos;
            exploredNode.Add(currentNode);

            while (true)
            {
                Debug.WriteLine("Expanding: " + currentNode.Coordinate);
                UI.Draw(pos, listGoalPos, robotMap.WallsList, currentNode, robotMap.Width, robotMap.Length);
                Thread.Sleep(200);

                // Select a random path to expand
                List<Path> frontier = new List<Path>();
                foreach (Grid grid in robotMap.Grids)
                {
                    if ((currentNode.X == grid.Pos.X) && (currentNode.Y == grid.Pos.Y))
                    {
                        if (grid.Paths.Count != 0)
                        {
                            foreach (Path path in grid.Paths)
                            {
                                if ((!exploredNode.Any(x => x.X == path.Location.Pos.X && x.Y == path.Location.Pos.Y)))
                                {
                                    frontier.Add(path);
                                }
                            }
                        }
                        break;
                    }
                }
                foreach (Node goalPos in listGoalPos)
                {
                    //If solution is found
                    if ((currentNode.X == goalPos.X) && (currentNode.Y == goalPos.Y))
                    {
                        return produceSolution("RS", Pos, goalPos, exploredNode);
                    }
                }

                if (frontier.Count == 0)
                {
                    return "No solution";
                }

                // selects a random next node from the frontier
                Path randomPath = frontier[new Random().Next(frontier.Count)];
                randomPath.Location.Pos.ParentNode = new Node(currentNode);
                currentNode = randomPath.Location.Pos;
                exploredNode.Add(currentNode);

            }
        }

        public string produceSolution(string method, Node initial, Node child, List<Node> expanded)
        {
            string solution = "";
            List<Node> path = new List<Node>();

            expanded.Reverse();
            foreach (Node pos in expanded)
            {
                if ((pos.X == child.X) && (pos.Y == child.Y))
                    path.Add(pos);

                if (path.Count() != 0)
                {
                    if ((path.Last().ParentNode.X == pos.X) && (path.Last().ParentNode.Y == pos.Y))
                    {
                        path.Add(pos);
                    }
                }
            }

            path.Reverse();
            for (int i = 0; i < path.Count(); i++)
            {
                if (i == path.Count() - 1)
                {
                    break;
                }
                if (path[i + 1].Y == path[i].Y - 1)
                {
                    solution = solution + "up; ";
                }
                if (path[i + 1].X == path[i].X - 1)
                {
                    solution = solution + "left; ";
                }
                if (path[i + 1].Y == path[i].Y + 1)
                {
                    solution = solution + "down; ";
                }
                if (path[i + 1].X == path[i].X + 1)
                {
                    solution = solution + "right; ";
                }
            }


            UI.DrawPath(pos, goalPos, robotMap.WallsList, robotMap.Width, robotMap.Length, path);

            return fileName + "\n" + method + ": " + expanded.Count() + " \n" + solution;
        }
    }
}

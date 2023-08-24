using System;

namespace Robot_Navigation
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = args[0];
            string searchStrategy = args[1].ToLower();

            //Reads in the text file data set: initaial state, goal state, map, walls
            //GenerateMap generateMap = new GenerateMap("RobotNav-test.txt");                //for easier degugging
            GenerateMap generateMap = new GenerateMap(filename);   

            //generate the map 
            Map Map = new Map(generateMap.Map, generateMap.Walls);

            //Pass in data for search
            Search search = new Search(generateMap.InitialState, generateMap.GoalStates, Map, filename);


            /*debugging
                generateMap.printInfo();
                Map.printMap();
                Console.ReadLine(); */
            //string algSearch = "rs";

            string IntialIsGoal = search.IntialIsGoal();
            if (IntialIsGoal == null)
            {
                //calls the required search algorithm
                // switch (algSearch)                                                          //for easier degugging
                switch (searchStrategy.ToLower())
                {
                    case "dfs":
                        Console.WriteLine(search.DfsSearch());
                        break;
                    case "bfs":
                        Console.WriteLine(search.BfsSearch());
                        break;
                    case "gbfs":
                        Console.WriteLine(search.GbfsSearch());
                        break;
                    case "as":
                        Console.WriteLine(search.AStarSearch());
                        break;
                    case "rs":
                        Console.WriteLine(search.RandomSearch());
                        break;
                    default:
                        Console.WriteLine("No search method called " + args[1]);
                        break;
                }
            }
            else
            {
                Console.WriteLine(IntialIsGoal);
            }
            Console.ReadLine();
        }
    }
}

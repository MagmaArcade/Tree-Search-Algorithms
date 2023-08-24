using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Navigation
{
    class DrawUI
    {
        public DrawUI() { }

        public void Draw(Node initial, List<Node> listGoalPos, List<Grid> wall, Node visitedNode, int mapWidth, int mapLength)
        {
            Console.Clear();
            bool wallDrawn = false;
            bool goalDrawn = false;


            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapLength; j++)
                {
                    if ((initial.X == j) && (initial.Y == i))
                    {
                        Console.Write("|i");
                        continue;
                    }

                    foreach (Node goal in listGoalPos)
                    {
                        if ((goal.X == j) && (goal.Y == i))
                        {
                            Console.Write("|g");
                            goalDrawn = true;
                            break;
                        }
                        goalDrawn = false;
                    }

                    if ((visitedNode.X == j) && (visitedNode.Y == i))
                    {
                        Console.Write("|x");
                        continue;
                    }

                    foreach (Grid grid in wall)
                    {
                        if ((grid.IsWall == true) && (grid.Pos.X == j) && (grid.Pos.Y == i))
                        {
                            Console.Write("|w");
                            wallDrawn = true;
                            break;
                        }
                        wallDrawn = false;
                    }

                    if (wallDrawn == false && goalDrawn == false)
                        Console.Write("| ");
                }
                Console.WriteLine("|");
            }
        }

        public void DrawPath(Node initial, Node goal, List<Grid> wall, int mapWidth, int mapLength, List<Node> path)
        {
            Console.Clear();
            bool wallDrawn = false;
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapLength; j++)
                {
                    if ((initial.X == j) && (initial.Y == i))
                    {
                        Console.Write("|i");
                        continue;
                    }

                    if ((goal.X == j) && (goal.Y == i))
                    {
                        Console.Write("|g");
                        continue;
                    }

                    if (path.Any(x => x.X == j && x.Y == i))
                    {
                        Console.Write("|x");
                        continue;
                    }

                    foreach (Grid grid in wall)
                    {
                        if ((grid.IsWall == true) && (grid.Pos.X == j) && (grid.Pos.Y == i))
                        {
                            Console.Write("|w");
                            wallDrawn = true;
                            break;
                        }
                        wallDrawn = false;
                    }

                    if (wallDrawn == false)
                        Console.Write("| ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine();
        }
    }
}

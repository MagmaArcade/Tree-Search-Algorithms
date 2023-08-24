using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Navigation
{
    class Map
    {
        List<Grid> grids = new List<Grid>();
        private int width, length;
        private List<string> wall;
        private List<Grid> wallsList = new List<Grid>();


        public List<Grid> Grids { get { return grids;} }
        public int Width { get { return width; }}
        public int Length { get { return length; }}
        public List<Grid> WallsList { get { return wallsList; } }


        //Map constructor
        public Map(string mapSize, List<string> walls)
        {
            ParseString num = new ParseString(mapSize);
            List<int> coordinate = num.getIntFromString();

            width = coordinate[0];
            length = coordinate[1];
            wall = walls;
            drawMap();
        }

        //Draw the map
        public void drawMap()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                { // draws the map with grid for width by length in grid list giving each grid a 2d coordinate
                    grids.Add(new Grid(new Node(j, i), false));
                }
            }

            for (int i = 0; i < wall.Count; i++)
            {
                drawWall(wall[i]);
            }

            drawPath();
        }

        //Populate adjacent available paths for grid
        public void drawPath()
        {
            for (int i = 0; i < grids.Count; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if ((i >= j * length) && (i < (j + 1) * length - 1))
                    {
                        grids[i].Paths.Add(new Path(grids[i + 1])); // right
                    }
                }

                if (i < length * width - length)
                {
                    if (!grids[i + length].IsWall)
                    {
                        grids[i].Paths.Add(new Path(grids[i + length])); // down
                    }
                }

                for (int j = 0; j < width; j++)
                {
                    if ((i > j * length) && (i < (j + 1) * length))
                    {
                        grids[i].Paths.Add(new Path(grids[i - 1])); // left
                    }
                }

                if (i > length - 1)
                {
                    if (!grids[i - length].IsWall)
                    {
                        grids[i].Paths.Add(new Path(grids[i - length])); // up
                    }
                }

            }

            //Remove paths that are obstacles
            foreach (Grid grid in grids)
            {
                for (int i = 0; i < grid.Paths.Count; i++)
                {
                    if (grid.Paths[i].Location.IsWall == true)
                    {
                    grid.Paths.Remove(grid.Paths[i]);
                    }
                }
            }
        }

        //Draw obstacles
        public void drawWall(string oneWall)
        {
            ParseString ifs = new ParseString(oneWall);
            List<int> coordinate = ifs.getIntFromString();

            for (int j = coordinate[1]; j < coordinate[1] + coordinate[3]; j++)
            {
                for (int i = coordinate[0]; i < coordinate[0] + coordinate[2]; i++)
                {
                    int index = grids.FindIndex(x => (x.Pos.X == i) && (x.Pos.Y == j));
                    grids[index].IsWall = true;
                }
            }

            foreach (Grid grid in grids)
            {
                if (grid.IsWall == true)
                {
                    WallsList.Add(grid);
                }
            }
        }


        //Print map info
        public void printMap()
        {
            foreach (Grid grid in grids)
            {
                Console.WriteLine("Grid: ({0},{1}), wall: {2}", grid.Pos.X, grid.Pos.Y, grid.IsWall);
                Console.WriteLine("Containing: ");
                foreach (Path path in grid.Paths)
                {
                    Console.WriteLine(path.Location.Pos.Coordinate);
                }
                Console.WriteLine("");
            }
        }
    }
}

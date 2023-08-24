using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Navigation
{
    class Grid
    {
        public Node Pos { get; }
        public bool IsWall { get; set; }
        public List<Path> Paths { get; set; } = new List<Path>();

        public Grid(Node pos, bool isWall)
        {
            Pos = pos;
            IsWall = isWall;
        }
    }
}

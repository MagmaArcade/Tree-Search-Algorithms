using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Navigation
{
    class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int DistanceToGoal { get; set; }
        public string Coordinate => $"({X},{Y})";
        public double FScore { get; set; }
        public double GScore { get; set; }
        public Node ParentNode { get; set; }

        public Node(int posX, int posY)
        {
            X = posX;
            Y = posY;
        }

        public Node(Node parent) : this(parent.X, parent.Y) { }
    }
}

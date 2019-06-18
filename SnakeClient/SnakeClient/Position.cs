using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeClient
{
    class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Position()
        {

        }
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsInBounds(int x1, int y1, int x2, int y2)
        {
            if(X >= x1 && Y >= y1 && X <= x2 && Y <= y2)
                return true;
            return false;
        }
    }
}

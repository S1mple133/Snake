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

        public bool IsInBounds(float x1, float y1, float x2, float y2)
        {
            if(X >= x1 && Y >= y1 && X <= x2 && Y <= y2)
                return true;
            return false;
        }
    }
}

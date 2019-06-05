using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeClient
{
    public enum Direction
    {
        NONE, LEFT, RIGHT, UP, DOWN
    }
    class Player
    {
        public int ID { get; set; }
        public Position Position { get; set; }
        public int Length { get; set; }

        public Direction Dir { get; set; }

        public Player()
        {
            Position = new Position();
            Dir = Direction.NONE;
        }

        public Player(int id, int length, int x, int y)
        {
            ID = id;
            Position = new Position(x, y);
            Length = length;
            Dir = Direction.NONE;
        }

        public void UpdatePosition()
        {
            switch (Dir)
            {
                case Direction.LEFT:
                    Position.X = Position.X != 0 ? Position.X - 1 : Util.GRID_SIZE - 1;
                    break;
                case Direction.RIGHT:
                    Position.X = Position.X != Util.GRID_SIZE - 1 ? Position.X + 1 : 0;
                    break;
                case Direction.UP:
                    Position.Y = Position.Y != 0 ? Position.Y - 1 : Util.GRID_SIZE - 1;
                    break;
                case Direction.DOWN:
                    Position.Y = Position.Y != Util.GRID_SIZE - 1 ? Position.Y + 1 : 0;
                    break;
            }
        }

    }
}

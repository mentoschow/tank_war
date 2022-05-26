using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank_War
{
    enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    class MovableObject : GameObject
    {
        private Object _lock = new Object();
        public Bitmap BitmapUp { get; set; }
        public Bitmap BitmapDown { get; set; }
        public Bitmap BitmapLeft { get; set; }
        public Bitmap BitmapRight { get; set; }

        public int MoveSpeed { get; set; }
        

        private Direction dir;
        public Direction Dir { get { return dir; }
            set
            {
                dir = value;
                Bitmap bitmap = null;
                switch (dir)
                {
                    case Direction.UP:
                        bitmap = BitmapUp;
                        break;
                    case Direction.DOWN:
                        bitmap = BitmapDown;
                        break;
                    case Direction.LEFT:
                        bitmap = BitmapLeft;
                        break;
                    case Direction.RIGHT:
                        bitmap = BitmapRight;
                        break;
                    default:
                        break;
                }
                lock (_lock)
                {                   
                    Width = bitmap.Width;
                    Height = bitmap.Height;
                }                               
            }
        }

        protected override Image GetImage()
        {
            Bitmap bitmap = null;
            switch (Dir)
            {
                case Direction.UP:
                    bitmap = BitmapUp;
                    break;
                case Direction.DOWN:
                    bitmap = BitmapDown;
                    break;
                case Direction.LEFT:
                    bitmap = BitmapLeft;
                    break;
                case Direction.RIGHT:
                    bitmap = BitmapRight;
                    break;
                default:
                    break;
            }
            bitmap.MakeTransparent(Color.Black);
            return bitmap;
        }

        public override void DrawSelf()
        {
            lock (_lock)
            {
                base.DrawSelf();
            }
        }
    }
}

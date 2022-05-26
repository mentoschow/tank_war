using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank_War
{
    abstract class GameObject
    {
        //坐标属性
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool isDestroy { get; set; }

        protected abstract Image GetImage();

        public virtual void DrawSelf()
        {
            Graphics g = GameFramework.g;
            g.DrawImage(GetImage(), new Point(X, Y));
        }

        public virtual void Update()
        {
            DrawSelf();
        }

        public Rectangle GetRectangle()  //用于碰撞检测
        {
            Rectangle rectangle = new Rectangle(X, Y, Width, Height);
            return rectangle;
        }
    }
}

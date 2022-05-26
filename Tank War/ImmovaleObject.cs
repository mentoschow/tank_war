using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank_War
{
    class ImmovaleObject : GameObject
    {
        private Image img;
        public Image Img { get { return img; } 
            set 
            {
                img = value;
                Width = img.Width;
                Height = img.Height;
            } 
        }

        public ImmovaleObject(int x,int y,Image img)
        {
            X = x;
            Y = y;
            Img = img;
        }

        protected override Image GetImage()
        {
            return Img;
        }
    }
}

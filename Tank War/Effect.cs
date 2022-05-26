using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank_War.Properties;

namespace Tank_War
{
    class Effect : GameObject
    {
        private int playSpeed = 2;
        private int playTimer = -1;
        private int index = 0;
        private Bitmap[] bmpArray = new Bitmap[]
        {
            Resources.EXP1,
            Resources.EXP2,
            Resources.EXP3,
            Resources.EXP4,
            Resources.EXP5
        };
        public Effect(int x, int y)
        {
            foreach(Bitmap bmp in bmpArray)
            {
                bmp.MakeTransparent(Color.Black);
            }
            X = x - bmpArray[0].Width / 2;
            Y = y - bmpArray[0].Height / 2;
            isDestroy = false;
        }
        protected override Image GetImage()
        {
            if (index > 4) 
            {
                isDestroy = true;
                return bmpArray[4];
            }               
            return bmpArray[index];
        }

        public override void Update()
        {
            playTimer++;
            index = (playTimer - 1) / playSpeed;
            base.Update();
        }
    }
}

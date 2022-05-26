using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tank_War.Properties;

namespace Tank_War
{
    public partial class Form1 : Form
    {
        private Thread t;
        private static Graphics windowG;
        private static Bitmap tempBmp;

        public Form1()  //构造函数
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;  //把窗口设置在屏幕中心

            //获取游戏画布
            windowG = this.CreateGraphics();           

            //前后帧缓冲原理，解决画面闪烁
            tempBmp = new Bitmap(450, 450);
            Graphics bmpG = Graphics.FromImage(tempBmp);
            GameFramework.g = bmpG;

            //游戏主线程:用于实时更新游戏
            t = new Thread(new ThreadStart(GameMainThread));
            t.Start();            
    }        

        private static void GameMainThread()
        {
            //游戏框架
            GameFramework.Start();
            int sleepTime = 1000 / 60;
            while (true)
            {
                //clear background color
                GameFramework.g.Clear(Color.Black);
                //draw in temp bmp
                GameFramework.Update();  //60 times per second

                //swap to windowG
                windowG.DrawImage(tempBmp, 0, 0);

                Thread.Sleep(sleepTime);
            }
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)  //绘制函数
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)  //窗口关闭事件
        {
            t.Abort();  //关闭游戏主线程
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameFramework.KeyDown(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameFramework.KeyUp(e);
        }
    }
}

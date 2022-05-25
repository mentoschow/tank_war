using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank_War
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;  //把窗口设置在屏幕中心          
        }

        private void Form1_Paint(object sender, PaintEventArgs e)  //绘制函数
        {
            //GDI:Graphics Device Interface
            Graphics graphics = this.CreateGraphics();  //画布
            Pen pen = new Pen(Color.Black);  //画笔
            graphics.DrawLine(pen, new Point(50, 50), new Point(100, 100));  //画直线

            graphics.DrawString("abc", new Font("Arial", 20), new SolidBrush(Color.Black), new Point(300, 300));  //写字体
        }       
    }
}

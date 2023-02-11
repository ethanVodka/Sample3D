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

namespace Sample3D
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private struct Pos
        {
            public float x;
            public float y;
            public float z;
        }

        //中心座標
        private static readonly PointF Center = new PointF(200, 200);

        //オリジナル座標
        private static Pos[] op =
        {
            //手前
            new Pos()
            {
                x = 100,
                y = 100,
                z = 0,
            }
            , new Pos()
             {
                x=100,
                y=300,
                z=0,
            }
            , new Pos()
            {
                x = 300,
                y = 300,
                z = 0,
            }
            , new Pos()
            {
                x = 300,
                y = 100,
                z = 0,
            }
            //奥
            , new Pos()
            {
                x = 150,
                y = 150,
                z = 0,
            }
            , new Pos()
             {
                x=150,
                y=250,
                z=0,
            }
            , new Pos()
            {
                x = 250,
                y = 250,
                z = 0,
            }
            , new Pos()
            {
                x = 250,
                y = 150,
                z = 0,
            }
        };



        static float Deg { get; set; } = 0;

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void Draw3D()
        {
            Bitmap objBmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics objGrp = Graphics.FromImage(objBmp);
            Pen objPen = new Pen(Color.Red, 2.0f);


            //変動座標定義
            Pos[] p = new Pos[8];

            float theta = Deg / 180;
            float s = (float)Math.Sin(theta);
            float c = (float)Math.Cos(theta);


            for (int i = 0; i < op.Length; i++)
            {
                p[i].x = op[i].x * c + op[i].y * -s;
                p[i].y = op[i].y;
                p[i].z = op[i].x * s + op[i].z * c;

                p[i].z += 2;

                ////z座標により奥行見せる
                p[i].x /= p[i].z;
                p[i].y /= p[i].z;
            }

            Deg += 5;
            for (int i = 0; i < op.Length / 2; i++)
            {
                int j = (i + 1) % 4;
                objGrp.DrawLine(objPen, p[i].x + Center.X, p[i].y + Center.Y, p[j].x + Center.X, p[j].y + Center.Y);
                objGrp.DrawLine(objPen, p[i + 4].x + Center.X, p[i + 4].y + Center.Y, p[j + 4].x + Center.X, p[j + 4].y + Center.Y);
                objGrp.DrawLine(objPen, p[i].x + Center.X, p[i].y + Center.Y, p[i + 4].x + Center.X, p[i + 4].y + Center.Y);
            }
            pictureBox.Image = objBmp;

            //ガベージ処理
            objGrp.Dispose();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            //Draw3D();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Draw3D();
        }
    }
}

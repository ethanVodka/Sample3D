using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rotate3D
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        //中心座標
        private float X { get; set; } = 0;
        private float Y { get; set; } = 0;

        //角度
        private float Deg { get; set; } = 0;

        //サイン
        private float Sin { get; set; } = 0;

        private void MainForm_Load(object sender, EventArgs e)
        {
            X = pictureBox.Width / 2;
            Y = pictureBox.Height / 2;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            /*
             * 繰り返し描画処理
             */

            Draw3D();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }

        //描画処理
        private void Draw3D()
        {
            Vector[] op = Vector.Init(X, Y);
            Vector[] p = op;
            //Bitmap作成
            Bitmap objBmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics objGrp = Graphics.FromImage(objBmp);


            //各点を回転させた座標取得
            for (int i = 0; i < p.Length; i++)
            {
                PointF P = RotatePoint(new PointF(op[i].x, op[i].y), new PointF(X, Y), Deg);
                p[i].x = P.X;
                p[i].y = P.Y;
                p[i].z = p[i].z;

                p[i].z += 2;

                p[i].x /= p[i].z;
                p[i].y /= p[i].z;
            }

            for (int i = 0; i < p.Length / 2; i++)
            {
                int j = (i + 1) % 4;
                objGrp.DrawLine(Pens.Red, p[i].x, p[i].y, p[j].x, p[j].y);
                objGrp.DrawLine(Pens.Red, p[i + 4].x, p[i + 4].y, p[j + 4].x, p[j + 4].y);
                objGrp.DrawLine(Pens.Red, p[i].x, p[i].y, p[i + 4].x, p[i + 4].y);
            }


            pictureBox.Image = objBmp;

            //繰り返し処理のため角度10度ずつ増やす
            Deg += 5f;
        }

        //点P の周りを回転させる
        private PointF RotatePoint(PointF pointToRotate, PointF centerPoint, float angleInDegrees)
        {
            float angleInRadians = angleInDegrees * (float)(Math.PI / 180);
            float cosTheta = (float)Math.Cos(angleInRadians);
            float sinTheta = (float)Math.Sin(angleInRadians);

            Sin = sinTheta;
            
            return new PointF
            {
                X = cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X,

                Y = sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y
            };
        }
    }
}

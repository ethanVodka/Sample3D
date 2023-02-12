using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotate3D
{
    public class Vector
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        // コンストラクター
        Vector(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }


        /*
         * 下の絵のように立方体の座標を定義する
         *  (x,y) を中心として囲う
         */


        //               //
        //               //
        //     (1)       //         (4)
        //               //
        //         (5)   //    (8)
        //               //
        //               //
        //  //////////////////////////////
        //               // (x, y)
        //               //
        //               //
        //         (6)   //    (7)
        //               //
        //      (2)      //         (3)
        //               //


        public static Vector[] Init(float x, float y)
        {
            Vector[] vectors =
            {
                //中心基準で正方形で囲うように作成する
                //手前
                new Vector(x - 100, y - 100, 1), //(1)
                new Vector(x - 100, y + 100, 1), //(2)
                new Vector(x + 100, y + 100, 1), //(3)
                new Vector(x + 100, y - 100, 1), //(4)
                
                //奥
                new Vector(x - 100, y - 100, -1), //(5)
                new Vector(x - 100, y + 100, -1), //(6)
                new Vector(x + 100, y + 100, -1), //(7)
                new Vector(x + 100, y - 100, -1), //(8)
            };

            return vectors;
        }
    }
}

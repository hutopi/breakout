using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace breakout {
    class Circle {

        private int x;
        public int X {
            get { return x; }
            set { x = value; }
        }
        private int y;
        public int Y {
            get { return y; }
            set { y = value; }
        }
        private double radius;
        public double Radius {
            get { return radius; }
            set { radius = value; }
        }

        public Circle(int x, int y, double radius) {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }
        public bool IntersectsRec(Rectangle rectangle) {


            int circleDistanceX = Math.Abs(this.x - (rectangle.X + rectangle.Width/2));
            int circleDistanceY = Math.Abs(this.y - (rectangle.Y + rectangle.Height/2));

            if (circleDistanceX > (rectangle.Width / 2) + this.radius) { return false; }
            if (circleDistanceY > (rectangle.Height / 2 )+ this.radius) { return false; }

            if (circleDistanceX <= (rectangle.Width / 2)) { return true; }
            if (circleDistanceY <= (rectangle.Height / 2) ) { return true; }

            double cornerDistance_sq = (circleDistanceX - rectangle.Width / 2) ^ 2 + (circleDistanceY - rectangle.Height / 2) ^ 2;

            return (cornerDistance_sq <= (Math.Pow(this.radius,2.0)));
        }
        
    }
}

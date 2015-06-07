// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Thomas Fossati
// Created          : 06-07-2015
//
// Last Modified By : Thomas Fossati
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="Circle.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//      Breakout is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Microsoft.Xna.Framework;

/// <summary>
/// The breakout namespace.
/// </summary>
namespace breakout.Util {
    /// <summary>
    /// Class Circle.
    /// Used to describe circular hitboxes.
    /// </summary>
    public class Circle {

        /// <summary>
        /// The x position.
        /// </summary>
        private int x;
        /// <summary>
        /// Gets or sets the x position.
        /// </summary>
        /// <value>The x.</value>
        public int X {
            get { return x; }
        }

        /// <summary>
        /// The y position.
        /// </summary>
        private int y;
        /// <summary>
        /// Gets or sets the y position.
        /// </summary>
        /// <value>The y.</value>
        public int Y {
            get { return y; }
        }

        /// <summary>
        /// The radius
        /// </summary>
        private double radius;
        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>The radius.</value>
        public double Radius {
            get { return radius; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="radius">The radius.</param>
        public Circle(int x, int y, double radius) {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }

        /// <summary>
        /// Intersection checking between a Circle and and Rectangle (from XNA framework)
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns><c>true</c> if the circle intersects the rectangle, <c>false</c> otherwise.</returns>
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

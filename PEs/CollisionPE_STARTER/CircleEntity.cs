using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollisionPE_STARTER
{
    /// <summary>
    /// CircleEntity represents any circular object in the game.
    /// Defined by a center and a radius.
    /// </summary>
    public class CircleEntity
    {
        // Fields
        private Texture2D texture;
        private Vector2 center;
        private int radius;

        /// <summary>
        /// Center point of this Circle
        /// </summary>
        public Vector2 Center
        {
            get { return center; }
        }

        /// <summary>
        /// Radius of this Circle
        /// </summary>
        public int Radius 
        { 
            get { return radius; }
        }

        /// <summary>
        /// Retrieve and/or change the X component of this circle's center
        /// </summary>
        public float X
        {
            get { return center.X; }
            set { center.X = value; }
        }

        /// <summary>
        /// Retrieve and/or change the Y component of this circle's center
        /// </summary>
        public float Y
        {
            get { return center.Y; }
            set { center.Y = value; }
        }

        /// <summary>
        /// Instantiates a CircleEntity
        /// </summary>
        /// <param name="texture">Image to use when rendering</param>
        /// <param name="x">X position of this circle's center</param>
        /// <param name="y">Y position of this circle's center</param>
        /// <param name="radius">Radius of this circle</param>
        public CircleEntity(Texture2D texture, int x, int y, int radius)
        {
            this.texture = texture;
            this.radius = radius;
            this.center = new Vector2(x, y); 
        }

        /// <summary>
        /// Checks if the player circle and any otherCircle's are intersecting.
        /// </summary>
        /// <param name="otherCircle">Other circles in the scene</param>
        /// <returns>If the circles are being intersected or not</returns>
        public bool IntersectsWith(CircleEntity otherCircle)
        {
            // computes the squared distance between the two circle centers
            float deltaX = X - otherCircle.X;
            float deltaY = Y - otherCircle.Y;
            double distanceSquared = Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2);

            // computes the sum of the radius squared
            int radiusSum = Radius + otherCircle.Radius;
            int radiusSumSquared = radiusSum * radiusSum;

            // returns the collision occurs if the squared distance is less than or equal to the squared radius sum
            return distanceSquared <= radiusSumSquared;
        }



        /// <summary>
        /// Draws this CircleEntity to the game window.
        /// </summary>
        /// <param name="spriteBatch">Reference to the SpriteBatch object in Game1.</param>
        /// <param name="tint">The color of this game object.</param>
        public void Draw(SpriteBatch spriteBatch, Color tint)
        {
            // ********************************************************************
            // Why do we need a Rectangle in a Circle class? Because the Draw method requires Rectangles!
            // Draw needs to know which area to render to the game window, and where the object 
            //   should be drawn at proper scale.
            // We *could* make this a field of the class, but we'd need to update it every frame that the
            //   circle moves/changes position. That requires a lot of bookkeeping and multiple
            //   opportunities for missed mathematics, or a Rectangle that's out of sync with the center position.
            // Instead, one option is to quickly calculate the bounds of this Rectangle every frame it's drawn.
            // (What do I mean by 'quickly'? Structs are fast to access, fast to instantiate, and fast
            //   to delete from the stack when they are out of scope. For 20 Circles, this is not a big deal.
            //   However, if you had millions of circles in your game, I recommend making them a field of the class
            //   and updating the Rectangles' X, Y, width, and/or height values when necessary.)

            Rectangle circleRect = new Rectangle(
                (int)center.X - Radius,
                (int)center.Y - Radius,
                Radius * 2,
                Radius * 2);
            // ********************************************************************


            // ********************************************************************
            // TODO: Use the DebugLib class to draw a rectangle outline around the circleRect.
            // This is helpful while debugging to ensure that the circle's center coordinates
            //   and Rectangle coordinates & size are accurate!
            // ********************************************************************


            // Draw this circle at the right spot with the correct size.
            spriteBatch.Draw(texture, circleRect, tint);
        }
    }
}

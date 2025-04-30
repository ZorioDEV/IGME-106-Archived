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
    /// SquareEntity represents any square object in the game.
    /// Defined by an (X,Y) and a side length.
    /// </summary>
    public class SquareEntity
    {
        // Fields
        private Texture2D texture;
        private Rectangle squareRect;

        /// <summary>
        /// Reference to the bounds of this object. Cannot be changed.
        /// </summary>
        public Rectangle SquareRect 
        { 
            get { return squareRect; } 
        }

        /// <summary>
        /// X position of the upper-left corner of the SquareEntity
        /// </summary>
        public int X 
        { 
            get { return squareRect.X; } 
            set { squareRect.X = value; } 
        }

        /// <summary>
        /// Y position of the upper-left corner of the SquareEntity
        /// </summary>
        public int Y 
        { 
            get { return squareRect.Y; } 
            set { squareRect.Y = value; } 
        }

        /// <summary>
        /// Instantiates a new SquareEntity.
        /// </summary>
        /// <param name="texture">Image to use when rendered</param>
        /// <param name="x">X position of the upper-left corner</param>
        /// <param name="y">Y position of the upper-left corner</param>
        /// <param name="width">Width of the square</param>
        /// <param name="height">Width of the square</param>
        public SquareEntity(Texture2D texture, int x, int y, int width, int height)
        {
            this.texture = texture;
            this.squareRect = new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Checks if the player square and any otherSquare's are intersecting.
        /// </summary>
        /// <param name="otherSquare">Other squares in the scene</param>
        /// <returns>If the squares are being intersected or not</returns>
        public bool IntersectsWith(SquareEntity otherSquare)
        {
            bool usingAABB = true;
        
            // checks if the you are using the AABB method
            if (usingAABB)
            {
                return (X < otherSquare.X + otherSquare.squareRect.Width && // left edge check
                       X + squareRect.Width > otherSquare.X &&              // right edge check
                       Y < otherSquare.Y + otherSquare.squareRect.Height && // top edge check
                       Y + squareRect.Height > otherSquare.Y);              // bottom edge check
            }
            else
            {
                return SquareRect.Intersects(otherSquare.SquareRect);
            }
        }



        /// <summary>
        /// Draws this SquareEntity to the game window.
        /// </summary>
        /// <param name="sb">Reference to the SpriteBatch object in Game1.</param>
        /// <param name="tint">The color of this game object.</param>
        public void Draw(SpriteBatch sb, Color tint)
        {
            sb.Draw(texture, squareRect, tint);

            // ********************************************************************
            // TODO: Use the DebugLib class to draw a rectangle outline around the squareRect.
            // This is helpful while debugging!
            // ********************************************************************

        }
    }
}

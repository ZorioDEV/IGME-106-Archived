using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGame_Basics_PE
{
    /// <summary>
    /// Information and logic for the rollover button.
    /// </summary>
    public class RolloverButton
    {
        // *** FIELDS ***
        private Texture2D hoveredImage;
        private Texture2D nonHoveredImage;
        private Rectangle buttonRect;

        // *** PROPERTIES ***
        /// <summary>
        /// Read-ONLY property for the rectangle's width.
        /// </summary>
        public int Width
        {
            get
            {
                return buttonRect.Width;
            }
        }
        /// <summary>
        /// Read-ONLY property for the rectangle's height.
        /// </summary>
        public int Height
        {
            get
            {
                return buttonRect.Width;
            }
        }
        /// <summary>
        /// Read-ONLY property for the rectangle's x position.
        /// </summary>
        public int X
        {
            get
            {
                return buttonRect.X;
            }
        }
        /// <summary>
        /// Read-ONLY property for the rectangle's y position.
        /// </summary>
        public int Y
        {
            get
            {
                return buttonRect.Y;
            }
        }

        // *** CONSTRUCTORS ***
        /// <summary>
        /// Main parameterized constructor for the rollover button.
        /// </summary>
        /// <param name="hoveredImage">Image for if mouse is hovering</param>
        /// <param name="nonHoveredImage">Image for if mouse is not hovering</param>
        /// <param name="x">X position of the button</param>
        /// <param name="y">Y position of the button</param>
        public RolloverButton(Texture2D hoveredImage, Texture2D nonHoveredImage, int x, int y)
        {
            this.hoveredImage = hoveredImage;
            this.nonHoveredImage = nonHoveredImage;
            buttonRect = new Rectangle(x, y, nonHoveredImage.Width, nonHoveredImage.Height);
        }

        // *** METHODS ***
        /// <summary>
        /// Update function of the button.
        /// </summary>
        public void Update()
        {
            MouseState mouse = Mouse.GetState();

            // test which side the mouse is on & moves button to that side
            if (mouse.X < GraphicsDeviceManager.DefaultBackBufferWidth / 2)
            {
                buttonRect.X = 0;
            }
            else
            {
                buttonRect.X = GraphicsDeviceManager.DefaultBackBufferWidth;
            }
        }

        /// <summary>
        /// Draw function for the button.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch Object</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            MouseState mouse = Mouse.GetState();

            Texture2D currentImage;

            // changes image if mouse is hovering
            if (buttonRect.Contains(mouse.Position))
            {
                currentImage = hoveredImage;
            }
            else
            {
                currentImage = nonHoveredImage;
            }

            spriteBatch.Draw(currentImage, buttonRect, Color.White);
        }

    }
}

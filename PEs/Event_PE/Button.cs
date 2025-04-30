using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Event_PE
{
    /// <summary>
    /// Delegate for button click events.
    /// </summary>
    public delegate void OnButtonClickDelegate();

    public class Button
    {
        #region ---FIELDS & EVENTS---
        // Positioning
        private SpriteFont font;
        private Rectangle rect;
        private Vector2 textPosition;

        // Appearance
        private string text;
        private Color textColor;
        private Texture2D buttonImage;

        // Input
        private MouseState prevMState;

        // Event(s)
        /// <summary>
        /// Event that fires when the button is left-clicked.
        /// </summary>
        public event OnButtonClickDelegate OnLeftButtonClick;

        /// <summary>
        /// Event that fires when the button is right-clicked.
        /// </summary>
        public event OnButtonClickDelegate OnRightButtonClick;
        #endregion

        #region ---CONSTRUCTOR---
        /// <summary>
        /// Create a new custom button
        /// </summary>
        /// <param name="device">The graphics device for this game.
        /// It's needed to create custom button textures.</param>
        /// <param name="rect">Where to draw the button's top left corner</param>
        /// <param name="text">The text to draw on the button</param>
        /// <param name="font">The font to use when drawing the button text.</param>
        /// <param name="color">The color to make the button's texture.</param>
        public Button(GraphicsDevice device, Rectangle rect, string text, SpriteFont font, Color color)
        {
            this.font = font;
            this.rect = rect;
            this.text = text;

            // Center the button's text
            Vector2 textSize = font.MeasureString(text);
            textPosition = new Vector2(
                (rect.X + rect.Width / 2) - textSize.X / 2,
                (rect.Y + rect.Height / 2) - textSize.Y / 2
            );

            // Invert the button color for the text color (just for fun!)
            textColor = new Color(255 - color.R, 255 - color.G, 255 - color.B);

            // Make a custom 2D texture for the button itself
            buttonImage = new Texture2D(
                device,
                rect.Width,
                rect.Height,
                false,
                SurfaceFormat.Color);

            // Array to hold all the pixels of the texture
            int[] colorData = new int[buttonImage.Width * buttonImage.Height];

            // Fill the array with all the same color
            Array.Fill<int>(colorData, (int)color.PackedValue);

            // Update the texture's data
            buttonImage.SetData<Int32>(colorData, 0, colorData.Length);
        }
        #endregion

        #region ---UPDATE---
        /// <summary>
        /// Each frame, update its status if the button been clicked.
        /// </summary>
        public void Update()
        {
            // Check/capture the mouse state (regardless of whether this button
            //   is active) so that it's up to date!
            MouseState mState = Mouse.GetState();

            // A left button click is detected!
            if (mState.LeftButton == ButtonState.Released &&
                prevMState.LeftButton == ButtonState.Pressed &&
                rect.Contains(mState.Position))
            {
                // invokes left click event if there are subscribers
                if (OnLeftButtonClick != null)
                {
                    OnLeftButtonClick();
                }
            }

            // A right button click is detected!
            if (mState.RightButton == ButtonState.Released &&
                prevMState.RightButton == ButtonState.Pressed &&
                rect.Contains(mState.Position))
            {
                // invokes right click event if there are subscribers
                if (OnRightButtonClick != null)
                {
                    OnRightButtonClick();
                }
            }

            // Save state as previous so it's up to date for next frame
            prevMState = mState;
        }
        #endregion

        #region ---DRAW---
        /// <summary>
        /// Override the GameObject Draw() to draw the button and then
        /// overlay it with text.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch on which to draw this button. 
        /// Button assumes Begin() has already been called and End() will be called later.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the button first
            spriteBatch.Draw(
                buttonImage,                    // Custom texture created in constructor
                rect,                           // Position and size
                Color.White);                   // No color overlay

            // Draw button text on the button
            spriteBatch.DrawString(
                font,                           // Sprite font
                text,                           // Button's description
                textPosition,                   // Calculated in constructor
                textColor);                     // Calculated in contructor
        } 
        #endregion
    }
}
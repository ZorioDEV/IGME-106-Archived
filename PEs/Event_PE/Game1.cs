using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System;

// *********************************************************
// Tyler Bye
// 04.06.2025
// *********************************************************
// PE:  Button Class for Events and Delegates
// Project starter code written by Erika Mesh/Erin Cascioli

namespace Event_PE
{
    public class Game1 : Game
    {
        #region ---FIELDS, CONTRUCTOR, & INITIALIZE---
        // Fields created by the MG template
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // The list of buttons along with setup for setting a random background color
        private SpriteFont font;
        private List<Button> buttons;
        private Color bgColor;
        private Random rng;

        // new fields for button click tracking and sprites
        private int leftClickCount;
        private int rightClickCount;
        private Texture2D spriteTexture;
        private List<Vector2> spritePositions;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Initialize objects
            buttons = new List<Button>();
            bgColor = Color.DimGray;
            rng = new Random();

            // initializes new fields
            leftClickCount = 0;
            rightClickCount = 0;
            spritePositions = new List<Vector2>();

            base.Initialize();
        }
        #endregion

        #region ---LOAD CONTENT---
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load in the button's font
            font = Content.Load<SpriteFont>("ButtonFont");

            // Load sprite texture (using a simple white square for demonstration)
            spriteTexture = Content.Load<Texture2D>("amongus");

            // Create 3 200 x 100 buttons down the left side of game window
            Button changeBackgroundButton =
                new Button(
                    _graphics.GraphicsDevice,           // device reference
                    new Rectangle(10, 40, 200, 100),    // position and size of button
                    "Random BG",                        // button label
                    font,                               // label font
                    Color.Black);                       // button color

            Button addSomethingButton =
                new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(10, 160, 200, 100),
                    "Add",
                    font,
                    Color.AliceBlue);

            Button removeSomethingButton =
                new Button(
                    _graphics.GraphicsDevice,
                    new Rectangle(10, 280, 200, 100),
                    "Remove",
                    font,
                    Color.DarkSeaGreen);

            // Add them, in order, to the Button list.
            buttons.Add(changeBackgroundButton);
            buttons.Add(addSomethingButton);
            buttons.Add(removeSomethingButton);

            // adds random BG button events
            buttons[0].OnLeftButtonClick += this.RandomizeBackground;
            buttons[0].OnLeftButtonClick += this.CountLeftButtonClicks;
            buttons[0].OnRightButtonClick += this.CountRightButtonClicks;

            // adds button events
            buttons[1].OnLeftButtonClick += this.AddSprite;
            buttons[1].OnLeftButtonClick += this.CountLeftButtonClicks;
            buttons[1].OnRightButtonClick += this.CountRightButtonClicks;

            // removes button events
            buttons[2].OnLeftButtonClick += this.RemoveSprite;
            buttons[2].OnLeftButtonClick += this.CountLeftButtonClicks;
            buttons[2].OnRightButtonClick += this.CountRightButtonClicks;
        }
        #endregion

        #region ---UPDATE---
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update each Button, which checks if the Button has been clicked.  
            // If it has been clicked, an action will occur.
            // This action is defined in the Button class.
            foreach (Button b in buttons)
            {
                b.Update();
            }

            base.Update(gameTime);
        }
        #endregion

        #region ---DRAW---
        protected override void Draw(GameTime gameTime)
        {
            // Clears to gray
            GraphicsDevice.Clear(bgColor);

            // Begin the SpriteBatch
            _spriteBatch.Begin();

            // draws all sprites
            foreach (Vector2 position in spritePositions)
            {
                _spriteBatch.Draw(spriteTexture, position, Color.White);
            }

            // draws click counter
            _spriteBatch.DrawString(
                font,
                $"Number of button clicks: Left - {leftClickCount} Right - {rightClickCount}",
                new Vector2(10, 10),
                Color.White
            );

            // Draw all buttons in the foreground and layered
            //   "on top" of any other entities in the game.
            foreach (Button b in buttons)
            {
                b.Draw(_spriteBatch);
            }

            // End the SpriteBatch
            _spriteBatch.End();

            // Parent (Game) call to Draw
            base.Draw(gameTime);
        }
        #endregion

        #region ---HELPERS---
        /// <summary>
        /// Method that changes the background color of the game window.
        /// </summary>
        public void RandomizeBackground()
        {
            bgColor = new Color(
                rng.Next(0, 256),
                rng.Next(0, 256),
                rng.Next(0, 256)
            );
        }

        /// <summary>
        /// Adds a sprite at a random position in the right side of the screen.
        /// </summary>
        public void AddSprite()
        {
            int x = rng.Next(210, _graphics.PreferredBackBufferWidth - 10);
            int y = rng.Next(10, _graphics.PreferredBackBufferHeight - 10);
            spritePositions.Add(new Vector2(x, y));
        }

        /// <summary>
        /// Removes a random sprite from the collection.
        /// </summary>
        public void RemoveSprite()
        {
            if (spritePositions.Count > 0)
            {
                int index = rng.Next(0, spritePositions.Count);
                spritePositions.RemoveAt(index);
            }
        }

        /// <summary>
        /// Increases the left click counter.
        /// </summary>
        public void CountLeftButtonClicks()
        {
            leftClickCount++;
        }

        /// <summary>
        /// Increases the right click counter.
        /// </summary>
        public void CountRightButtonClicks()
        {
            rightClickCount++;
        } 
        #endregion
    }
}
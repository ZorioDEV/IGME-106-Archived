// Tyler Bye
// 1/30/25
// Learning MonoGame Basics

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGame_Basics_PE
{
    /// <summary>
    /// Main class holding a bouncing image, tinted image, and grid of images.
    /// </summary>
    public class Game1 : Game
    {
        // *** FIELDS ***
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D image;
        private Vector2 imagePosition;
        private Rectangle tintedRect;
        private SpriteFont arial36Bold;

        private int _speedX = 1;
        private int _speedY = 1;

        private int _windowWidth = 1000;
        private int _windowHeight = 800;

        private KeyboardState currentState;
        private KeyboardState previousState;
        private MouseState mousePosition;

        private RolloverButton rolloverButton;

        // allows for grid to be shown or not
        /// <summary>
        /// Visability state of the entire grid. 
        /// </summary>
        private enum GridState { Hidden, Visible }
        // grid turned: off
        private GridState gridState = GridState.Hidden;

        // *** CONSTRUCTORS ***
        /// <summary>
        /// Main constructor for the MonoGame project.
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // makes the screen 1000x800
            _graphics.PreferredBackBufferWidth = _windowWidth;
            _graphics.PreferredBackBufferHeight = _windowHeight;
            _graphics.ApplyChanges();

        }

        /// <summary>
        /// Initializes the program & places image in starting position.
        /// </summary>
        protected override void Initialize()
        {
            // places image at (50,50)
            imagePosition = new Vector2(50, 50);

            // current and previous keys
            currentState = Keyboard.GetState();
            previousState = Keyboard.GetState();

            base.Initialize();
        }

        /// <summary>
        /// Loads image and graphics for the window.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            image = Content.Load<Texture2D>("436px-Cloud9_New_York_lightmode");
            arial36Bold = Content.Load<SpriteFont>("arial-36-bold");

            // intializes the rollover button & its data
            Texture2D nonHoveredImage = Content.Load<Texture2D>("green");
            Texture2D hoveredImage = Content.Load<Texture2D>("red");
            int x = _windowWidth - nonHoveredImage.Width;
            int y = 0;
            rolloverButton = new RolloverButton(hoveredImage, nonHoveredImage, x, y);

            // intializes rectangle object thats placed in the center
            tintedRect = new Rectangle(
                (_windowWidth - image.Width) / 2,
                (_windowHeight - image.Height) / 2,
                image.Width,
                image.Height
            );
        }

        /// <summary>
        /// Updates the window and objects for each second
        /// </summary>
        /// <param name="gameTime">Amount of time that has passed</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // imagePosition.X += 1;

            // makes the image bounce around the window
            imagePosition.X += _speedX;
            imagePosition.Y += _speedY;

            // tests if image collides with the boards on the X-value
            if (imagePosition.X <= 0 
                || (imagePosition.X + image.Width) >= _windowWidth)
            {
                // changes direction
                _speedX *= -1;
            }

            // tests if image collides with the boards on the Y-value
            if (imagePosition.Y <= 0 
                || (imagePosition.Y + image.Height) >= _windowHeight)
            {
                // changes direction
                _speedY *= -1;
            }

            // movement using WASD
            MoveImage();

            // impliments the rolloverButton's update function
            rolloverButton.Update();

            // current mouse position
            mousePosition = Mouse.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws all the objects into the window.
        /// </summary>
        /// <param name="gameTime">Amount of time that has passed</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            // base image
            _spriteBatch.Draw(
                image,
                imagePosition,
                Color.White
            );

            // tinted image
            _spriteBatch.Draw(
                image,
                tintedRect,
                Color.Green
            );

            // image grid
            // tests if the grid is visable
            if (gridState == GridState.Visible)
            {
                // grid variables
                int gridSize = 5;
                int spacingX = _windowWidth / gridSize;
                int spacingY = _windowHeight / gridSize;

                // loops across the whole window drawing the image on the x- and y-axis
                for (int x = 0; x < gridSize; x++)
                {
                    for (int y = 0; y < gridSize; y++)
                    {
                        Vector2 gridPosition = new Vector2(x * spacingX, y * spacingY);
                        _spriteBatch.Draw(
                            image,
                            new Rectangle(
                            x * spacingX,
                            y * spacingY,
                            spacingX,
                            spacingY
                            ),
                            Color.Red);
                    }
                }
            }
            
            // name text
            _spriteBatch.DrawString(
                arial36Bold,
                "Tyler Bye",
                new Vector2(10, _windowHeight - 160),
                Color.Black
            );

            // tintedRect position text
            _spriteBatch.DrawString(
                arial36Bold,
                $"X: {tintedRect.X} Y: {tintedRect.Y}",
                new Vector2(10, _windowHeight - 110),
                Color.Black
            );

            // mouse position text
            _spriteBatch.DrawString(
                arial36Bold,
                $"Mouse X: {mousePosition.X} Mouse Y: {mousePosition.Y}",
                new Vector2(10, _windowHeight - 60),
                Color.Black
            );

            // draws the rolloverButton & its logic
            rolloverButton.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Moves the image around the screen.
        /// </summary>
        public void MoveImage()
        {
            currentState = Keyboard.GetState();

            // easily changable movement speed variable
            int movementSpeed = 5;

            // input and movement using WASD
            if (currentState.IsKeyDown(Keys.W) &&
               previousState.IsKeyDown(Keys.W))
            {
                tintedRect.Y -= movementSpeed;
            }

            if(currentState.IsKeyDown(Keys.A) &&
               previousState.IsKeyDown(Keys.A))
            {
                tintedRect.X -= movementSpeed;
            }

            if (currentState.IsKeyDown(Keys.S) &&
               previousState.IsKeyDown(Keys.S))
            {
                tintedRect.Y += movementSpeed;
            }

            if (currentState.IsKeyDown(Keys.D) &&
               previousState.IsKeyDown(Keys.D))
            {
                tintedRect.X += movementSpeed;
            }

            previousState = currentState;
        }
    }
}

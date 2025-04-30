using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

// ********************************************************
// Tyler Bye
// 02/05/2025
// Learning Finite State Machines with Mario
// ********************************************************


namespace MarioFSM_STARTER
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Mario texture fields
        private Texture2D marioTexture;
        private Vector2 marioPosition;

        // Sprite sheet data
        private int numSpritesInSheet;
        private int widthOfSingleSprite;
        private SpriteFont arial36Bold;

        // Animation data
        private int mariosCurrentFrame;
        private double fps;
        private double secondsPerFrame;
        private double timeCounter;

        // Mario state options
        public enum MarioState
        {
            FaceLeft,
            WalkLeft,
            FaceRight,
            WalkRight
        }

        // Current states
        private MarioState marioState;
        private KeyboardState currentState;
        private KeyboardState previousState;

        // Speed of walking
        private int marioSpeed = 5;

        /// <summary>
        /// Main Game Constructor
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        
        /// <summary>
        /// Initializes Content
        /// </summary>
        protected override void Initialize()
        {
            // current and previous keys
            currentState = Keyboard.GetState();
            previousState = Keyboard.GetState();

            base.Initialize();
        }

        /// <summary>
        /// Loads all assets and data into the game
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the sprite sheet and fill in sprite data
            marioTexture = Content.Load<Texture2D>("MarioSpriteSheet");
            arial36Bold = Content.Load<SpriteFont>("arial-36-bold");
            numSpritesInSheet = 4;
            widthOfSingleSprite = marioTexture.Width / numSpritesInSheet;

            // Start Mario at (200, 200) in the game window
            marioPosition = new Vector2(200, 200);
            marioState = MarioState.FaceRight;

            // Set up animation data:
            fps = 8.0;                      // Animation frames to cycle through per second
            secondsPerFrame = 1.0 / fps;    // How long each animation frame lasts
            timeCounter = 0;                // Time passed since animation
            mariosCurrentFrame = 1;         // Sprite sheet's first animation frame is 1 (not 0)
        }

        /// <summary>
        /// Updates player data based off of changing states
        /// </summary>
        /// <param name="gameTime">Time that has passed in the game</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentState = Keyboard.GetState();

            // tests which state mario is currently in
            switch (marioState)
            {
                // tests option when mario is facing left
                case MarioState.FaceLeft:
                    if (currentState.IsKeyDown(Keys.A) &&
                        previousState.IsKeyDown(Keys.A))
                        marioState = MarioState.WalkLeft;
                    else if (currentState.IsKeyDown(Keys.D) &&
                            previousState.IsKeyDown(Keys.D))
                        marioState = MarioState.FaceRight;
                    break;

                // tests option when mario is walking left
                case MarioState.WalkLeft:
                    // moves mario left
                    marioPosition.X -= marioSpeed;
                    if (!currentState.IsKeyDown(Keys.A) &&
                        !previousState.IsKeyDown(Keys.A))
                        marioState = MarioState.FaceLeft;
                    break;

                // tests option when mario is facing right
                case MarioState.FaceRight:
                    if (currentState.IsKeyDown(Keys.D) &&
                        previousState.IsKeyDown(Keys.D))
                        marioState = MarioState.WalkRight;
                    else if (currentState.IsKeyDown(Keys.A) &&
                            previousState.IsKeyDown(Keys.A))
                        marioState = MarioState.FaceLeft;
                    break;

                // tests option when mario is walking right
                case MarioState.WalkRight:
                    // moves mario right
                    marioPosition.X += marioSpeed;
                    if (!currentState.IsKeyDown(Keys.D) &&
                        !previousState.IsKeyDown(Keys.D))
                        marioState = MarioState.FaceRight;
                    break;

                // does nothing
                default:
                    break;
            }

            previousState = currentState;

            // Always update Mario's animation
            UpdateAnimation(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws sprites and animations into the console
        /// </summary>
        /// <param name="gameTime">Time that has passed in the game</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            // displays marios current state
            _spriteBatch.DrawString(
                arial36Bold,
                $"Current state: {marioState}",
                new Vector2(10, 10),
                Color.White
            );

            // displays marios current position
            _spriteBatch.DrawString(
                arial36Bold,
                $"Mario Position: {{X:{marioPosition.X} Y:{marioPosition.Y}}}",
                new Vector2(10, 60),
                Color.White
            );


            // Checks Finite State Machine
            switch (marioState)
            {
                // Draws mario facing left
                case MarioState.FaceLeft:
                    DrawMarioStanding(SpriteEffects.FlipHorizontally);
                    break;

                // Draws mario walking left
                case MarioState.WalkLeft:
                    DrawMarioWalking(SpriteEffects.FlipHorizontally);
                    break;

                // Draws mario facing right
                case MarioState.FaceRight:
                    DrawMarioStanding(SpriteEffects.None);
                    break;

                // Draws mario walking left
                case MarioState.WalkRight:
                    DrawMarioWalking(SpriteEffects.None);
                    break;

                // does nothing
                default:
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
		/// Helper for updating Mario's animation based on time
		/// </summary>
		/// <param name="gameTime">Info about time from MonoGame</param>
		private void UpdateAnimation(GameTime gameTime)
        {
            // ElapsedGameTime is the duration of the last GAME frame
            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

            // Has enough time passed to flip to the next frame?
            if (timeCounter >= secondsPerFrame)
            {
                // Change which frame is active, ensuring the frame is reset back to the first 
                mariosCurrentFrame++;
                if (mariosCurrentFrame >= 4)
                {
                    mariosCurrentFrame = 1;
                }

                // Reset the time counter, keeping remaining elapsed time
                timeCounter -= secondsPerFrame;
            }
        }

        /// <summary>
		/// Draws Mario with a walking animation.
		/// </summary>
		/// <param name="flip">Should he be flipped horizontally or vertically?</param>
		private void DrawMarioWalking(SpriteEffects flip)
        {
            // This version of draw can flip (mirror) the image horizontally or vertically,
            // depending on the method's SpriteEffects parameter.

            // Mario is animated with this method.
            // He is drawn starting at the second animation frame in the sprite sheet 
            //   and cycles through animation frames 1, 2, and 3.
            //   (i.e. the second through fourth images in the sheet)
            _spriteBatch.Draw(
                marioTexture,                                   // Whole sprite sheet
                marioPosition,                                  // Position of the Mario sprite
                new Rectangle(                                  // Which portion of the sheet is drawn:
                    mariosCurrentFrame * widthOfSingleSprite,   // - Left edge
                    0,                                          // - Top of sprite sheet
                    widthOfSingleSprite,                        // - Width 
                    marioTexture.Height),                       // - Height
                Color.White,                                    // No change in color
                0.0f,                                           // No rotation
                Vector2.Zero,                                   // Start origin at (0, 0) of sprite sheet 
                1.0f,                                           // Scale
                flip,                                           // Flip it horizontally or vertically?    
                0.0f);                                          // Layer depth
        }

        /// <summary>
        /// Draws Mario in a standing position.  Mario is not animated.
        /// </summary>
        /// <param name="flip">Should he be flipped horizontally or vertically?</param>
        private void DrawMarioStanding(SpriteEffects flip)
        {
            // This version of draw can flip (mirror) the image horizontally or vertically,
            // depending on the method's SpriteEffects parameter.

            // Mario is not animated with this method.
            // He is drawn using the first animation frame in the sprite sheet 
            //   (i.e. the first image in the sheet)
            _spriteBatch.Draw(
                marioTexture,                                   // Whole sprite sheet
                marioPosition,                                  // Position of the Mario sprite
                new Rectangle(                                  // Which portion of the sheet is drawn:
                    0,                                          // - Left edge
                    0,                                          // - Top of sprite sheet
                    widthOfSingleSprite,                        // - Width 
                    marioTexture.Height),                       // - Height
                Color.White,                                    // No change in color
                0.0f,                                           // No rotation
                Vector2.Zero,                                   // Start origin at (0, 0) of sprite sheet 
                1.0f,                                           // Scale
                flip,                                           // Flip it horizontally or vertically?    
                0.0f);                                          // Layer depth
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


// ****************************************************************************
// Tyler Bye
// 02/12/25
// Learning about various collisions
// ****************************************************************************



namespace CollisionPE_STARTER
{
    /// <summary>
    /// Used for switching between Circle and Square mode in the Collisions PE
    /// </summary>
    public enum SimulationState
    {
        Instructions,
        Circle,
        Square
    }


    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // --------------------------------------------------------------------
        // Fields for the Collision PE
        // --------------------------------------------------------------------

        // Random object for use throughout class
        private Random rng;

        // Textures for Squares and Circles
        private Texture2D squareTexture;
        private Texture2D circleTexture;

        // Lists of all Squares and Circles
        private List<SquareEntity> squareList;
        private List<CircleEntity> circleList;

        // Player-controlled Square and Circle
        private SquareEntity playerSquare;
        private CircleEntity playerCircle;

        // Text positioning
        private SpriteFont arial20;
        private Vector2 textPosition;
        private Vector2 instructionPosition;

        // Window size information
        private int windowWidth;
        private int windowHeight;

        // Keyboard State for first-key-presses
        private KeyboardState previousKBState;

        // State for the FSM
        private SimulationState currentFSM_State;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
        protected override void Initialize()
        {
            // Get window size information
            windowWidth = GraphicsDevice.Viewport.Width;
            windowHeight = GraphicsDevice.Viewport.Height;

            // Start the game in Square mode
            currentFSM_State = SimulationState.Instructions;

            // Instantiate Random for use throughout game
            rng = new Random();

            // Initialize list of Squares and Circles
            squareList = new List<SquareEntity>();
            circleList = new List<CircleEntity>();

            // Init previous keyboard state so its not null
            previousKBState = Keyboard.GetState();

            base.Initialize();
        }

        /// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // --------------------------------------------------------------------
            // Load content for the PE
            // --------------------------------------------------------------------

            // Load the 2 textures for the game
            squareTexture = Content.Load<Texture2D>("Square");
            circleTexture = Content.Load<Texture2D>("Circle");

            // Get SpriteFont and text positions ready
            arial20 = Content.Load<SpriteFont>("arial20");
            textPosition = new Vector2(20, 400);
            instructionPosition = new Vector2(20, 100);

            // --------------------------------------------------------------------
            // Initialize values & objects dependent on loaded content
            // --------------------------------------------------------------------

            // Initialize and position ten random circles and squares
            for (int i = 0; i < 10; i++)
            {
                squareList.Add(
                    new SquareEntity(
                        squareTexture, 
                        rng.Next(100, windowWidth - 100), 
                        rng.Next(0, windowHeight - 100), 
                        rng.Next(30, 100),
                        rng.Next(30, 100)));
                circleList.Add(
                    new CircleEntity(
                        circleTexture, 
                        rng.Next(100, windowWidth - 100), 
                        rng.Next(0, windowHeight - 100), 
                        rng.Next(20, 50)));
            }

            // Get player-controlled units ready
            playerSquare = new SquareEntity(squareTexture, 0, 0, 100, 100);
            playerCircle = new CircleEntity(circleTexture, 50, 50, 50);
        }

        /// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // --------------------------------------------------------------------
            // Keyboard input ready for the PE
            // --------------------------------------------------------------------

            // Get the current state of the keyboard
            KeyboardState currentKBState = Keyboard.GetState();

            int speed = 5;

            // ****************************************************************
            // checks current state of game & makes sure one key is being pressed down
            
            // switches to current game state
            switch (currentFSM_State)
            {
                case SimulationState.Instructions:
                    if (IsKeyPressed(currentKBState, Keys.D1))
                    {
                        currentFSM_State = SimulationState.Square;
                    }
                    else if (IsKeyPressed(currentKBState, Keys.D2))
                    {
                        currentFSM_State = SimulationState.Circle;
                    }
                    break;

                case SimulationState.Square:
                    // moves the player square using WASD
                    if (currentKBState.IsKeyDown(Keys.W))
                    { 
                        playerSquare.Y -= speed;
                    }
                    if (currentKBState.IsKeyDown(Keys.A))
                    { 
                        playerSquare.X -= speed;
                    }
                    if (currentKBState.IsKeyDown(Keys.S))
                    {
                        playerSquare.Y += speed;
                    }
                    if (currentKBState.IsKeyDown(Keys.D))
                    {
                        playerSquare.X += speed;
                    }

                    if (IsKeyPressed(currentKBState, Keys.D1))
                    {
                        currentFSM_State = SimulationState.Square;
                    }
                    else if (IsKeyPressed(currentKBState, Keys.D2))
                    {
                        currentFSM_State = SimulationState.Circle;
                    }
                    else if (IsKeyPressed(currentKBState, Keys.I))
                    {
                        currentFSM_State = SimulationState.Instructions;
                    }
                    break;

                case SimulationState.Circle:
                    // moves the player circle using WASD
                    if (currentKBState.IsKeyDown(Keys.W))
                    {
                        playerCircle.Y -= speed;
                    }
                    if (currentKBState.IsKeyDown(Keys.A))
                    {
                        playerCircle.X -= speed;
                    }
                    if (currentKBState.IsKeyDown(Keys.S))
                    {
                        playerCircle.Y += speed;
                    }
                    if (currentKBState.IsKeyDown(Keys.D))
                    {
                        playerCircle.X += speed;
                    }

                    if (IsKeyPressed(currentKBState, Keys.D1))
                    {
                        currentFSM_State = SimulationState.Square;
                    }
                    else if (IsKeyPressed(currentKBState, Keys.D2))
                    {
                        currentFSM_State = SimulationState.Circle;
                    }
                    else if (IsKeyPressed(currentKBState, Keys.I))
                    {
                        currentFSM_State = SimulationState.Instructions;
                    }
                    break;
            }
            // ****************************************************************

            // Save this current state as the previous so next frame we know what occurred this frame.
            previousKBState = currentKBState;

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            // --------------------------------------------------------------------
            // Check the current simulation state to determine what to render
            // to the game window.
            // --------------------------------------------------------------------

            switch (currentFSM_State)
            {
                // ----- Instructions state -----------------------------------
                case SimulationState.Instructions:

                    // Draw user instructions to the game window
                    string instructions = 
                        "Begin the simulation:" +
                        "\nPress 1 to see Square mode" +
                        "\nPress 2 to see Circle mode";

                    _spriteBatch.DrawString(
                        arial20, 
                        instructions, 
                        instructionPosition, 
                        Color.White);
                    break;

                // ----- Square shape state -----------------------------------
                case SimulationState.Square:

                    // Draw current mode to the game window
                    _spriteBatch.DrawString(arial20, "Intersects", textPosition, Color.White);

                    // collision detection for the squares
                    bool isColliding = false;
                    foreach (SquareEntity square in squareList)
                    {
                        if (playerSquare.IntersectsWith(square))
                        {
                            isColliding = true;
                            square.Draw(_spriteBatch, Color.Red);
                        }
                        else
                        {
                            square.Draw(_spriteBatch, Color.White);
                        }
                    }

                    // changes player color if collision detected
                    Color playerColor = Color.Blue;
                    if (isColliding)
                    {
                        playerColor = Color.Red;
                    }
                    playerSquare.Draw(_spriteBatch, playerColor);

                    break;
                    // ********************************************************

                // ----- Circle shape state -----------------------------------
                case SimulationState.Circle:

                    // Draw current mode to the game window
                    _spriteBatch.DrawString(arial20, "Circle-Circle", textPosition, Color.White);

                    // collision detection for circles
                    isColliding = false;
                    foreach (CircleEntity circle in circleList)
                    {
                        if (playerCircle.IntersectsWith(circle))
                        {
                            isColliding = true;
                            circle.Draw(_spriteBatch, Color.Red);

                        }
                        else
                        {
                            circle.Draw(_spriteBatch, Color.White);

                        }
                    }

                    // changes player color if collision detected
                    playerColor = Color.Blue;
                    if (isColliding)
                    {
                        playerColor = Color.Red;
                    }
                    playerCircle.Draw(_spriteBatch, playerColor);

                    break;
                    // ********************************************************
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Standard check if a single key is pressed down.
        /// </summary>
        /// <param name="currentKBState">Current key being pressed</param>
        /// <param name="key">Specific Key</param>
        /// <returns>If the specific key was the only key being pressed.</returns>
        private bool IsKeyPressed(KeyboardState currentKBState, Keys key)
        {
            return currentKBState.IsKeyDown(key) && previousKBState.IsKeyUp(key);
        }
    }
}
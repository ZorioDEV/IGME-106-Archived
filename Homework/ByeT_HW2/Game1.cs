using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace ByeT_HW2
{
    /// <summary>
    /// Main Game class.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont arial36Bold;
        private Texture2D playerTexture;
        private Texture2D coinTexture;
        private Texture2D redTexture;

        Player player;
        List<Collectibles> collectibles = new List<Collectibles>();
        Random rng = new Random();

        int currentLevel = 0;
        double timer;
        bool debugMode = false;

        /// <summary>
        /// Enum for the various gamemodes.
        /// </summary>
        enum GameMode 
        { 
            Menu, 
            Game, 
            GameOver 
        }
        // sets initial gamemode to Menu
        GameMode currentMode = GameMode.Menu;
        KeyboardState previousKbState;

        /// <summary>
        /// Main contructor for Game1.
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Main initialize method for Game1.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Loads all the content for the game.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // image and text files
            playerTexture = Content.Load<Texture2D>("knight_solo");
            coinTexture = Content.Load<Texture2D>("coin_solo");
            arial36Bold = Content.Load<SpriteFont>("arial-36-bold");

            // creates a 1x1 red texture for debug usage
            redTexture = new Texture2D(GraphicsDevice, 1, 1);
            redTexture.SetData(new Color[] { Color.Red });

            // finds viewport width and height
            int width = _graphics.GraphicsDevice.Viewport.Width;
            int height = _graphics.GraphicsDevice.Viewport.Height;

            // makes player object with sprites
            player = new Player(
                playerTexture,
                new Rectangle(width / 2, height / 2, 50, 50),
                width,
                height
            );
        }

        /// <summary>
        /// Resets all game stat variables and time.
        /// </summary>
        private void ResetGame()
        {
            currentLevel = 0;
            player.TotalScore = 0;
            player.TotalScore = 0;
            NextLevel();
        }

        /// <summary>
        /// Creates new level variables and resets time.
        /// </summary>
        private void NextLevel()
        {
            // updates level number
            currentLevel++;
            // adds more time
            timer = 10 + (currentLevel - 1);
            // sets level score to zero
            player.LevelScore = 0;
            // places character in the center of the screen
            player.Center();

            // clears previous collectibles' data
            collectibles.Clear();
            // increases collectibles amount
            int numCollectibles = ((currentLevel - 1) * 2) + 5;

            // spawns in all the collectibles in random locations
            for (int i = 0; i < numCollectibles; i++)
            {
                int x = rng.Next(0, _graphics.GraphicsDevice.Viewport.Width - 50);
                int y = rng.Next(0, _graphics.GraphicsDevice.Viewport.Height - 50);

                collectibles.Add(new Collectibles(coinTexture, new Rectangle(x, y, 50, 50)));
            }
        }

        /// <summary>
        /// Makes sure only one key is being pressed at a time.
        /// </summary>
        /// <param name="key">Specific key being pressed</param>
        /// <param name="currentState">Current button being pressed</param>
        /// <param name="previousState">Previous button being pressed</param>
        /// <returns>True or False</returns>
        private bool IsKeyPress(Keys key, KeyboardState currentState, KeyboardState previousState)
        {
            return currentState.IsKeyDown(key) && previousState.IsKeyUp(key);
        }

        /// <summary>
        /// Main update method of Game1.
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState currentKbState = Keyboard.GetState();

            // tests which gamemode the game is in
            switch (currentMode)
            {
                // main menu screen
                case GameMode.Menu:
                    if (IsKeyPress(Keys.Enter, currentKbState, previousKbState))
                    {
                        // resets past data and starts new game
                        ResetGame();
                        currentMode = GameMode.Game;
                    }
                    break;

                // main game screen
                case GameMode.Game:
                    // gets timer of the game level
                    timer -= gameTime.ElapsedGameTime.TotalSeconds;
                    // updates player class
                    player.Update(gameTime);

                    // test if player is colliding with each collectible
                    for (int i = collectibles.Count - 1; i >= 0; i--)
                    {
                        if (collectibles[i].CheckCollision(player))
                        {
                            // added 10 to the player score
                            player.LevelScore += 10;
                            player.TotalScore += 10;

                            // removes collectible object
                            collectibles.RemoveAt(i);
                        }
                    }

                    // when colllected all objects, the next level starts
                    if (collectibles.Count == 0)
                    {
                        NextLevel();
                    }

                    // when the timer hits 0, the player is sent to the end screen
                    if (timer <= 0)
                    {
                        currentMode = GameMode.GameOver;
                    }
                    break;

                // main end screen
                case GameMode.GameOver:
                    // sends the player to the menu screen
                    if (IsKeyPress(Keys.Enter, currentKbState, previousKbState))
                    {
                        currentMode = GameMode.Menu;
                    }
                    break;
            }

            // allows for player to activate debugMode
            if (IsKeyPress(Keys.I, currentKbState, previousKbState))
            {
                debugMode = !debugMode;
            }

            previousKbState = currentKbState;
            base.Update(gameTime);
        }

        /// <summary>
        /// Main draw method of Game1.
        /// </summary>
        /// <param name="gameTime">Runtime of the game</param>
        protected override void Draw(GameTime gameTime)
        {
            // makes the background green
            GraphicsDevice.Clear(Color.Green);
            _spriteBatch.Begin();

            // tests which gamemode the game is in
            switch (currentMode)
            {
                // main menu screen
                case GameMode.Menu:
                    // prints out welcoming text and instructions to the user
                    _spriteBatch.DrawString(
                        arial36Bold,
                        "Welcome!" +
                        "\nPress Enter to begin" +
                        "\nThen use the WASD to help the knight collect coins!" +
                        "\nUse Spacebar to dash!",
                        new Vector2(10, 160),
                        Color.White
                    );
                    break;

                // main game screen
                case GameMode.Game:
                    // calls player Draw method
                    player.Draw(_spriteBatch);

                    // calls each collectibles' Draw method
                    foreach (Collectibles coins in collectibles)
                    {
                        coins.Draw(_spriteBatch);
                    }

                    // prints the level's and player's current data and score
                    _spriteBatch.DrawString(
                        arial36Bold,
                        $"Level {currentLevel}: {player.TotalScore}" +
                        $"\nScore: {player.LevelScore}",
                        new Vector2(10, 10),
                        Color.White
                    );

                    // prints out timer & turns red when under 5 seconds
                    if (timer <= 5)
                    {
                        _spriteBatch.DrawString(
                            arial36Bold,
                            $"Timer: {timer:0.00}s",
                            new Vector2(10, 80),
                            Color.Red
                        );
                    }
                    else
                    {
                        _spriteBatch.DrawString(
                            arial36Bold,
                            $"Timer: {timer:0.00}s",
                            new Vector2(10, 80),
                            Color.White
                        );
                    }

                    // prints out dash cooldown when on cooldown
                    if (player.DashCooldownTimer <= 0)
                    {
                        _spriteBatch.DrawString(
                            arial36Bold,
                            $"Dash CD: {player.DashCooldownTimer:0.00}s",
                            new Vector2(10, 115),
                            Color.Green
                        );
                    }
                    else
                    {
                        _spriteBatch.DrawString(
                            arial36Bold,
                            $"Dash CD: {player.DashCooldownTimer:0.00}s",
                            new Vector2(10, 115),
                            Color.White
                        );
                    }

                    // prints debug option message
                    _spriteBatch.DrawString(
                        arial36Bold,
                        $"Press 'I' to toggle Debug",
                        new Vector2(225, 425),
                        Color.White
                    );

                    // prints various debug functions and data about the player
                    if (debugMode)
                    {
                        // prints the player's position
                        _spriteBatch.DrawString(
                            arial36Bold,
                            $"Player Pos: ({player.Position.X}, {player.Position.Y})",
                            new Vector2(10, 150),
                            Color.Red
                        );

                        // prints remaining collectibles count
                        _spriteBatch.DrawString(
                            arial36Bold,
                            $"Remaining Collectibles: {collectibles.Count}",
                            new Vector2(10, 200),
                            Color.Red
                        );

                        // prints current game state
                        _spriteBatch.DrawString(
                            arial36Bold,
                            $"Game Mode: {currentMode}",
                            new Vector2(10, 250),
                            Color.Red
                        );

                        // prints if the dash available
                        _spriteBatch.DrawString(
                            arial36Bold,
                            $"Able to Dash: {player.IsDashAvailable}",
                            new Vector2(10, 300),
                            Color.Red
                        );

                        // draws outline for player
                        DrawRectangleOutline(player.Position, Color.Red);

                        // draws outline for collectibles
                        foreach (Collectibles coin in collectibles)
                        {
                            DrawRectangleOutline(coin.Position, Color.Blue);
                        }
                    }
                    break;

                // main end screen
                case GameMode.GameOver:
                    // prints end screen text, highest level achieved, and total player score
                    _spriteBatch.DrawString(
                        arial36Bold,
                        $"Game Over!" +
                        $"\nPress Enter to return ot the main menu." +
                        $"\n\nHighest Level {currentLevel}" +
                        $"\nFinal Score: {player.TotalScore}",
                        new Vector2(10, 150),
                        Color.White
                    );
                    break;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Draws a colored outline around a current rectangle
        /// </summary>
        /// <param name="rectangle">Current rectangle</param>
        /// <param name="color">Color of outline</param>
        private void DrawRectangleOutline(Rectangle rectangle, Color color)
        {
            int thickness = 3;

            // draws the top line of the rectangle
            _spriteBatch.Draw(
                redTexture,
                new Rectangle(
                    rectangle.X,
                    rectangle.Y,
                    rectangle.Width,
                    thickness
                ), 
                color
            );

            // draws the bottom line of the rectangle
            _spriteBatch.Draw(
                redTexture, 
                new Rectangle(
                    rectangle.X, 
                    rectangle.Y + rectangle.Height - 1, 
                    rectangle.Width,
                    thickness
                ), 
                color
            );

            // draws the left line of the rectangle
            _spriteBatch.Draw(
                redTexture, 
                new Rectangle(
                    rectangle.X, 
                    rectangle.Y,
                    thickness, 
                    rectangle.Height
                ), 
                color
            );

            // draws the right line of the rectangle
            _spriteBatch.Draw(
                redTexture, 
                new Rectangle(
                    rectangle.X + rectangle.Width - 1, 
                    rectangle.Y,
                    thickness, 
                    rectangle.Height
                ), 
                color
            );
        }
    }
}

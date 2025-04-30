using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace Gravity_Collisions
{
    public class Game1 : Game
    {
        #region ---VARIABLES---
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D playerTexture;
        private Texture2D obstacleTexture;

        private float playerSpeedX;
        private Vector2 playerVelocity;
        private Vector2 jumpVelocity;
        private Vector2 playerPosition;
        private Vector2 gravity;

        private List<Rectangle> obstacleRects;
        private KeyboardState prevKB;
        #endregion

        #region ---CONSTRUCTOR---
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        #endregion

        #region ---INITIALIZE---
        protected override void Initialize()
        {
            playerPosition = new Vector2(400, 100);
            playerVelocity = Vector2.Zero;
            jumpVelocity = new Vector2(0, -15.0f);
            gravity = new Vector2(0, 0.5f);

            playerSpeedX = 5.0f;

            obstacleRects = new List<Rectangle>();

            base.Initialize();
        }
        #endregion

        #region ---LOAD---
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerTexture = Content.Load<Texture2D>("mario");
            obstacleTexture = Content.Load<Texture2D>("pixel");

            LoadObstaclesFromFile("Content/obstacles.txt");
        }
        #endregion

        #region ---UPDATE---
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // handle input, apply gravity and then deal with collisions
            ProcessInput();
            ApplyGravity();
            ResolveCollisions();

            // save the old state at the end of the frame
            prevKB = Keyboard.GetState();
            base.Update(gameTime);
        }
        #endregion

        #region ---DRAW---
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            // draw the player using a rectangle make from their position
            _spriteBatch.Draw(playerTexture, GetPlayerRect(), Color.White);

            // draw each obstactle
            foreach (Rectangle rect in obstacleRects)
            {
                _spriteBatch.Draw(obstacleTexture, rect, Color.SeaGreen);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        #region ---HELPERS---
        private void LoadObstaclesFromFile(string file)
        {
            // test if the file exists
            if (!File.Exists(file))
            {
                System.Diagnostics.Debug.WriteLine($"Error: Cannot find file '{file}' in the output directory!");
                return;
            }

            // open the file for reading
            using (StreamReader reader = new StreamReader(file))
            {
                string line;

                // read each line from the file
                while ((line = reader.ReadLine()) != null)
                {
                    // skip blank lines and comments
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//"))
                    {
                        continue;
                    }

                    // split the line by commas
                    string[] values = line.Split(',');

                    // ensure we have exactly 4 values (x, y, width, height)
                    if (values.Length == 4)
                    {
                        int x = int.Parse(values[0]);
                        int y = int.Parse(values[1]);
                        int width = int.Parse(values[2]);
                        int height = int.Parse(values[3]);

                        // create a new rectangle and add it to the list
                        Rectangle obstacle = new Rectangle(x, y, width, height);
                        obstacleRects.Add(obstacle);
                    }
                }
            }
        }

        private void ProcessInput()
        {
            KeyboardState kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.A) || kb.IsKeyDown(Keys.Left))
            {
                // move left
                playerPosition.X -= playerSpeedX;
            }

            if (kb.IsKeyDown(Keys.D) || kb.IsKeyDown(Keys.Right))
            {
                // move right
                playerPosition.X += playerSpeedX;
            }

            if (SingleKeyPress(Keys.Space))
            {
                // apply the jump velocity to the player's vertical velocity
                playerVelocity.Y = jumpVelocity.Y;
            }
        }

        private void ApplyGravity()
        {
            // add gravity to the player's velocity
            playerVelocity += gravity;

            // apply velocity to position
            playerPosition += playerVelocity;
        }

        private void ResolveCollisions()
        {
            Rectangle playerRect = GetPlayerRect();

            // create a list of obstacles the player is intersecting with
            List<Rectangle> intersections = new List<Rectangle>();

            // loop through all obstacles and check for intersections
            foreach (Rectangle obstacle in obstacleRects)
            {
                if (playerRect.Intersects(obstacle))
                {
                    intersections.Add(obstacle);
                }
            }

            // resolve horizontal collisions
            foreach (Rectangle obstacle in intersections)
            {
                Rectangle overlap = Rectangle.Intersect(playerRect, obstacle);

                // if the overlap is taller than wide (or equal), resolve horizontally
                if (overlap.Height >= overlap.Width)
                {
                    // determine which direction to push the player
                    if (playerRect.Center.X < obstacle.Center.X)
                    {
                        // player is on the left side of the obstacle, push left
                        playerRect.X -= overlap.Width;
                    }
                    else
                    {
                        // player is on the right side of the obstacle, push right
                        playerRect.X += overlap.Width;
                    }
                }
            }

            // resolve vertical collisions
            foreach (Rectangle obstacle in intersections)
            {
                Rectangle overlap = Rectangle.Intersect(playerRect, obstacle);

                // if the overlap is wider than tall, resolve vertically
                if (overlap.Width > overlap.Height)
                {
                    // determine which direction to push the player
                    if (playerRect.Center.Y < obstacle.Center.Y)
                    {
                        // player is above the obstacle, push up
                        playerRect.Y -= overlap.Height;
                    }
                    else
                    {
                        // player is below the obstacle, push down
                        playerRect.Y += overlap.Height;
                    }

                    // reset the player's vertical velocity when colliding vertically
                    playerVelocity.Y = 0;
                }
            }

            // update the player's position based on the adjusted rectangle
            playerPosition.X = playerRect.X;
            playerPosition.Y = playerRect.Y;
        }

        private bool SingleKeyPress(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key) && prevKB.IsKeyUp(key);
        }

        private Rectangle GetPlayerRect()
        {
            return new Rectangle(
                (int)playerPosition.X,
                (int)playerPosition.Y,
                playerTexture.Width,
                playerTexture.Height);
        } 
        #endregion
    }
}

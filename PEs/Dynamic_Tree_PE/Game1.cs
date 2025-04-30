// Tyler Bye
// 04.14.2025
// Creating Dynamic Tre 

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Dynamic_Tree_PE
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // The three trees
        private Tree treeRed;
        private Tree treeGreen;
        private Tree treeBlue;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Initialize all three trees
            treeRed = new Tree(_spriteBatch, Color.Red);
            treeGreen = new Tree(_spriteBatch, Color.Green);
            treeBlue = new Tree(_spriteBatch, Color.DodgerBlue);

            // creates random number generator
            Random rng = new Random();

            // red tree - balanced distribution (values between -50 and 50)
            for (int i = 0; i < 100; i++)
            {
                treeRed.Add(rng.Next(-50, 51));
            }

            // blue tree - mostly right-branching (starts with a base value and mostly add larger numbers)
            int baseValue = 50;
            treeBlue.Add(baseValue);

            for (int i = 0; i < 99; i++)
            {
                // 70% chance to add a larger number
                if (rng.NextDouble() < 0.7)
                {
                    // adds a number between 'current + 1' and 'current + 10'
                    treeBlue.Add(baseValue + rng.Next(1, 11));
                }
                else
                {
                    // adds a number between 'current - 10' and 'current - 1'
                    treeBlue.Add(baseValue - rng.Next(1, 11));
                }
            }

            // green tree - strictly right-branching (increasing values)
            for (int i = 0; i < 100; i++)
            {
                treeGreen.Add(i);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // *************************************************************
            // *** After you have the rest of the assignment working: ******
            //  What happens if you insert a new piece of 
            //  data into the trees each frame?


            // *************************************************************

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Draw the trees
            treeRed.Draw(new Vector2(200, 400));
            treeGreen.Draw(new Vector2(400, 400));
            treeBlue.Draw(new Vector2(600, 400));

            base.Draw(gameTime);
        }

    }
}

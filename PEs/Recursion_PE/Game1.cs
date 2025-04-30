// Tyler Bye
// 3.29.2024
// Drawing Shapes Using Recursion

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShapeBatchUtils;
using System;

namespace Recursion_PE
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // makes the defualt fractal to state 1
        private int currentFractal = 1;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        #region ---INITIALIZE---
        protected override void Initialize()
        {
            // creates phrases to test
            string[] testPhrases = { "tacocat", "TacoCat", "Banana", "barb" };

            // tests each phrase for palindromes
            foreach (string phrase in testPhrases)
            {
                bool isPal = IsPalindrome(phrase, 0, phrase.Length - 1);

                // prints to the console if the phrase is a palindrome
                if (isPal)
                {
                    System.Diagnostics.Debug.WriteLine($"\"{phrase}\" IS a palindrome");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"\"{phrase}\" is NOT a palindrome");
                }
            }

            base.Initialize();
        } 
        #endregion

        #region ---LOAD---
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        } 
        #endregion

        #region ---UPDATE---
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // switches between fractal states
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
                currentFractal = 1;
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
                currentFractal = 2;

            base.Update(gameTime);
        } 
        #endregion

        #region ---DRAW---
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            ShapeBatch.Begin(GraphicsDevice);

            // tests which fractal state
            if (currentFractal == 1)
            {
                // draws the tree fractal
                RecursiveTree(new Vector2(400, 400), 100, MathHelper.PiOver2);
            }
            else if (currentFractal == 2)
            {
                // creates 3 points to use for the koch snowflake
                Vector2 p1 = new Vector2(300, 150);
                Vector2 p2 = new Vector2(500, 150);
                Vector2 p3 = new Vector2(400, 350);

                // creates a connected fractal using each others starting & ending points
                KochSnowflake(p1, p2, 3);
                KochSnowflake(p2, p3, 3);
                KochSnowflake(p3, p1, 3);
            }

            ShapeBatch.End();
            base.Draw(gameTime);
        } 
        #endregion

        #region ---HELPERS---
        /// <summary>
        /// Draws several example shapes to showcase ShapeBatch capabilities
        /// </summary>
        /// <param name="gt">Game time information</param>
        public void DrawExampleShapes(GameTime gt)
        {
            // Sample values for "animation"
            float rotation = (float)gt.TotalGameTime.TotalSeconds;
            float sinWave = MathF.Sin(rotation) + 2.0f;

            // Lines
            ShapeBatch.Line(new Vector2(50, 50), new Vector2(150, 70), Color.White);
            ShapeBatch.Line(new Vector2(50, 150), new Vector2(150, 220), 8.0f, Color.Green, Color.Yellow);
            ShapeBatch.Line(new Vector2(100, 250), 75.0f, rotation, Color.DarkKhaki);
            ShapeBatch.Line(new Vector2(100, 350), 75.0f, rotation + MathF.PI / 2.0f, 10.0f, Color.CadetBlue);

            // Boxes
            ShapeBatch.Box(200, 50, 50, 50, Color.IndianRed, Color.IndianRed, Color.BlueViolet, Color.BlueViolet);
            ShapeBatch.Box(new Rectangle(220, 150, 50, 60), Color.RosyBrown);

            // Outline boxes
            ShapeBatch.BoxOutline(200, 250, 50, 50, Color.Gainsboro);
            ShapeBatch.BoxOutline(new Rectangle(220, 350, 75, 60), Color.Red, Color.Green, Color.Blue, Color.White);

            // Circles
            ShapeBatch.Circle(new Vector2(350, 75), sinWave * 20.0f, Color.Black, Color.Aquamarine);
            ShapeBatch.Circle(new Vector2(350, 225), sinWave * 10.0f + 20.0f, 8, Color.AliceBlue);
            ShapeBatch.Circle(new Vector2(350, 375), sinWave * 5.0f + 20.0f, 5, -rotation, Color.LightGoldenrodYellow);

            // Outline circles
            ShapeBatch.CircleOutline(new Vector2(425, 125), 25.0f, Color.Aquamarine);
            ShapeBatch.CircleOutline(new Vector2(425, 275), 30.0f, 8, Color.AliceBlue);
            ShapeBatch.CircleOutline(new Vector2(425, 425), 35.0f, 5, rotation, Color.LightGoldenrodYellow);

            // Triangles
            ShapeBatch.Triangle(new Vector2(550, 75), 100, Color.Orange, Color.OrangeRed, Color.Yellow);
            ShapeBatch.Triangle(new Vector2(550, 250), 75, rotation, Color.Red, Color.ForestGreen, Color.Blue);
            ShapeBatch.Triangle(new Vector2(550, 475), new Vector2(475, 420), new Vector2(675, 350), Color.CornflowerBlue, Color.DarkBlue, Color.LightBlue);

            // Outline triangles
            ShapeBatch.TriangleOutline(new Vector2(650, 150), 60, Color.Orange);
            ShapeBatch.TriangleOutline(new Vector2(650, 275), 60, rotation / -2.0f, Color.Red, Color.ForestGreen, Color.Blue);
            ShapeBatch.TriangleOutline(new Vector2(650, 450), new Vector2(550, 360), new Vector2(750, 400), Color.CornflowerBlue);
        }

        /// <summary>
        /// Recursively draws a fractal tree.
        /// </summary>
        /// <param name="position">Starting position of the line</param>
        /// <param name="length">Length of the line</param>
        /// <param name="angle">Angle of the line</param>
        void RecursiveTree(Vector2 position, float length, float angle)
        {
            // stops when length is too small
            if (length < 5)
                return;

            // draws the current branch/trunk
            Vector2 endPos = ShapeBatch.Line(position, length, angle, 2f, Color.White);

            // recursive calls for left and right branches
            float newLength = length * 0.7f; // 70% of parent length
            RecursiveTree(endPos, newLength, angle - MathHelper.PiOver4 / 2); // left branch
            RecursiveTree(endPos, newLength, angle + MathHelper.PiOver4 / 2); // right branch
        }

        /// <summary>
        /// Recursively draws a fractal Koch Snowflake.
        /// </summary>
        /// <param name="start">Starting point of the curve</param>
        /// <param name="end">Ending point of the curve</param>
        /// <param name="depth">Current recursion depth</param>
        void KochSnowflake(Vector2 start, Vector2 end, int depth)
        {
            // draws a line
            if (depth == 0)
            {
                ShapeBatch.Line(start, end, 1f, Color.Cyan);
                return;
            }

            // calculates the points for the curve, spliting into 3 segments
            Vector2 dir = end - start;
            Vector2 oneThird = start + dir / 3f;
            Vector2 twoThirds = start + dir * (2f / 3f);

            // calculates the peak point to form an equilateral triangle
            Vector2 middle = start + dir / 2f;
            Vector2 peak = middle + new Vector2(-dir.Y, dir.X) / (3f * (float)Math.Sqrt(3));

            // recursive draws all 4 segments of the curve
            KochSnowflake(start, oneThird, depth - 1);  // left
            KochSnowflake(oneThird, peak, depth - 1);  // left to peak
            KochSnowflake(peak, twoThirds, depth - 1); // right from peak
            KochSnowflake(twoThirds, end, depth - 1);  // right 
        }

        /// <summary>
        /// Checks if the phrase is a palindrome.
        /// </summary>
        /// <param name="phrase">Specific phrase given</param>
        /// <param name="startIndex">Starting index to compare</param>
        /// <param name="endIndex">Ending index to compare</param>
        /// <returns>True or False of it being a palindrome</returns>
        public bool IsPalindrome(string phrase, int startIndex, int endIndex)
        {
            // returns true if all characters were checked
            if (startIndex >= endIndex)
                return true;

            // returns false if characters don't match
            if (char.ToLower(phrase[startIndex]) != char.ToLower(phrase[endIndex]))
                return false;

            // recursive checks next pair of characters
            return IsPalindrome(phrase, startIndex + 1, endIndex - 1);
        } 
        #endregion
    }
}

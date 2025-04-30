using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ByeT_HW2
{
    /// <summary>
    /// Player data.
    /// </summary>
    class Player : GameObject
    {
        private int windowWidth;
        private int windowHeight;
        private float speed = 2.5f;

        private bool isDashAvailable = true;
        private float dashDistance = 100f;
        private float dashCooldown = 2f;
        private float dashCooldownTimer = 0f;
        private Vector2 lastDirection;

        /// <summary>
        /// Read-ONLY Property of the Dash CD
        /// </summary>
        public float DashCooldownTimer
        {
            get
            {
                return dashCooldownTimer;
            }
        }
        /// <summary>
        /// Read-ONLY Property of if the Dash is happening
        /// </summary>
        public bool IsDashAvailable
        {
            get
            {
                return isDashAvailable;
            }
        }

        // ERIN SAID I COULD DO THIS WHEN THEY ARENT DECLARED FIELDS
        public int LevelScore { get; set; }
        public int TotalScore { get; set; }

        /// <summary>
        /// Main contrustor for Player.
        /// </summary>
        /// <param name="texture">Texture of player</param>
        /// <param name="position">Position of player</param>
        /// <param name="windowWidth">Width of window</param>
        /// <param name="windowHeight">Height of window</param>
        public Player(Texture2D texture, Rectangle position, int windowWidth, int windowHeight)
            : base(texture, position)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        /// <summary>
        /// Main update method of Player.
        /// </summary>
        /// <param name="gameTime">Runtime of the game</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // creates empty vector for movement
            Vector2 movement = new Vector2(0, 0);

            // decreases cooldown timer if above zero & checks if dash is available
            if (dashCooldownTimer > 0)
            {
                dashCooldownTimer -= deltaTime;
                isDashAvailable = false;
            }
            else
            {
                isDashAvailable = true;
            }

            // character movement using WASD
            if (kb.IsKeyDown(Keys.W))
            {
                movement.Y -= speed;
            }
            if (kb.IsKeyDown(Keys.A))
            {
                movement.X -= speed;
            }
            if (kb.IsKeyDown(Keys.S))
            {
                movement.Y += speed;
            }
            if (kb.IsKeyDown(Keys.D))
            {
                movement.X += speed;
            }

            // applies movement to player's position
            position.X += (int)(movement.X * speed);
            position.Y += (int)(movement.Y * speed);

            // stores last movement direction if there is movement
            if (movement.X != 0 || movement.Y != 0)
            {
                float length = movement.Length();
                lastDirection = new Vector2(movement.X / length, movement.Y / length);
            }

            // triggers dash ability
            if (kb.IsKeyDown(Keys.Space))
            {
                PerformDash();
            }

            // wraps player around the screen
            if (position.X < 0)
            {
                position.X = windowWidth - position.Width;
            }
            if (position.X > windowWidth)
            {
                position.X = 0;
            }
            if (position.Y < 0)
            {
                position.Y = windowHeight - position.Height;
            }
            if (position.Y > windowHeight)
            {
                position.Y = 0;
            }
        }

        /// <summary>
        /// Performs a dash in the last movement direction.
        /// </summary>
        private void PerformDash()
        {
            // tests if dash cooldown has ended and player has previously moved
            if (dashCooldownTimer <= 0 && lastDirection != Vector2.Zero)
            {
                // moves the player position quickly in the last known direction
                position.X += (int)(lastDirection.X * dashDistance);
                position.Y += (int)(lastDirection.Y * dashDistance);

                // resets cooldown timer
                dashCooldownTimer = dashCooldown;
            }
        }

        /// <summary>
        /// Calculates the center of the screen.
        /// </summary>
        public void Center()
        {
            position.X = (windowWidth / 2) - (position.Width / 2);
            position.Y = (windowHeight / 2) - (position.Height / 2);
        }

        /// <summary>
        /// Main draw method of Player.
        /// </summary>
        /// <param name="spriteBatch">General game SpriteBatch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Demo
{
    public class Game1 : Game
    {
        // Pre-generated fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Texture field
        private Texture2D myTotoroImage;
        private Vector2 position;
        private Rectangle positionRec;
        private SpriteFont arial36Bold;
        private KeyboardState currentState;
        private KeyboardState previousState;
        private KeyboardState kbState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            position = new Vector2(100, 75);

            currentState = Keyboard.GetState();
            previousState = Keyboard.GetState();
            kbState = Keyboard.GetState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            myTotoroImage = Content.Load<Texture2D>("TotoroDrawing");
            arial36Bold = Content.Load<SpriteFont>("arial-36-bold");

            positionRec = new Rectangle(
                50,
                50,
                50,
                50
            );
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // System.Diagnostics.Debug.WriteLine("Debugging");

            // Move the image
            position.Y += 1;

            kbState = Keyboard.GetState();
            currentState = Keyboard.GetState();

            if(currentState.IsKeyDown(Keys.Space) && 
               previousState.IsKeyDown(Keys.Space))
            {
                // TODO: ADD CODE
            }

            previousState = currentState;

            // bool enterIsPressed = kbState.IsKeyDown(Keys.Enter);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(
                myTotoroImage,      // Texture2d
                position,           // (100, 75)
                Color.White         // White
            );

            _spriteBatch.Draw(
                myTotoroImage,      // Texture2d
                positionRec,        // (100, 75)
                Color.Green         // Green
            );

            _spriteBatch.DrawString(
                arial36Bold,
                "Hi all",
                new Vector2(0, 0),
                Color.Indigo
            );

            if (kbState.IsKeyDown(Keys.Enter))
            {
                _spriteBatch.DrawString(
                    arial36Bold,
                    (497).ToString(),
                    new Vector2(0, 0),
                    Color.Indigo
                );
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

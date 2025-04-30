using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


// ****************************************************************************
// Tyler Bye
// 02/17/25
// FileIO & Dictionaries To Use TileMaps
// ****************************************************************************



namespace TileSet_Dictionaries
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Level of LevelTile objects
        private Level myLevel;

        // Sprite Sheet (where all images are drawn from)
        private Texture2D spriteSheet;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            // Change size to fit an 8 x 8 grid of 64 X 64 tiles.
            _graphics.PreferredBackBufferWidth = 512;
            _graphics.PreferredBackBufferHeight = 512;
            _graphics.ApplyChanges();

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

            // Load all necessary content
            spriteSheet = Content.Load<Texture2D>("roguelikeSheet_transparent");
            myLevel = new Level(spriteSheet, "../../../Content/textureMappingData.txt", _spriteBatch);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Ensure that though tiles are stretched from 16x16 to 64x64, that interpolation
            //   does not occur between sprites.
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            // Draw all LevelTiles to the game window.
            myLevel.DisplayTiles();

            // End drawing.
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
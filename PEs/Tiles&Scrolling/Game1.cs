using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tiles_Scrolling
{
    public class Game1 : Game
    {
        #region ---VARIABLES---
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // tile
        private Texture2D[] _tileTextures;
        private Texture2D[,] _map;
        private const int TileSize = 128;
        private const int MapWidth = 20;
        private const int MapHeight = 20;

        // player
        private Texture2D _playerTexture;
        private Vector2 _playerScreenPosition; // where player is drawn on screen
        private Vector2 _playerWorldPosition;  // player's position in the world
        private float _playerSpeed = 200f;     // speed in pixels per second

        // for the window boundaries
        private int _screenWidth;
        private int _screenHeight;

        // camera
        private Vector2 _cameraPosition;

        // random generator for tile selection
        private Random _random;
        #endregion

        #region ---CONSTRUCTOR---
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;

            _random = new Random();
        } 
        #endregion

        #region ---INITIALIZE---
        protected override void Initialize()
        {
            _screenWidth = _graphics.PreferredBackBufferWidth;
            _screenHeight = _graphics.PreferredBackBufferHeight;

            _map = new Texture2D[MapWidth, MapHeight];

            base.Initialize();
        } 
        #endregion

        #region ---LOAD---
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _tileTextures = new Texture2D[5];
            _tileTextures[0] = Content.Load<Texture2D>("tile1");
            _tileTextures[1] = Content.Load<Texture2D>("tile2");
            _tileTextures[2] = Content.Load<Texture2D>("tile3");
            _tileTextures[3] = Content.Load<Texture2D>("tile4");
            _tileTextures[4] = Content.Load<Texture2D>("tile5");
            _playerTexture = Content.Load<Texture2D>("player");

            // randomly fill the map with tiles
            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    int tileIndex = _random.Next(0, _tileTextures.Length);
                    _map[x, y] = _tileTextures[tileIndex];
                }
            }

            // fixed player screen position to center of screen
            _playerScreenPosition = new Vector2(
                _screenWidth / 2 - _playerTexture.Width / 2,
                _screenHeight / 2 - _playerTexture.Height / 2);

            // initialize player world position at center of the map
            _playerWorldPosition = new Vector2(
                (MapWidth * TileSize) / 2 - _playerTexture.Width / 2,
                (MapHeight * TileSize) / 2 - _playerTexture.Height / 2);

            _cameraPosition = Vector2.Zero;
        } 
        #endregion

        #region ---UPDATE---
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // get elapsed time for smooth movement
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyboardState = Keyboard.GetState();

            // move player based on input
            Vector2 movement = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.W))
                movement.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.S))
                movement.Y += 1;
            if (keyboardState.IsKeyDown(Keys.A))
                movement.X -= 1;
            if (keyboardState.IsKeyDown(Keys.D))
                movement.X += 1;

            // normalize movement vector if moving diagonally
            if (movement != Vector2.Zero)
            {
                movement.Normalize();

                // update player world position
                Vector2 newPosition = _playerWorldPosition + movement * _playerSpeed * deltaTime;

                // clamp player world position to map boundaries
                newPosition.X = MathHelper.Clamp(newPosition.X, 0, MapWidth * TileSize - _playerTexture.Width);
                newPosition.Y = MathHelper.Clamp(newPosition.Y, 0, MapHeight * TileSize - _playerTexture.Height);

                _playerWorldPosition = newPosition;
            }

            UpdateCamera();

            base.Update(gameTime);
        } 
        #endregion

        #region ---DRAW---
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            // determine which tiles are visible on screen
            int startX = Math.Max(0, (int)(_cameraPosition.X / TileSize));
            int startY = Math.Max(0, (int)(_cameraPosition.Y / TileSize));
            int endX = Math.Min(MapWidth - 1, (int)((_cameraPosition.X + _screenWidth) / TileSize) + 1);
            int endY = Math.Min(MapHeight - 1, (int)((_cameraPosition.Y + _screenHeight) / TileSize) + 1);

            // draw only visible tiles (optimization)
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    // calculate the position to draw this tile
                    Vector2 tilePosition = new Vector2(x * TileSize, y * TileSize);

                    // convert from world position to screen position
                    Vector2 screenPosition = tilePosition - _cameraPosition;

                    // draw the tile
                    _spriteBatch.Draw(_map[x, y], screenPosition, Color.White);
                }
            }

            // calculate player's actual screen position based on world position and camera
            Vector2 playerDrawPosition = _playerWorldPosition - _cameraPosition;

            // draw the player at the calculated screen position
            _spriteBatch.Draw(_playerTexture, playerDrawPosition, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        } 
        #endregion

        #region ---HELPERS---
        private void UpdateCamera()
        {
            // calculate ideal camera position (centered on player)
            Vector2 idealCameraPos = _playerWorldPosition - _playerScreenPosition;

            // ensure camera doesn't show outside the map boundaries
            float maxCameraX = (MapWidth * TileSize) - _screenWidth;
            float maxCameraY = (MapHeight * TileSize) - _screenHeight;

            // clamp camera position to map boundaries
            _cameraPosition.X = MathHelper.Clamp(idealCameraPos.X, 0, maxCameraX);
            _cameraPosition.Y = MathHelper.Clamp(idealCameraPos.Y, 0, maxCameraY);
        } 
        #endregion
    }
}

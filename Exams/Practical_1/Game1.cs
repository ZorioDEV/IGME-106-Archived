// Tyler Bye
// 02/28/25
// Practial #1
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Practical_1;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // assets of the project
    private Texture2D texture;
    private SpriteFont consolas24;

    // window height & width
    int windowX;
    int windowY;

    // random X & Y positions
    int randomX;
    int randomY;

    // adds to the amoungus Count
    int amongusCount;
    
    // random generated number
    private Random rng;

    // stores all the amoungus rectagle images
    List<Rectangle> amongusList = new List<Rectangle>();
    List<Rectangle> killList = new List<Rectangle>();

    // previously pressed key
    private KeyboardState previousKBState;

    /// <summary>
    /// Enum for all the GameStates
    /// </summary>
    public enum GameState
    {
        PositionState,
        RemovalState
    }

    // current state of the game
    private GameState currentState;

    /// <summary>
    /// Main game constructor
    /// </summary>
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    /// <summary>
    /// Initializes certain variables at the start.
    /// </summary>
    protected override void Initialize()
    {
        _graphics.PreferredBackBufferHeight = 800;
        _graphics.PreferredBackBufferWidth = 800;

        windowX = GraphicsDevice.Viewport.Width;
        windowY = GraphicsDevice.Viewport.Height;

        amongusCount = 0;
        rng = new Random();

        base.Initialize();
    }

    /// <summary>
    /// Loads the content and sprites.
    /// </summary>
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        texture = Content.Load<Texture2D>("amongus");
        consolas24 = Content.Load<SpriteFont>("consolas24");
    }

    /// <summary>
    /// Updates the game logic constantly.
    /// </summary>
    /// <param name="gameTime">Time of the game.</param>
    protected override void Update(GameTime gameTime)
    {
        // gets the keyboard and mouse states
        KeyboardState currentKBState = Keyboard.GetState();
        Vector2 msPos = Mouse.GetState().Position.ToVector2();

        // allows for menu switching
        if (currentKBState.IsKeyDown(Keys.C) &&
            previousKBState.IsKeyUp(Keys.C))
        {
            // switches to removal
            if (currentState == GameState.PositionState)
            {
                currentState = GameState.RemovalState;
            }
            // switches to position
            else
            {
                currentState = GameState.PositionState;
            }
        }

        // tests which state the game is currently in
        switch (currentState)
        {
            // allows for users to place down amongus
            case GameState.PositionState:
                if (currentKBState.IsKeyDown(Keys.P) &&
                    previousKBState.IsKeyUp(Keys.P))
                {
                    // get random positions
                    randomX = rng.Next(0, windowX + 1);
                    randomY = rng.Next(0, windowY + 1);

                    // adds a rectangle to the list
                    amongusList.Add(new Rectangle(randomX, randomY, 100, 100));

                    // updates amongus counter
                    amongusCount++;
                }
                    break;
            // allows for users to remove amongus
            case GameState.RemovalState:
                Rectangle mouseRect = new Rectangle(
                    (int)msPos.X,
                    (int)msPos.Y,
                    1,
                    1
                );

                // if mouse collides with rectangle image of a amongus
                foreach (Rectangle amongus in amongusList)
                {
                    if (mouseRect.Intersects(amongus) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        killList.Add(amongus);

                        // lower amongus counter amount
                        amongusCount--;
                    }
                }
                // remove that amongus from the list
                foreach (Rectangle amongus in killList)
                {
                    amongusList.Remove(amongus);
                }
                break;
            default:
                break;
        }

        // updates previous key pressed
        previousKBState = currentKBState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();

        // test which state the game is currently in
        switch (currentState)
        {
            case GameState.PositionState:
                // makes background white
                GraphicsDevice.Clear(Color.White);

                // prints title of game state
                _spriteBatch.DrawString(
                    consolas24,
                    "POSITION STATE",
                    new Vector2(20,20),
                    Color.Black
                );

                // prints name
                _spriteBatch.DrawString(
                    consolas24,
                    "Tyler Bye",
                    new Vector2((float)windowX - 250, (float)windowY - 150),
                    Color.Black
                );

                // prints amoung us counter
                _spriteBatch.DrawString(
                    consolas24,
                    $"Amongus: {amongusCount}",
                    new Vector2((float)windowX - 250, (float)windowY - 100),
                    Color.Black
                );

                // goes througth the list and randomly prints out the amongus
                foreach (Rectangle amongus in amongusList)
                {
                    _spriteBatch.Draw(texture, amongus, Color.White);
                }

                break;
            case GameState.RemovalState:
                // makes background white
                GraphicsDevice.Clear(Color.Yellow);

                // prints title of game state
                _spriteBatch.DrawString(
                    consolas24,
                    "REMOVAL STATE",
                    new Vector2(20, 20),
                    Color.Black
                );

                // prints name
                _spriteBatch.DrawString(
                    consolas24,
                    "Tyler Bye",
                    new Vector2((float)windowX - 250, (float)windowY - 150),
                    Color.Black
                );

                // prints amoung us counter
                _spriteBatch.DrawString(
                    consolas24,
                    $"Amongus: {amongusCount}",
                    new Vector2((float)windowX - 250, (float)windowY - 100),
                    Color.Black
                );

                // goes througth the list and randomly prints out the amongus
                foreach (Rectangle amongus in amongusList)
                {
                    _spriteBatch.Draw(texture, amongus, Color.Red);
                }

                break;
            default:
                // does nothing, impossible to get to
                break;
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}

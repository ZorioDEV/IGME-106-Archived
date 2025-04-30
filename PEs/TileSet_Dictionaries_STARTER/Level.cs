using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.IO;

namespace TileSet_Dictionaries
{
    /// <summary>
    /// Class representing a series of LevelTile objects in a top-down game.
    /// </summary>
    public class Level
    {
        // Drawing: Draw the level to game window
        private SpriteBatch _spriteBatch;

        // Level design: Need a set of tiles for the floor
        private LevelTile[,] tileSet;

        // Level design: What size should each floor tile be?
        private int intendedSize;

        // Texture mapping: Which sprite sheet is the level pulling images from?
        private Texture2D spriteSheet;

        // Texture mapping: Uses a file to store texture name to source rectangle information
        private Dictionary<string, Rectangle> textureMap;


        /// <summary>
        /// Constructs a Level object.
        /// </summary>
        /// <param name="spriteSheet">Which image are the sprites coming from?</param>
        /// <param name="filepath">Which file holds image data?</param>
        public Level(Texture2D spriteSheet, string filepath, SpriteBatch _spriteBatch)
        {
            // *** SET FIELDS OF THE CLASS: ***
            // ------------------------------------------------------------------------------------
            // SpriteBatch reference is set
            this._spriteBatch = _spriteBatch;
            
            // Each LevelTile is 64 x 64 pixels by default
            intendedSize = 64;

            // The sprite sheet that contains all images
            this.spriteSheet = spriteSheet;

            // The Dictionary calculates a source rectangle for each of the chosen image tiles
            textureMap = new Dictionary<string, Rectangle>();


            // *** TEXTURE MAPPING IS PERFORMED HERE: ***
            // ------------------------------------------------------------------------------------
            // Need to know each sprite's size for texture mapping
            // (This info will come from the file. For now, set to 0 before we read the data.)
            int spriteWidth = 0;
            int spriteHeight = 0;

            try
            {
                StreamReader reader = new StreamReader(filepath);

                // Get string variables ready for file lines being split!
                string line = "";
                string[] splitData = null;

                while ( (line = reader.ReadLine()) != null)
                {
                    // ************************************************************************
                    // skips lines starting with / and -
                    if (line.StartsWith("/") || line.StartsWith("-"))
                    {
                        continue;
                    }

                    // splits line using comma
                    splitData = line.Split(',');

                    // tests if there are exactly 2 elements & assigns them as the sprite's width and height
                    if (splitData.Length == 2)
                    {
                        spriteWidth = int.Parse(splitData[0]);
                        spriteHeight = int.Parse(splitData[1]);
                    }

                    // tests if there are 3 elements & assigns them as a texture mapping
                    else if (splitData.Length == 3)
                    {
                        string textureName = splitData[0];
                        int row = int.Parse(splitData[1]);
                        int col = int.Parse(splitData[2]);

                        // calculates the x and y coordinates with single pixel separator
                        int x = col * (spriteWidth + 1);
                        int y = row * (spriteHeight + 1);

                        textureMap[textureName] = new Rectangle(x, y, spriteWidth, spriteHeight);
                    }
                    // ************************************************************************
                }

                // Close the stream
                reader.Close();
            }
            catch(Exception error)
            {
                System.Diagnostics.Debug.WriteLine("FILE-READING ERROR UPON CONSTRUCTING LEVEL!");
                System.Diagnostics.Debug.WriteLine(error.Message);
            }


            // *** TEST LEVELS: ***
            // ------------------------------------------------------------------------------------
            // Test BEFORE the dictionary is in place.
            //TestLevel_BEFORE_Dictionary();

            // Test AFTER the dictionary is in place, but BEFORE level data is read in.
            //TestLevel_AFTER_Dictionary_NoLevelData();

            // Test AFTER everything is complete!
            LoadLevel("../../../Content/level1.csv");
        }


        #region Displaying Tiles in the window
        /// <summary>
        /// Draw all LevelTiles to the game window.
        /// </summary>
        /// <param name="_spriteBatch">SpriteBatch object (passed in from Game1 Draw)</param>
        public void DisplayTiles()
        {
            // Iterate and draw all tiles in the 2D array of LevelTiles.
            for(int r = 0; r < tileSet.GetLength(0); r++)
            {
                for(int c = 0; c < tileSet.GetLength(1); c++)
                {
                    tileSet[r, c].Draw(_spriteBatch);
                }
            }
        }
        #endregion


        #region Level Testing Methods called in the constructor
        /// <summary>
        /// Creates a 3 x 3 set of tiles that focus on the water feature.
        /// Does not need neither the texture mapping dictionary nor level data from files.
        /// </summary>
        public void TestLevel_BEFORE_Dictionary()
        {
            // Generate a 3 x 3 set of LevelTiles.
            tileSet = new LevelTile[3,3];

            // First row of tiles:
            tileSet[0, 0] = 
                new LevelTile(
                    spriteSheet,                                            // Sprite sheet to pull from
                    new Rectangle(0, 0, intendedSize, intendedSize),        // Drawn location, width and height
                    0,                                                      // Sprite sheet info: Which row?
                    2);                                                     // Sprite sheet info: Which column?
            tileSet[0, 1] = 
                new LevelTile(
                    spriteSheet, 
                    new Rectangle(64, 0, intendedSize, intendedSize), 
                    0, 
                    3);
            tileSet[0, 2] = 
                new LevelTile(
                    spriteSheet, 
                    new Rectangle(128, 0, intendedSize, intendedSize), 
                    0, 
                    4);

            // Second row of tiles:
            tileSet[1, 0] = 
                new LevelTile(
                    spriteSheet, 
                    new Rectangle(0, 64, intendedSize, intendedSize), 
                    1, 
                    2);
            tileSet[1, 1] = 
                new LevelTile(
                    spriteSheet, 
                    new Rectangle(64, 64, intendedSize, intendedSize), 
                    1, 
                    3);
            tileSet[1, 2] = 
                new LevelTile(
                    spriteSheet, 
                    new Rectangle(128, 64, intendedSize, intendedSize), 
                    1, 
                    4);

            // Third row of tiles:
            tileSet[2, 0] = 
                new LevelTile(
                    spriteSheet, 
                    new Rectangle(0, 128, intendedSize, intendedSize), 
                    2, 
                    2);
            tileSet[2, 1] = 
                new LevelTile(
                    spriteSheet, 
                    new Rectangle(64, 128, intendedSize, intendedSize), 
                    2, 
                    3);
            tileSet[2, 2] = 
                new LevelTile(
                    spriteSheet, 
                    new Rectangle(128, 128, intendedSize, intendedSize), 
                    2, 
                    4);
        }

        /// <summary>
        /// Creates a 3 x 3 set of tiles that focus on the water feature.
        /// Requires the texture mapping dictionary, but not level data.
        /// </summary>
        public void TestLevel_AFTER_Dictionary_NoLevelData()
        {
            // Generate a 3 x 3 set of LevelTiles.
            tileSet = new LevelTile[3, 3];

            // First row of tiles:
            tileSet[0, 0] = 
                new LevelTile(
                    spriteSheet,                                                // Sprite sheet to pull from
                    new Rectangle(0, 0, intendedSize, intendedSize),            // Drawn location, width and height 
                    textureMap["WATER-corner-upper-left"]);                     // Looksup course rectangle from the texture map dictionary
            tileSet[0, 1] = 
                new LevelTile(
                    spriteSheet, 
                    new Rectangle(64, 0, intendedSize, intendedSize), 
                    textureMap["WATER-edge-top"]);
            tileSet[0, 2] = 
                new LevelTile(
                    spriteSheet, 
                    new Rectangle(128, 0, intendedSize, intendedSize), 
                    textureMap["WATER-corner-upper-right"]);

            // Second row of tiles:
            tileSet[1, 0] = 
                new LevelTile(
                    spriteSheet, 
                    new Rectangle(0, 64, intendedSize, intendedSize), 
                    textureMap["WATER-edge-left"]);
            tileSet[1, 1] = 
                new LevelTile(
                    spriteSheet, 
                    new Rectangle(64, 64, intendedSize, intendedSize), 
                    textureMap["WATER-inner"]);
            tileSet[1, 2] = 
                new LevelTile(
                    spriteSheet, 
                    new Rectangle(128, 64, intendedSize, intendedSize), 
                    textureMap["WATER-edge-right"]);

            // Third row of tiles:
            tileSet[2, 0] = 
                new LevelTile(
                    spriteSheet, 
                    new Rectangle(0, 128, intendedSize, intendedSize), 
                    textureMap["WATER-corner-lower-left"]);
            tileSet[2, 1] = 
                new LevelTile(
                    spriteSheet, 
                    new Rectangle(64, 128, intendedSize, intendedSize), 
                    textureMap["WATER-edge-bottom"]);
            tileSet[2, 2] = 
                new LevelTile(
                    spriteSheet, 
                    new Rectangle(128, 128, intendedSize, intendedSize), 
                    textureMap["WATER-corner-lower-right"]);
        }
        #endregion


        /// <summary>
        /// Reads data from a level file.
        /// </summary>
        /// <param name="filepath">Which level file to use?</param>
        public void LoadLevel(string filepath)
        {
            // Read data from a text file and create the tileset that way
            try
            {
                // ***SETUP VARIABLES FOR FILE READING: ***
                // ------------------------------------------------------------------------------------
                StreamReader reader = new StreamReader(filepath);
                string line = "";
                string[] splitData = null;

                // Needed for determining individual tile data placement
                int currentRow = 0;

                // ***GET DATA FROM FIRST 2 LINES FOR TILE INFORMATION ***
                // ------------------------------------------------------------------------------------
                // The first 2 lines give information about this level:
                // Line 1: How large should the level tiles be?
                line = reader.ReadLine();
                splitData = line.Split(',');
                int tileWidth = int.Parse(splitData[1]);
                int tileHeight = int.Parse(splitData[2]);

                // Line 2: How many tiles are there?
                line = reader.ReadLine();
                splitData = line.Split(',');
                int tilesetColumns = int.Parse(splitData[1]);
                int tilesetRows = int.Parse(splitData[2]);

                // Initialize the tileSet array to the correct size
                tileSet = new LevelTile[tilesetColumns, tilesetRows];

                // ***READ TILE TEXTURE INFORMATION TO GENERATE LEVELTILES ***
                // ------------------------------------------------------------------------------------
                // Read data line by line for tiles
                while ((line = reader.ReadLine()) != null)
                {
                    // Get this line of tile data and split by comma.
                    // That gives us data like: "WATER-inner,GRASS-1,DIRT-2"
                    splitData = line.Split(',');

                    // For each of the tiles across a row...
                    for(int c = 0; c < splitData.Length; c++)
                    {
                        // ************************************************************************
                        string tileType = splitData[c];

                        // test if the tileType exists in dictionary
                        if (textureMap.ContainsKey(tileType))
                        {
                            // calculates position in world
                            int xPos = c * tileWidth;
                            int yPos = currentRow * tileHeight;

                            // creates LevelTile and places it in an array
                            tileSet[currentRow, c] = new LevelTile(
                                spriteSheet,
                                new Rectangle(
                                    xPos,
                                    yPos,
                                    tileWidth,
                                    tileHeight
                                ),
                                textureMap[tileType]
                            );
                        }
                        // ************************************************************************
                    }

                    // Increase the row
                    currentRow++;
                }

                // Close the stream
                reader.Close();
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine("FILE-READING ERROR UPON LOADING LEVEL!");
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
        }
    }
}

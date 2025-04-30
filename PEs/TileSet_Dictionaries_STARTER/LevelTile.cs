using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace TileSet_Dictionaries
{
    /// <summary>
    /// A single floor tile in a top-down game.
    /// </summary>
    public class LevelTile
    {
        private Rectangle drawnRect;
        private Rectangle sourceRect;
        private Texture2D spriteSheet;

        #region Used for testing BEFORE the tile map dictionary is set.

        /// <summary>
        /// Constructs a LevelTile object
        /// </summary>
        /// <param name="spriteSheet">Which image is being used?</param>
        /// <param name="drawnRect">Where will this tile be drawn in the game window?</param>
        /// <param name="row">Which row is the source tile in the sprite sheet?</param>
        /// <param name="column">Which column is the source tile in the sprite sheet?</param>
        public LevelTile(Texture2D spriteSheet, Rectangle drawnRect, int row, int column)
        {
            // Which image is being used?
            this.spriteSheet = spriteSheet;

            // External class will decide where this LevelTile location will be rendered
            this.drawnRect = drawnRect;

            // This class will calculate what to draw from the sprite sheet
            sourceRect = CalculateSourceRectangle(row, column, 16, 16);
        }


        /// <summary>
        /// Calculates the exact source rectangle from a much larger sprite sheet
        /// </summary>
        /// <param name="row">Row of intended image (starts at 0)</param>
        /// <param name="col">Column of intended image (starts at 0)</param>
        /// <param name="width">How wide are the tiles in the sprite sheet?</param>
        /// <param name="height">How high are the tiles in the sprite sheet?</param>
        /// <returns></returns>
        private Rectangle CalculateSourceRectangle(int row, int col, int width, int height)
        {
            return new Rectangle(
                col * height + col, // x = (col number) * (size in pixels) + col (1 pixel between images in the spritesheet)
                row * width + row,  // y = (row number) * (size in pixels) + row (1 pixel between images in the spritesheet)
                width,
                height);
        }
        #endregion


        #region Used AFTER the tile map dictionary is set.

        /// <summary>
        /// Constructs a LevelTile object
        /// </summary>
        /// <param name="spriteSheet">Which image is being used?</param>
        /// <param name="drawnRect">Where will this tile be drawn in the game window?</param>
        /// <param name="sourceRect">Which rectangle in the sprite sheet will be rendered?</param>
        public LevelTile(Texture2D spriteSheet, Rectangle drawnRect, Rectangle sourceRect)
        {
            // Which image is being used?
            this.spriteSheet = spriteSheet;

            // External class will decide where this LevelTile location will be rendered
            //   and where the source rectangle is.
            this.drawnRect = drawnRect;
            this.sourceRect = sourceRect;
        }
        #endregion


        /// <summary>
        /// Render this LevelTile to the game window.
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet, drawnRect, sourceRect, Color.White);
        }
    }
}

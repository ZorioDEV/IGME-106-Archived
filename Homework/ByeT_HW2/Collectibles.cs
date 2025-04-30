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
    /// Coin collectible data.
    /// </summary>
    class Collectibles : GameObject
    {
        /// <summary>
        /// Main constructor of Collectibles.
        /// </summary>
        /// <param name="texture">Texture of the coin</param>
        /// <param name="position">Position of coin</param>
        public Collectibles(Texture2D texture, Rectangle position)
        : base(texture, position)
        { 
        
        }

        /// <summary>
        /// Checks if player is with collectible object.
        /// </summary>
        /// <param name="otherObject">Player object</param>
        /// <returns>True or False</returns>
        public bool CheckCollision(GameObject otherObject)
        {
            return Position.Intersects(otherObject.Position);
        }

        /// <summary>
        /// Main update method of Collectibles.
        /// </summary>
        /// <param name="gameTime">Runtime of the game</param>
        public override void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Main draw method of Collectibles.
        /// </summary>
        /// <param name="spriteBatch">General game SpriteBatch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}

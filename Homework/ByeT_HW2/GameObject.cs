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
    /// General game object class.
    /// </summary>
    abstract class GameObject
    {
        protected Texture2D texture;
        protected Rectangle position;

        /// <summary>
        /// Read-ONLY Property for position
        /// </summary>
        public Rectangle Position
        {
            get
            {
                return position;
            }
        }

        /// <summary>
        /// Main constructor for GameObject.
        /// </summary>
        /// <param name="texture">Texture of the object</param>
        /// <param name="position">Position of the object</param>
        protected GameObject(Texture2D texture, Rectangle position)
        {
            this.texture = texture;
            this.position = position;
        }

        /// <summary>
        /// Main draw method of GameObject.
        /// </summary>
        /// <param name="spriteBatch">General game SpriteBatch</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        /// <summary>
        /// Main update method of GameOject.
        /// </summary>
        /// <param name="gameTime">Runtime of the game</param>
        public abstract void Update(GameTime gameTime);
    }
}

using Microsoft.Xna.Framework;

namespace QuadTree_STARTER
{
	class GameObject
	{
		/// <summary>
		/// This game object's rectangle
		/// </summary>
		public Rectangle Rectangle { get; private set; }

		/// <summary>
		/// This object's color
		/// </summary>
		public Color Color { get; private set; }


		/// <summary>
		/// Creates a new game object
		/// </summary>
		/// <param name="rect">The rectangle for this game object</param>
		/// <param name="color">The color of this object</param>
		public GameObject(Rectangle rect, Color color)
		{
			Rectangle = rect;
			Color = color;
		}
	}
}

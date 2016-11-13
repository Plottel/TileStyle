using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the class responsible for Rendering everything to the screen. Everything that is Rendered
    /// must register itself with the Renderer.
    /// The Renderer will draw everything in passes (i.e. by layer).
    /// </summary>
    public static class Renderer
    {
        /// <summary>
        /// Represents the size of a Tile in the program.
        /// </summary>
        public const int TILE_SIZE = 32;

        /// <summary>
        /// Represents the master list of everything which will be Rendered.
        /// </summary>
        private static List<Renderable> _renderables = new List<Renderable>();

        /// <summary>
        /// Adds the passed in Renderable to the master list of Renderables.
        /// </summary>
        /// <param name="r">The Renderable to be added.</param>
        public static void Register(Renderable r)
        {
            if (!(_renderables.Contains(r)))
                _renderables.Add(r);
        }

        /// <summary>
        /// Removes the passed in Renderable from the master list of Renderables.
        /// </summary>
        /// <param name="r">The Renderable to remove.</param>
        public static void Deregister(Renderable r)
        {
            _renderables.Remove(r);
        }

        /// <summary>
        /// Renders everything in the master list of Renderables. Everything is drawn in layers, e.g.
        ///     All layer 0 Renderables are Rendered, THEN
        ///     All layer 1 Renderables are Rendered, THEN
        ///     All layer 2 Renderables are Rendered, etc.
        /// </summary>
        public static void Render()
        {
            for (Layer layer = 0; layer < Layer.Count; layer++)
            {
                foreach (Renderable r in _renderables)
                {
                    if (r.ExistsOnLayer(layer))
                        r.Render(layer);
                }
            }
        }

        /// <summary>
        /// Returns the current Position of the World. Used to determine Camera positions and Absolute Positions.
        /// </summary>
        /// <returns>The position of the World.</returns>
        public static Point2D WorldPos()
        {
            return WorldAnchor.Instance.Pos;
        }

        /// <summary>
        /// Returns the X coordinate of the World. Used to determine Camera positions and Absolute Positions.
        /// </summary>
        /// <returns>The x coordinate of the World.</returns>
        public static float WorldX()
        {
            return WorldAnchor.Instance.X;
        }

        /// <summary>
        /// Returns the Y coordinate of the World. Used to determine Camera positions and Absolute Positions.
        /// </summary>
        /// <returns>The y coordinate of the World.</returns>
        public static float WorldY()
        {
            return WorldAnchor.Instance.Y;
        }

        /// <summary>
        /// Moves the World by the specified values.
        /// </summary>
        /// <param name="x">The amount to move the World by on the x-axis.</param>
        /// <param name="y">The amount to move the World by on the y-axis.</param>
        public static void MoveWorldBy(float x, float y)
        {
            WorldAnchor.Instance.X += x;
            WorldAnchor.Instance.Y += y;
        }

        /// <summary>
        /// Moves the World to the given x and y coordinates.
        /// </summary>
        /// <param name="x">The x coordinate to move the World to.</param>
        /// <param name="y">The y coordinate to move the World to.</param>
        public static void MoveWorldTo(float x, float y)
        {
            WorldAnchor.Instance.X = x;
            WorldAnchor.Instance.Y = y;
        }

        /// <seealso cref="MoveWorldTo(float, float)"/>
        /// <summary>
        /// Overload for Point2D instead of x / y coordinates.
        /// </summary>
        /// <param name="pos">The position to move the World to.</param>
        public static void MoveWorldTo(Point2D pos)
        {
            WorldAnchor.Instance.Pos = pos;
        }
    }
}
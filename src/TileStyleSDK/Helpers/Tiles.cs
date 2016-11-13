using System;
using System.IO;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the class responsible for handling all things related to Tiles. This includes creation logic, saving and loading.
    /// </summary>
    public static class Tiles
    {
        #region TILE CREATION METHODS

        /// <summary>
        /// Represents the "true" Tile creation method. All other creation methods will manipulate variables
        /// to match this method signature.
        /// </summary>
        /// <returns>The Tile.</returns>
        /// <param name="anchor">The anchor the Tile be drawn relative to.</param>
        /// <param name="pos">The position of the Tile.</param>
        public static Tile CreateTile(Renderable anchor, Point2D pos)
        {
            Tile result = new Tile();
            result.Anchor = anchor;
            result.Pos = pos;

            return result;
        }

        /// <seealso cref="CreateTile(Renderable, Point2D)"/>
        /// <summary>
        /// Overload for x / y coordinates instead of Point2D.
        /// </summary>
        /// <returns>The Tile.</returns>
        public static Tile CreateTile(Renderable anchor, float x, float y)
        {
            return CreateTile(anchor, SwinGame.PointAt(x, y));
        }

        /// <seealso cref="CreateTile(Renderable, Point2D)"/>
        /// <summary>
        /// Tiles are anchored to the World by default.
        /// </summary>
        /// <returns>The Tile.</returns>
        public static Tile CreateTile(Point2D pos)
        {
            return CreateTile(WorldAnchor.Instance, pos);
        }

        /// <seealso cref="CreateTile(Point2D)"/>
        /// <summary>
        /// Overload for x / y coordinates instead of Point2D.
        /// </summary>
        /// <returns>The Tile.</returns>
        public static Tile CreateTile(float x, float y)
        {
            return CreateTile(WorldAnchor.Instance, SwinGame.PointAt(x, y));
        }   

        #endregion TILE CREATION METHODS

        /// <summary>
        /// Saves the passed in Tile to the passed in StreamWriter. This will subsequently
        /// save each of its Slides to the passed in StreamWriter. This is used as a subdivision of
        /// "Save Tileset" as a division of responsibilities.
        /// </summary>
        /// <param name="toSave">The Tile to save.</param>
        /// <param name="writer">The writer to save the Tile to.</param>
        public static void SaveTo(Tile toSave, StreamWriter writer)
        {
            writer.WriteLine(toSave.Pos.X);
            writer.WriteLine(toSave.Pos.Y);
            writer.WriteLine(toSave.Passable);

            for (Layer layer = 0; layer < Layer.Count; layer++)
                Slides.SaveTo(toSave[layer], writer);
        }

        /// <summary>
        /// Loads a Tile from the passed in StreamReader. This will subsequently
        /// load each Slide from the passed in StreamReader. This is used as a subdivision of 
        /// "Load Tileset" as a division of respnosibilities.
        /// </summary>
        /// <param name="anchor">Anchor.</param>
        /// <param name="reader">Reader.</param>
        public static Tile Load(Renderable anchor, StreamReader reader)
        {
            int x = Utils.ReadInteger(reader);
            int y = Utils.ReadInteger(reader);

            Tile result = CreateTile(anchor, SwinGame.PointAt(x, y));
            result.Passable = Convert.ToBoolean(reader.ReadLine());

            for (Layer layer = 0; layer < Layer.Count; layer++)
                result[layer] = Slides.Load(reader);

            return result;
        }
    }
}
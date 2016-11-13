using System;
namespace TileStyleSDK
{
    /// <summary>
    /// Represents the class responsible for assembling custom Tiles being built in the editor mode.
    /// The user can add Slides to the Tile; modify its properties; and add it to the Palette so it can be placed.
    /// </summary>
    internal static class TileBuilder
    {
        /// <summary>
        /// Gets or sets the custom Tile being built.
        /// </summary>
        /// <value>The Tile being built.</value>
        public static Tile Building {get; set;}

        /// <summary>
        /// Initializes the <see cref="T:TileStyleSDK.TileBuilder"/> class.
        /// Deregisters the Tile from the Renderer as it is directly drawn
        /// inside the Tile Viewer Constructor Panel.
        /// </summary>
        static TileBuilder()
        {
            Building = new Tile();
            Building.Deregister();
        }

        /// <summary>
        /// Adds the constructed Tile to the Palette so it can be placed.
        /// This also resets the Building Tile so a new custom Tile can be constructed.
        /// </summary>
        public static void AddToPalette()
        {
            PaletteManager.AddTileToPalette(Building);
            ClearBuilding();
        }

        /// <summary>
        /// Resets the Tile being constructed to allow the user to start again.
        /// This is called automatically when the custom Tile is added to the Palette.
        /// </summary>
        public static void ClearBuilding()
        {
            for (Layer layer = 0; layer < Layer.Count; layer++)
                Building[layer].RootBitmap = null;
        }

        /// <summary>
        /// Toggles the Passable property of the Tile being constructed.
        /// This value will be reflected in the Tile when it is placed after being added to the Palette.
        /// </summary>
        public static void TogglePassable()
        {
            Building.Passable = !Building.Passable;
        }
    }
}
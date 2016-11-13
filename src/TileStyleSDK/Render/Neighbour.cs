using System;
namespace TileStyleSDK
{
    /// <summary>
    /// Represents the enum responsible for determining the Neighbours of a Tile.
    /// This is used to dynamic Bitmaps to update their cell based on which Tiles are next to them.
    /// Each combination of N/E/S/W yields a unique integer (mask) used to reference a Bitmap cell inside Tile Resources.
    /// </summary>
    [Flags]
    public enum Neighbour
    {
        None = 0,
        North = 1 << 0,
        East = 1 << 1,
        South = 1 << 2,
        West = 1 << 3,
    }
}
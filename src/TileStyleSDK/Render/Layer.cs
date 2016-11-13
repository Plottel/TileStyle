using System;
namespace TileStyleSDK
{
    /// <summary>
    /// Represents the enum used to assign a Layer to each Renderable. The Renderer will draw the world in passes by Layer.
    /// Layer.Count exists to enable simple looping over Tile Slides, e.g.
    /// for (Layer layer = 0; layer less than Layer.Count; layer++) 
    /// </summary>
    public enum Layer
    {
        Ground,
        Wall,
        Entity,
        Sky,
        UI,
        Count //For simple iteration
    }
}
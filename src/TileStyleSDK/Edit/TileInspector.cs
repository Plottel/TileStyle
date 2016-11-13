using System;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the class responsible for allowing the user to "inspect" a Tile in the Tileset.
    /// When the user presses "I", this class will select the Tile at the given location and send its 
    /// details to the InspectorPanel, allowing the user to view the properties of the Tile.
    /// </summary>
    internal static class TileInspector
    {
        /// <summary>
        /// Gets or sets the Tile being inspected.
        /// </summary>
        /// <value>The Tile being inspected.</value>
        public static Tile Inspecting {get; set;}

        /// <summary>
        /// Initializes the <see cref="T:TileStyleSDK.TileInspector"/> class.
        /// Deregisters the Tile from the Renderer as it is directly drawn inside the
        /// Tile Viewer Inspector Panel.
        /// </summary>
        static TileInspector()
        {
            Inspecting = new Tile();
            Inspecting.Deregister();
        }

        /// <summary>
        /// Gets the currently active Tileset from the TilesetManager
        /// This is a wrapper property for clearer function calls.
        /// </summary>
        /// <value>The active Tileset.</value>
        private static Tileset ActiveTileset
        {
            get
            {
                return TilesetManager.Active;
            }
        }

        /// <summary>
        /// Handles input relating to the Tile Inspector.
        /// If the user presses I, a Tile at the mouse position is located and its details
        /// sent to the Tile Viewer Inspector Panel. The Inspector Panel will also be set to "Show"
        /// so the user can view the Tile they are Inspecting.
        /// </summary>
        public static void HandleInput()
        {
            if (SwinGame.KeyTyped(KeyCode.IKey))
            {
                if (ActiveTileset.IsAt(SwinGame.MousePosition()))
                {
                    Tile toInspect = ActiveTileset.TileAt(SwinGame.MousePosition());
                    Inspecting.Passable = toInspect.Passable;

                    for (Layer layer = 0; layer < Layer.Count; layer++)
                        Inspecting[layer].RootBitmap = toInspect[layer].RootBitmap;

                    //Make the Inspector Panel visible and move it to where the user clicked
                    Panels.PanelNamed("InspectorPanel").Showing = true;
                    Panels.PanelNamed("InspectorPanel").Pos = SwinGame.MousePosition();
                }
            }
        }       
    }
}
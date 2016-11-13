using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the core class of the SDK which defines methods through which the user can use the SDK.
    /// </summary>
    public static class TileStyle
    {
        /// <summary>
        /// Initialise the World. Default position is so the Tileset will not be underneath Panels.
        /// </summary>
        private static WorldAnchor _world = new WorldAnchor {X = 200, Y = 10};

        /// <summary>
        /// Initialise the Screen
        /// </summary>
        private static ScreenAnchor _screen = new ScreenAnchor();

        /// <summary>
        /// Sets up the SDK in the default environment. This involves creating the Panels required for 
        /// the Tileset editor and loading any Tilesets previously saved
        /// </summary>
        public static void Init()
        {
            /// <summary>
            /// Loads any Tilesets previously saved so the user can interact with them automatically.
            /// </summary>
            TilesetManager.LoadAllTilesets();

            /// <summary>
            /// Add the default panels.
            /// </summary>
            Panels.AddPanel(new SettingsPanel(5, 5, 175, 170, true));
            Panels.AddPanel(new TileViewerPanel(SwinGame.PointAt(160, 180), 150, 250, "BuilderPanel", TileBuilder.Building, true));
            Panels.AddPanel(new TileViewerPanel(SwinGame.PointAt(5, 475), 150, 170, "ViewerPanel", TilePlacer.Placing, true));
            Panels.AddPanel(new TileViewerPanel(SwinGame.PointAt(0, 0), 150, 170, "InspectorPanel", TileInspector.Inspecting, false));
            Panels.AddPanel(new PalettePanel(SwinGame.PointAt(5, 180), 150, 290, true));

            /// <summary>
            /// Add default buttons. These are defined here as some of the Buttons reference other Panels and therefore must be 
            /// added after the Panels are created.
            /// </summary>
            Panels.AddButtonToPanelNamed(Buttons.CreateButton(120, 25, "Add To Palette", TileBuilder.AddToPalette, SwinGame.PointAt(5, 220)), "BuilderPanel");
            Panels.AddButtonToPanelNamed(Buttons.CreateButton(50, 25, "Clear", TileBuilder.ClearBuilding, SwinGame.PointAt(5, 190)), "BuilderPanel");
            Panels.AddButtonToPanelNamed(Buttons.CreateButton(10, 10, TileBuilder.TogglePassable, SwinGame.PointAt(115, 150)), "BuilderPanel");
            Panels.AddButtonToPanelNamed(Buttons.CreateButton(30, 20, "Hide", Panels.PanelNamed("InspectorPanel").ToggleDisplay, SwinGame.PointAt(115, 5)), "InspectorPanel");
        }

        /// <summary>
        /// Wrapper method the user can use to Register everything in the Tileset editor with the Renderer.
        /// </summary>
        public static void Register()
        {
            Panels.Register();
        }

        /// <summary>
        /// Wrapper method the user can use to Deregister everything in the Tileset editor from the Renderer.
        /// </summary>
        public static void Deregister()
        {
            Panels.Deregister();
        }

        /// <summary>
        /// Wrapper property the user can use to return the Active Tileset.
        /// </summary>
        /// <value>The active tileset.</value>
        public static Tileset ActiveTileset
        {
            get {return TilesetManager.Active;}
        }

        /// <summary>
        /// Method the user will need to call in order to interact with the Tileset.
        /// Handles input for each Panel and allows the user to place / remove / build / inspect Tiles.
        /// </summary>
        public static void HandleInput()
        {
            Panels.HandleInput();
            TileInspector.HandleInput();

            //Prevent interaction with level by clicking "through" a panel
            if (!Panels.ClickedOnPanel())
                TilePlacer.HandleInput(); 
        }
    }
}
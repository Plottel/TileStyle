using System;
using System.Collections.Generic;
using System.Linq;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the class responsible for handling the Tile Palettes the user can select Tiles from.
    /// This class interfaces with the PaletteManager to coordinate what should be rendered and what the user can select.
    /// </summary>
    internal class PalettePanel : Panel
    {
        /// <summary>
        /// The x offset from the Panel's top-left corner where the Palettes are drawn.
        /// </summary>
        public const int TILESET_X_OFFSET = 43;

        /// <summary>
        /// The y offset from the Panel's top-left corner where the Palettes are drawn.
        /// </summary>
        public const int TILESET_Y_OFFSET = 75;

        public PalettePanel(Point2D pt, int width, int height, bool showing) : this(pt.X, pt.Y, width, height, showing)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TileStyleSDK.PalettePanel"/> class.
        /// Also creates the Buttons which define the Palette Panel's behaviour.
        /// </summary>
        /// <param name="x">The x coordinate of the Panel.</param>
        /// <param name="y">The y coordinate of the Panel.</param>
        /// <param name="width">The width of the Panel.</param>
        /// <param name="height">The height of the Panel.</param>
        /// <param name="showing">Whether or not the Panel should be initially Rendered.</param>
        public PalettePanel(float x, float y, int width, int height, bool showing) : base(x, y, width, height, "PalettePanel", showing)
        {
            /// <summary>
            /// Represents the Button which changes the Palette the user selects Tiles from to the next one in the list.
            /// </summary>
            AddButton(Buttons.CreateButton(Buttons.NEXT_ACTIVE, Buttons.NEXT_INACTIVE, PaletteManager.NextPalette, Width - 42, 40));

            /// <summary>
            /// Represents the Button which changes the Palette the user selects Tiles from to the previous one in the list.
            /// </summary>
            AddButton(Buttons.CreateButton(Buttons.PREV_ACTIVE, Buttons.PREV_INACTIVE, PaletteManager.PrevPalette, 10, 40));

            /// <summary>
            /// Represents the Button which Toggles the display of the Panel where the user can construct their own custom Tiles.
            /// </summary>
            AddButton(Buttons.CreateButton(Buttons.NEXT_ACTIVE, Buttons.NEXT_INACTIVE, Panels.PanelNamed("BuilderPanel").ToggleDisplay, 122, 2));
        }

        /// <summary>
        /// Gets the Palete the user can currently select Tiles from.
        /// This is a wrapper property for nicer method calls.
        /// </summary>
        /// <value>The active palette.</value>
        internal Tileset ActivePalette
        {
            get {return PaletteManager.ActivePalette;}
        }

        /// <summary>
        /// Handles input for the Panel. Actions this Panel handles include:
        ///     User left clicks on a Tile -> its details are displayed in the Tile Viewer Panel and it becomes the new Tile to place.
        ///     User right-clicks on a Tile -> its details are submitted to the Constructor Panel as a new custom Tile. 
        ///     User presses X on a Tile -> it is removed from the Palette.
        /// </summary>
        public override void HandleInput()
        {
            base.HandleInput (); //Input for buttons

            if (ActivePalette.IsAt(SwinGame.MousePosition()))
            {
                /// <summary>
                /// Select the Tile at the mouse position and send its details to the Tile Placer
                /// so it becomes the new Tile the user will palce.
                /// </summary>
                if (SwinGame.MouseClicked(MouseButton.LeftButton))
                {
                    Tile selectedTile = ActivePalette.TileAt(SwinGame.MousePosition());
                    TilePlacer.Placing.Passable = selectedTile.Passable;

                    for (Layer layer = 0; layer < Layer.Count; layer++)
                    {
                        TilePlacer.Placing[layer].RootBitmap = null;

                        if (selectedTile.ExistsOnLayer(layer))
                        {
                            TilePlacer.Placing[layer].RootBitmap = selectedTile[layer].RootBitmap;
                            TilePlacer.Placing[layer].RootIndex = selectedTile[layer].RootIndex;
                        }
                    }
                }

                /// <summary>
                /// Select the Tile at the mouse position and send its details to the Tile Builder
                /// so that it will be added to the custom Tile the user is building.
                /// </summary>
                if (SwinGame.MouseClicked(MouseButton.RightButton))
                {
                    Tile selectedTile = ActivePalette.TileAt(SwinGame.MousePosition());

                    for (Layer layer = 0; layer < Layer.Count; layer++)
                    {
                        if (selectedTile.ExistsOnLayer(layer))
                        {
                            TileBuilder.Building[layer].RootBitmap = selectedTile[layer].RootBitmap;
                            TileBuilder.Building[layer].RootIndex = selectedTile[layer].RootIndex;
                        }
                    }          
                }

                if (SwinGame.KeyTyped(KeyCode.XKey))
                    PaletteManager.RemoveTileFromPaletteAtMousePos();
            }
        }

        /// <summary>
        /// Renders the Panel. This involves Rendering the currently Active Palette, as well as various helper text.
        /// </summary>
        public override void Render (Layer layer)
        {
            base.Render(layer);
            SwinGame.DrawText("Choose Tiles", Color.Black, SwinGame.FontNamed("GameFont"), AbsPos.X + 10, AbsPos.Y + 10);
            SwinGame.DrawText("Left-Click to Select", Color.Black, SwinGame.FontNamed("SmallGameFont"), AbsPos.X + 4, AbsPos.Y + 210);
            SwinGame.DrawText("Right-Click to Construct", Color.Black, SwinGame.FontNamed("SmallGameFont"), AbsPos.X + 4, AbsPos.Y + 230);
            SwinGame.DrawText("Press X to Remove", Color.Black, SwinGame.FontNamed("SmallGameFont"), AbsPos.X + 4, AbsPos.Y + 250);
            SwinGame.DrawText("Press I to Inspect", Color.Black, SwinGame.FontNamed("SmallGameFont"), AbsPos.X + 4, AbsPos.Y + 270);
            SwinGame.DrawText(ActivePalette.Name, Color.Black, SwinGame.FontNamed("SmallGameFont"), AbsPos.X + 60, AbsPos.Y + 50);
            RenderPalette();
        } 

        /// <summary>
        /// Renders the currently active Palette.
        /// Tiles exist on multiple layers; this method allows the Tile to be drawn on purely on UI layer so that it is
        /// visible in the Panel.
        /// </summary>
        private void RenderPalette()
        {
            for (Layer layer = 0; layer < Layer.Count; layer++)
            {
                for (int col = 0; col < ActivePalette.Cols; col++)
                {
                    for (int row = 0; row < ActivePalette.Rows; row++)
                    {
                        if (ActivePalette[col, row].ExistsOnLayer(layer))
                            ActivePalette[col, row].Render(layer);
                    }
                }
            }
        }
    }
}
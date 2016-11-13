using System;
using System.Collections.Generic;
using System.Linq;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the class responsible for handling everything related to the Tile Palette.
    /// Implements various helper functions to add and remove new Tiles to the Palette.
    /// When a Palette is full, this class will automatically create a new one.
    /// </summary>
    internal static class PaletteManager
    {
        /// <summary>
        /// The number of columns in each Tile Palette.
        /// </summary>
        private const int PALETTE_COLS = 2;

        /// <summary>
        /// The number of rows in each Tile Palette.
        /// </summary>
        private const int PALETTE_ROWS = 4;

        /// <summary>
        /// Represents the Tile Palettes. The user may only interact with the active Palette. 
        /// </summary>
        private static ListWithActive<Tileset> _palettes = new ListWithActive<Tileset>();

        /// <summary>
        /// Initializes the <see cref="T:TileStyleSDK.PaletteManager"/> class.
        /// Creates an initial blank Palette to accommodate loading Tiles.
        /// </summary>
        static PaletteManager()
        {
            AddBlankPalette();
            NextPalette();
        }

        /// <summary>
        /// Gets the active Palette.
        /// </summary>
        /// <value>The active palette.</value>
        public static Tileset ActivePalette
        {
            get {return _palettes.Active;}
        }

        /// <summary>
        /// Copies the details of the passed in Tile to the first available cell in the Palette. 
        /// It is assigned to the UI Layer so that it can be drawn in the Panel. 
        /// If the current Palette is full, then a new one is created and the Tile is placed in the first cell of the new Palette.
        /// </summary>
        /// <param name="toAdd">The Tile being added to the Palette.</param>
        public static void AddTileToPalette(Tile toAdd)
        {
            if (PaletteIsFull())
                AddBlankPalette();

            //Get first available cell and copy Tile details
            Tile destination = GetFirstBlankTile();
            destination.Passable = toAdd.Passable;
            destination.Layer = Layer.UI;

            //Copy Bitmap for each layer
            for (Layer layer = 0; layer < Layer.Count; layer++)
            {
                if (toAdd.ExistsOnLayer(layer))
                    destination[layer].RootBitmap = toAdd[layer].RootBitmap;
            }
        }

        /// <summary>
        /// Creates a Tile from the passed in Bitmap identifier to add to the Tile Palette. From the passed in string,
        /// the cell corresponding to zero neighbours is fetched and added as the thumbnail for the Tile in the Palette.
        /// </summary>
        /// <param name="rootBitmap">The Bitmap identifier.</param>
        /// <param name="layer">The Layer the Tile exists on.</param>
        public static void AddTileToPaletteFromBitmap(string rootBitmap, Layer layer)
        {
            Tile newTile = new Tile();
            newTile.Deregister();
            newTile[layer].RootBitmap = rootBitmap;
            newTile[layer].RootIndex = TileResources.GetBitmapIndexForMask(rootBitmap, Neighbour.None);

            AddTileToPalette(newTile);
        }

        /// <summary>
        /// Creates a new Tileset and adds it to the PaletteManager as a blank Palette. 
        /// The Tileset is deregistered from the Renderer so it does not draw on top of the current Palette.
        /// </summary>
        private static void AddBlankPalette()
        {
            //Declare Tileset details and create new Tileset.
            //Palettes are drawn relative to the position of the Palette Panel.
            string name = "Tiles " + _palettes.Count;
            Renderable anchor = Panels.PanelNamed("PalettePanel");
            float x = PalettePanel.TILESET_X_OFFSET;
            float y = PalettePanel.TILESET_Y_OFFSET;
            int cols = PALETTE_COLS;
            int rows = PALETTE_ROWS;

            //Create Tileset and initialise variables.
            Tileset newTileset = Tilesets.CreateTileset(name, anchor, x, y, cols, rows);
            newTileset.Layer = Layer.UI;
            newTileset.ShowGrid = true;
            newTileset.Deregister();

            //Add to list of Palettes
            _palettes.Add(newTileset);
        }

        /// <summary>
        /// Removes the Tile from the Palette at the mouse's location. This will 
        /// nullify all data in the cell and allow it to be re-used when a new Tile is added.
        /// </summary>
        public static void RemoveTileFromPaletteAtMousePos()
        {
            Tile toRemove = ActivePalette.TileAt(SwinGame.MousePosition());

            for (Layer layer = 0; layer < Layer.Count; layer++)
                toRemove[layer].RootBitmap = null;         
        }  

        /// <summary>
        /// Specifies if a Tile is "blank". If a Tile has no Bitmaps, it is considered blank.
        /// </summary>
        /// <returns><c>true</c>, if the Tile is blank, <c>false</c> otherwise.</returns>
        /// <param name="t">T.</param>
        private static bool TileHasNoLayers(Tile t)
        {
            for (Layer layer = 0; layer < Layer.Count; layer++)
            {
                if (t.ExistsOnLayer(layer))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Fetches the first Tile in the Palette Manager which is not being used. The result of
        /// this method determines where newly added Tiles will be placed on the Palette. The method
        /// will check all Palettes, so any slots opened up by removed Tiles will be filled back in as new Tiles are added.
        /// </summary>
        /// <returns>The first blank tile.</returns>
        private static Tile GetFirstBlankTile()
        {
            foreach (Tileset palette in _palettes.List)
            {
                for (int col = 0; col < PALETTE_COLS; col++)
                {
                    for (int row = 0; row < PALETTE_ROWS; row++)
                    {
                        //If the Tile has no Bitmaps, then it is blank and therefore free to use.
                        if (TileHasNoLayers(palette[col, row]))
                            return palette[col, row];
                    }
                }
            }
            return null; //If no blank Tiles, the Palette is full.
        }

        /// <summary>
        /// Specifies if there are any "free" cells in the current Palette. This is used
        /// to determine if a new Palette needs to be created.
        /// </summary>
        /// <returns><c>true</c>, if the Palette is full, <c>false</c> otherwise.</returns>
        private static bool PaletteIsFull()
        {
            return GetFirstBlankTile() == null;
        }

        /// <summary>
        /// Changes the active Palette to the next Palette in the list. Also interfaces with the 
        /// Renderer to prevent two Palettes being drawn at once.
        /// </summary>
        public static void NextPalette()
        {
            if (ActivePalette != null)
                ActivePalette.Deregister();

            _palettes.Next();
            ActivePalette.Register();
        }

        /// <summary>
        /// Changes the active Palette to the previous Palette in the list. Also interfaces with the
        /// Renderer to prevent two Palettes being drawn at once.
        /// </summary>
        public static void PrevPalette()
        {
            if (ActivePalette != null)
                ActivePalette.Deregister();

            _palettes.Previous();
            ActivePalette.Register();
        }
    }
}
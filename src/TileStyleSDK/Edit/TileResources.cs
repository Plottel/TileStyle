using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the class responsible for storing all Tile Bitmaps and their corresponding details.
    /// This includes all cells of the Bitmap; the corresponding indexes for each Neighbour mask; and whether
    /// or not the cell is dynamic (subject to autocompletions).
    /// All Slides will reference this class to fetch their corresponding Bitmap.
    /// </summary>
    public static class TileResources
    {
        /// <summary>
        /// Represents the struct which stores details of each Tile Bitmap. These are stored in the 
        /// Dictionary along with the matching RootBitmap string.
        /// </summary>
        private struct TileBitmapData
        {
            /// <summary>
            /// An array containing the cells of the Bitmap.
            /// </summary>
            public Bitmap[] Cells;

            /// <summary>
            /// Indicates whether or not the Bitmap should be affected by autocompletions.
            /// </summary>
            public bool IsDynamic;

            /// <summary>
            /// Specifies which Bitmap cell corresponds to each Neighbour Mask.
            /// </summary>
            public Dictionary<Neighbour, int> DynamicCellMasks;
        }

        /// <summary>
        /// The master list of all Tile Bitmaps and their corresponding details.
        /// All Tile Bitmap-related methods will reference this Dictionary.
        /// </summary>
        private static Dictionary<string, TileBitmapData> _tileResources = new Dictionary<string, TileBitmapData>();

        /// <summary>
        /// Adds the passed in Bitmap to the master list of Tile Bitmaps and adds it to the Tile Palette.
        /// The Bitmap will be split into its individual cells and index 0 will become the default cell for all Neighbour Masks.
        /// </summary>
        /// <param name="toAdd">The Bitmap to add.</param>
        /// <param name="layer">The layer the Bitmap is on.</param>
        /// <param name="isDynamic">Whether or not the Bitmap is affected by autocompletions.</param>
        public static void AddTileBitmap(Bitmap toAdd, Layer layer, bool isDynamic)
        {
            //The name the Bitmap can be referenced by
            string name = SwinGame.BitmapName(toAdd);

            //Create Bitmap Data from passed in details
            TileBitmapData newTileData = new TileBitmapData();
            newTileData.Cells = Utils.SplitBitmapCells(toAdd);
            newTileData.IsDynamic = isDynamic;
            newTileData.DynamicCellMasks = new Dictionary<Neighbour, int>();

            //Set default index for each Neighbour Mask to 0
            for (int i = 0; i < 16; i++)
                newTileData.DynamicCellMasks.Add((Neighbour)i, 0);

            //Add the data to the master list of Tile Bitmaps
            _tileResources.Add(name, newTileData);

            //Add the Bitmap to the Palette so the user can select and place it.
            PaletteManager.AddTileToPaletteFromBitmap(name, layer);
        }

        /// <summary>
        /// Sets the Bitmap cell corresponding to the passed in Neighbour Mask for the passed in Bitmap name.
        /// </summary>
        /// <param name="rootBitmap">The Bitmap name.</param>
        /// <param name="mask">The Neighbour Mask.</param>
        /// <param name="cell">The index of the cell.</param>
        public static void SetBitmapCellForMask(string rootBitmap, Neighbour mask, int cell)
        {
            _tileResources[rootBitmap].DynamicCellMasks[mask] = cell;
        }

        /// <summary>
        /// Returns the Bitmap cell index for the corresponding Bitmap name and Neighbour Mask.
        /// </summary>
        /// <returns>The cell index.</returns>
        /// <param name="rootBitmap">The Bitmap name.</param>
        /// <param name="mask">The Neighbour Mask.</param>
        public static int GetBitmapIndexForMask(string rootBitmap, Neighbour mask)
        {
            return _tileResources[rootBitmap].DynamicCellMasks[mask];
        }

        /// <summary>
        /// Specifies whether or not the passed in Bitmap name should be affected by autocompletions.
        /// </summary>
        /// <returns><c>true</c>, if the Bitmap should be affected by autocompletions, <c>false</c> otherwise.</returns>
        /// <param name="rootBitmap">Root bitmap.</param>
        public static bool BitmapIsDynamic(string rootBitmap)
        {
            return _tileResources[rootBitmap].IsDynamic;
        }

        /// <summary>
        /// Returns the corresponding Bitmap cell for the passed in Bitmap name and index.
        /// </summary>
        /// <returns>The Bitmap cell.</returns>
        /// <param name="rootBitmap">The Bitmap name.</param>
        /// <param name="index">The Bitmap index.</param>
        public static Bitmap GetBitmap(string rootBitmap, int index)
        {
            return _tileResources[rootBitmap].Cells[index];
        }

        /// <summary>
        /// Fetches all Bitmap cells for the corresponding Bitmap name and returns them as an array.
        /// </summary>
        /// <returns>All Bitmap cells for the passed in name.</returns>
        /// <param name="rootBitmap">The Bitmap name.</param>
        public static Bitmap[] GetBitmapListNamed(string rootBitmap)
        {
            return _tileResources[rootBitmap].Cells;
        }
    }
}
using System;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the class responsible for placing and removing Tiles in the editor.
    /// When a Tile is placed or removed, its neighbours re-evalute their Neighbour Masks
    /// and update their Bitmaps accordingly.
    /// </summary>
    internal static class TilePlacer
    {
        /// <summary>
        /// Gets or sets the Tile currently being placed.
        /// </summary>
        public static Tile Placing {get; set;}

        /// <summary>
        /// Initializes the <see cref="T:TileStyleSDK.TilePlacer"/> class.
        /// Deregisters the Placing Tile from the Renderer.
        /// </summary>
        static TilePlacer()
        {
            Placing = new Tile();
            Placing.Deregister();
        }

        /// <summary>
        /// Gets the active Tileset from the TilesetManager.
        /// Wrapper method for nicer method calls.
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
        /// Handles input for the Tile Placer. Actions this class handles include:
        ///         Placing a Tile when left click is down
        ///         Removing a Tile when right click is down
        /// </summary>
        public static void HandleInput()
        {
            if (ActiveTileset.IsAt(SwinGame.MousePosition()))
            {
                if (SwinGame.MouseDown(MouseButton.LeftButton))
                    PlaceTileAt(SwinGame.MousePosition());

                if (SwinGame.MouseDown(MouseButton.RightButton))
                    RemoveTileAt(SwinGame.MousePosition());
            }
        }

        /// <summary>
        /// Places the currently selected Tile on the Tileset. This will subsequently call
        /// the Tile's neighbours to re-evaluate their Neighbour Masks and update their Bitmaps accordingly.
        /// </summary>
        /// <param name="pt">The position to place the Tile to.</param>
        private static void PlaceTileAt(Point2D pt)
        {
            Tile placingTo = ActiveTileset.TileAt(pt);

            for (Layer layer = 0; layer < Layer.Count; layer++)
            {
                if (Placing.ExistsOnLayer(layer))
                    placingTo[layer].RootBitmap = Placing[layer].RootBitmap;
            }

            DoAutoCompletions(placingTo);
        }

        /// <summary>
        /// Removes the Tile at the given point from the Tileset. This will subsequently call
        /// the Tile's neighbours to re-evaluate their Neighbour Masks and update their Bitmaps accordingly.
        /// </summary>
        /// <param name="pt">The position to remove the Tile at.</param>
        private static void RemoveTileAt(Point2D pt)
        {
            Tile toRemove = ActiveTileset.TileAt(pt);
            
            for (Layer layer = 0; layer < Layer.Count; layer++)
                toRemove[layer].RootBitmap = null;

            DoAutoCompletions(toRemove);
        }

        /// <summary>
        /// Evaluates the Neighbour Masks of the passed in Tile and its neighbours and updates their Bitmaps accordingly.
        /// This is called when a Tile is placed or removed, the area of Tiles effected is displayed below:
        /// T = the Tile being placed or removed.
        /// N = a Neighbour of the Tile being placed or removed.
        /// X = a Neighbour of a Neighbour of the TIle being placed or removed.
        /// 0 = Unaffected Tile
        /// 
        /// 
        /// 0 0 X 0 0
        /// 0 X N X 0
        /// X N T N X
        /// 0 X N X 0
        /// 0 0 X 0 0
        /// </summary>
        /// <param name="t">The Tile whose mask is being re-evaluated.</param>
        private static void DoAutoCompletions(Tile t)
        {
            //The column index of the original Tile to be autocompleted.
            int col = ActiveTileset.GetColFromX(t.AbsPos.X);

            //The row index of the original Tile to be autocompleted.
            int row = ActiveTileset.GetRowFromY(t.AbsPos.Y);

            if (ActiveTileset.IndexInGrid(col, row))
                CompareNeighbours(ActiveTileset[col][row], col, row); //Original Tile

            if (ActiveTileset.IndexInGrid(col, row - 1))
                CompareNeighbours(ActiveTileset[col][row - 1], col, row - 1); //North

            if (ActiveTileset.IndexInGrid(col + 1, row))
                CompareNeighbours(ActiveTileset[col + 1][row], col + 1, row); //East

            if (ActiveTileset.IndexInGrid(col, row + 1))
                CompareNeighbours(ActiveTileset[col][row + 1], col, row + 1); //South

            if (ActiveTileset.IndexInGrid(col - 1, row))
                CompareNeighbours(ActiveTileset[col - 1][row], col - 1, row); //West
        }

        /// <summary>
        /// Compares the Neighbours of the passed in Tile and updates the Neighbour Mask and Bitmap of each layer accordingly.
        /// </summary>
        /// <param name="t">The Tile whose neighbours are being compared.</param>
        /// <param name="col">The column of the Tile whose neighbours are being compared.</param>
        /// <param name="row">The row of the Tile whose neighbours are being compared.</param>
        private static void CompareNeighbours(Tile t, int col, int row)
        {
            for (Layer layer = 0; layer < Layer.Count; layer++)
            {
                if (t.ExistsOnLayer(layer) && TileResources.BitmapIsDynamic(t[layer].RootBitmap))
                {
                    Neighbour mask = Neighbour.None;

                    if (ActiveTileset.IndexInGrid(col, row - 1))
                    {
                        if (ActiveTileset[col, row - 1][layer].RootBitmap == t[layer].RootBitmap) //North
                            mask |= Neighbour.North;
                    }

                    if (ActiveTileset.IndexInGrid(col + 1, row))
                    {
                        if (ActiveTileset[col + 1, row][layer].RootBitmap == t[layer].RootBitmap) //East
                            mask |= Neighbour.East;
                    }

                    if (ActiveTileset.IndexInGrid(col, row + 1))
                    {
                        if (ActiveTileset[col, row + 1][layer].RootBitmap == t[layer].RootBitmap) //South
                            mask |= Neighbour.South;
                    }

                    if (ActiveTileset.IndexInGrid(col - 1, row))
                    {
                        if (ActiveTileset[col - 1, row][layer].RootBitmap == t[layer].RootBitmap) //West
                            mask |= Neighbour.West;
                    }

                    t[layer].RootIndex = TileResources.GetBitmapIndexForMask(t[layer].RootBitmap, mask);
                }
            }
        }
    }
}
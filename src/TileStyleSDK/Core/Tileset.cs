using System;
using System.Collections.Generic;
using System.Linq;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the class responsible for storing a collection of Tiles and forming the "Levels" in the program.
    /// A Tileset acts as a "container" for Tiles and is also the basis for most Editor interaction.
    /// Tilesets define several helper functions for locating specific Tiles they contain.
    /// </summary>
    public class Tileset : Renderable
    {
        /// <summary>
        /// Gets or sets the name of the Tileset. This is used for Saving / Loading
        /// as well as by the end user for fetching a specified Tileset.
        /// </summary>
        /// <value>The name.</value>
        public string Name {get; set;}

        /// <summary>
        /// The Tiles in the Tileset. Stored as a 2D List representing a grid.
        /// The first dimension represents a column; the second dimension represents a row.
        /// </summary>
        private readonly List<List<Tile>> _tiles = new List<List<Tile>>();

        /// <summary>
        /// Gets the Tiles in the Tileset.
        /// </summary>
        /// <value>The tiles.</value>
        public List<List<Tile>> Tiles
        {
            get {return _tiles;}
        }

        /// <summary>
        /// Gets or sets the number of columns in the Tileset grid.
        /// </summary>
        /// <value>The number of columns.</value>
        public int Cols {get; set;}

        /// <summary>
        /// Gets or sets the number of rows in the Tileset grid.
        /// </summary>
        /// <value>The rows.</value>
        public int Rows {get; set;}

        /// <summary>
        /// Gets or sets a value indicating whether the grid should be shown.
        /// Generally, the grid should be shown if editing the Tileset, and 
        /// not shown if in the game.
        /// </summary>
        /// <value><c>true</c> if show grid; otherwise, <c>false</c>.</value>
        public bool ShowGrid {get; set;}

        /// <summary>
        /// Gets the width of the Tileset. This is determined directly from
        /// the number of Tiles contained in the Tileset.
        /// </summary>
        /// <value>The width.</value>
        public override int Width
        {
            get
            {
                return Renderer.TILE_SIZE * Cols - 1;
            }   //-1 so border of last column won't register as next cell and cause null reference.
        }

        /// <summary>
        /// Gets the height of the Tileset. This is determined directly from
        /// the number of Tiles contained in the Tileset.
        /// </summary>
        /// <value>The height.</value>
        public override int Height
        {
            get
            {
                return Renderer.TILE_SIZE * Rows - 1;
            }   //-1 so border of last row won't register as next cell and cause null reference.
        }

        /// <summary>
        /// Gets or sets the first dimension (column) of the Tileset.
        /// Default property indexer allows columns to be accessed with
        /// Tileset[col] rather than Tileset.Tiles[col]
        /// </summary>
        /// <param name="col">The index of the column</param>
        public List<Tile> this[int col]
        {
            get {return Tiles[col];}
            set {Tiles[col] = value;}
        }

        /// <summary>
        /// Gets or sets the Tile at the specified index.
        /// Default property indexer allows Tiles to be accessed with
        /// Tileset[col, row] rather than Tileset.Tiles[col][row]
        /// </summary>
        /// <param name="col">The index of the column.</param>
        /// <param name="row">The index of the row.</param>
        public Tile this[int col, int row]
        {
            get {return Tiles[col][row];}
            set {Tiles[col][row] = value;}
        }

        /// <summary>
        /// Fetches the Tile at the specified coordinate.
        /// Tile locations are fixed based on the location of the Tileset and therefore
        /// can be directly indexed without the need for iteration.
        /// </summary>
        /// <returns>The Tile at the given coordinate.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Tile TileAt(float x, float y)
        {
            int col = (int)Math.Truncate((x - AbsPos.X) / Renderer.TILE_SIZE);
            int row = (int)Math.Truncate((y - AbsPos.Y) / Renderer.TILE_SIZE);

            return Tiles[col][row];
        }

        /// <seealso cref="TileAt(float, float)"/>
        public Tile TileAt(Point2D pt)
        {
            return TileAt(pt.X, pt.Y);
        }

        /// <summary>
        /// Specifies if a given column and row index are within the grid of the Tileset.
        /// </summary>
        /// <returns><c>true</c>, if the index is in the grid, <c>false</c> otherwise.</returns>
        /// <param name="col">The column index to check.</param>
        /// <param name="row">The row index to check.</param>
        public bool IndexInGrid(int col, int row)
        {
            return col < Cols && row < Rows && col >= 0 && row >= 0;
        }

        /// <summary>
        /// Returns the index of the column at the given X coordinate
        /// </summary>
        /// <returns>The column index.</returns>
        /// <param name="x">The x coordinate.</param>
        public int GetColFromX(float x)
        {
            return (int)Math.Truncate((x - AbsPos.X) / Renderer.TILE_SIZE);
        }

        /// <summary>
        /// Returns the index of the row at the given Y coordinate
        /// </summary>
        /// <returns>The row index.</returns>
        /// <param name="y">The y coordinate.</param>
        public int GetRowFromY(float y)
        {
            return (int)Math.Truncate((y - AbsPos.Y) / Renderer.TILE_SIZE);
        }

        /// <summary>
        /// If the Tileset is set to Show Grid, a black rectangle is rendered around the edges
        /// of each Tile. This is used primarily when editing Tilesets to view exactly where Tiles will be placed.
        /// </summary>
        /// <param name="layer">The layer to Render on.</param>
        public override void Render(Layer layer)
        {
            if (ShowGrid)
            {
                for (int col = 0; col < Cols; col++)
                {
                    for (int row = 0; row < Rows; row++)
                    {
                        SwinGame.DrawRectangle(Color.Black, Tiles[col][row].AbsPos.X, Tiles[col][row].AbsPos.Y, Renderer.TILE_SIZE, Renderer.TILE_SIZE);
                    }
                }
            }
        }

        /// <summary>
        /// Registers the Tileset with the Renderer. This will subsequently
        /// Register each Tile in the grid.
        /// </summary>
        public override void Register()
        {
            for (int col = 0; col < Cols; col++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    Renderer.Register(Tiles[col][row]);
                }
            }
            base.Register();
        }

        /// <summary>
        /// Deregisters the Tileset from the Renderer. This will subsequently
        /// Deregister each Tile in the grid.
        /// </summary>
        public override void Deregister()
        {
            for (int col = 0; col < Cols; col++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    Renderer.Deregister(Tiles[col][row]);
                }
            }
            base.Deregister();
        }
    }
}
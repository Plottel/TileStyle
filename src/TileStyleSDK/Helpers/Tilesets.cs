using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the class responsible for handling all things related to Tilesets. This includes creation logic; manipulating
    /// Tileset size; saving and loading. The user will interface with this class indirectly through the editor and its Buttons, but 
    /// may also interface with it directly to manage their own custom Tilesets.
    /// </summary>
    public static class Tilesets
    {
        /// <summary>
        /// The default number of columns a Tileset is created with.
        /// </summary>
        public static int DEFAULT_COLS = 10;

        /// <summary>
        /// The default number of rows a Tileset is created with.
        /// </summary>
        public static int DEFAULT_ROWS = 10;

        /// <summary>
        /// Represents the "true" Tileset creation method. All other creation methods will manipulate variables
        /// to match this method's signature.
        /// </summary>
        /// <returns>The Tileset.</returns>
        /// <param name="name">The name of the Tileset.</param>
        /// <param name="anchor">The anchor the Tileset will be drawn relative to.</param>
        /// <param name="pos">The position of the Tileset.</param>
        /// <param name="cols">The number of columns in the Tileset.</param>
        /// <param name="rows">The number of rows in the Tileset.</param>
        public static Tileset CreateTileset(string name, Renderable anchor, Point2D pos, int cols, int rows)
        {
            Tileset result = new Tileset();
            result.Name = name;
            result.Anchor = anchor;
            result.Pos = pos;

            //Initialise values in each Tile to prevent null references
            for (int i = 0; i < cols; i++)
                AddColumn(result);

            //Initialise values in each Tile to prevent null references
            for (int i = 0; i < rows; i++)
                AddRow(result);

            return result;
        }

        /// <seealso cref="CreateTileset(string, Renderable, Point2D, int, int)"/>
        /// <summary>
        /// Overload for x / y coordinates instead of Point2D.
        /// </summary>
        /// <returns>The Tileset.</returns>
        public static Tileset CreateTileset(string name, Renderable anchor, float x, float y, int cols, int rows)
        {
            return CreateTileset(name, anchor, SwinGame.PointAt(x, y), cols, rows);
        }

        /// <seealso cref="CreateTileset(string, Renderable, Point2D, int, int)"/>
        /// <summary>
        /// When no dimensions are specified, the Tileset is created using default values.
        /// </summary>
        /// <returns>The Tileset.</returns>
        public static Tileset CreateTileset(string name, Renderable anchor, Point2D pos) 
        {
            return CreateTileset(name, anchor, pos, DEFAULT_COLS, DEFAULT_ROWS);
        }

        /// <seealso cref="CreateTileset(string, Renderable, Point2D)"/>
        /// <summary>
        /// Overload for x / y coordinates instead of Point2D.
        /// </summary>
        /// <returns>The Tileset.</returns>
        public static Tileset CreateTileset(string name, Renderable anchor, float x, float y)
        {
            return CreateTileset(name, anchor, SwinGame.PointAt(x, y));
        }

        /// <seealso cref="CreateTileset(string, Renderable, Point2D)"/>
        /// <summary>
        /// By default, a Tileset is anchored to the World and called "New Tileset".
        /// </summary>
        /// <returns>The Tileset.</returns>
        public static Tileset CreateTileset() 
        {
            return CreateTileset("New Tileset", WorldAnchor.Instance, new Point2D());
        }

        /// <summary>
        /// Adds a column to the passed in Tileset. This involves adding a new List of Tiles
        /// to the first dimension of the Tileset and setting each Tile's position.
        /// </summary>
        /// <param name="addingTo">The Tileset to add the column to.</param>
        public static void AddColumn(Tileset addingTo)
        {
            List<Tile> newCol = new List<Tile>();
            int x;
            int y;
            Tile newTile;

            for (int i = 0; i < addingTo.Rows; i++)
            {
                x = (addingTo.Cols) * Renderer.TILE_SIZE;
                y = i * Renderer.TILE_SIZE;
                newTile = Tiles.CreateTile(addingTo, x, y);

                newCol.Add(newTile);
            }

            addingTo.Tiles.Add(newCol);
            addingTo.Cols++;
        }

        /// <summary>
        /// Adds a row to the passed in Tileset. This involves adding an extra Tile to the end 
        /// of each List in the Tileset's first dimension and intialising their positions and values.
        /// </summary>
        /// <param name="addingTo">The Tileset to add the row to.</param>
        public static void AddRow(Tileset addingTo)
        {
            int x, y;
            Tile newTile;

            for (int i = 0; i < addingTo.Cols; i++)
            {
                x = i * Renderer.TILE_SIZE;
                y = (addingTo.Rows) * Renderer.TILE_SIZE;

                newTile = Tiles.CreateTile(addingTo, x, y);

                addingTo[i].Add(newTile);
            }
            addingTo.Rows++;
        }

        /// <summary>
        /// Removes a column from the passed in Tileset. This involves removing the Last list
        /// in the first dimension of the Tileset's Tiles.
        /// </summary>
        /// <param name="removingFrom">The Tileset to remove the column from.</param>
        public static void RemoveCol(Tileset removingFrom)
        {
            if (removingFrom.Cols > 0)
            {
                removingFrom.Tiles.Remove(removingFrom.Tiles.Last());
                removingFrom.Cols--;
            }
        }

        /// <summary>
        /// Removes a row from the passed in Tileset. This involves removing the last Tile
        /// from each List in the Tileset's first dimension.
        /// </summary>
        /// <param name="removingFrom">The Tileset to remove the row from.</param>
        public static void RemoveRow(Tileset removingFrom)
        {
            if (removingFrom.Rows > 0)
            {
                for (int col = 0; col < removingFrom.Rows; col++)
                    removingFrom[col].Remove(removingFrom[col].Last());

                removingFrom.Rows--;
            }
        }

        /// <summary>
        /// Saves the passed in Tileset to the passed in StreamWriter. This subsequently saves each Tile in its grid.
        /// </summary>
        /// <param name="toSave">The Tileset to save.</param>
        /// <param name="writer">The StreamWriter to save the Tileset to.</param>
        public static void SaveTo(Tileset toSave, StreamWriter writer)
        {
            writer.WriteLine(toSave.Name);
            writer.WriteLine(toSave.Anchor);
            writer.WriteLine(toSave.Pos.X);
            writer.WriteLine(toSave.Pos.Y);
            writer.WriteLine(toSave.Cols);
            writer.WriteLine(toSave.Rows);

            for (int col = 0; col < toSave.Cols; col++)
            {
                for (int row = 0; row < toSave.Rows; row++)
                    Tiles.SaveTo(toSave[col, row], writer);
            }
        }

        /// <summary>
        /// Loads a Tileset from the passed in StreamReader. This subsequently loads each Tile in its grid.
        /// </summary>
        /// <returns>The Tileset.</returns>
        /// <param name="reader">The StreamReader to load the Tileset from.</param>
        public static Tileset Load(StreamReader reader)
        {
            Tileset newTileset;

            string name = reader.ReadLine();
            string anchorText = reader.ReadLine();

            Renderable anchor;

            if (anchorText == "TileStyleSDK.WorldAnchor")
                anchor = WorldAnchor.Instance;
            else
                anchor = ScreenAnchor.Instance;

            int x = Utils.ReadInteger(reader);
            int y = Utils.ReadInteger(reader);
            int cols = Utils.ReadInteger(reader);
            int rows = Utils.ReadInteger(reader);

            //Create Tileset using read in values
            newTileset = CreateTileset(name, anchor, SwinGame.PointAt(x, y), cols, rows);

            //Initialise each Tile in the grid by loading them.
            for (int col = 0; col < newTileset.Cols; col++)
            {
                for (int row = 0; row < newTileset.Rows; row++)
                    newTileset[col, row] = Tiles.Load(newTileset, reader);
            }

            reader.Close();
            return newTileset;
        }
    }
}
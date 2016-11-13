using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the class responsible for handling and controlling all the Tilesets which 
    /// may exist in the program. This class can be thought of as the overall "Program Controller"
    /// and is referenced by many other classes.
    /// </summary>
    internal static class TilesetManager
    {
        /// <summary>
        /// The X coordinate where Tilesets are created when added to the master list.
        /// </summary>
        public const int DEFAULT_START_X = 150;

        /// <summary>
        /// The Y coordinate where Tilesets are created when added to the master list.
        /// </summary>
        public const int DEFAULT_START_Y = 10;

        /// <summary>
        /// The master list of Tilesets.
        /// </summary>
        private static ListWithActive<Tileset> _tilesets = new ListWithActive<Tileset>();

        /// <summary>
        /// Fetches the Tileset with the passed in name from the master list.
        /// </summary>
        /// <returns>The Tileset matching the passed in name.</returns>
        /// <param name="name">The name to check for.</param>
        public static Tileset GetTilesetNamed(string name)
        {
            foreach (Tileset tileset in _tilesets.List)
            {
                if (tileset.Name == name)
                    return tileset;
            }
            throw new Exception ("Tileset with name: " + name + " could not be found");
        }

        /// <summary>
        /// Gets the currently active Tileset. This is referenced by most other classes
        /// as it represents the "official" Tileset which is to be interacted with.
        /// </summary>
        /// <value>The active Tileset.</value>
        public static Tileset Active
        {
            get {return _tilesets.Active;}
        }

        /// <summary>
        /// Changes the active Tileset to the previous one in the master list. This method also
        /// interfaces with the Renderer to prevent two Tilesets being drawn at once.
        /// </summary>
        public static void PreviousTileset()
        {
            Active.Deregister();
            _tilesets.Previous();
            Active.Register();
        }

        /// <summary>
        /// Changes the active Tileset to the next one in the master list. This method also
        /// interfaces with the Renderer to prevent two Tilesets being drawn at once.
        /// </summary>
        public static void NextTileset()
        {
            Active.Deregister();
            _tilesets.Next();
            Active.Register();
        }

        /// <summary>
        /// Toggles the grid display in the level editor. If the grid is on, then a square
        /// will be drawn around each Tile in the Tileset.
        /// </summary>
        public static void ToggleGrid()
        {
            Active.ShowGrid = !Active.ShowGrid;
        }

        /// <summary>
        /// Creates a new Tileset and adds it to the master list.
        /// The active Tileset is also changed to the new Tileset.
        /// </summary>
        public static void AddNewTileset()
        {
            _tilesets.Add(Tilesets.CreateTileset());

            if (Active != null)
                Active.Deregister();
            
            _tilesets.Active = _tilesets.List.Last();
        }

        /// <summary>
        /// Adds the passed in Tileset to the master list. This is used for loading Tilesets
        /// and also gives the user flexibility to supply their own Tilesets.
        /// </summary>
        /// <param name="toAdd">To add.</param>
        public static void AddTileset(Tileset toAdd)
        {
            if (Active == null)
                _tilesets.Active = toAdd;
            
            _tilesets.Add(toAdd);
        }

        /// <summary>
        /// Saves all Tilesets in the master list. This is called when the "Save" button is pressed
        /// in order to keep all text files in sync and prevent loss of changes.
        /// </summary>
        public static void SaveAllTilesets()
        {
            StreamWriter totalTilesets = new StreamWriter("Resources/tilesets.txt");
            totalTilesets.WriteLine(_tilesets.Count);
            totalTilesets.Close();

            //Save each Tileset in the master list
            for (int i = 0; i < _tilesets.Count; i++)
            {
                StreamWriter tilesetWriter = new StreamWriter("Resources/tileset" + i + ".txt");
                Tilesets.SaveTo(_tilesets[i], tilesetWriter);
                tilesetWriter.Close();
            }
        }

        /// <summary>
        /// Loads all Tilesets from text files in the resources directory. "tilesets.txt" contains an integer
        /// representing the total number of Tilesets. Each Tileset is stored in a text file following the format
        /// "tileset" + index + ".txt"
        /// </summary>
        public static void LoadAllTilesets()
        {
            _tilesets = new ListWithActive<Tileset>();

            //If the text file containing the total number of Tilesets is empty
            if (Utils.GetTotalFileLines("Resources/tilesets.txt") == 0)
            {
                _tilesets.Add(Tilesets.CreateTileset("New Level", WorldAnchor.Instance, SwinGame.PointAt(DEFAULT_START_X, DEFAULT_START_Y)));
            }
            else
            {
                StreamReader totalTilesets = new StreamReader("Resources/tilesets.txt");
                int numTilesets = Utils.ReadInteger(totalTilesets);
                totalTilesets.Close();

                //For each Tileset to be loaded
                for (int i = 0; i < numTilesets; i++)
                {
                    StreamReader tilesetReader = new StreamReader("Resources/tileset" + i + ".txt");
                    _tilesets.Add(Tilesets.Load(tilesetReader));
                    tilesetReader.Close();
                }
            }

            //Deregister each Tileset from the Renderer so they don't draw on top of each other.
            foreach (Tileset tileset in _tilesets.List)
                tileset.Deregister();

            //Once all Tilesets are loaded, make the first one in the list the active one.
            _tilesets.Active = _tilesets[0];
            _tilesets[0].Register();
        }

        /// <summary>
        /// Removes the active Tileset from the master list. This will remove it from the Renderer, as well
        /// as deleting the text file it's saved in and updating the total number of Tilesets.
        /// </summary>
        public static void RemoveTileset()
        {
            if (_tilesets.Count > 1) //Must always have at least one Tileset
            {
                File.Delete("Resources/tileset" + _tilesets.List.IndexOf(Active) + ".txt");
                Active.Deregister();
                _tilesets.Remove(Active);
                SaveAllTilesets();
                PreviousTileset();
            }
        }
    }
}
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the child Panel responsible for allowing the user to modify the settings of the Tileset
    /// currently being built. This involves changing the name; adding columns or rows; saving; loading or removing Tilesets.
    /// </summary>
    internal class SettingsPanel : Panel
    {
        /// <summary>
        /// Represents the Rectangle where the name of the Active Tileset should be Rendered.
        /// </summary>
        private Rectangle _renderTilesetNameAt;

        /// <summary>
        /// Indicates whether or not the Tileset's name is currently being changed. This extra variable exists due to a bug
        /// with SwinGame's Reading Text methods.
        /// </summary>
        private bool _readingText = false;

        public SettingsPanel(float x, float y, int width, int height, bool showing) : this (SwinGame.PointAt(x, y), width, height, showing)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TileStyleSDK.SettingsPanel"/> class.
        /// Also creates Buttons defining the behaviour of the SettingsPanel and adds them to the Panel.
        /// </summary>
        /// <param name="pt">The position of the Panel.</param>
        /// <param name="width">The width of the Panel.</param>
        /// <param name="height">The height of the Panel.</param>
        /// <param name="showing">Whether or not the Panel should be initially Rendered.</param>
        public SettingsPanel(Point2D pt, int width, int height, bool showing) : base(pt, width, height, "SettingsPanel", showing)
        {
            //Initialise where the Tileset name should be Rendered.
            _renderTilesetNameAt = SwinGame.CreateRectangle(AbsPos.X + 33, AbsPos.Y + 46, Width - 64, 20);

            /// <summary>
            /// Represents the Button to change the Active Tileset to the previous one in the list.
            /// </summary>
            AddButton(Buttons.CreateButton(Buttons.PREV_ACTIVE, Buttons.PREV_INACTIVE, TilesetManager.PreviousTileset, 5, 30));

            /// <summary>
            /// Represents the Button to change the Active Tileset to the next one in the list.
            /// </summary>
            AddButton(Buttons.CreateButton(Buttons.NEXT_ACTIVE, Buttons.NEXT_INACTIVE, TilesetManager.NextTileset, Width - 37, 30));

            /// <summary>
            /// Represents the Button to add a column to the Active Tileset.
            /// </summary>
            AddButton(Buttons.CreateButton(Buttons.PLUS_ACTIVE, Buttons.PLUS_INACTIVE, AddColumn, Width - 42, 70));

            /// <summary>
            /// Represents the Button to add a row to the Active Tileset.
            /// </summary>
            AddButton(Buttons.CreateButton(Buttons.PLUS_ACTIVE, Buttons.PLUS_INACTIVE, AddRow, Width - 42, 105));

            /// <summary>
            /// Represents the Button to remove a column from the Active Tileset.
            /// </summary>
            AddButton(Buttons.CreateButton(Buttons.MINUS_ACTIVE, Buttons.MINUS_INACTIVE, RemoveCol, 70, 70));

            /// <summary>
            /// Represents the Button to remove a row from the Active Tileset.
            /// </summary>
            AddButton(Buttons.CreateButton(Buttons.MINUS_ACTIVE, Buttons.MINUS_INACTIVE, RemoveRow, 70, 105));


            /// <summary>
            /// Represents the Button to Toggle the display of the grid for the Active Tileset.
            /// </summary>
            AddButton(Buttons.CreateButton(30, 20, "Grid", TilesetManager.ToggleGrid, Width - 35, 5));

            /// <summary>
            /// Represents the Button to add a new Tileset to be edited.
            /// </summary>
            AddButton(Buttons.CreateButton(50, 25, "Add", TilesetManager.AddNewTileset, 60, 137));

            /// <summary>
            /// Represents the Button to save Tilesets currently being constructed.
            /// </summary>
            AddButton(Buttons.CreateButton(45, 25, "Save", TilesetManager.SaveAllTilesets, 5, 137));

            /// <summary>
            /// Represents the Button to delete the Active Tileset.
            /// </summary>
            AddButton(Buttons.CreateButton(59, 25, "Remove", TilesetManager.RemoveTileset, 112, 137));
        }

        /// <summary>
        /// Gets the active tileset from the TilesetManager.
        /// This is a wrapper property to make smoother method calls.
        /// </summary>
        /// <value>The active tileset.</value>
        public Tileset ActiveTileset
        {
            get {return TilesetManager.Active;}
        }

        /// <summary>
        /// Wrapper method for a Button as Buttons can only store methods which return void and take zero parameters.
        /// </summary>
        public void AddColumn()
        {
            Tilesets.AddColumn(ActiveTileset);
        }

        /// <summary>
        /// Wrapper method for a Button as Buttons can only store methods which return void and take zero parameters.
        /// </summary>
        public void AddRow()
        {
            Tilesets.AddRow(ActiveTileset);
        }

        /// <summary>
        /// Wrapper method for a Button as Buttons can only store methods which return void and take zero parameters.
        /// </summary>
        public void RemoveCol()
        {
            Tilesets.RemoveCol(ActiveTileset);
        }

        /// <summary>
        /// Wrapper method for a Button as Buttons can only store methods which return void and take zero parameters.
        /// </summary>
        public void RemoveRow()
        {
            Tilesets.RemoveRow(ActiveTileset);
        }

        /// <summary>
        /// Handles input for the Settings Panel. Actions this Panel handles include changing the name of the Active Tileset
        /// </summary>
        public override void HandleInput()
        {
            base.HandleInput();

            /// <summary>
            /// Indicates the user has clicked on the Tileset name rectangle to change its name.
            /// </summary>
            if (SwinGame.MouseClicked(MouseButton.LeftButton) && SwinGame.PointInRect(SwinGame.MousePosition(), _renderTilesetNameAt))
            {
                if (!_readingText)
                {
                    SwinGame.StartReadingText(Color.Black, 12, SwinGame.FontNamed("SmallGameFont"), _renderTilesetNameAt);
                    _readingText = true;
                }
            }

            /// <summary>
            /// Indicates the user has submitted the typed in string as the new name for the Active Tileset.
            /// </summary>
            if (SwinGame.KeyTyped(KeyCode.ReturnKey) && _readingText)
            {
                if (_readingText)
                {
                    TilesetManager.Active.Name = SwinGame.EndReadingText();
                    _readingText = false;
                }
            }
        }

        /// <summary>
        /// Renders the Settings Panel. This involves Rendering the Panel itself; its buttons and helper text.
        /// </summary>
        public override void Render (Layer layer)
        {
            base.Render(layer); 

            //Render the Tileset Name if it's not currently being changed.
            if (!_readingText)
            {
                SwinGame.DrawText(ActiveTileset.Name, 
                                  Color.Black, 
                                  Color.Transparent, 
                                  SwinGame.FontNamed("SmallGameFont"), 
                                  FontAlignment.AlignCenter, 
                                  _renderTilesetNameAt);
            }

            SwinGame.DrawText("Level Settings", Color.Black, SwinGame.FontNamed("GameFont"), AbsPos.X + 10, AbsPos.Y + 10);
            SwinGame.DrawText("Columns:", Color.Black, SwinGame.FontNamed("SmallGameFont"), AbsPos.X + 15, AbsPos.Y + 81);
            SwinGame.DrawText("Rows:", Color.Black, SwinGame.FontNamed("SmallGameFont"), AbsPos.X + 15, AbsPos.Y + 116);
            SwinGame.DrawText(ActiveTileset.Cols.ToString(), Color.Black, AbsPos.X + 115, AbsPos.Y + 81);
            SwinGame.DrawText(ActiveTileset.Rows.ToString(), Color.Black, AbsPos.X + 115, AbsPos.Y + 116);
        }
    }
}
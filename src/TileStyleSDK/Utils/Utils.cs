using System;
using System.Linq;
using System.IO;
using SwinGameSDK;

namespace TileStyleSDK
{
    internal static class Utils
    {
        /// <summary>
        /// Splits the passed in Bitmap into an array containing all of its BitmapCells.
        /// These BitmapCells are also converted to a proper Bitmap by creating a new Bitmap
        /// and drawing the cell's contents to it.
        /// This is used by TileResources to add Tiles to the Palette.
        /// </summary>
        /// <returns>An array of Bitmaps representing each cell.</returns>
        /// <param name="toSplit">The Bitmap to split.</param>
        public static Bitmap[] SplitBitmapCells(Bitmap toSplit)
        {
            Bitmap[] result = new Bitmap[SwinGame.BitmapCellCount(toSplit)];
            int rows = SwinGame.BitmapCellRows(toSplit);
            int cols = SwinGame.BitmapCellColumns(toSplit);
            int w = SwinGame.BitmapCellWidth(toSplit);
            int h = SwinGame.BitmapCellHeight(toSplit);

            //The rectangle in the original Bitmap to draw to the new Bitmap cell.
            Rectangle copyAt;

            //The column in the original Bitmap of the cell currently being copied
            int atCol;

            //The row in the original Bitmap of the cell currently being copied
            int atRow;

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = SwinGame.CreateBitmap(w, h);
                SwinGame.ClearSurface(result[i], Color.Transparent);

                atCol = i % cols;
                atRow = (int)Math.Floor((double)(i / cols));

                copyAt = SwinGame.CreateRectangle(atCol * w, atRow * h, w, h);

                SwinGame.DrawBitmap(toSplit, 0, 0, SwinGame.OptionPartBmp(copyAt, SwinGame.OptionDrawTo(result[i])));
            }

            return result;
        }

        /// <summary>
        /// Creates a Bitmap representing the border of Panels and Buttons in the program.
        /// </summary>
        /// <returns>The Panel or Button border.</returns>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="thickness">The border thickness.</param>
        /// <param name="borderClr">The border color.</param>
        /// <param name="fillClr">The background color.</param>
        public static Bitmap CreatePanelBorder(int width, int height, int thickness, Color borderClr, Color fillClr)
        {
            Bitmap result = SwinGame.CreateBitmap(width, height);
            SwinGame.ClearSurface(result, borderClr);

            Bitmap fill = SwinGame.CreateBitmap(width - (thickness * 2), height - (thickness * 2));
            SwinGame.ClearSurface(fill, fillClr);

            SwinGame.DrawBitmap(fill, 3, 3, SwinGame.OptionPartBmp(thickness, thickness, fill.Width, fill.Height, SwinGame.OptionDrawTo(result)));

            return result;
        }

        /// <seealso cref="CreatePanelBorder(int, int, int, Color, Color)"/>
        /// <summary>
        /// Creates a Bitmap representing the border of Buttons with a label in the program.
        /// Overload for creating a Panel Border which also draws the passed in string to the centre of the Bitmap as a label.
        /// </summary>
        /// <returns>The panel border.</returns>
        /// <param name="text">The Panel or Button label.</param>
        public static Bitmap CreatePanelBorder(int width, int height, int thickness, Color borderClr, Color fillClr, string text)
        {
            Bitmap result = CreatePanelBorder(width, height, thickness, borderClr, fillClr);

            Color textColor = Color.Black;
            Color textBgColor = Color.Transparent;
            Font textFont = SwinGame.FontNamed("SmallGameFont");
            int textWidth = SwinGame.TextWidth(SwinGame.FontNamed("SmallGameFont"), text);
            int textHeight = SwinGame.TextHeight(SwinGame.FontNamed("SmallGameFont"), text);
            int drawLabelAtX = (SwinGame.BitmapWidth(result) / 2) - (textWidth / 2);
            int drawLabelAtY = (SwinGame.BitmapHeight(result) / 2) - (textHeight / 2);
            Rectangle labelRect = SwinGame.CreateRectangle(drawLabelAtX, drawLabelAtY, textWidth, textHeight);

            SwinGame.DrawText(text, textColor, textBgColor, textFont, FontAlignment.AlignCenter, labelRect, SwinGame.OptionDrawTo(result));

            return result;
        }

        /// <summary>
        /// Wrapper to allow StreamReaders to Read an Integer rather than a string.
        /// </summary>
        /// <returns>String converted to integer.</returns>
        /// <param name="reader">The Reader to read the Integer from.</param>
        public static int ReadInteger(StreamReader reader)
        {
            return Convert.ToInt32(reader.ReadLine());
        }

        /// <summary>
        /// Returns the total number of lines in a passed in file path. This is used by the TilesetManager
        /// to determine if any Tilesets exist to load.
        /// </summary>
        /// <returns>The total number of lines.</returns>
        /// <param name="file">The filepath.</param>
        public static int GetTotalFileLines(string file)
        {
            return File.ReadLines(file).Count();
        }
    }
}
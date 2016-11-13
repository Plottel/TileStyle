using System;
using System.Collections.Generic;
using System.IO;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the class for handling all things related to Slides, specifically Saving and Loading.
    /// </summary>
    internal static class Slides
    {
        /// <summary>
        /// Writes the details of the passed in Slide to the passed in StreamWriter.
        /// This is used as a subdivision of "Save Tileset" to divide responsibilities.
        /// </summary>
        /// <param name="toSave">The Slide to save.</param>
        /// <param name="writer">The writer to save the Slide to.</param>
        public static void SaveTo(Slide toSave, StreamWriter writer)
        {
            //Slides are loaded with ReadLine() which returns a string.
            //If the Bitmap is null, save it as a "null" string to allow smoother Loading.
            if (toSave.RootBitmap == null)
                writer.WriteLine("null");
            else
                writer.WriteLine(toSave.RootBitmap);

            writer.WriteLine(toSave.RootIndex);
        }

        /// <summary>
        /// Loads a Slide from the passed in StreamReader.
        /// This is used as a subdivision of "Load Tileset" to divide responsibilities.
        /// </summary>
        /// <param name="reader">The reader to load the Slide from.</param>
        /// <returns>The loaded Slide.</returns>
        public static Slide Load(StreamReader reader)
        {
            string rootBitmap = reader.ReadLine();
            int rootIndex = Utils.ReadInteger(reader);

            if (rootBitmap == "null")
                rootBitmap = null;

            //Create a Slide initialised with read values and return it.
            return new Slide
            {
                RootBitmap = rootBitmap,
                RootIndex = rootIndex
            };
        }
    }
}
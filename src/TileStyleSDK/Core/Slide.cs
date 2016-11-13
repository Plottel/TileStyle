using System;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the class responsible for breaking a Tile up in to layers.
    /// Each Slide contains a string and int representing a Bitmap and cell index 
    /// stored in TileResources. When a Slide is asked to Render, it performs 
    /// a dictionary lookup in TileResources to fetch the corresponding Bitmap.
    /// </summary>
    public class Slide
    {
        /// <summary>
        /// The name of the Bitmap stored in TileResources.
        /// </summary>
        private string _rootBitmap;

        /// <summary>
        /// The index of the Bitmap stored in TileREsources.
        /// </summary>
        public int RootIndex {get; set;}

        /// <summary>
        /// Gets or sets the name of the Bitmap the Slide references.
        /// When this is changed, the index is set to 0 to avoid null references.
        /// E.g. if the Slide was previously referencing a Bitmap with 8 cells, but is now
        /// referencing a Bitmap with 5 cells, then fetching the 8th cell for the new Bitmap
        /// will return null.
        /// </summary>
        /// <value>The new root Bitmap.</value>
        public string RootBitmap
        {
            get
            {
                return _rootBitmap;
            }

            set
            {
                _rootBitmap = value;
                RootIndex = 0;
            }
        }

        /// <summary>
        /// The Bitmap to be drawn. This is fetched from TileResources using
        /// the stored RootBitmap and RootIndex values.
        /// </summary>
        /// <value>The Bitmap the Slide references.</value>
        public Bitmap Img
        {
            get
            {
                if (RootBitmap != null)
                    return TileResources.GetBitmap(RootBitmap, RootIndex);
                
                return null;
            }
        }
    }
}
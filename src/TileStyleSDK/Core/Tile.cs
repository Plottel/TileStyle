using System;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the core class of the SDK, the Tile. A Tile is defined as a 32 x 32
    /// "container" of Slides. A Tile is responsible for Rendering each of its Slides and is
    /// also either Passable or not Passable.
    /// </summary>
    public class Tile : Renderable
    {
        /// <summary>
        /// The Slides; one for each Layer. The Tile references these when Rendering.
        /// </summary>
        private Slide[] _slides = new Slide[(int)Layer.Count];

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:TileStyleSDK.Tile"/> is passable.
        /// </summary>
        /// <value><c>true</c> if the Tile is passable; otherwise, <c>false</c>.</value>
        public bool Passable {get; set;}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TileStyleSDK.Tile"/> class.
        /// </summary>
        public Tile()
        {
            //Initialise each Slide
            for (int i = 0; i < (int)Layer.Count; i++)
                _slides[i] = new Slide();
            Passable = true;
        }

        /// <summary>
        /// Default property indexer which gets or sets the Slide at the specified Layer.
        /// Allows Slides to be referenced with Tile[index] rather than Tile.Slides[index]
        /// </summary>
        /// <param name="layer">The Layer to fetch the Slide for.</param>
        public Slide this[Layer layer]
        {
            get {return _slides[(int)layer];}
            set {_slides[(int)layer] = value;}
        }

        /// <summary>
        /// Specifies whether or not the Tile exists on the passed in layer.
        /// This is used by the Renderer to determine if the Tile should be Rendered.
        /// This override from Renderable.ExistsOnLayer() as Tiles exist on multiple layers.
        /// </summary>
        /// <returns><c>true</c>, if the Tile exists on the given layer, <c>false</c> otherwise.</returns>
        /// <param name="layer">The Layer to check.</param>
        public override bool ExistsOnLayer(Layer layer)
        {
            return _slides[(int)layer].Img != null;
        }

        /// <summary>
        /// Renders the Slide corresponding to the passed in Layer. This is only called by the 
        /// Renderer, which will first check if the Tile exists on the layer to prevent drawing null Bitmaps.
        /// </summary>
        /// <param name="layer">Layer.</param>
        public override void Render(Layer layer)
        {
            SwinGame.DrawBitmap(_slides[(int)layer].Img, AbsPos.X, AbsPos.Y);
        }
    }
}
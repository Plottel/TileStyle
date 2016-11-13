using System;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the type signature for methods a Button can store.
    /// </summary>
    public delegate void ButtonDelegate();

    /// <summary>
    /// Represents the class that resembles interactive Buttons in the program.
    /// A Button has an active an inactive state, with a separate Bitmap for each.
    /// When the mouse is released, the method stored in the Button is called.
    /// </summary>
    public class Button : Renderable
    {
        /// <summary>
        /// The method called when the Button is executed.
        /// </summary>
        private ButtonDelegate _execute;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TileStyleSDK.Button"/> class.
        /// </summary>
        /// <param name="execute">Execute.</param>
        public Button(ButtonDelegate execute)
        {
            _execute = execute;
        }

        /// <summary>
        /// Gets the method called when the Button is executed.
        /// </summary>
        /// <value>The Button's method.</value>
        public ButtonDelegate Execute
        {
            get {return _execute;}
        }

        /// <summary>
        /// Gets or sets the Bitmap the Button will Render when active.
        /// </summary>
        /// <value>The active bitmap.</value>
        public Bitmap ActiveBitmap {get; set;}

        /// <summary>
        /// Gets or sets the Bitmap the Button will Render when inactive.
        /// </summary>
        /// <value>The inactive bitmap.</value>
        public Bitmap InactiveBitmap {get; set;}

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:TileStyleSDK.Button"/> is selected.
        /// </summary>
        /// <value><c>true</c> if the Button is selected; otherwise, <c>false</c>.</value>
        public bool Selected {get; set;} = false;

        /// <summary>
        /// Gets the width of the Button. This is determined directly from the Bitmap
        /// corresponding to the state of the Button.
        /// </summary>
        /// <value>The width of the Button.</value>
        public override int Width
        {
            get
            {
                if (Selected)
                    return SwinGame.BitmapWidth(ActiveBitmap);
                else
                    return SwinGame.BitmapWidth(InactiveBitmap);
            }
        }

        /// <summary>
        /// Gets the height of the Button. This is determined directly from the Bitmap
        /// corresponding to the state of the Button.
        /// </summary>
        /// <value>The width of the Button.</value>
        public override int Height
        {
            get
            {
                if (Selected)
                    return SwinGame.BitmapHeight(ActiveBitmap);
                else
                    return SwinGame.BitmapHeight(InactiveBitmap);
            }
        }

        /// <summary>
        /// Renders the Button's bitmap based on its current state if the Button exists
        /// on the given layer. By default, Buttons exist on Layer.UI
        /// </summary>
        /// <param name="layer">The layer currently being Rendered.</param>
        public override void Render(Layer layer)
        {
            if (Selected)
                SwinGame.DrawBitmap(ActiveBitmap, AbsPos.X, AbsPos.Y);
            else
                SwinGame.DrawBitmap(InactiveBitmap, AbsPos.X, AbsPos.Y);
        }
    }
}
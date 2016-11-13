using System;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the Renderable used as the Screen. Its position is always (0, 0).
    /// Objects anchored to the Screen will always be Rendered in the same place.
    /// The Screen's position is used to convert World coordinates into Screen coordinates
    /// so the Renderer can figure out where on the screen a Renderable should be Rendered.
    /// </summary>
    internal class ScreenAnchor : Renderable
    {
        /// <summary>
        /// There is only one screen, therefore the ScreenAnchor is a Singleton object.
        /// Ideally this would be a static class, but static classes can't inherit from non-static classes.
        /// </summary>
        private static ScreenAnchor _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TileStyleSDK.ScreenAnchor"/> class.
        /// </summary>
        public ScreenAnchor()
        {
            if (_instance == null)
            {
                X = 0;
                Y = 0;
                _instance = this;
                Anchor = this;
            }
            else
            {
                throw new Exception("Cannot have more than one instance of Screen Anchor");
            }
        }

        /// <summary>
        /// Gets the Screen Anchor Singleton through which its values are accessed.
        /// </summary>
        public static ScreenAnchor Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Gets the Absolute Position of the Screen.
        /// This is always (0, 0);
        /// </summary>
        /// <value>The Absolute Position of the Screen.</value>
        public override Point2D AbsPos
        {
            get
            {
                return SwinGame.PointAt(0, 0);
            }
        }
    }
}
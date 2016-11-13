using System;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the Renderable used to determine the current position of the World.
    /// Objects anchored to the World will change position as the World moves. (e.g. camera changes)
    /// </summary>
    internal class WorldAnchor : Renderable
    {
        /// <summary>
        /// There is only one World active at a time, therefore the WorldAnchor is a Singleton object.
        /// Ideally this would be a static class, but static classes can't inherit from non-static classes.
        /// </summary>
        private static WorldAnchor _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TileStyleSDK.WorldAnchor"/> class.
        /// </summary>
        public WorldAnchor()
        {
            if (_instance == null)
            {
                _instance = this;
                Anchor = this;
            }
            else
            {
                throw new Exception("Cannot have more than one instance of World Anchor");
            }
        }

        /// <summary>
        /// Gets the World Anchor Singleton through which its values are accessed.
        /// </summary>
        internal static WorldAnchor Instance 
        {
            get 
            {
                return _instance;
            }
        }

        /// <summary>
        /// Gets the Absolute Position of the World.
        /// </summary>
        /// <value>The Absolute Position of the World.</value>
        public override Point2D AbsPos
        {
            get
            {
                return Pos;
            }
        }
    }
}
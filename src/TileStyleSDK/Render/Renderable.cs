using System;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the core class used in the program to indicate that an object should be Rendered.
    /// Each Renderable has an anchor which it will be Rendered relative to the position of. This means
    /// that all X / Y positions stored in a Renderable object are their positions RELATIVE TO THEIR ANCHOR.
    /// In order to know where they should be drawn on the screen, the ABSOLUTE POSITION is derived from their anchor.
    /// Most things are anchored to either the World or the Screen, but any Renderable can be used as an anchor.
    /// For example, most Buttons are anchored to a Panel.
    /// </summary>
    public abstract class Renderable
    {
        /// <summary>
        /// Gets or sets the Renderable the object will be drawn relative to.
        /// </summary>
        /// <value>The anchor.</value>
        public Renderable Anchor {get; set;}

        /// <summary>
        /// Gets or sets the layer the Renderable exists on. 
        /// This is used by the Renderer to determine when the Renderable should be Rendered.
        /// </summary>
        /// <value>The layer.</value>
        public Layer Layer {get; set;}

        /// <summary>
        /// The width of the Renderable.
        /// </summary>
        private int _width;

        /// <summary>
        /// The height of the Renderable.
        /// </summary>
        private int _height;

        /// <summary>
        /// Gets or sets the width of the Renderable. Child classes can override
        /// how this is determined (e.g. Tileset derives width based on number of columns).
        /// </summary>
        /// <value>The width.</value>
        public virtual int Width
        {
            get {return _width;}
            set {_width = value;}
        }

        /// <summary>
        /// Gets or sets the height of the Renderable. Child classes can override
        /// how this is determined (e.g. Tileset derives height based on number of rows).
        /// </summary>
        /// <value>The height.</value>
        public virtual int Height
        {
            get {return _height;}
            set {_height = value;}
        }

        /// <summary>
        /// Gets or sets the relative X coordinate.
        /// </summary>
        /// <value>The x.</value>
        public float X {get; set;}

        /// <summary>
        /// Gets or sets the relative Y coordinate.
        /// </summary>
        /// <value>The y.</value>
        public float Y {get; set;}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TileStyleSDK.Renderable"/> class.
        /// Also registers it with the Renderer.
        /// </summary>
        public Renderable()
        {
            Renderer.Register(this);
        }      

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TileStyleSDK.Renderable"/> class.
        /// </summary>
        /// <param name="anchor">The Renderable the object will be drawn relative to.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="layer">The layer the object exists on.</param>
        /// <param name="width">The width of the Renderable.</param>
        /// <param name="height">The height of the Renderable.</param>
        public Renderable(Renderable anchor, float x, float y, Layer layer, int width, int height)
        {
            Renderer.Register(this);
            Anchor = anchor;
            X = x;
            Y = y;
            _width = width;
            _height = height;
            Layer = layer;
        }

        /// <seealso cref="Renderable(Renderable, float, float, Layer, int, int)"/>
        /// <summary>
        /// Overload for Point2D instead of x / y coordinates.
        /// </summary>
        public Renderable(Renderable anchor, Point2D pos, Layer layer, int width, int height) : this (anchor, pos.X, pos.Y, layer, width, height)
        {  
        }

        /// <summary>
        /// Represents the relative Position of the Renderable.
        /// </summary>
        /// <value>The position.</value>
        public Point2D Pos
        {
            get
            {
                return SwinGame.PointAt(X, Y);
            }

            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// Gets the absolute Position of the Renderable. This is where the Renderable exists relative to the screen and 
        /// is used to determine where on the screen the Renderable should be Rendered.
        /// </summary>
        /// <value>The absolute position.</value>
        public virtual Point2D AbsPos
        {
            get
            {   
                return SwinGame.AddVectors(Pos, Anchor.AbsPos);
            }
        }

        /// <summary>
        /// Specifies whether or not the Renderable is at a specified position.
        /// Child classes can override how this is determined
        /// </summary>
        /// <returns><c>true</c> if the Renderable is at ths position, otherwise <c>false</c>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public virtual bool IsAt(float x, float y)
        {
            return SwinGame.PointInRect(x, y, AbsPos.X, AbsPos.Y, Width, Height);
        }

        /// <seealso cref="IsAt(float, float)"/>
        /// <summary>
        /// Overload for Point2D instead of x / y coordinates.
        /// </summary>
        public virtual bool IsAt(Point2D pt)
        {
            return IsAt(pt.X, pt.Y);
        }

        /// <summary>
        /// Specifies if the Renderable exists on the passed in Layer.
        /// This is used by the Renderer to determine when the Renderable should be Rendered.
        /// Child classes can override how this is determined (e.g. Tiles exist on multiple layers)
        /// </summary>
        /// <returns><c>true</c>, if the Renderable exists on the Layer <c>false</c> otherwise.</returns>
        /// <param name="layer">The layer to check.</param>
        public virtual bool ExistsOnLayer(Layer layer)
        {
            return Layer == layer;
        }

        /// <summary>
        /// Registers the Renderable with the Renderer so it can be Rendered.
        /// Child classes can override this implementation (e.g. a Tileset will Register all its Tiles).
        /// </summary>
        public virtual void Register()
        {
            Renderer.Register(this);
        }

        /// <summary>
        /// Deregisters the Renderable from the Renderer so it will no longer be Rendered.
        /// Child classes can override this implementation (e.g. a Tileset will Deregister all its Tiles).
        /// </summary>
        public virtual void Deregister()
        {
            Renderer.Deregister(this);
        }

        /// <summary>
        /// Renders the Renderable at the specified layer. This is blank to allow full flexibility in
        /// child classes. This also allows for debug information to be added here (e.g. draw a rectangle
        /// around each Renderable to visually see collision boxes).
        /// </summary>
        /// <param name="layer">The layer to Render on.</param>
        public virtual void Render(Layer layer)
        {}
    }
}
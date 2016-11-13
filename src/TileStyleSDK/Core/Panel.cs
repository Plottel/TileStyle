using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the base class for Panels which exist in the program.
    /// Fundamentally, a Panel is a collection of, and a "container" for, Buttons.
    /// A Panel has its own visual representation and acts as a Rendering anchor for its Buttons.
    /// </summary>
    public class Panel : Renderable
    {
        /// <summary>
        /// The name of the Panel. This is used as an identifier.
        /// </summary>
        private string _name;

        /// <summary>
        /// The Bitmap representing the Panel's border and background.
        /// </summary>
        protected Bitmap _border;

        /// <summary>
        /// Indicates whether or not the Panel should be Rendered.
        /// </summary>
        private bool _showing;

        /// <summary>
        /// The Buttons contained within the Panel.
        /// </summary>
        protected readonly ListWithActive<Button> _buttons;


        /// <summary>
        /// Initializes a new instance of the <see cref="T:TileStyleSDK.Panel"/> class.
        /// </summary>
        /// <param name="x">The x coordinate where the Panel is created.</param>
        /// <param name="y">The y coordinate where the Panel is created.</param>
        /// <param name="width">The width of the Panel.</param>
        /// <param name="height">The height of the Panel.</param>
        /// <param name="name">The name of the Panel.</param>
        /// <param name="showing">Whether or not the Panel should be Rendered.</param>
        public Panel(float x, float y, int width, int height, string name, bool showing) : base (ScreenAnchor.Instance, x, y, Layer.UI, width, height)
        {
            _buttons = new ListWithActive<Button>();
            _name = name;
            _showing = showing;
            _border = Utils.CreatePanelBorder(width, height, Panels.BORDER_WIDTH, Panels.DEFAULT_BORDER_CLR, Panels.DEFAULT_BG_CLR);
        }

        /// <seealso cref="Panel(float, float, int, int, string, bool)"/>
        public Panel(Point2D pt, int width, int height, string name, bool showing) : this (pt.X, pt.Y, width, height, name, showing)
        {
        }

        /// <summary>
        /// Gets the name of the Panel.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get {return _name;}
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:TileStyleSDK.Panel"/> should be Rendered.
        /// </summary>
        /// <value><c>true</c> if showing; otherwise, <c>false</c>.</value>
        public bool Showing
        {
            get
            {
                return _showing;
            }

            set
            {
                _showing = value;

                //When a Panel is toggled, it interfaces with the Renderer to de / re-register itself.
                if (Showing)
                    Register();
                else
                    Deregister();
            }
        }

        /// <summary>
        /// Toggles the display of the Panel (i.e if showing, then hide; if hiding, then show).
        /// </summary>
        public void ToggleDisplay()
        {
            Showing = !Showing;
        }

        /// <summary>
        /// Adds the passed in Button to the Panel's list.
        /// The Button's anchor will become the Panel.
        /// </summary>
        /// <param name="toAdd">To add.</param>
        public void AddButton(Button toAdd)
        {
            toAdd.Anchor = this;
            _buttons.Add(toAdd);

            //If the Panel is hidden, deregister the Button so it won't be Rendered.
            if (!Showing)
                toAdd.Deregister();
        }

        /// <summary>
        /// Specifies if the Panel can be identified by the passed in string.
        /// </summary>
        /// <returns><c>true</c>, if the Panel is identified by the string, <c>false</c> otherwise.</returns>
        /// <param name="id">Identifier.</param>
        public bool AreYou(string id)
        {
            return _name == id;
        }

        /// <summary>
        /// Changes the state of the Button at the given point to "active".
        /// If no Button is at the point, nothing happens.
        /// </summary>
        /// <param name="pt">The point to check.</param>
        private void SelectButtonAt(Point2D pt) //Selecting buttons is purely a panel responsibility
        {
            foreach (Button b in _buttons.List)
            {
                if (b.IsAt(pt))
                {
                    b.Selected = true;
                    _buttons.Active = b;
                    break; //Buttons cannot overlap so only one can be selected at a time
                }
            }
        }

        /// <summary>
        /// Registers keyboard and mouse input for the Panel.
        /// At the base level, this checks if there is a Button to be selected.
        /// Child Panels can implement their own Input method.
        /// </summary>
        public virtual void HandleInput()
        {
            /// <summary>
            /// When the Left Mouse Button is released, if there is an active Button then its method is called.
            /// </summary>
            if (SwinGame.MouseClicked(MouseButton.LeftButton))
            {
                if (_buttons.Active != null)
                {
                    _buttons.Active.Execute();
                    _buttons.Active.Selected = false;
                    _buttons.Active = null; 
                }
            }

            if (SwinGame.MouseDown(MouseButton.LeftButton))
                SelectButtonAt(SwinGame.MousePosition());
        }

        /// <summary>
        /// Registers the Panel and all its Buttons with the Renderer.
        /// </summary>
        public override void Register()
        {
            base.Register();

            foreach (Button b in _buttons.List)
                b.Register();
        }

        /// <summary>
        /// Deregisters the Panel and all its Buttons from the Renderer.
        /// </summary>
        public override void Deregister()
        {
            base.Deregister();

            foreach (Button b in _buttons.List)
                b.Deregister();
        }

        /// <summary>
        /// Renders the Panel if it exists at the specified layer.
        /// By default, Panels exsit on Layer.UI.
        /// </summary>
        /// <param name="layer">The layer currently being drawn.</param>
        public override void Render(Layer layer)
        {
            if (Showing)
                SwinGame.DrawBitmap(_border, AbsPos.X, AbsPos.Y);
        }
    }
}
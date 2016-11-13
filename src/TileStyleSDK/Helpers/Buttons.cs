using System;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the class responsible for handling all things related to Buttons. This includes
    /// storing commonly used Button Bitmaps and defining creation logic. The user will interface 
    /// with this class when creating Buttons.
    /// </summary>
    public static class Buttons
    {
        /// <summary>
        /// The default background color for a Button's active state. This is used when Buttons are created
        /// without a passed in Bitmap, or with a label.
        /// </summary>
        public static Color DEFAULT_ACTIVE_BG_CLR = Color.DarkGray;

        /// <summary>
        /// The default background color for a Button's inactive state. This is used when Buttons are created
        /// without a passed in Bitmap, or with a label.
        /// </summary>
        public static Color DEFAULT_INACTIVE_BG_CLR = Color.LightGray;

        /// <summary>
        /// The default border color for a Button. This is used when Buttons are created
        /// without a passed in Bitmap, or with a label.
        /// </summary>
        public static Color DEFAULT_BORDER_CLR = Color.Blue;

        /// <summary>
        /// The default border width for a Button. This is used when Buttons are created
        /// without a passed in Bitmap, or with a label.
        /// </summary>
        public static int DEFAULT_BORDER_WIDTH = 3;

        /// <summary>
        /// Represents a left-facing arrow used for a list-navigation Button in its active state.
        /// </summary>
        public static Bitmap PREV_ACTIVE = SwinGame.BitmapNamed("PrevActive");

        /// <summary>
        /// Represents a left-facing arrow used for a list-navigation Button in its inactive state.
        /// </summary>
        public static Bitmap PREV_INACTIVE = SwinGame.BitmapNamed("PrevInactive");

        /// <summary>
        /// Represents a right-facing arrow used for a list-navigation Button in its active state.
        /// </summary>
        public static Bitmap NEXT_ACTIVE = SwinGame.BitmapNamed("NextActive");

        /// <summary>
        /// Represents a right-facing arrow used for a list-navigation Button in its inactive state.
        /// </summary>
        public static Bitmap NEXT_INACTIVE = SwinGame.BitmapNamed("NextInactive");

        /// <summary>
        /// Represents a plus sign used for a Button in its active state.
        /// </summary>
        public static Bitmap PLUS_ACTIVE = SwinGame.BitmapNamed("PlusActive");

        /// <summary>
        /// Represents a plus sign used for a Button in its inactive state.
        /// </summary>
        public static Bitmap PLUS_INACTIVE = SwinGame.BitmapNamed("PlusInactive");

        /// <summary>
        /// Represents a minus sign used for a Button in its active state.
        /// </summary>
        public static Bitmap MINUS_ACTIVE = SwinGame.BitmapNamed("MinusActive");

        /// <summary>
        /// Represents a minus sign used for a Button in its inactive sate.
        /// </summary>
        public static Bitmap MINUS_INACTIVE = SwinGame.BitmapNamed("MinusInactive");


        #region BUTTON CREATION METHODS

        #region CREATE BUTTON FROM BITMAPS

        /// <summary>
        /// Represents the "true" Button creation method. All overloads will manipulate data to create variables
        /// matching this method signature.
        /// </summary>
        /// <returns>The Button.</returns>
        /// <param name="activeBitmap">The Bitmap for the Button's active state.</param>
        /// <param name="inactiveBitmap">The Bitmap for the Button's inactive state.</param>
        /// <param name="execute">The method the Button will call when it is clicked on.</param>
        /// <param name="anchor">The Renderable the Button is Rendered relative to.</param>
        /// <param name="pos">The position of the Button.</param>
        public static Button CreateButton(Bitmap activeBitmap, Bitmap inactiveBitmap, ButtonDelegate execute, Renderable anchor, Point2D pos)
        {
            Button newButton = new Button(execute);
            newButton.ActiveBitmap = activeBitmap;
            newButton.InactiveBitmap = inactiveBitmap;
            newButton.Anchor = anchor;
            newButton.Pos = pos;
            newButton.Layer = Layer.UI;

            return newButton;
        }

        /// <seealso cref="CreateButton(Bitmap, Bitmap, ButtonDelegate, Renderable, Point2D)"/>
        /// <summary>
        /// Overload for x / y coordinates instead of Point2D.
        /// </summary>
        /// <returns>The Button.</returns>
        public static Button CreateButton(int width, int height, ButtonDelegate execute, float x, float y)
        {
            return CreateButton(width, height, execute, SwinGame.PointAt(x, y));
        }

        public static Button CreateButton(Bitmap activeBitmap, Bitmap inactiveBitmap, ButtonDelegate execute, float x, float y)
        {
            return CreateButton(activeBitmap, inactiveBitmap, execute, SwinGame.PointAt(x, y));
        }

        public static Button CreateButton(Bitmap activeBitmap, Bitmap inactiveBitmap, ButtonDelegate execute, Point2D pos)
        {
            return CreateButton(activeBitmap, inactiveBitmap, execute, ScreenAnchor.Instance, pos);
        }

        #endregion CREATE BUTTON FROM BITMAPS

        #region CREATE BUTTON WITHOUT BITMAPS

        /// <seealso cref="CreateButton(Bitmap, Bitmap, ButtonDelegate, Renderable, Point2D)"/>
        /// <summary>
        /// Creates a Button with a label when a Bitmap is not specified.
        /// This represents the small, blue-bordered Buttons with a text label seen in the program.
        /// </summary>
        /// <returns>The Button.</returns>
        /// <param name="label">The text label of the Button to be Rendered.</param>
        public static Button CreateButton(int width, int height, string label, ButtonDelegate execute, Renderable anchor, Point2D pos)
        {
            Bitmap activeBitmap = Utils.CreatePanelBorder(width, height, DEFAULT_BORDER_WIDTH, DEFAULT_BORDER_CLR, DEFAULT_ACTIVE_BG_CLR, label);
            Bitmap inactiveBitmap = Utils.CreatePanelBorder(width, height, DEFAULT_BORDER_WIDTH, DEFAULT_BORDER_CLR, DEFAULT_INACTIVE_BG_CLR, label);

            return CreateButton(activeBitmap, inactiveBitmap, execute, anchor, pos);
        }

        /// <seealso cref="CreateButton(int, int, string, ButtonDelegate, Renderable, Point2D)"/>
        /// <summary>
        /// Overload for x / y coordinates instead of Point2D.
        /// </summary>
        /// <returns>The Button.</returns>
        public static Button CreateButton(int width, int height, string label, ButtonDelegate execute, Renderable anchor, float x, float y)
        {
            return CreateButton(width, height, label, execute, anchor, SwinGame.PointAt(x, y));
        }

        /// <seealso cref="CreateButton(int, int, string, ButtonDelegate, Renderable, Point2D)"/>
        /// <summary>
        /// When no Anchor is specified, the Button is anchored by default to the Screen.
        /// </summary>
        /// <returns>The Button.</returns>
        public static Button CreateButton(int width, int height, string label, ButtonDelegate execute, Point2D pos)
        {
            return CreateButton(width, height, label, execute, ScreenAnchor.Instance, pos);
        }

        /// <seealso cref="CreateButton(int, int, string, ButtonDelegate, Point2D)"/>
        /// <summary>
        /// Overload for x / y coordinates instead of Point2D.
        /// </summary>
        /// <returns>The Button.</returns>
        public static Button CreateButton (int width, int height, string label, ButtonDelegate execute, float x, float y)
        {
            return CreateButton(width, height, label, execute, SwinGame.PointAt(x, y));
        }

        /// <seealso cref="CreateButton(Bitmap, Bitmap, ButtonDelegate, Renderable, Point2D)"/>
        /// <summary>
        /// Creates a Button using default values when neither a Bitmap nor a label is specified
        /// This represents the small, blue-bordered Buttons without a label seen in the program.
        /// </summary>
        /// <returns>The button.</returns> 
        public static Button CreateButton(int width, int height, ButtonDelegate execute, Point2D pos)
        {
            Bitmap activeBitmap = Utils.CreatePanelBorder(width, height, DEFAULT_BORDER_WIDTH, DEFAULT_BORDER_CLR, DEFAULT_ACTIVE_BG_CLR);
            Bitmap inactiveBitmap = Utils.CreatePanelBorder(width, height, DEFAULT_BORDER_WIDTH, DEFAULT_BORDER_CLR, DEFAULT_INACTIVE_BG_CLR);

            return CreateButton(activeBitmap, inactiveBitmap, execute, ScreenAnchor.Instance, pos);
        }
        #endregion CREATE BUTTON WITHOUT BITMAPS
        #endregion BUTTON CREATION METHODS
    }
}
using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the class responsible for handling all things related to Panels. This class manages a list
    /// of Panels to be used in the program and provides various helper functions for the user to interact with them.
    /// Panels are fetched by name and therefore must be named uniquely.
    /// </summary>
    public static class Panels
    {
        /// <summary>
        /// The default border color Panels are created with.
        /// </summary>
        public static Color DEFAULT_BORDER_CLR = Color.Blue;

        /// <summary>
        /// The default background color Panels are created with.
        /// </summary>
        public static Color DEFAULT_BG_CLR = Color.DarkGray;

        /// <summary>
        /// The default border with Panels are created with.
        /// </summary>
        public const int BORDER_WIDTH = 3;

        /// <summary>
        /// Represents the list of Panels the program is currently managing.
        /// </summary>
        private static List<Panel> _panels = new List<Panel>();

        /// <summary>
        /// Registers each Panel with the Renderer. Used when changing Game State.
        /// </summary>
        public static void Register()
        {
            foreach (Panel p in _panels)
                p.Register();
        }

        /// <summary>
        /// Deregisters each Panel from the Renderer. Used when changing Game State.
        /// </summary>
        public static void Deregister()
        {
            foreach (Panel p in _panels)
                p.Deregister();
        }

        /// <summary>
        /// Handles input for each Panel in the master list. Called once per frame by
        /// the core controller when in editing mode.
        /// </summary>
        public static void HandleInput()
        {
            foreach (Panel p in _panels)
                p.HandleInput();
        }       

        /// <summary>
        /// Adds the passed in Panel to the list of Panels. An exception is thrown if the list
        /// already contains a Panel with the given name.
        /// </summary>
        /// <param name="toAdd">To add.</param>
        public static void AddPanel(Panel toAdd)
        {
            if (!HasPanelNamed(toAdd.Name))
                _panels.Add(toAdd);
            else
                throw new InvalidOperationException("Panel with name: " + toAdd.Name + " already exists!");
        }

        /// <summary>
        /// Creates a Panel using default parameters and adds it to the master list of Panels.
        /// </summary>
        /// <param name="x">The x coordinate of the Panel.</param>
        /// <param name="y">The y coordinate of the Panel.</param>
        /// <param name="width">The width of the Panel.</param>
        /// <param name="height">The height of the Panel.</param>
        /// <param name="name">The name of the Panel.</param>
        /// <param name="showing">Whether or not the Panel should be visible.</param>
        public static void AddNewPanel(float x, float y, int width, int height, string name, bool showing)
        {
            AddPanel(new Panel(x, y, width, height, name, showing));
        }

        /// <seealso cref="AddNewPanel(float, float, int, int, string, bool)"/>
        /// <summary>
        /// Overload for Point2D instead of x / y coordinates.
        /// </summary>
        public static void AddNewPanel(Point2D pt, int width, int height, string name, bool showing)
        {
            AddPanel(new Panel(pt, width, height, name, showing));
        }

        /// <summary>
        /// Adds the passed in Button to the Panel with the passed in name. An exception is thrown
        /// if trying to add a Button to a Panel which does not exist.
        /// </summary>
        /// <param name="toAdd">The Button to add.</param>
        /// <param name="panelName">The Panel to add the Button to.</param>
        public static void AddButtonToPanelNamed(Button toAdd, string panelName)
        {
            foreach (Panel p in _panels)
            {
                if (p.AreYou(panelName))
                {
                    p.AddButton(toAdd);
                    return;
                }
            }

            throw new PanelNotFoundException("Panel with name: " + panelName + " could not be found");
        }

        /// <summary>
        /// Specifies whether or not the user has clicked on a Panel.
        /// This is used to prevent the user interacting unintentionally with the TIleset by 
        /// clicking "through" a Panel.
        /// </summary>
        /// <returns><c>true</c>, if a Panel has been clicked on, <c>false</c> otherwise.</returns>
        internal static bool ClickedOnPanel()
        {
            foreach (Panel p in _panels)
            {
                if (p.IsAt(SwinGame.MousePosition()) && p.Showing)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Specifies whether or not the list contains a Panel with the given name.
        /// </summary>
        /// <returns><c>true</c>, if a Panel exists with the given name, <c>false</c> otherwise.</returns>
        /// <param name="panelName">Panel name.</param>
        public static bool HasPanelNamed(string panelName)
        {
            foreach (Panel p in _panels)
            {
                if (p.AreYou(panelName))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Fetches the Panel with the given name from the master list of Panels. An exception is 
        /// thrown if the list does not contain the Panel.
        /// </summary>
        /// <returns>The Panel with the given name.</returns>
        /// <param name="panelName">The panel name to fetch.</param>
        public static Panel PanelNamed(string panelName)
        {
            foreach (Panel p in _panels)
            {
                if (p.AreYou(panelName))
                    return p;
            }

            throw new PanelNotFoundException("Panel with name: " + panelName + " could not be found");
        }

        /// <summary>
        /// Hides the Panel with the passed in name so it won't be Rendered. An exception is thrown
        /// if trying to hide a Panel which does not exist.
        /// </summary>
        /// <param name="panelName">The Panel name to hide.</param>
        public static void HidePanelNamed(string panelName)
        {
            foreach (Panel p in _panels)
            {
                if (p.AreYou(panelName))
                {
                    p.Showing = false;
                    return;
                }
            }

            throw new PanelNotFoundException("Panel with name: " + panelName + " could not be found");
        }

        /// <summary>
        /// Shows the Panel with the passed in name so it will be Rendered. An exception is thrown
        /// if trying to show a Panel which does not exist.
        /// </summary>
        /// <param name="panelName">Panel name.</param>
        public static void ShowPanelNamed(string panelName)
        {
            foreach (Panel p in _panels)
            {
                if (p.AreYou(panelName))
                {
                    p.Showing = true;
                    return;
                }
            }

            throw new PanelNotFoundException("Panel with name: " + panelName + " could not be found");
        }


        /// <summary>
        /// Toggles the display of the Panel with the passed in name (i.e. if it was showing, hide; if it was hiding, show).
        /// An exception is thrown if trying to Toggle the display of a Panel which does not exist.
        /// </summary>
        /// <param name="panelName">Panel name.</param>
        public static void TogglePanelDisplay(string panelName)
        {
            foreach (Panel p in _panels)
            {
                if (p.AreYou(panelName))
                {
                    p.ToggleDisplay();
                    return;
                }
            }

            throw new PanelNotFoundException("Panel with name: " + panelName + " could not be found");
        }
    }
}
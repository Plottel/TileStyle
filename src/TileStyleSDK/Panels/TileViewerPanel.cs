using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace TileStyleSDK
{
    /// <summary>
    /// Represents the class responsible for displaying details of a Tile. This is used in various points in the program to 
    /// allow the user to view the details of a Tile; including the Bitmap in each Slide and whether or not the Tile is passable.
    /// </summary>
    internal class TileViewerPanel : Panel
    {
        /// <summary>
        /// Gets or sets the Tile the Panel will display the details of.
        /// </summary>
        /// <value>The viewing.</value>
        public Tile Viewing {get; set;}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TileStyleSDK.TileViewerPanel"/> class.
        /// </summary>
        /// <param name="x">The x coordinate of the Panel.</param>
        /// <param name="y">The y coordinate of the Panel.</param>
        /// <param name="width">The width of the Panel.</param>
        /// <param name="height">The height of the Panel.</param>
        /// <param name="name">The name of the Panel.</param>
        /// <param name="viewing">The Tile the Panel will display the details of.</param>
        /// <param name="showing">Whether or not the Panel should be initially Rendered.</param>
        public TileViewerPanel(float x, float y, int width, int height, string name, Tile viewing, bool showing) : base(x, y, width, height, name, showing)
        {
            Viewing = viewing;
        }

        public TileViewerPanel(Point2D pt, int width, int height, string name, Tile viewing, bool showing) : this (pt.X, pt.Y, width, height, name, viewing, showing)
        {
        }

        /// <summary>
        /// Renders the Panel. This involves Rendering each layer of the Tile in a separate rectangle, as
        /// well as various helper text.
        /// </summary>
        public override void Render(Layer layer)
        {
            if (Showing)
            {
                base.Render(layer);

                if (Viewing != null)
                {
                    SwinGame.DrawBitmap(Viewing[Layer.Sky].Img, AbsPos.X + 10, AbsPos.Y + 5); //Render Sky layer Bitmap
                    SwinGame.DrawBitmap(Viewing[Layer.Wall].Img, AbsPos.X + 10, AbsPos.Y + 47); //Render Wall layer Bitmap
                    SwinGame.DrawBitmap(Viewing[Layer.Ground].Img, AbsPos.X + 10, AbsPos.Y + 89); //Render Ground layer Bitmap

                    SwinGame.DrawText("Passable: " + Viewing.Passable, Color.Black, SwinGame.FontNamed("SmallGameFont"), AbsPos.X + 10, AbsPos.Y + 150);
                }

                //Render Rectangles indicating where Bitmaps will be Rendered if the Tile has a Slide on that layer.
                SwinGame.DrawText("Sky Layer", Color.Black, SwinGame.FontNamed("SmallGameFont"), AbsPos.X + 45, AbsPos.Y + 16);
                SwinGame.DrawText("Wall Layer", Color.Black, SwinGame.FontNamed("SmallGameFont"), AbsPos.X + 45, AbsPos.Y + 58);
                SwinGame.DrawText("Ground Layer", Color.Black, SwinGame.FontNamed("SmallGameFont"), AbsPos.X + 45, AbsPos.Y + 110);

                //Draw outlines for Tile layer slots
                SwinGame.DrawRectangle(Color.Black, AbsPos.X + 10, AbsPos.Y + 5, Renderer.TILE_SIZE, Renderer.TILE_SIZE);
                SwinGame.DrawRectangle(Color.Black, AbsPos.X + 10, AbsPos.Y + 47, Renderer.TILE_SIZE, Renderer.TILE_SIZE);
                SwinGame.DrawRectangle(Color.Black, AbsPos.X + 10, AbsPos.Y + 89, Renderer.TILE_SIZE, Renderer.TILE_SIZE);
            }
        }
    }
}
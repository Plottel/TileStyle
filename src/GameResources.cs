using System;
using SwinGameSDK;
using TileStyleSDK;

namespace MyGame
{
    public static class GameResources
    {
        public static void LoadResources()
        {
            //Declare all Bitmaps
            TileResources.AddTileBitmap(SwinGame.BitmapNamed("Grass"), Layer.Ground, false);
            TileResources.AddTileBitmap(SwinGame.BitmapNamed("Barrel"), Layer.Wall, false);
            TileResources.AddTileBitmap(SwinGame.BitmapNamed("Bush"), Layer.Wall, false);
            TileResources.AddTileBitmap(SwinGame.BitmapNamed("Fence"), Layer.Wall, true);
            TileResources.AddTileBitmap(SwinGame.BitmapNamed("StoneWall"), Layer.Wall, true);
            TileResources.AddTileBitmap(SwinGame.BitmapNamed("Cloud"), Layer.Sky, true);
            TileResources.AddTileBitmap(SwinGame.BitmapNamed("Dirt"), Layer.Ground, true);
            TileResources.AddTileBitmap(SwinGame.BitmapNamed("RedBrickPath"), Layer.Ground, true);
            TileResources.AddTileBitmap(SwinGame.BitmapNamed("CobblePath"), Layer.Ground, true);
            TileResources.AddTileBitmap(SwinGame.BitmapNamed("StonePath"), Layer.Ground, true);
            TileResources.AddTileBitmap(SwinGame.BitmapNamed("Water"), Layer.Ground, true);

            /// <summary>
            /// Set cell mask details
            /// </summary>
            TileResources.SetBitmapCellForMask("Fence", Neighbour.None, 0);
            TileResources.SetBitmapCellForMask("Fence", Neighbour.North, 0);
            TileResources.SetBitmapCellForMask("Fence", Neighbour.East, 1);
            TileResources.SetBitmapCellForMask("Fence", Neighbour.East | Neighbour.North, 1);
            TileResources.SetBitmapCellForMask("Fence", Neighbour.South, 2);
            TileResources.SetBitmapCellForMask("Fence", Neighbour.South | Neighbour.North, 2);
            TileResources.SetBitmapCellForMask("Fence", Neighbour.South | Neighbour.East, 3);
            TileResources.SetBitmapCellForMask("Fence", Neighbour.South | Neighbour.East | Neighbour.North, 3);
            TileResources.SetBitmapCellForMask("Fence", Neighbour.West, 4);
            TileResources.SetBitmapCellForMask("Fence", Neighbour.West | Neighbour.North, 4);
            TileResources.SetBitmapCellForMask("Fence", Neighbour.West | Neighbour.East, 5);
            TileResources.SetBitmapCellForMask("Fence", Neighbour.West | Neighbour.East | Neighbour.North, 5);
            TileResources.SetBitmapCellForMask("Fence", Neighbour.West | Neighbour.South, 6);
            TileResources.SetBitmapCellForMask("Fence", Neighbour.West | Neighbour.South | Neighbour.North, 6);
            TileResources.SetBitmapCellForMask("Fence", Neighbour.West | Neighbour.South | Neighbour.East, 7);
            TileResources.SetBitmapCellForMask("Fence", Neighbour.West | Neighbour.South | Neighbour.East | Neighbour.North, 7);

            TileResources.SetBitmapCellForMask("StoneWall", Neighbour.None, 0);
            TileResources.SetBitmapCellForMask("StoneWall", Neighbour.North, 1);
            TileResources.SetBitmapCellForMask("StoneWall", Neighbour.East, 5);
            TileResources.SetBitmapCellForMask("StoneWall", Neighbour.East | Neighbour.North, 2);
            TileResources.SetBitmapCellForMask("StoneWall", Neighbour.South, 8);
            TileResources.SetBitmapCellForMask("StoneWall", Neighbour.South | Neighbour.North, 3);
            TileResources.SetBitmapCellForMask("StoneWall", Neighbour.South | Neighbour.East, 9);
            TileResources.SetBitmapCellForMask("StoneWall", Neighbour.South | Neighbour.East | Neighbour.North, 11);
            TileResources.SetBitmapCellForMask("StoneWall", Neighbour.West, 7);
            TileResources.SetBitmapCellForMask("StoneWall", Neighbour.West | Neighbour.North, 4);
            TileResources.SetBitmapCellForMask("StoneWall", Neighbour.West | Neighbour.East, 6);
            TileResources.SetBitmapCellForMask("StoneWall", Neighbour.West | Neighbour.East | Neighbour.North, 14);
            TileResources.SetBitmapCellForMask("StoneWall", Neighbour.West | Neighbour.South, 10);
            TileResources.SetBitmapCellForMask("StoneWall", Neighbour.West | Neighbour.South | Neighbour.North, 12);
            TileResources.SetBitmapCellForMask("StoneWall", Neighbour.West | Neighbour.South | Neighbour.East, 13);
            TileResources.SetBitmapCellForMask("StoneWall", Neighbour.West | Neighbour.South | Neighbour.East | Neighbour.North, 15);

            TileResources.SetBitmapCellForMask("Cloud", Neighbour.None, 0);
            TileResources.SetBitmapCellForMask("Cloud", Neighbour.North, 0);
            TileResources.SetBitmapCellForMask("Cloud", Neighbour.East, 1);
            TileResources.SetBitmapCellForMask("Cloud", Neighbour.East | Neighbour.North, 6);
            TileResources.SetBitmapCellForMask("Cloud", Neighbour.South, 0);
            TileResources.SetBitmapCellForMask("Cloud", Neighbour.South | Neighbour.North, 0);
            TileResources.SetBitmapCellForMask("Cloud", Neighbour.South | Neighbour.East, 4);
            TileResources.SetBitmapCellForMask("Cloud", Neighbour.South | Neighbour.East | Neighbour.North, 5);
            TileResources.SetBitmapCellForMask("Cloud", Neighbour.West, 2);
            TileResources.SetBitmapCellForMask("Cloud", Neighbour.West | Neighbour.North, 9);
            TileResources.SetBitmapCellForMask("Cloud", Neighbour.West | Neighbour.East, 12);
            TileResources.SetBitmapCellForMask("Cloud", Neighbour.West | Neighbour.East | Neighbour.North, 11);
            TileResources.SetBitmapCellForMask("Cloud", Neighbour.West | Neighbour.South, 7);
            TileResources.SetBitmapCellForMask("Cloud", Neighbour.West | Neighbour.South | Neighbour.North, 8);
            TileResources.SetBitmapCellForMask("Cloud", Neighbour.West | Neighbour.South | Neighbour.East, 10);
            TileResources.SetBitmapCellForMask("Cloud", Neighbour.West | Neighbour.South | Neighbour.East | Neighbour.North, 3);

            TileResources.SetBitmapCellForMask("Dirt", Neighbour.None, 0);
            TileResources.SetBitmapCellForMask("Dirt", Neighbour.North, 3);
            TileResources.SetBitmapCellForMask("Dirt", Neighbour.East, 2);
            TileResources.SetBitmapCellForMask("Dirt", Neighbour.East | Neighbour.North, 8);
            TileResources.SetBitmapCellForMask("Dirt", Neighbour.South, 4);
            TileResources.SetBitmapCellForMask("Dirt", Neighbour.South | Neighbour.North, 14);
            TileResources.SetBitmapCellForMask("Dirt", Neighbour.South | Neighbour.East, 5);
            TileResources.SetBitmapCellForMask("Dirt", Neighbour.South | Neighbour.East | Neighbour.North, 12);
            TileResources.SetBitmapCellForMask("Dirt", Neighbour.West, 1);
            TileResources.SetBitmapCellForMask("Dirt", Neighbour.West | Neighbour.North, 7);
            TileResources.SetBitmapCellForMask("Dirt", Neighbour.West | Neighbour.East, 15);
            TileResources.SetBitmapCellForMask("Dirt", Neighbour.West | Neighbour.East | Neighbour.North, 11);
            TileResources.SetBitmapCellForMask("Dirt", Neighbour.West | Neighbour.South, 6);
            TileResources.SetBitmapCellForMask("Dirt", Neighbour.West | Neighbour.South | Neighbour.North, 10);
            TileResources.SetBitmapCellForMask("Dirt", Neighbour.West | Neighbour.South | Neighbour.East, 9);
            TileResources.SetBitmapCellForMask("Dirt", Neighbour.West | Neighbour.South | Neighbour.East | Neighbour.North, 13);

            TileResources.SetBitmapCellForMask("RedBrickPath", Neighbour.None, 0);
            TileResources.SetBitmapCellForMask("RedBrickPath", Neighbour.North, 1);
            TileResources.SetBitmapCellForMask("RedBrickPath", Neighbour.East, 10);
            TileResources.SetBitmapCellForMask("RedBrickPath", Neighbour.East | Neighbour.North, 2);
            TileResources.SetBitmapCellForMask("RedBrickPath", Neighbour.South, 1);
            TileResources.SetBitmapCellForMask("RedBrickPath", Neighbour.South | Neighbour.North, 1);
            TileResources.SetBitmapCellForMask("RedBrickPath", Neighbour.South | Neighbour.East, 6);
            TileResources.SetBitmapCellForMask("RedBrickPath", Neighbour.South | Neighbour.East | Neighbour.North, 4);
            TileResources.SetBitmapCellForMask("RedBrickPath", Neighbour.West, 10);
            TileResources.SetBitmapCellForMask("RedBrickPath", Neighbour.West | Neighbour.North, 3);
            TileResources.SetBitmapCellForMask("RedBrickPath", Neighbour.West | Neighbour.East, 10);
            TileResources.SetBitmapCellForMask("RedBrickPath", Neighbour.West | Neighbour.East | Neighbour.North, 9);
            TileResources.SetBitmapCellForMask("RedBrickPath", Neighbour.West | Neighbour.South, 7);
            TileResources.SetBitmapCellForMask("RedBrickPath", Neighbour.West | Neighbour.South | Neighbour.North, 5);
            TileResources.SetBitmapCellForMask("RedBrickPath", Neighbour.West | Neighbour.South | Neighbour.East, 8);
            TileResources.SetBitmapCellForMask("RedBrickPath", Neighbour.West | Neighbour.South | Neighbour.East | Neighbour.North, 0);

            TileResources.SetBitmapCellForMask("CobblePath", Neighbour.None, 0);
            TileResources.SetBitmapCellForMask("CobblePath", Neighbour.North, 4);
            TileResources.SetBitmapCellForMask("CobblePath", Neighbour.East, 2);
            TileResources.SetBitmapCellForMask("CobblePath", Neighbour.East | Neighbour.North, 7);
            TileResources.SetBitmapCellForMask("CobblePath", Neighbour.South, 1);
            TileResources.SetBitmapCellForMask("CobblePath", Neighbour.South | Neighbour.North, 14);
            TileResources.SetBitmapCellForMask("CobblePath", Neighbour.South | Neighbour.East, 6);
            TileResources.SetBitmapCellForMask("CobblePath", Neighbour.South | Neighbour.East | Neighbour.North, 10);
            TileResources.SetBitmapCellForMask("CobblePath", Neighbour.West, 3);
            TileResources.SetBitmapCellForMask("CobblePath", Neighbour.West | Neighbour.North, 8);
            TileResources.SetBitmapCellForMask("CobblePath", Neighbour.West | Neighbour.East, 13);
            TileResources.SetBitmapCellForMask("CobblePath", Neighbour.West | Neighbour.East | Neighbour.North, 11);
            TileResources.SetBitmapCellForMask("CobblePath", Neighbour.West | Neighbour.South, 5);
            TileResources.SetBitmapCellForMask("CobblePath", Neighbour.West | Neighbour.South | Neighbour.North, 12);
            TileResources.SetBitmapCellForMask("CobblePath", Neighbour.West | Neighbour.South | Neighbour.East, 9);
            TileResources.SetBitmapCellForMask("CobblePath", Neighbour.West | Neighbour.South | Neighbour.East | Neighbour.North, 15);

            TileResources.SetBitmapCellForMask("StonePath", Neighbour.None, 2);
            TileResources.SetBitmapCellForMask("StonePath", Neighbour.North, 4);
            TileResources.SetBitmapCellForMask("StonePath", Neighbour.East, 0);
            TileResources.SetBitmapCellForMask("StonePath", Neighbour.East | Neighbour.North, 8);
            TileResources.SetBitmapCellForMask("StonePath", Neighbour.South, 3);
            TileResources.SetBitmapCellForMask("StonePath", Neighbour.South | Neighbour.North, 7);
            TileResources.SetBitmapCellForMask("StonePath", Neighbour.South | Neighbour.East, 5);
            TileResources.SetBitmapCellForMask("StonePath", Neighbour.South | Neighbour.East | Neighbour.North, 11);
            TileResources.SetBitmapCellForMask("StonePath", Neighbour.West, 1);
            TileResources.SetBitmapCellForMask("StonePath", Neighbour.West | Neighbour.North, 9);
            TileResources.SetBitmapCellForMask("StonePath", Neighbour.West | Neighbour.East, 2);
            TileResources.SetBitmapCellForMask("StonePath", Neighbour.West | Neighbour.East | Neighbour.North, 12);
            TileResources.SetBitmapCellForMask("StonePath", Neighbour.West | Neighbour.South, 6);
            TileResources.SetBitmapCellForMask("StonePath", Neighbour.West | Neighbour.South | Neighbour.North, 10);
            TileResources.SetBitmapCellForMask("StonePath", Neighbour.West | Neighbour.South | Neighbour.East, 13);
            TileResources.SetBitmapCellForMask("StonePath", Neighbour.West | Neighbour.South | Neighbour.East | Neighbour.North, 14);

            TileResources.SetBitmapCellForMask("Water", Neighbour.None, 0);
            TileResources.SetBitmapCellForMask("Water", Neighbour.North, 1);
            TileResources.SetBitmapCellForMask("Water", Neighbour.East, 2);
            TileResources.SetBitmapCellForMask("Water", Neighbour.East | Neighbour.North, 8);
            TileResources.SetBitmapCellForMask("Water", Neighbour.South, 3);
            TileResources.SetBitmapCellForMask("Water", Neighbour.South | Neighbour.North, 14);
            TileResources.SetBitmapCellForMask("Water", Neighbour.South | Neighbour.East, 5);
            TileResources.SetBitmapCellForMask("Water", Neighbour.South | Neighbour.East | Neighbour.North, 12);
            TileResources.SetBitmapCellForMask("Water", Neighbour.West, 4);
            TileResources.SetBitmapCellForMask("Water", Neighbour.West | Neighbour.North, 7);
            TileResources.SetBitmapCellForMask("Water", Neighbour.West | Neighbour.East, 13);
            TileResources.SetBitmapCellForMask("Water", Neighbour.West | Neighbour.East | Neighbour.North, 11);
            TileResources.SetBitmapCellForMask("Water", Neighbour.West | Neighbour.South, 6);
            TileResources.SetBitmapCellForMask("Water", Neighbour.West | Neighbour.South | Neighbour.North, 10);
            TileResources.SetBitmapCellForMask("Water", Neighbour.West | Neighbour.South | Neighbour.East, 9);
            TileResources.SetBitmapCellForMask("Water", Neighbour.West | Neighbour.South | Neighbour.East | Neighbour.North, 15);
        }
    }
}
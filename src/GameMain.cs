using System;
using SwinGameSDK;
using TileStyleSDK;

namespace MyGame
{
    public class GameMain
    {
        public static void Main()
        {
            SwinGame.LoadResourceBundleNamed("GameResources", "FFImages.txt", false);

            TileStyle.Init();
            GameResources.LoadResources();

            //Open the game window
            SwinGame.OpenGraphicsWindow("GameMain", 1152, 648);

            //HACK: Added escape key to exit the game
            while(false == SwinGame.WindowCloseRequested() && SwinGame.KeyDown(KeyCode.EscapeKey) == false)
            {
                SwinGame.ProcessEvents();

                SwinGame.ClearScreen(Color.White);

                TileStyle.HandleInput();
                Renderer.Render();

                SwinGame.DrawFramerate(950, 10);
                SwinGame.RefreshScreen(60);

                if (SwinGame.KeyDown(KeyCode.LeftKey))
                    Renderer.MoveWorldBy(-1, 0);

                if (SwinGame.KeyDown(KeyCode.RightKey))
                    Renderer.MoveWorldBy(1, 0);

                if (SwinGame.KeyDown(KeyCode.UpKey))
                    Renderer.MoveWorldBy(0, -1);

                if (SwinGame.KeyDown(KeyCode.DownKey))
                    Renderer.MoveWorldBy(0, 1);
            }
        }
    }
}
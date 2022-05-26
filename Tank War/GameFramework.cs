using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank_War
{
    enum GameState
    {
        RUNNING,
        OVER
    };
    class GameFramework
    {
        public static Graphics g;
        private static GameState gameState = GameState.RUNNING;

        public static void Start()
        {
            GameObjectManager.CreateMap();
            GameObjectManager.CreatePlayer(8, 24);
            SoundManager.InitSoundPlayer();
            SoundManager.Play(SoundState.START);
        }

        public static void Update()
        {         
            CheckGameState();
        }

        public static void KeyDown(KeyEventArgs args)
        {
            GameObjectManager.KeyDown(args);
        }
        public static void KeyUp(KeyEventArgs args)
        {
            GameObjectManager.KeyUp(args);
        }
        private static void CheckGameState()
        {
            if (gameState == GameState.RUNNING)
            {
                GameObjectManager.Update();
            }
            if (gameState == GameState.OVER)
            {
                GameOver();
            }
        }

        private static void GameOver()
        {
            int x = 390 / 2 - Properties.Resources.GameOver.Width / 2;
            int y = 390 / 2 - Properties.Resources.GameOver.Height / 2;
            g.DrawImage(Properties.Resources.GameOver, new Point(x, y));
        }

        public static void ChangeToGameOver()
        {
            gameState = GameState.OVER;
        }
    }
}

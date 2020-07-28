using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDown
{
    class Game
    {
        public Game()
        {

        }

        public static Game Instance;

        public ConsoleKeyInfo info;
        public bool isRunning;
        public char[,] map;
        public Vector2 mapSize;
        private Player player;
        private Random random;
        private Renderer renderer;

        public void ClearDialogue() {}

        public char GetRandomGroundChar() {
            return 'f';
        }

        private void Generate()
        {

        }
        private bool IsGameOver()
        {
            return false;
        }
        private void PlaceSpecialTile(int y, int x)
        {

        }
        private void ProcessAndRender()
        {

        }
    }
}

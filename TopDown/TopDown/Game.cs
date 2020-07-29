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
            Instance = this;
            IsRunning = true;

            Generate();
            ProcessAndRender();
        }

        public static Game Instance;
        public static Vector2 MapSize = new Vector2(60, 30);

        public ConsoleKeyInfo info;
        public bool IsRunning;
        public char[,] map = new char[MapSize.x, MapSize.y];

        private Player player = new Player(new Vector2(10, 5));
        private Random random = new Random();
        private Renderer renderer = new Renderer();

        public void ClearDialogue() {
            int linesToClear = 6;
            string output = "";
            for (int i = 0; i < linesToClear; i++) {
                for (int j = 0; j < Console.WindowWidth; j++) {
                    output += " ";
                }
            }
            Console.Write(output);
        }

        public char GetRandomGroundChar() {
            string terrainChars = " \".,'`\'";
            int r = random.Next(30);
            char selectedChar = ' ';
            if (r < terrainChars.Length) {
                selectedChar = terrainChars[r];
            }
            return selectedChar;
        }

        private void Generate()
        {
            for (int y = 0; y < MapSize.y; y++) {
                for (int x = 0; x < MapSize.x; x++) {
                    if (x == 0 && y == 0) {
                        map[x, y] = 'A';
                    } else {
                        map[x, y] = GetRandomGroundChar();
                    }
                    PlaceSpecialTile(x, y);
                }
            }
            renderer.Move();
            renderer.Render();
        }

        private bool IsGameOver()
        {
            if (player.hp < 1) {
                IsRunning = false;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou have died. Press ENTER to play again.");
                ConsoleKey key = ConsoleKey.D0;
                while (key != ConsoleKey.Enter) {
                    key = Console.ReadKey(true).Key;
                }
                return true;
            }
            return false;
        }

        private void PlaceSpecialTile(int x, int y)
        {
            if (random.Next(100) > 1) return;
            /* E = Enemy
             * I = Item
             * R = Rock
             * C = Cactus
             */
            int r = random.Next(10);
            map[x, y] = '▲';
            if (r > 3) map[x, y] = '♣';
            if (r > 7) map[x, y] = '§';
            if (r > 8) map[x, y] = 'Φ';
        }

        private void ProcessAndRender()
        {
            while (IsRunning) {
                player.Log();
                IsRunning = !IsGameOver();
                if (!IsRunning) {
                    return;
                }
                ClearDialogue();
                info = Console.ReadKey(true);
                player.Act();
                renderer.Move();
                renderer.Render();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDown {
    class Renderer {

        public Renderer() {
            Console.WindowHeight = screenSize.y + 10;
            Console.WindowWidth = screenSize.x + 5;
        }

        private static readonly Vector2 screenSize = new Vector2(41, 21);

        private char[,] screen = new char[screenSize.x, screenSize.y];
        private Vector2 screenPosition = Vector2.Zero;

        public void Render() {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            RenderVerticalBoundary();
            for (int y = 0; y < screenSize.y; y++) {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n||");
                for (int x = 0; x < screenSize.x; x++) {
                    if ((x + screenPosition.x) < Game.MapSize.x
                    && (y + screenPosition.y) < Game.MapSize.y
                    && (x + screenPosition.x) > -1
                    && (y + screenPosition.y) > -1
                    ) {
                        screen[x, y] = Game.Instance.map[x + screenPosition.x, y + screenPosition.y];
                        SetObjectColor(screen[x, y]);
                    } else {
                        screen[x, y] = '░';
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    if (Player.Position == (new Vector2(x, y) + screenPosition)) {
                        screen[x, y] = '♥';
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    Console.Write(screen[x, y]);
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("||");
            }
            Console.Write("\n");
            RenderVerticalBoundary();
            Console.Write("\n");
        }

        public void Move() {
            screenPosition = Player.Position - (screenSize / 2);
        }

        private void RenderVerticalBoundary() {
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i <= screenSize.x + 3; i++) {
                Console.Write("=");
            }
        }

        private void SetObjectColor(char _c) {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            switch (_c) {
                case '▲':
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case '♣':
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case '§':
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 'Φ':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    break;
            }

        }
    }
}
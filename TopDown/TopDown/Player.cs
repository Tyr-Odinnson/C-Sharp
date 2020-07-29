using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDown
{
    class Player
    {
        public Player(Vector2 _position)
        {
            Position = _position;
            maxHP = hp;
        }

        public static Vector2 Position;

        public int hp = 10;

        private string log = "";
        private int maxHP;
        private int potions = 1;

        public void Act()
        {
            log = "";

            if (Game.Instance.info.Key == ConsoleKey.I) {
                UsePotion();
            }

            Vector2 direction = GetInputDirection();

            if (IsObstructed(direction)) {
                Position += direction;
            }
        }

        private bool CollideCactus()
        {
            log += "\nYou walk into a cactus & lose 2hp lol.";
            hp -= 2;
            if (hp <0) {
                hp = 0;
            }
            return false;
        }

        private bool CollideEnemy()
        {
            log += "\n An enemy blocks your way.";
            return true;
        }

        private bool CollidePotion(ref char _c)
        {
            log += "\nYou find a potion.";
            potions++;
            _c = Game.Instance.GetRandomGroundChar();
            return false;
        }

        private bool CollideRock()
        {
            log += "\nYou've been rock-blocked";
            return true;
        }

        private Vector2 GetInputDirection() {
            if (Game.Instance.info.Key == ConsoleKey.W) return Vector2.Down;
            if (Game.Instance.info.Key == ConsoleKey.S) return Vector2.Up;
            if (Game.Instance.info.Key == ConsoleKey.A) return Vector2.Left;
            if (Game.Instance.info.Key == ConsoleKey.D) return Vector2.Right;

            return Vector2.Zero;
        }

        private bool HandleObstruction(ref char _c)
        {
            switch (_c) {
                case '▲':
                    return CollideRock();
                case '♣':
                    return CollideCactus();
                case '§':
                    return CollideEnemy();
                case 'Φ':
                    return CollidePotion(ref _c);
                default:
                    return true;
            }
            
        }

        private bool IsObstructed(Vector2 _direction) {
            Vector2 nextPosition = Position + _direction;

            if (nextPosition.x > Game.Instance.map.GetLength(0) - 1
            || nextPosition.y > Game.Instance.map.GetLength(1) - 1
            || nextPosition.x < 0
            || nextPosition.y < 0
            ) {
                log += "\nYou shall not pass!";
                return true;
            }

            string terrainChars = " \".,'`\'";
            bool isObjectTile = false;
            foreach (char c in terrainChars) {
                if (!terrainChars.Contains(Game.Instance.map[nextPosition.x, nextPosition.y].ToString())) {
                    isObjectTile = true;
                }
            }

            if (isObjectTile) {
                isObjectTile = HandleObstruction(ref Game.Instance.map[nextPosition.x, nextPosition.y]);
            }
            return isObjectTile;
        }


        public void Log() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("hpL " + hp + "\t\tpotions: " + potions);
            Console.ResetColor();
            Console.Write("\nWASD = Move\tI=Use Potion");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\npos: ()" + Position.ToString());

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(log);
        }

        private void UsePotion()
        {
            if (potions > 0) {
                if (hp < maxHP) {
                    log = "\nYou used a potion, recovering 5hp.";
                    potions--;
                    hp += 5;

                    if (hp > maxHP) {
                        hp = maxHP;
                    }
                } else {
                    log = "\nYou don't have any potions";
                }
            }
        }

    }
}

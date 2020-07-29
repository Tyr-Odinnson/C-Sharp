using System;
using System.Text;

namespace TopDown {
    class Program {
        static void Main(string[] args) {
            // Console.ReadKey(true);

            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            while (true)
            {
                new Game();
            }
        }
    }
}

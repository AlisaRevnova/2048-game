using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2048
{
    class Game
    {
        public static Field game = new Field();
        public static FileStream file = new FileStream("Game_2048.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

        static void Main(string[] args)
        {
            bool CheckForEnd = true;
            byte[] arr = new byte[200];
            file.Read(arr, 0, 200);
            if(!arr.All(x=>x==0))
            {
                Console.WriteLine("Previous game\t\tNew game");
                var t = Console.ReadKey().Key;
                if(t == ConsoleKey.LeftArrow)
                {
                    game.Reset();
                    Serialization.Deserialize();
                }
            }
            Console.Clear();
            game.ShowField();
            var r = Console.ReadKey().Key;
            while (r != ConsoleKey.Escape)
            {
                Console.Clear();
                switch (r)
                {
                    case ConsoleKey.RightArrow:
                        CheckForEnd = game.Right();
                        break;
                    case ConsoleKey.LeftArrow:
                        CheckForEnd = game.Left();
                        break;
                    case ConsoleKey.UpArrow:
                        CheckForEnd = game.Up();
                        break;
                    case ConsoleKey.DownArrow:
                        CheckForEnd = game.Down();
                        break;
                }
                if(game.Congratulations() == true)
                {
                    Console.WriteLine("You win! Your score: " + game.UserCount);
                    Console.ReadKey();
                    file.Close();
                    return;
                }
                if (CheckForEnd == true)
                {
                    Console.WriteLine("Game over.You lose");
                    Console.ReadKey();
                    file.Close();
                    return;
                }
                game.ShowField();
                r = Console.ReadKey().Key;
                if(r == ConsoleKey.Escape)
                {
                    Console.WriteLine("Are you sure?\nYes\tNo");
                    var n = Console.ReadKey().Key;
                    if(n == ConsoleKey.LeftArrow)
                    {
                        Console.WriteLine("Game over. Your score: " + game.UserCount);
                        Serialization.Serialize();
                        Console.ReadKey();
                        file.Close();
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Game proceeds");
                        game.ShowField();
                        r = Console.ReadKey().Key;
                    }
                }
                //Serialization.Serialize();
            }
            file.Close();
        }
    }
}

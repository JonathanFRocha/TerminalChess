using System;
using Board;

namespace terminal_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Position position;

            position = new Position(3, 4);

            Console.WriteLine(position);
        }
    }
}

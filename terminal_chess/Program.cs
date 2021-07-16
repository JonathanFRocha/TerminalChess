using System;
using board;
using Chess;

namespace terminal_chess
{
    class Program
    {
        static void Main(string[] args)
        {


            try
            {
                ChessMatch match = new ChessMatch();


                Screen.ShowBoard(match.Board);
            }
            catch (BoardException e)
            {
                Console.WriteLine("Board Exception: " + e.Message);
            }

            Console.ReadLine();
        }
    }
}

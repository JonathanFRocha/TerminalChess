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
                while (!match.Finished)
                {
                    Console.Clear();
                    Screen.ShowBoard(match.Board);

                    Console.WriteLine();
                    Console.WriteLine("Turn: " + match.Turn);
                    Console.WriteLine("Awaiting player: " + match.CurrentPlayer);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();

                    bool[,] possiblePositions = match.Board.piece(origin).possibleMoves();
                    Console.Clear();
                    Screen.ShowBoard(match.Board, possiblePositions);

                    Console.Write("Destiny: ");
                    Position destiny = Screen.ReadChessPosition().ToPosition();

                    match.executeTurn(origin, destiny);
                }

               
            }
            catch (BoardException e)
            {
                Console.WriteLine("Board Exception: " + e.Message);
            }

            Console.ReadLine();
        }
    }
}

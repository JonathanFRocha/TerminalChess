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
                    Console.Write("Origem: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();

                    bool[,] possiblePositions = match.Board.piece(origin).possibleMoves();
                    Console.Clear();
                    Screen.ShowBoard(match.Board, possiblePositions);

                    Console.Write("Destino: ");
                    Position destiny = Screen.ReadChessPosition().ToPosition();

                    match.executeMove(origin, destiny);
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

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
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);
                       

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidateOriginPosition(origin);


                        bool[,] possiblePositions = match.Board.piece(origin).possibleMoves();
                        Console.Clear();
                        Screen.ShowBoard(match.Board, possiblePositions);

                        Console.Write("Destiny: ");
                        Position destiny = Screen.ReadChessPosition().ToPosition();
                        match.ValidateDesinyPosition(origin, destiny);

                        match.ExecuteTurn(origin, destiny);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine("Board Exception: " + e.Message);
                        Console.ReadLine();
                    }
                  
                }
                Console.Clear();
                Screen.PrintMatch(match);


            }
            catch (BoardException e)
            {
                Console.WriteLine("Board Exception: " + e.Message);
            }

            Console.ReadLine();
        }
    }
}

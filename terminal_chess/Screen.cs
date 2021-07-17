using System;
using System.Collections.Generic;
using board;
using Chess;

namespace terminal_chess
{
    class Screen
    {

        public static void PrintMatch(ChessMatch match)
        {
            ShowBoard(match.Board);
            Console.WriteLine();
            PrintCapturedPieces(match);

            Console.WriteLine();
            Console.WriteLine("Turn: " + match.Turn);
            Console.WriteLine("Awaiting player: " + match.CurrentPlayer);
            if (match.Check)
            {
                Console.WriteLine("CHECK!");
            }
        }

        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured Pieces:");
            Console.Write("Whites: ");
            PrintCollection(match.CapturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Blacks: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintCollection(match.CapturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void PrintCollection(HashSet<Piece> collection) 
        {
            Console.Write("[");
            foreach(Piece x in collection)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
        public static void ShowBoard(Board brd)
        {
            for (int i = 0; i < brd.Rows; i += 1)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < brd.Columns; j += 1)
                {
                    PrintPiece(brd.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ShowBoard(Board brd, bool[,] possiblePositions)
        {
            ConsoleColor originalBg = Console.BackgroundColor;
            ConsoleColor bgChanged = ConsoleColor.DarkGray;
            for (int i = 0; i < brd.Rows; i += 1)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < brd.Columns; j += 1)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = bgChanged;
                    }else
                    {
                        Console.BackgroundColor = originalBg;
                    }
                    PrintPiece(brd.piece(i, j));
                    Console.BackgroundColor = originalBg;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBg;
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
        public static PositionChess ReadChessPosition()
        {
            string s = Console.ReadLine();
            char ch = s[0];
            int column = int.Parse(s[1] + "");
            return new PositionChess(ch, column);
        }
    }
}

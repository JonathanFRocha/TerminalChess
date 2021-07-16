using System;
using board;

namespace terminal_chess
{
    class Screen
    {
        public static void ShowBoard(Board brd)
        {
            for (int i = 0; i < brd.Rows; i += 1)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < brd.Columns; j += 1)
                    if (brd.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        PrintPiece(brd.piece(i, j));
                        Console.Write(" ");
                    }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void PrintPiece (Piece piece)
        {
            if(piece.color == Color.White)
            {
                Console.Write(piece);
            }else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}

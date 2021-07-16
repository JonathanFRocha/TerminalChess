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
                for (int j = 0; j < brd.Columns; j += 1)
                    if (brd.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(brd.piece(i, j) + " ");
                    }
                Console.WriteLine();
            }
        }
    }
}

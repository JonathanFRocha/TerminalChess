﻿using System;
using board;
using Chess;

namespace terminal_chess
{
    class Program
    {
        static void Main(string[] args)
        {

            PositionChess pos = new PositionChess('a', 1);
            Console.WriteLine(pos);

            //try
            //{
            //    Board board = new Board(8, 8);

            //    board.InsertPiece(new Tower(board, Color.Black), new Position(0, 0));
            //    board.InsertPiece(new King(board, Color.Black), new Position(0, 6));
            //    board.InsertPiece(new Tower(board, Color.Black), new Position(1, 3));
            //    board.InsertPiece(new King(board, Color.Black), new Position(2, 4));

            //    Screen.ShowBoard(board);
            //}
            //catch(BoardException e)
            //{
            //    Console.WriteLine("Board Exception: " + e.Message);
            //}

            Console.ReadLine();
        }
    }
}

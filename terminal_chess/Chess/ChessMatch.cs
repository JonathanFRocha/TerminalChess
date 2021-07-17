﻿using System;
using board;

namespace Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public bool Finished { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8,8);
            Turn = 0;
            CurrentPlayer = Color.White;
            Finished = false;
            insertPieces();
        }

        public void executeMove(Position origin, Position destiny)
        {
            Piece p = Board.RetrivePiece(origin);
            p.incrementMovesQty();
            Piece capturedPiece = Board.RetrivePiece(destiny);
            Board.InsertPiece(p, destiny);

        }

        public void executeTurn (Position origin, Position destiny)
        {
            executeMove(origin, destiny);
            Turn += 1;
            ChangePlayer();
        }

        private void ChangePlayer()
        {
            if(CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }else
            {
                CurrentPlayer = Color.White;
            }
        }

        private void insertPieces()
        {
            Board.InsertPiece(new Tower(Board, Color.White), new PositionChess('c', 1).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new PositionChess('c', 2).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new PositionChess('d', 2).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new PositionChess('e', 2).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new PositionChess('e', 1).ToPosition());

            Board.InsertPiece(new King(Board, Color.White), new PositionChess('d', 1).ToPosition());


            Board.InsertPiece(new Tower(Board, Color.Black), new PositionChess('c', 7).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new PositionChess('c', 8).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new PositionChess('d', 7).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new PositionChess('e', 7).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new PositionChess('e', 8).ToPosition());

            Board.InsertPiece(new King(Board, Color.Black), new PositionChess('d', 8).ToPosition());
        }
    }
}
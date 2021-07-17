using System;
using System.Collections.Generic;
using board;

namespace Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public bool Finished { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public HashSet<Piece> Pieces;
        public HashSet<Piece> Captured;

        public ChessMatch()
        {
            Board = new Board(8,8);
            Turn = 0;
            CurrentPlayer = Color.White;
            Finished = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            insertPieces();
        }

        public void executeMove(Position origin, Position destiny)
        {
            Piece p = Board.RetrivePiece(origin);
            p.incrementMovesQty();
            Piece capturedPiece = Board.RetrivePiece(destiny);
            Board.InsertPiece(p, destiny);
            if(capturedPiece != null)
            {
                Captured.Add(capturedPiece);
            }
        }

        public void executeTurn (Position origin, Position destiny)
        {
            executeMove(origin, destiny);
            Turn += 1;
            ChangePlayer();
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in Captured)
            {
                if(x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Pieces)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        public void ValidateOriginPosition(Position pos)
        {
            if(Board.piece(pos) == null)
            {
                throw new BoardException("Dont exists a piece in that position!");
            }
            if(CurrentPlayer != Board.piece(pos).color)
            {
                throw new BoardException("This piece is not from the current player color");
            }
            if (!Board.piece(pos).HasAtLeastOneMove())
            {
                throw new BoardException("This piece cant move");
            }
        }

        public void ValidateDesinyPosition (Position origin, Position destiny)
        {
            if (!Board.piece(origin).CanMoveTo(destiny))
            {
                throw new BoardException("Invalid Destination position!");
            }
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

        public void insertNewPiece(char column, int row, Piece piece)
        {
            Board.InsertPiece(piece, new PositionChess(column, row).ToPosition());
            Pieces.Add(piece);
        }

        private void insertPieces()
        {
            insertNewPiece('c', 1, new Tower(Board, Color.White));
            insertNewPiece('c', 2, new Tower(Board, Color.White));
            insertNewPiece('d', 2, new Tower(Board, Color.White));
            insertNewPiece('e', 2, new Tower(Board, Color.White));
            insertNewPiece('e', 1, new Tower(Board, Color.White));
            insertNewPiece('d', 1, new King(Board, Color.White));

            insertNewPiece('c', 7, new Tower(Board, Color.Black));
            insertNewPiece('c', 8, new Tower(Board, Color.Black));
            insertNewPiece('d', 7, new Tower(Board, Color.Black));
            insertNewPiece('e', 7, new Tower(Board, Color.Black));
            insertNewPiece('e', 8, new Tower(Board, Color.Black));
            insertNewPiece('d', 8, new King(Board, Color.Black));


        }
    }
}

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
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 0;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            Pieces = new HashSet<Piece>();
            VulnerableEnPassant = null;
            Captured = new HashSet<Piece>();
            insertPieces();
        }

        public Piece ExecuteMove(Position origin, Position destiny)
        {
            Piece p = Board.RetrivePiece(origin);
            p.incrementMovesQty();
            Piece capturedPiece = Board.RetrivePiece(destiny);
            Board.InsertPiece(p, destiny);
            if (capturedPiece != null)
            {
                Captured.Add(capturedPiece);
            }

            // king special move

            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Row, origin.Column + 3);
                Position destinyT = new Position(origin.Row, origin.Column + 1);
                Piece T = Board.RetrivePiece(originT);
                T.incrementMovesQty();
                Board.InsertPiece(T, destinyT);
            }
            // king special move 2
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Row, origin.Column - 4);
                Position destinyT = new Position(origin.Row, origin.Column -1);
                Piece T = Board.RetrivePiece(originT);
                T.incrementMovesQty();
                Board.InsertPiece(T, destinyT);
            }

            return capturedPiece;
        }

        public void ExecuteTurn(Position origin, Position destiny)
        {
            Piece CapturedPiece = ExecuteMove(origin, destiny);
            if (isInCheck(CurrentPlayer))
            {
                UndoMove(origin, destiny, CapturedPiece);
                throw new BoardException("You cant place yourself in Check!");
            }

            if (isInCheck(Enemy(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (TestCheckMate(Enemy(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Turn += 1;
                ChangePlayer();
            }

            Piece p = Board.piece(destiny);
            
            //#enpassant
            if(p is Pawn && (destiny.Row == origin.Row -2 || destiny.Row == origin.Row + 2))
            {
                VulnerableEnPassant = p;
            }
        }

        public void UndoMove(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = Board.RetrivePiece(destiny);
            p.DecrementQtyMoves();
            if (capturedPiece != null)
            {
                Board.InsertPiece(capturedPiece, destiny);
                Captured.Remove(capturedPiece);
            }
            Board.InsertPiece(p, origin);

            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Row, origin.Column + 3);
                Position destinyT = new Position(origin.Row, origin.Column + 1);
                Piece T = Board.RetrivePiece(destinyT);
                T.DecrementQtyMoves();
                Board.InsertPiece(T, originT);
            }

            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Row, origin.Column - 4);
                Position destinyT = new Position(origin.Row, origin.Column - 1);
                Piece T = Board.RetrivePiece(destinyT);
                T.DecrementQtyMoves();
                Board.InsertPiece(T, originT);
            }
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Captured)
            {
                if (x.color == color)
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

        private Color Enemy(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece King(Color color)
        {
            foreach (Piece x in PiecesInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool isInCheck(Color color)
        {
            Piece K = King(color);
            if (K == null)
            {
                throw new BoardException("No King in game!");
            }
            foreach (Piece piece in PiecesInGame(Enemy(color)))
            {
                bool[,] mat = piece.possibleMoves();
                if (mat[K.position.Row, K.position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TestCheckMate(Color color)
        {
            if (!isInCheck(color))
            {
                return false;
            }
            foreach (Piece x in PiecesInGame(color))
            {
                bool[,] mat = x.possibleMoves();
                for (int i = 0; i < Board.Rows; i += 1)
                {
                    for (int j = 0; j < Board.Rows; j += 1)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = ExecuteMove(origin, destiny);
                            bool testCheck = isInCheck(color);
                            UndoMove(origin, destiny, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (Board.piece(pos) == null)
            {
                throw new BoardException("Dont exists a piece in that position!");
            }
            if (CurrentPlayer != Board.piece(pos).color)
            {
                throw new BoardException("This piece is not from the current player color");
            }
            if (!Board.piece(pos).HasAtLeastOneMove())
            {
                throw new BoardException("This piece cant move");
            }
        }

        public void ValidateDesinyPosition(Position origin, Position destiny)
        {
            if (!Board.piece(origin).PossibleMove(destiny))
            {
                throw new BoardException("Invalid Destination position!");
            }
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
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
            insertNewPiece('a', 1, new Tower(Board, Color.White));
            insertNewPiece('b', 1, new Knight(Board, Color.White));
            insertNewPiece('c', 1, new Bishop(Board, Color.White));
            insertNewPiece('d', 1, new Queen(Board, Color.White));
            insertNewPiece('e', 1, new King(Board, Color.White, this));
            insertNewPiece('f', 1, new Bishop(Board, Color.White));
            insertNewPiece('g', 1, new Knight(Board, Color.White));
            insertNewPiece('h', 1, new Tower(Board, Color.White));

            insertNewPiece('a', 2, new Pawn(Board, Color.White, this));
            insertNewPiece('b', 2, new Pawn(Board, Color.White, this));
            insertNewPiece('c', 2, new Pawn(Board, Color.White, this));
            insertNewPiece('d', 2, new Pawn(Board, Color.White, this));
            insertNewPiece('e', 2, new Pawn(Board, Color.White, this));
            insertNewPiece('f', 2, new Pawn(Board, Color.White, this));
            insertNewPiece('g', 2, new Pawn(Board, Color.White, this));
            insertNewPiece('h', 2, new Pawn(Board, Color.White, this));

            insertNewPiece('a', 8, new Tower(Board, Color.Black));
            insertNewPiece('b', 8, new Knight(Board, Color.Black));
            insertNewPiece('c', 8, new Bishop(Board, Color.Black));
            insertNewPiece('d', 8, new Queen(Board, Color.Black));
            insertNewPiece('e', 8, new King(Board, Color.Black, this));
            insertNewPiece('f', 8, new Bishop(Board, Color.Black));
            insertNewPiece('g', 8, new Knight(Board, Color.Black));
            insertNewPiece('h', 8, new Tower(Board, Color.Black));

            insertNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            insertNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            insertNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            insertNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            insertNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            insertNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            insertNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            insertNewPiece('h', 7, new Pawn(Board, Color.Black, this));


        }
    }
}

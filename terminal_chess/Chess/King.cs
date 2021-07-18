using System;
using board;

namespace Chess
{
    class King:Piece
    {

        private ChessMatch Match;

        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            Match = match;
        }
        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        private bool TestTowerSpecialRoque(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p is Tower && p.color == color && p.QtyMoves == 0;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.Rows, board.Columns];
            Position pos = new Position(0, 0);

            pos.defineValues(position.Row - 1, position.Column);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }


            pos.defineValues(position.Row - 1, position.Column + 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }


            pos.defineValues(position.Row, position.Column +1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }


            pos.defineValues(position.Row + 1, position.Column +1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }


            pos.defineValues(position.Row + 1, position.Column);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }


            pos.defineValues(position.Row + 1, position.Column - 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }


            pos.defineValues(position.Row, position.Column -1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }


            pos.defineValues(position.Row - 1, position.Column-1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //# special 1

            if(QtyMoves == 0 && !Match.Check)
            {
                Position posT1 = new Position(position.Row, position.Column + 3);
                if (TestTowerSpecialRoque(posT1))
                {
                    Position p1 = new Position(position.Row, position.Column + 1);
                    Position p2 = new Position(position.Row, position.Column + 2);
                    if (board.piece(p1) == null && board.piece(p2) == null)
                    {
                        mat[position.Row, position.Column + 2] = true;
                    }
                }
            }

            //# special 2

            if (QtyMoves == 0 && !Match.Check)
            {
                Position posT2 = new Position(position.Row, position.Column - 4);
                if (TestTowerSpecialRoque(posT2))
                {
                    Position p1 = new Position(position.Row, position.Column - 1);
                    Position p2 = new Position(position.Row, position.Column - 2);
                    Position p3 = new Position(position.Row, position.Column - 3);
                    if (board.piece(p1) == null && board.piece(p2) == null && board.piece(p3) == null)
                    {
                        mat[position.Row, position.Column - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}

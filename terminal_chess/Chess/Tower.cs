using System;
using board;

namespace Chess
{
    class Tower : Piece
    {
        public Tower(Board board, Color color) : base(board, color)
        {

        }
        public override string ToString()
        {
            return "T";
        }

        private bool CanMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.Rows, board.Columns];

            Position pos = new Position(0, 0);

            pos.defineValues(position.Row - 1, position.Column);
            // above
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.Row = pos.Row - 1;
            }
            //below
            pos.defineValues(position.Row + 1, position.Column);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.Row = pos.Row + 1;
            }

            //Right

            pos.defineValues(position.Row, position.Column + 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.Column = pos.Column + 1;
            }

            //Left

            pos.defineValues(position.Row, position.Column - 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.Column = pos.Column - 1;
            }

            return mat;
        }
    }
}

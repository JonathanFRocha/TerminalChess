using System;
using board;

namespace Chess
{
    class Knight:Piece
    {

        public Knight(Board board, Color color) : base(board, color)
        {

        }
        public override string ToString()
        {
            return "C";
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

            pos.defineValues(position.Row - 1, position.Column - 2);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            pos.defineValues(position.Row - 2, position.Column - 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            pos.defineValues(position.Row - 2, position.Column + 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            pos.defineValues(position.Row - 1, position.Column + 2);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            pos.defineValues(position.Row + 1, position.Column + 2);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            pos.defineValues(position.Row + 2, position.Column + 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            pos.defineValues(position.Row + 2, position.Column - 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            pos.defineValues(position.Row + 1, position.Column - 2);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            return mat;
        }
    }
}

using System;
using board;

namespace Chess
{
    class Pawn:Piece
    {
        private ChessMatch Match;

        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            Match = match;
        }
        public override string ToString()
        {
            return "P";
        }
        private bool EnemyExists(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p.color != color;
        }

        private bool Free(Position pos)
        {
            return board.piece(pos) == null;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.Rows, board.Columns];
            Position pos = new Position(0, 0);

            if (color == Color.White)
            {
                pos.defineValues(position.Row - 1, position.Column);
                if (board.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.defineValues(position.Row - 2, position.Column);
                Position p2 = new Position(position.Row - 1, position.Column);
                if (board.ValidPosition(p2) && Free(p2) && board.ValidPosition(pos) && Free(pos) && QtyMoves == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.defineValues(position.Row - 1, position.Column - 1);
                if (board.ValidPosition(pos) && EnemyExists(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.defineValues(position.Row - 1, position.Column + 1);
                if (board.ValidPosition(pos) && EnemyExists(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                // #jogadaespecial en passant
                if (position.Row == 3)
                {
                    Position left = new Position(position.Row, position.Column - 1);
                    if (board.ValidPosition(left) && EnemyExists(left) && board.piece(left) == Match.VulnerableEnPassant)
                    {
                        mat[left.Row - 1, left.Column] = true;
                    }
                    Position direita = new Position(position.Row, position.Column + 1);
                    if (board.ValidPosition(direita) && EnemyExists(direita) && board.piece(direita) == Match.VulnerableEnPassant)
                    {
                        mat[direita.Row - 1, direita.Column] = true;
                    }
                }
            }
            else
            {
                pos.defineValues(position.Row + 1, position.Column);
                if (board.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.defineValues(position.Row + 2, position.Column);
                Position p2 = new Position(position.Row + 1, position.Column);
                if (board.ValidPosition(p2) && Free(p2) && board.ValidPosition(pos) && Free(pos) && QtyMoves == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.defineValues(position.Row + 1, position.Column - 1);
                if (board.ValidPosition(pos) && EnemyExists(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.defineValues(position.Row + 1, position.Column + 1);
                if (board.ValidPosition(pos) && EnemyExists(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                // #jogadaespecial en passant
                if (position.Row == 4)
                {
                    Position left = new Position(position.Row, position.Column - 1);
                    if (board.ValidPosition(left) && EnemyExists(left) && board.piece(left) == Match.VulnerableEnPassant)
                    {
                        mat[left.Row + 1, left.Column] = true;
                    }
                    Position direita = new Position(position.Row, position.Column + 1);
                    if (board.ValidPosition(direita) && EnemyExists(direita) && board.piece(direita) == Match.VulnerableEnPassant)
                    {
                        mat[direita.Row + 1, direita.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace board
{
    class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Piece[rows, columns];
        }

        public Piece piece(int row, int column)
        {
            return Pieces[row, column];
        }

        public Piece piece(Position pos)
        {
            return Pieces[pos.Row, pos.Column];
        }

        public void InsertPiece(Piece p, Position pos)
        {
            if (ThereIsAPiece(pos))
            {
                throw new BoardException("There is a piece in this position");
            }
            Pieces[pos.Row, pos.Column] = p;
            p.position = pos;
        }

        public Piece RetrivePiece (Position pos)
        {
            if (piece(pos) == null)
            {
                return null;
            }
            Piece aux = piece(pos);
            aux.position = null;
            Pieces[pos.Row, pos.Column] = null;
            return aux;
        }

        public bool ThereIsAPiece(Position pos)
        {
            ValidatePosition(pos);
            return piece(pos) != null;
        }

        public bool ValidPosition(Position pos)
        {
            if(pos.Row < 0 || pos.Row >=Rows || pos.Column < 0 || pos.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!ValidPosition(pos))
            {
                throw new BoardException("Invalid Position!");
            }
        }
    }
}

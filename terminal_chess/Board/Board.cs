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

        public void InsertPiece(Piece p, Position pos)
        {
            Pieces[pos.Row, pos.Column] = p;
            p.position = pos;
        }
    }
}

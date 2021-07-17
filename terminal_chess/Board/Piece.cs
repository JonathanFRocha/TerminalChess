namespace board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int QtyMoves { get; protected set; }
        public Board board { get; protected set; }

        public Piece (Board board, Color color)
        {
            this.position = null;
            this.board = board;
            this.color = color;
            QtyMoves = 0;
        }

        public void incrementMovesQty()
        {
            QtyMoves += 1;
        }

        public void DecrementQtyMoves()
        {
            QtyMoves -= 1;
        }

        public bool HasAtLeastOneMove()
        {
            bool[,] mat = possibleMoves();
            for(int i = 0; i<board.Rows; i += 1)
            {
                for(int j = 0; j< board.Columns; j+=1)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
               
            }
            return false;
        }

        public bool CanMoveTo(Position pos)
        {
            return possibleMoves()[pos.Row, pos.Column];
        }

        public abstract bool[,] possibleMoves();
    }
}

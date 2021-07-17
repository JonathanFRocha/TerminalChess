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

        public bool HasAtLeastOneMove()
        {
            bool[,] mat = possibleMoves();

        }

        public abstract bool[,] possibleMoves();
    }
}

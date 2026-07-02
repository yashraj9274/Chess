namespace ChessLogic
{
    public class Move
    {
        public Position FromPos { get; }
        public Position ToPos { get; }

        public Move(Position fromPos, Position toPos)
        {
            FromPos = fromPos;
            ToPos = toPos;
        }
        
        public void Execute(Board board)
        {
            Piece piece = board[FromPos];
            board[ToPos] = piece;
            board[FromPos] = null;
            piece.HasMoved = true;
        }
    }
}

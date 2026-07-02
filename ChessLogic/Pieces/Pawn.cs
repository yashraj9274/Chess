using System.Collections.Generic;

namespace ChessLogic.Pieces
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }

        private readonly Direction forward;

        public Pawn(Player color)
        {
            Color = color;
            if (color == Player.White)
            {
                forward = Direction.North;
            }
            else
            {
                forward = Direction.South;
            }
        }

        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public override IEnumerable<Move> GetPossibleMoves(Position from, Board board)
        {
            Position oneMove = from + forward;
            if (Board.IsInside(oneMove) && board.IsEmpty(oneMove))
            {
                yield return new Move(from, oneMove);

                if (!HasMoved)
                {
                    Position twoMoves = oneMove + forward;
                    if (Board.IsInside(twoMoves) && board.IsEmpty(twoMoves))
                    {
                        yield return new Move(from, twoMoves);
                    }
                }
            }

            Direction[] captureDirs = new Direction[] { new Direction(forward.RowDelta, -1), new Direction(forward.RowDelta, 1) };
            foreach (Direction dir in captureDirs)
            {
                Position capturePos = from + dir;
                if (Board.IsInside(capturePos) && !board.IsEmpty(capturePos) && board[capturePos].Color != Color)
                {
                    yield return new Move(from, capturePos);
                }
            }
        }
    }
}

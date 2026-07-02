using System.Collections.Generic;

namespace ChessLogic.Pieces
{
    public class Bishop : Piece
    {
        public override PieceType Type => PieceType.Bishop;
        public override Player Color { get; }

        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.NorthWest,
            Direction.NorthEast,
            Direction.SouthWest,
            Direction.SouthEast
        };

        public Bishop(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Bishop copy = new Bishop(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public override IEnumerable<Move> GetPossibleMoves(Position from, Board board)
        {
            foreach (Position pos in MovePositionsInDirs(from, board, dirs))
            {
                yield return new Move(from, pos);
            }
        }
    }
}

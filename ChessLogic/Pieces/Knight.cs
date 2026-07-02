using System.Collections.Generic;

namespace ChessLogic.Pieces
{
    public class Knight : Piece
    {
        public override PieceType Type => PieceType.Knight;
        public override Player Color { get; }

        public Knight(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Knight copy = new Knight(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private static IEnumerable<Position> PotentialToPositions(Position from)
        {
            yield return new Position(from.Row - 2, from.Col - 1);
            yield return new Position(from.Row - 2, from.Col + 1);
            yield return new Position(from.Row + 2, from.Col - 1);
            yield return new Position(from.Row + 2, from.Col + 1);
            yield return new Position(from.Row - 1, from.Col - 2);
            yield return new Position(from.Row - 1, from.Col + 2);
            yield return new Position(from.Row + 1, from.Col - 2);
            yield return new Position(from.Row + 1, from.Col + 2);
        }

        public override IEnumerable<Move> GetPossibleMoves(Position from, Board board)
        {
            foreach (Position pos in PotentialToPositions(from))
            {
                if (Board.IsInside(pos) && (board.IsEmpty(pos) || board[pos].Color != Color))
                {
                    yield return new Move(from, pos);
                }
            }
        }
    }
}

using ChessLogic.Pieces;

namespace ChessLogic
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8, 8];

        public Piece this[int row, int col]
        {
            get { return pieces[row, col]; }
            set { pieces[row, col] = value; }
        }

        public Piece this[Position pos]
        {
            get { return this[pos.Row, pos.Col]; }
            set { this[pos.Row, pos.Col] = value; }
        }

        public static Board Initial()
        {
            Board board = new Board();
            board.AddStartPieces();
            return board;
        }

        private void AddStartPieces()
        {
            this[0, 0] = new Rook(Player.Black);
            this[0, 1] = new Knight(Player.Black);
            this[0, 2] = new Bishop(Player.Black);
            this[0, 3] = new Queen(Player.Black);
            this[0, 4] = new King(Player.Black);
            this[0, 5] = new Bishop(Player.Black);
            this[0, 6] = new Knight(Player.Black);
            this[0, 7] = new Rook(Player.Black);

            this[7, 0] = new Rook(Player.White);
            this[7, 1] = new Knight(Player.White);
            this[7, 2] = new Bishop(Player.White);
            this[7, 3] = new Queen(Player.White);
            this[7, 4] = new King(Player.White);
            this[7, 5] = new Bishop(Player.White);
            this[7, 6] = new Knight(Player.White);
            this[7, 7] = new Rook(Player.White);

            for (int c = 0; c < 8; c++)
            {
                this[1, c] = new Pawn(Player.Black);
                this[6, c] = new Pawn(Player.White);
            }
        }

        public static bool IsInside(Position pos)
        {
            return pos.Row >= 0 && pos.Row < 8 && pos.Col >= 0 && pos.Col < 8;
        }

        public bool IsEmpty(Position pos)
        {
            return this[pos] == null;
        }

        public Board Copy()
        {
            Board copy = new Board();
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (pieces[r, c] != null)
                    {
                        copy[r, c] = pieces[r, c].Copy();
                    }
                }
            }
            return copy;
        }

        public System.Collections.Generic.IEnumerable<Position> PiecePositionsFor(Player player)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Position pos = new Position(r, c);
                    if (!IsEmpty(pos) && this[pos].Color == player)
                    {
                        yield return pos;
                    }
                }
            }
        }

        public Position GetKingPos(Player player)
        {
            foreach (Position pos in PiecePositionsFor(player))
            {
                if (this[pos].Type == PieceType.King)
                {
                    return pos;
                }
            }
            return null; // Should not happen in a valid game
        }

        public bool IsInCheck(Player player)
        {
            Position kingPos = GetKingPos(player);
            Player opponent = player.Opponent();

            foreach (Position pos in PiecePositionsFor(opponent))
            {
                // We use LINQ Any, so we need System.Linq if not included, but we can just loop
                foreach (Move move in this[pos].GetPossibleMoves(pos, this))
                {
                    if (move.ToPos == kingPos)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

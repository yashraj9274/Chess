using System.Collections.Generic;
using System.Linq;

namespace ChessLogic
{
    public class GameState
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; private set; }
        public Result Result { get; private set; } = null;

        public GameState(Player player, Board board)
        {
            CurrentPlayer = player;
            Board = board;
        }

        public IEnumerable<Move> LegalMovesForPiece(Position pos)
        {
            if (Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
            {
                yield break;
            }

            IEnumerable<Move> pseudoLegalMoves = Board[pos].GetPossibleMoves(pos, Board);
            foreach (Move move in pseudoLegalMoves)
            {
                Board boardCopy = Board.Copy();
                move.Execute(boardCopy);
                if (!boardCopy.IsInCheck(CurrentPlayer))
                {
                    yield return move;
                }
            }
        }

        public IEnumerable<Move> AllLegalMovesFor(Player player)
        {
            IEnumerable<Move> allMoves = Enumerable.Empty<Move>();
            foreach (Position pos in Board.PiecePositionsFor(player))
            {
                allMoves = allMoves.Concat(LegalMovesForPiece(pos));
            }
            return allMoves;
        }

        public void MakeMove(Move move)
        {
            move.Execute(Board);
            CurrentPlayer = CurrentPlayer.Opponent();
            CheckForGameOver();
        }

        private void CheckForGameOver()
        {
            if (!AllLegalMovesFor(CurrentPlayer).Any())
            {
                if (Board.IsInCheck(CurrentPlayer))
                {
                    Result = new Result(CurrentPlayer.Opponent(), EndReason.Checkmate);
                }
                else
                {
                    Result = new Result(Player.None, EndReason.Stalemate);
                }
            }
        }
    }
}

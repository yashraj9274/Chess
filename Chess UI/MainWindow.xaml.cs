using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ChessLogic;

namespace Chess_UI
{
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];

        private GameState gameState;
        private Position selectedPos = null;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
            
            gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);
        }

        private void InitializeBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Image image = new Image();
                    pieceImages[r, c] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    highlight.Fill = Brushes.Transparent;
                    highlights[r, c] = highlight;
                    HighlightGrid.Children.Add(highlight);
                }
            }
        }

        private void DrawBoard(Board board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = board[r, c];
                    pieceImages[r, c].Source = Images.GetImage(piece);
                    highlights[r, c].Fill = Brushes.Transparent;
                }
            }

            if (board.IsInCheck(gameState.CurrentPlayer))
            {
                Position kingPos = board.GetKingPos(gameState.CurrentPlayer);
                if (kingPos != null)
                {
                    highlights[kingPos.Row, kingPos.Col].Fill = Brushes.Red;
                }
            }
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(PieceGrid);
            double squareWidth = PieceGrid.ActualWidth / 8;
            double squareHeight = PieceGrid.ActualHeight / 8;
            int row = (int)(point.Y / squareHeight);
            int col = (int)(point.X / squareWidth);

            Position pos = new Position(row, col);

            if (selectedPos == null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }

        private void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = gameState.LegalMovesForPiece(pos);
            
            if (moves.Any())
            {
                selectedPos = pos;
                HighlightMove(pos, Brushes.LightBlue);

                foreach (Move move in moves)
                {
                    HighlightMove(move.ToPos, Brushes.LightGreen);
                }
            }
        }

        private void OnToPositionSelected(Position pos)
        {
            Position fromPos = selectedPos;
            selectedPos = null;
            DrawBoard(gameState.Board);

            if (gameState.Board[pos]?.Color == gameState.CurrentPlayer)
            {
                OnFromPositionSelected(pos);
                return;
            }

            Move move = gameState.LegalMovesForPiece(fromPos).FirstOrDefault(m => m.ToPos == pos);
            if (move != null)
            {
                gameState.MakeMove(move);
                DrawBoard(gameState.Board);

                if (gameState.Result != null)
                {
                    ShowGameOver();
                }
            }
        }

        private void HighlightMove(Position pos, Brush brush)
        {
            highlights[pos.Row, pos.Col].Fill = brush;
        }

        private void ShowGameOver()
        {
            if (gameState.Result.Reason == EndReason.Checkmate)
            {
                WinnerText.Text = $"CHECKMATE! {gameState.Result.Winner} Wins!";
            }
            else
            {
                WinnerText.Text = "STALEMATE! It's a draw.";
            }
            GameOverMenu.Visibility = Visibility.Visible;
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            selectedPos = null;
            gameState = new GameState(Player.White, Board.Initial());
            GameOverMenu.Visibility = Visibility.Hidden;
            DrawBoard(gameState.Board);
        }
    }
}
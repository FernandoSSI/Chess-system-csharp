using boardgame;
using Chess_system_csharp.boardgame;
using Chess_system_csharp.ChessGame;

namespace Chess_system_csharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.putPiece(new Rook(board, Color.White), new Position(0, 0));
            board.putPiece(new King(board, Color.Black), new Position(3, 4));
            board.putPiece(new Rook(board, Color.White), new Position(0, 7));
            board.putPiece(new Rook(board, Color.Black), new Position(7, 0));

            UI.printBoard(board);
        }
    }
}

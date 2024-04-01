using boardgame;
using Chess_system_csharp.boardgame;

namespace Chess_system_csharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            UI.printBoard(board);
        }
    }
}

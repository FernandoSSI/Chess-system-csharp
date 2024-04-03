using boardgame;
using Chess_system_csharp.boardgame;
using Chess_system_csharp.ChessGame;

namespace Chess_system_csharp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ChessPosition pos = new ChessPosition('a', 1);
            Console.WriteLine(pos);

            Console.WriteLine(pos.toPosition());
        }
    }
}

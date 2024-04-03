using boardgame;
using Chess_system_csharp.boardgame;
using Chess_system_csharp.ChessGame;

namespace Chess_system_csharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
           try {
                ChessMatch match = new ChessMatch();

                while (!match.finished)
                {
                    Console.Clear();
                    UI.printBoard(match.Board);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = UI.readChessPosition().toPosition();
                    Console.Write("Destiny: ");
                    Position destiny = UI.readChessPosition().toPosition();
                    
                    match.pieceMoevement(origin, destiny);  
                
                }

                
            } 
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine();
        }
    }
}

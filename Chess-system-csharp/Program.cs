using boardgame;
using Chess_system_csharp.boardgame;
using Chess_system_csharp.ChessGame;

namespace Chess_system_csharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.finished)
                {
                    try
                    {
                        Console.Clear();
                        UI.printMatch(match);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = UI.readChessPosition().toPosition();

                        match.validateOriginPosition(origin);
                        bool[,] possiblePositions = match.Board.piece(origin).possibleMovements();

                        Console.Clear();
                        UI.printBoard(match.Board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = UI.readChessPosition().toPosition();
                        match.validateDestinyPosition(origin, destiny);
                        match.makePlay(origin, destiny);
                    } catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                UI.printMatch(match);

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine();
        }
    }
}

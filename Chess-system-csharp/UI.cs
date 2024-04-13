using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess_system_csharp.boardgame;
using Chess_system_csharp.ChessGame;

namespace Chess_system_csharp
{
    internal class UI
    {

        public static void printMatch(ChessMatch match)
        {
            printBoard(match.Board);
            printCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turn: " + match.turn);
           

            if (!match.finished)
            {
                Console.WriteLine("Waiting for move: " + match.currentPlayer);
                if (match.Check)
                {
                    Console.WriteLine("CHECK!");
                }
            } else
            {
                Console.WriteLine("CHECKMATE");
                Console.WriteLine("Winner: " + match.currentPlayer);
            }
        }


        public static void printCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured pieces:");
            Console.Write("White: ");
            printSet(match.CapturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            printSet(match.CapturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();

           
        }

        public static void printSet(HashSet<Piece> set)
        {
            Console.Write("{");
            foreach(Piece x in set)
            {
                Console.Write(x + " ");
            }
            Console.Write("}");
        }


        public static void printBoard(Board board)
        {
            for (int i = 0; i< board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j< board.Cols; j++)
                {
                    printPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printBoard(Board board, bool[,] possiblePositions)
        {

            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor changedBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Cols; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = changedBackground;
                    } else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    printPiece(board.piece(i, j));
                }
                Console.WriteLine();
                Console.BackgroundColor = originalBackground;
            }

            Console.WriteLine("  a b c d e f g h");
           
        }

        public static void printPiece(Piece piece) 
        {

            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {

                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        public static ChessPosition readChessPosition()
        {
            string s = Console.ReadLine();
            char col = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(col, row);

        }
    }
}

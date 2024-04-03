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

        public static void printBoard(Board board)
        {
            for (int i = 0; i< board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j< board.Cols; j++)
                {
                    if(board.piece(i, j) != null)
                    {
                        printPiece(board.piece(i, j));
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printPiece(Piece piece) 
        {
            if(piece.Color == Color.White)
            {
                Console.Write(piece);
            } else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
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

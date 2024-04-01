using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess_system_csharp.boardgame;

namespace Chess_system_csharp
{
    internal class UI
    {

        public static void printBoard(Board board)
        {
            for (int i = 0; i< board.Rows; i++)
            {
                for (int j = 0; j< board.Cols; j++)
                {
                    if(board.piece(i, j) != null)
                    {
                        Console.Write(board.piece(i, j) + " ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                    
                }
                Console.WriteLine();
            }
        }
    }
}

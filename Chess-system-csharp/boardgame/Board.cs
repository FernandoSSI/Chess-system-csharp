using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_system_csharp.boardgame
{
    internal class Board
    {

        public int Rows { get; set; }
        public int Cols { get; set; }
        public Piece[,] Pieces;

        public Board() 
        { 
        }

        public Board(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Pieces = new Piece[rows, cols];
        }

        public Piece piece(int row, int col)
        {
            return Pieces[row, col];
        }
    }
}

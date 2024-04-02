using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using boardgame;

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

        public Piece piece(Position pos)
        {
            return Pieces[pos.row, pos.col];
        }

        public bool existPiece(Position pos)
        {
            validatePosition(pos);
            return piece(pos) != null;
        }

        public void putPiece(Piece p, Position pos)
        {
            if (existPiece(pos))
            {
                throw new BoardException("There is already a piece in that position")
            }
            Pieces[pos.row, pos.col] = p;
            p.Position = pos;
        }

        public bool validPosition(Position pos)
        {
            if (pos.row < 0 || pos.row >= Rows || pos.col < 0 || pos.col >= Cols)
            {
                return false;
            }
            return true;

        }

        public void validatePosition(Position pos)
        {
            if (!validPosition(pos)) 
            {
                throw new BoardException("invalid Position!");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using boardgame;
using Chess_system_csharp.boardgame;

namespace Chess_system_csharp.ChessGame
{
    internal class Knight : Piece
    {

        public Knight(Board board, Color color) : base(color, board) { }

        private bool canMove(Position pos)
        {
            Piece p = Board.piece(pos);
            return p == null || p.Color != Color;
        }

        public override string ToString()
        {
            return "N";
        }

        public override bool[,] possibleMovements()
        {

            bool[,] mat = new bool[Board.Rows, Board.Cols];

            Position pos = new Position(0, 0);

            // up right
            pos.defineValues(Position.row - 2, Position.col + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // up left
            pos.defineValues(Position.row - 2, Position.col - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // right up
            pos.defineValues(Position.row -1, Position.col + 2);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // right up
            pos.defineValues(Position.row + 1, Position.col + 2);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // down right
            pos.defineValues(Position.row + 2, Position.col + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // down left
            pos.defineValues(Position.row + 2, Position.col - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;
            }


            // left up
            pos.defineValues(Position.row - 1, Position.col - 2);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // left up
            pos.defineValues(Position.row + 1, Position.col - 2);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            return mat;
        }
    }
}

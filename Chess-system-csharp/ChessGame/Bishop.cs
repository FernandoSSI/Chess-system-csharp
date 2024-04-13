using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using boardgame;
using Chess_system_csharp.boardgame;

namespace Chess_system_csharp.ChessGame
{
    internal class Bishop : Piece
    {


        public Bishop(Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        private bool canMove(Position pos)
        {
            Piece p = Board.piece(pos);
            return p == null || p.Color != Color;
        }


        public override bool[,] possibleMovements()
        {

            bool[,] mat = new bool[Board.Rows, Board.Cols];

            Position pos = new Position(0, 0);

            // right up
            pos.defineValues(Position.row - 1, Position.col + 1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;

                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                {
                    break;
                }

                pos.row--;
                pos.col++;
            }

            // left up
            pos.defineValues(Position.row - 1, Position.col - 1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;

                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                {
                    break;
                }

                pos.row--;
                pos.col--;
            }

            // right down
            pos.defineValues(Position.row + 1, Position.col + 1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;

                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                {
                    break;
                }

                pos.row++;
                pos.col++;
            }

            // left down
            pos.defineValues(Position.row + 1, Position.col - 1);
            while (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;

                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                {
                    break;
                }

                pos.row++;
                pos.col--;
            }

            return mat;
        }
    }
}

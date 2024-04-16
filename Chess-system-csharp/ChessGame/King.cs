using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using boardgame;
using Chess_system_csharp.boardgame;

namespace Chess_system_csharp.ChessGame
{
    internal class King: Piece
    {
        private ChessMatch match;

        public King(Board board, Color color, ChessMatch match): base(color, board)        {
            this.match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool canMove(Position pos)
        {

            Piece p = Board.piece(pos);
            return p == null || p.Color != Color;
        }

        private bool testRookforCastling(Position pos)
        {
            Piece p = Board.piece(pos);
            return p != null && p is Rook && p.Color == Color && p.MovementQt == 0;
        }

        public override bool[,] possibleMovements() {

            bool[,] mat = new bool[Board.Rows, Board.Cols];

            Position pos = new Position(0, 0);
            
            //up
            pos.defineValues(Position.row - 1, Position.col);
            if (Board.validPosition(pos) && canMove(pos)) {
                mat[pos.row, pos.col] = true;
            }

            //up- right
            pos.defineValues(Position.row - 1, Position.col + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            //up- left
            pos.defineValues(Position.row - 1, Position.col - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            //down
            pos.defineValues(Position.row + 1, Position.col);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            //down- right
            pos.defineValues(Position.row + 1, Position.col + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            //down- left
            pos.defineValues(Position.row + 1, Position.col - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            //right
            pos.defineValues(Position.row, Position.col + 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            //left
            pos.defineValues(Position.row, Position.col - 1);
            if (Board.validPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.col] = true;
            }

            // # Special move: short castling
            if(MovementQt == 0 && !match.Check)
            {
                Position posR1 = new Position(Position.row, Position.col + 3);
                if (testRookforCastling(posR1))
                {
                    Position p1 = new Position(Position.row, Position.col + 1);
                    Position p2 = new Position(Position.row, Position.col + 2);

                    if(Board.piece(p1) == null && Board.piece(p2) == null)
                    {
                        mat[Position.row, Position.col + 2] = true;
                    }
                }

            }

            // # Special move: long castling
            if (MovementQt == 0 && !match.Check)
            {
                Position posR1 = new Position(Position.row, Position.col - 4);
                if (testRookforCastling(posR1))
                {
                    Position p1 = new Position(Position.row, Position.col - 1);
                    Position p2 = new Position(Position.row, Position.col - 2);
                    Position p3 = new Position(Position.row, Position.col - 3);


                    if (Board.piece(p1) == null && Board.piece(p2) == null && Board.piece(p3) == null)
                    {
                        mat[Position.row, Position.col - 2] = true;
                    }
                }

            }

            return mat;

        }
    }
}

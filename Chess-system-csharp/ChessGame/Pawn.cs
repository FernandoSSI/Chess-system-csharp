using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using boardgame;
using Chess_system_csharp.boardgame;

namespace Chess_system_csharp.ChessGame
{
    internal class Pawn : Piece
    {
        private ChessMatch match;

        public Pawn(Board board , Color color, ChessMatch match) :base(color, board) { 
            this.match = match;
        }

        public override string ToString()
        {
            return "P";
        }


        private bool enemy(Position pos)
        {
            Piece p = Board.piece(pos);
            return p != null && p.Color != Color;
        }

        private bool empty(Position pos)
        {
            return Board.piece(pos) == null;
        }

        public override bool[,] possibleMovements()
        {
            bool[,] mat = new bool[Board.Rows, Board.Cols];

            Position pos = new Position(0, 0);
            if(Color == Color.White)
            {
                
                // white up
                pos.defineValues(Position.row - 1, Position.col);
                if (Board.validPosition(pos) && empty(pos) )
                {
                    mat[pos.row, pos.col] = true;
                }
                
                //first movement 
                if (Board.validPosition(pos) && MovementQt == 0 && empty(pos) && empty(new Position(pos.row -1, pos.col)) )
                {
                    mat[pos.row - 1, pos.col] = true;
                }

                // white left capture
                pos.defineValues(Position.row-1, Position.col-1);
                if (Board.validPosition(pos) && enemy(pos) )
                {
                    mat[pos.row, pos.col] = true;
                }

                // white right capture
                pos.defineValues(Position.row - 1, Position.col + 1);
                if (Board.validPosition(pos) && enemy(pos))
                {
                    mat[pos.row, pos.col] = true;
                }

                //#Special move: in passant
                if(Position.row == 3)
                {
                    Position left = new Position(Position.row, Position.col - 1);
                    Position right = new Position(Position.row, Position.col + 1);

                    if(Board.validPosition(left) && enemy(left) && Board.piece(left) == match.vulnerablePieceInPassant)
                    {
                        mat[left.row-1, left.col] = true;
                    }

                    if (Board.validPosition(right) && enemy(right) && Board.piece(right) == match.vulnerablePieceInPassant)
                    {
                        mat[right.row-1, right.col] = true;
                    }
                }
                
            }

            if (Color == Color.Black)
            {
                // black up
                pos.defineValues(Position.row + 1, Position.col);
                if (Board.validPosition(pos) && empty(pos))
                {
                    mat[pos.row, pos.col] = true;
                }

                //first movement 
                if (Board.validPosition(pos) && MovementQt == 0 && empty(pos) && empty(new Position(pos.row + 1, pos.col)))
                {
                    mat[pos.row + 1, pos.col] = true;
                }

                // Black left capture
                pos.defineValues(Position.row + 1, Position.col + 1);
                if (Board.validPosition(pos) && enemy(pos))
                {
                    mat[pos.row, pos.col] = true;
                }

                // Black right capture
                pos.defineValues(Position.row + 1, Position.col - 1);
                if (Board.validPosition(pos) && enemy(pos))
                {
                    mat[pos.row, pos.col] = true;
                }

                //#Special move: in passant
                if (Position.row == 4)
                {
                    Position left = new Position(Position.row, Position.col - 1);
                    Position right = new Position(Position.row, Position.col + 1);

                    if (Board.validPosition(left) && enemy(left) && Board.piece(left) == match.vulnerablePieceInPassant)
                    {
                        mat[left.row+1, left.col] = true;
                    }

                    if (Board.validPosition(right) && enemy(right) && Board.piece(right) == match.vulnerablePieceInPassant)
                    {
                        mat[right.row+1, right.col] = true;
                    }
                }


            }

            return mat;
        }
    }
}

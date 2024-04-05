using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using boardgame;

namespace Chess_system_csharp.boardgame
{
    internal  abstract class Piece
    {
        public Position Position {  get; set; }
        public Color Color { get; protected set; }
        public int MovementQt { get; protected set; }
        public Board Board { get; protected set; }


        public Piece()
        {

        }

        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            MovementQt = 0;
            Board = board;
        }

        public void movementIncrement()
        {
            MovementQt++;
        }

        public bool therePossibleMovements()
        {
            bool[,] mat = possibleMovements();
            for (int i = 0; i< Board.Rows; i++)
            {
                for(int j = 0; i< Board.Cols; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool canMoveTo(Position pos)
        {
            return possibleMovements()[pos.row, pos.col];
        }


        public abstract bool[,] possibleMovements();



    }
}

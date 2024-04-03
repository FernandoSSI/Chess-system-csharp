using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using boardgame;

namespace Chess_system_csharp.boardgame
{
    internal class Piece
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

        
    }
}

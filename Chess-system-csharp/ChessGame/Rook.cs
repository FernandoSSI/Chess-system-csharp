using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess_system_csharp.boardgame;

namespace Chess_system_csharp.ChessGame
{
    internal class Rook: Piece
    {

        public Rook(Board board, Color color) : base(color, board)
        {

        }

        public override string ToString()
        {
            return "R";
        }
    }
}

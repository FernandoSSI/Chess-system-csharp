using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using boardgame;
using Chess_system_csharp.boardgame;

namespace Chess_system_csharp.ChessGame
{
    internal class ChessMatch
    {

        public Board Board {  get; private set; }
        private int turn;
        private Color currentPlayer;
        public bool finished {  get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            putPieces();
        }

        public void pieceMoevement(Position origin, Position destiny)
        {
            Piece p = Board.removePiece(origin);
            p.movementIncrement();
            Piece capturedPiece = Board.removePiece(destiny);
            Board.putPiece(p, destiny);
        }

        private void putPieces()
        {
            Board.putPiece(new Rook(Board, Color.White), new ChessPosition('c', 1).toPosition());
        }
    }
}

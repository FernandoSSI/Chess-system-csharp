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
        public int turn {  get; private set; }
        public Color currentPlayer {  get; private set; }
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

        public void makePlay(Position origin, Position destiny)
        {
            pieceMoevement(origin, destiny);
            turn++;
            changePlayer();
        }

        public void validateOriginPosition(Position pos)
        {
            if (Board.piece(pos) == null)
            {
                throw new BoardException("There is no piece in the chosen position");
            }

            if(currentPlayer != Board.piece(pos).Color) {
                throw new BoardException("it is not the turn of the chosen piece");
            }

            if (Board.piece(pos).therePossibleMovements() == false)
            {
                throw new BoardException("There are no possible moves for this piece");
            }
        }

        public void validateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.piece(origin).canMoveTo(destiny))
            {
                throw new BoardException("Invalid destiny position");
            }
        }

        private void changePlayer()
        {
            if(currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            } else
            {
                currentPlayer = Color.White;
            }
        }

        private void putPieces()
        {
            Board.putPiece(new Rook(Board, Color.White), new ChessPosition('c', 1).toPosition());
            Board.putPiece(new King(Board, Color.White), new ChessPosition('d', 1).toPosition());
            Board.putPiece(new King(Board, Color.Black), new ChessPosition('a', 8).toPosition());
        }
    }
}

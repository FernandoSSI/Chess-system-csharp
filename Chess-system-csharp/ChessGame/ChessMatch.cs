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
        private HashSet<Piece> pieces;
        private HashSet<Piece> capturedPieces;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            putPieces();
        }

        public void pieceMoevement(Position origin, Position destiny)
        {
            Piece p = Board.removePiece(origin);
            p.movementIncrement();
            Piece capturedPiece = Board.removePiece(destiny);
            Board.putPiece(p, destiny);
            if(capturedPiece != null) {
                capturedPieces.Add(capturedPiece);
            }
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

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach(Piece x in capturedPieces)
            {
                if(x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> piecesInPlay(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece x in pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        public void putNewPiece(char col, int row, Piece piece)
        {
            Board.putPiece(piece, new ChessPosition(col, row).toPosition());
            pieces.Add(piece);
        }

        private void putPieces()
        {

            putNewPiece('h', 1, new Rook(Board, Color.White));
            putNewPiece('a', 1, new Rook(Board, Color.White));
            putNewPiece('a', 8, new Rook(Board, Color.Black));
            putNewPiece('h', 8, new Rook(Board, Color.Black));
            putNewPiece('e', 1, new King(Board, Color.White));
            putNewPiece('e', 8, new King(Board, Color.Black));


        }
    }
}

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
        public bool Check {  get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            Check = false;
            putPieces();
        }

        public Piece pieceMoevement(Position origin, Position destiny)
        {
            Piece p = Board.removePiece(origin);
            p.movementIncrement();
            Piece capturedPiece = Board.removePiece(destiny);
            Board.putPiece(p, destiny);
            if(capturedPiece != null) {
                capturedPieces.Add(capturedPiece);
            }
            return capturedPiece;
        }

        public void undoMovement(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = Board.removePiece(destiny);
            p.movementDecrement();
            if(capturedPiece != null)
            {
                Board.putPiece(capturedPiece, destiny);
                capturedPieces.Remove(capturedPiece);
            }
            Board.putPiece(p, origin);
        }

        public void makePlay(Position origin, Position destiny)
        {
            Piece capturedPiece = pieceMoevement(origin, destiny);

            if (inCheck(currentPlayer))
            {
                undoMovement(origin, destiny, capturedPiece);
                throw new BoardException("you can't put yourself in check");
            }

            if(inCheck(adversary(currentPlayer)))
            {
                Check = true;
            } else
            {
                Check = false;
            }

            if (checkmateTest(adversary(currentPlayer)))
            {
                finished = true;
                changePlayer();
            }

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
            if (!Board.piece(origin).possibleMovement(destiny))
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

        private Color adversary(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            } else
            {
                return Color.White;
            }
        }

        private Piece king(Color color)
        {
            foreach(Piece x in piecesInPlay(color))
            {
                if(x is King)
                {
                    return x;
                }
                
            }
            return null;
        }

        public bool inCheck(Color color)
        {
            foreach(Piece x in piecesInPlay(adversary(color)))
            {
                Piece k = king(color);
                if(k == null)
                {
                    throw new BoardException("Dont exist king");
                }
                bool[,] mat = x.possibleMovements();
                if (mat[k.Position.row, k.Position.col])
                {
                    return true;
                }

            }
            return false;
        }

        public bool checkmateTest(Color color)
        {
            if (!inCheck(color))
            {
                return false;
            }

            foreach(Piece x in piecesInPlay(color))
            {
                bool[,] mat = x.possibleMovements();
                for(int i=0; i<Board.Rows; i++)
                {
                    for(int j=0; j<Board.Cols; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = pieceMoevement(origin, new Position(i, j));
                            bool checkTest = inCheck(color);
                            undoMovement(origin, destiny, capturedPiece);
                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void putNewPiece(char col, int row, Piece piece)
        {
            Board.putPiece(piece, new ChessPosition(col, row).toPosition());
            pieces.Add(piece);
        }

        private void putPieces()
        {
            
            putNewPiece('a', 2, new Pawn(Board, Color.White));
            putNewPiece('b', 2, new Pawn(Board, Color.White));
            putNewPiece('c', 2, new Bishop(Board, Color.White));
            putNewPiece('d', 2, new Queen(Board, Color.White));

            putNewPiece('a', 7, new Pawn(Board, Color.Black));
            putNewPiece('b', 7, new Pawn(Board, Color.Black));
            putNewPiece('c', 7, new Bishop(Board, Color.Black));
            putNewPiece('d', 7, new Queen(Board, Color.Black));

            putNewPiece('h', 1, new Rook(Board, Color.White));
            putNewPiece('a', 1, new Rook(Board, Color.White));
            putNewPiece('a', 8, new Rook(Board, Color.Black));
            putNewPiece('h', 8, new Rook(Board, Color.Black));
            putNewPiece('e', 1, new King(Board, Color.White));
            putNewPiece('e', 8, new King(Board, Color.Black));


        }
    }
}

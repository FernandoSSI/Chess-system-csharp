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

        public Board Board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> capturedPieces;
        public bool Check { get; private set; }

        public Piece vulnerablePieceInPassant { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            vulnerablePieceInPassant = null;
            Check = false;
            putPieces();
        }

        public Piece pieceMoevement(Position origin, Position destiny)
        {
            Piece p = Board.removePiece(origin);
            p.movementIncrement();
            Piece capturedPiece = Board.removePiece(destiny);
            Board.putPiece(p, destiny);
            if (capturedPiece != null)
            {
                capturedPieces.Add(capturedPiece);
            }

            //#Special move: short castling
            if (p is King && destiny.col == origin.col + 2)
            {
                Position rookOrigin = new Position(origin.row, origin.col + 3);
                Position rookDestiny = new Position(origin.row, origin.col + 1);
                Piece rook = Board.removePiece(rookOrigin);
                rook.movementIncrement();
                Board.putPiece(rook, rookDestiny);
            }

            //#Special move: long castling
            if (p is King && destiny.col == origin.col - 2)
            {
                Position rookOrigin = new Position(origin.row, origin.col - 4);
                Position rookDestiny = new Position(origin.row, origin.col - 1);
                Piece rook = Board.removePiece(rookOrigin);
                rook.movementIncrement();
                Board.putPiece(rook, rookDestiny);
            }

            //#Special move: in passant
            if (p is Pawn)
            {
                if (origin.col != destiny.col && capturedPiece == null)
                {
                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(destiny.row + 1, destiny.col);
                    }
                    else
                    {
                        posP = new Position(destiny.row - 1, destiny.col);

                    }
                    capturedPiece = Board.removePiece(posP);
                    capturedPieces.Add(capturedPiece);
                }
            }


            return capturedPiece;
        }

        public void undoMovement(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = Board.removePiece(destiny);
            p.movementDecrement();
            if (capturedPiece != null)
            {
                Board.putPiece(capturedPiece, destiny);
                capturedPieces.Remove(capturedPiece);
            }
            Board.putPiece(p, origin);


            //#Special move: short castling
            if (p is King && destiny.col == origin.col + 2)
            {
                Position rookOrigin = new Position(origin.row, origin.col + 3);
                Position rookDestiny = new Position(origin.row, origin.col + 1);
                Piece rook = Board.removePiece(rookDestiny);
                rook.movementDecrement();
                Board.putPiece(rook, rookOrigin);
            }

            //#Special move: long castling
            if (p is King && destiny.col == origin.col - 2)
            {
                Position rookOrigin = new Position(origin.row, origin.col - 4);
                Position rookDestiny = new Position(origin.row, origin.col - 1);
                Piece rook = Board.removePiece(rookDestiny);
                rook.movementIncrement();
                Board.putPiece(rook, rookOrigin);
            }

            //#Special move: in passant
            if (p is Pawn)
            {
                if (origin.col != destiny.col && capturedPiece == vulnerablePieceInPassant)
                {
                    Piece pawn = Board.removePiece(destiny);
                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(3, destiny.col);
                    }
                    else
                    {
                        posP = new Position(4, destiny.col);
                    }
                    Board.putPiece(pawn, posP);
                }

            }

        }

        public void makePlay(Position origin, Position destiny)
        {
            Piece capturedPiece = pieceMoevement(origin, destiny);

            if (inCheck(currentPlayer))
            {
                undoMovement(origin, destiny, capturedPiece);
                throw new BoardException("you can't put yourself in check");
            }

            Piece p = Board.piece(destiny);

            //#Special move: promotion
            if (p is Pawn)
            {
                if ((p.Color == Color.White && destiny.row == 0) || (p.Color == Color.Black && destiny.row == 7))
                {
                    p = Board.removePiece(destiny);
                    pieces.Remove(p);
                    Piece queen = new Queen(Board, p.Color);
                    Board.putPiece(queen, destiny);
                    pieces.Add(queen);


                }
            }

            if (inCheck(adversary(currentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }


            if (checkmateTest(adversary(currentPlayer)))
            {
                finished = true;
                changePlayer();
            }
            else
            {
                turn++;
                changePlayer();
            }




            //#Special move: in passant
            if (p is Pawn && (destiny.row == origin.row + 2 || destiny.row == origin.row - 2))
            {
                vulnerablePieceInPassant = p;
            }
            else
            {
                vulnerablePieceInPassant = null;
            }

        }

        public void validateOriginPosition(Position pos)
        {
            if (Board.piece(pos) == null)
            {
                throw new BoardException("There is no piece in the chosen position");
            }

            if (currentPlayer != Board.piece(pos).Color)
            {
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
            if (currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            }
            else
            {
                currentPlayer = Color.White;
            }
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece x in capturedPieces)
            {
                if (x.Color == color)
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
            }
            else
            {
                return Color.White;
            }
        }

        private Piece king(Color color)
        {
            foreach (Piece x in piecesInPlay(color))
            {
                if (x is King)
                {
                    return x;
                }

            }
            return null;
        }

        public bool inCheck(Color color)
        {
            foreach (Piece x in piecesInPlay(adversary(color)))
            {
                Piece k = king(color);
                if (k == null)
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

            foreach (Piece x in piecesInPlay(color))
            {
                bool[,] mat = x.possibleMovements();
                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Cols; j++)
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

            putNewPiece('a', 2, new Pawn(Board, Color.White, this));
            putNewPiece('b', 2, new Pawn(Board, Color.White, this));
            putNewPiece('c', 2, new Pawn(Board, Color.White, this));
            putNewPiece('d', 2, new Pawn(Board, Color.White, this));
            putNewPiece('e', 2, new Pawn(Board, Color.White, this));
            putNewPiece('f', 2, new Pawn(Board, Color.White, this));
            putNewPiece('g', 2, new Pawn(Board, Color.White, this));
            putNewPiece('h', 2, new Pawn(Board, Color.White, this));

            putNewPiece('e', 1, new King(Board, Color.White, this));

            putNewPiece('d', 1, new Queen(Board, Color.White));

            putNewPiece('c', 1, new Bishop(Board, Color.White));
            putNewPiece('f', 1, new Bishop(Board, Color.White));

            putNewPiece('h', 1, new Rook(Board, Color.White));
            putNewPiece('a', 1, new Rook(Board, Color.White));

            putNewPiece('b', 1, new Knight(Board, Color.White));
            putNewPiece('g', 1, new Knight(Board, Color.White));


            putNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            putNewPiece('h', 7, new Pawn(Board, Color.Black, this));
            
            putNewPiece('e', 8, new King(Board, Color.Black, this));

            putNewPiece('d', 8, new Queen(Board, Color.Black));

            putNewPiece('c', 8, new Bishop(Board, Color.Black));
            putNewPiece('f', 8, new Bishop(Board, Color.Black));

            putNewPiece('h', 8, new Rook(Board, Color.Black));
            putNewPiece('a', 8, new Rook(Board, Color.Black));

            putNewPiece('b', 8, new Knight(Board, Color.Black));
            putNewPiece('g', 8, new Knight(Board, Color.Black));
            

        }
    }
}

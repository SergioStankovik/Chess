﻿using System;
using System.Collections.Generic;

namespace GameCore
{
    internal static class UnderCheck
    {                   
        internal static bool AfterMove(Move move) 
        {
            // White player
            if (GameState.CurrentPlayer == Color.White) 
            {
                // King's move
                if (move.start.Equals(GameState.KingPositionWhite)) 
                {
                    return AttackingSquareBlack(move.end);
                }

                // Other's move
                if (!OnKingStar(move.end) && !GameState.Board.ContainsKey(move.end) && GameState.UnderCheckWhite) return true; // Move can't protect king

                if (!OnKingStar(move.start) && !GameState.UnderCheckWhite) return false; // King can't be hurt by the move


                return AttackingSquareWithoutPieceBlack(GameState.KingPositionWhite, move.end, move.end, move.start); 
              
                
            }
            // Black player
            else
            {
                // King's move
                if (move.start.Equals(GameState.KingPositionBlack))
                {
                    return AttackingSquareWhite(move.end);
                }

                // Other's move               
                if (!OnKingStar(move.end) && !GameState.Board.ContainsKey(move.end) && GameState.UnderCheckBlack) return true; // Move can't protect king 

                if (!OnKingStar(move.start) && !GameState.UnderCheckBlack) return false; // King can't be hurt by the move


                return AttackingSquareWithoutPieceWhite(GameState.KingPositionBlack, move.end, move.end, move.start);
            }
        }

        // When we move king
        internal static bool AttackingSquareWhite(Square square) 
        {
            foreach (KeyValuePair<Square, Piece> pair in GameState.Board)
            {
                if (pair.Value.color == Color.Black) continue;

                if (PieceAttackingSquare(pair.Key, square)) return true;
            }

            return false;
        }
        internal static bool AttackingSquareBlack(Square square) 
        {
            foreach (KeyValuePair<Square, Piece> pair in GameState.Board)
            {
                if (pair.Value.color == Color.White) continue;

                if (PieceAttackingSquare(pair.Key, square)) return true;
            }

            return false;
        }
        private static bool PieceAttackingSquare(Square start, Square end)
        {
            switch (GameState.Board[start].type)
            {
                case PieceType.Pawn: return Pawn.AttackingSquare(start, end);

                case PieceType.Rook: return Rook.AttackingSquare(start, end, new Square(20, 20), new Square(20, 20));

                case PieceType.Bishop: return Bishop.AttackingSquare(start, end, new Square(20, 20), new Square(20, 20));

                case PieceType.Knight: return Knight.AttackingSquare(start, end);

                case PieceType.King: return King.AttackingSquare(start, end);

                case PieceType.Queen: return Queen.AttackingSquare(start, end, new Square(20, 20), new Square(20, 20));
            }

            return false;
        }

        // When we move other pieces
        internal static bool AttackingSquareWithoutPieceWhite(Square target, Square pieceSquare, Square block, Square empty)
        {
            foreach (KeyValuePair<Square, Piece> pair in GameState.Board)
            {
                if (pair.Value.color == Color.Black) continue;
                if (pair.Key.Equals(pieceSquare)) continue;

                if (PieceAttackingSquare(pair.Key, target, block, empty)) return true;
            }

            return false;
        }
        internal static bool AttackingSquareWithoutPieceBlack(Square target, Square pieceSquare, Square block, Square empty)
        {
            foreach (KeyValuePair<Square, Piece> pair in GameState.Board)
            {
                if (pair.Value.color == Color.White) continue;
                if (pair.Key.Equals(pieceSquare)) continue;

                if (PieceAttackingSquare(pair.Key, target, block, empty)) return true;
            }

            return false;
        }        
        
        private static bool PieceAttackingSquare(Square start, Square end, Square block, Square empty) 
        {
            switch (GameState.Board[start].type)
            {
                case PieceType.Pawn: return Pawn.AttackingSquare(start, end);

                case PieceType.Rook: return Rook.AttackingSquare(start, end, block, empty);

                case PieceType.Bishop: return Bishop.AttackingSquare(start, end, block, empty);

                case PieceType.Knight: return Knight.AttackingSquare(start, end);

                case PieceType.King: return King.AttackingSquare(start, end);

                case PieceType.Queen: return Queen.AttackingSquare(start, end, block, empty);
            }

            return false;
        }

        private static bool OnKingStar(Square s) 
        {
            if (GameState.CurrentPlayer == Color.White)
            {
                bool bishop = Math.Abs(s.file - GameState.KingPositionWhite.file) == Math.Abs(s.rank - GameState.KingPositionWhite.rank);

                bool rook = s.rank == GameState.KingPositionWhite.rank || s.file == GameState.KingPositionWhite.file;

                return bishop || rook;
            }
            else 
            {
                bool bishop = Math.Abs(s.file - GameState.KingPositionBlack.file) == Math.Abs(s.rank - GameState.KingPositionBlack.rank);

                bool rook = s.rank == GameState.KingPositionBlack.rank || s.file == GameState.KingPositionBlack.file;

                return bishop || rook;
            }
        }
    }
}



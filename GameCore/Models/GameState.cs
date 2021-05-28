﻿using System.Collections.Generic;

namespace GameCore
{
    internal static class GameState
    {
        internal static Dictionary<Square, Piece> Board = new Dictionary<Square, Piece>();

        internal static bool CastleWhite_OO = true;
        internal static bool CastleWhite_OOO = true;
        internal static bool CastleBlack_OO = true;
        internal static bool CastleBlack_OOO= true;

        internal static byte EnPassantFile = 0;

        internal static Color CurrentPlayer = Color.White;

        internal static short MoveNumber = 1;
        internal static byte HalfmoveNumber = 0;

        internal static Square KingPositionWhite = new Square(5, 1);
        internal static Square KingPositionBlack = new Square(8, 1);

        internal static bool UnderCheckWhite = false;
        internal static bool UnderCheckBlack = false;

    }
}

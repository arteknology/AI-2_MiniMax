using System;
using System.Collections.Generic;
using System.Linq;
using Players;
using UnityEngine;

namespace GameSet
{
    public class Board : ICloneable
    {
        public Piece[,] Matrix = new Piece[8,8];

        public List<Piece> Pieces
        {
            get
            {
                return Matrix.Cast<Piece>().ToList();
            }
        }

        public List<Piece> PlayerPieces(PlayerColor color)
        {
            return AvailablePieces(color).ToList();
        }

        public void BoardSetup()
        {
            Matrix = new Piece[,]
            {
                {null, new Pawn(PlayerColor.Red, new Coord(0,1)), null, new Pawn(PlayerColor.Red, new Coord(0,3)), null, new Pawn(PlayerColor.Red, new Coord(0,5)), null, new Pawn(PlayerColor.Red, new Coord(0,7))},
                {new Pawn(PlayerColor.Red, new Coord(1,0)), null, new Pawn(PlayerColor.Red, new Coord(1,2)), null, new Pawn(PlayerColor.Red, new Coord(1,4)), null, new Pawn(PlayerColor.Red, new Coord(1,6)), null},
                {null, new Pawn(PlayerColor.Red, new Coord(2,1)), null, new Pawn(PlayerColor.Red, new Coord(2,3)), null, new Pawn(PlayerColor.Red, new Coord(2,5)), null, new Pawn(PlayerColor.Red, new Coord(2,7))},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {new Pawn(PlayerColor.White, new Coord(5,0)), null, new Pawn(PlayerColor.White, new Coord(5,2)), null, new Pawn(PlayerColor.White, new Coord(5,4)), null, new Pawn(PlayerColor.White, new Coord(5,6)), null},
                {null, new Pawn(PlayerColor.White, new Coord(6,1)), null, new Pawn(PlayerColor.White, new Coord(6,3)), null, new Pawn(PlayerColor.White, new Coord(6,5)), null, new Pawn(PlayerColor.White, new Coord(6,7))},
                {new Pawn(PlayerColor.White, new Coord(7,0)), null, new Pawn(PlayerColor.White, new Coord(7,2)), null, new Pawn(PlayerColor.White, new Coord(7,4)), null, new Pawn(PlayerColor.White, new Coord(7,6)), null},
            };
        }

        public IEnumerable<Piece> AvailablePieces(PlayerColor playerColor)
        {
            return
                from Piece piece in Matrix
                where piece != null && piece.Owner == playerColor
                select piece;
        }
        
        public object Clone()
        {
            Board board = new Board();
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (Matrix[i, j] != null) board.Matrix[i, j] = (Piece)Matrix[i, j].Clone();
                }
            }
            return board;
        }

        public bool ValidCoord(Coord coord)
        {
            return coord.X < Matrix.GetLength(0) && coord.X >= 0 && 
                coord.Y < Matrix.GetLength(1) && coord.Y >= 0;
        }

        public bool OccupiedCoord(Coord coord)
        {
            return Matrix[coord.X, coord.Y] != null;
        }
        
        public Piece GetPiece(Coord originMove)
        {
            Piece piece = Matrix[originMove.X, originMove.Y];
            return piece;
        }
    }
}
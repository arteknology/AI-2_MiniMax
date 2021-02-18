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

        public void BoardSetup()
        {
            Matrix = new Piece[,]
            {
                {new Pawn(PlayerColor.White, new Coord(0,2)), null, new Pawn(PlayerColor.White, new Coord(2,2)), null, new Pawn(PlayerColor.White, new Coord(4,2)), null, new Pawn(PlayerColor.White, new  Coord(6,2)), null},
                {null, new Pawn(PlayerColor.White, new Coord(1,1)), null, new Pawn(PlayerColor.White, new Coord(3,1)), null, new Pawn(PlayerColor.White, new Coord(5,1)), null, new Pawn(PlayerColor.White, new  Coord(7,1))},
                {new Pawn(PlayerColor.White, new Coord(0,0)), null, new Pawn(PlayerColor.White, new Coord(2,0)), null, new Pawn(PlayerColor.White, new Coord(4,0)), null, new Pawn(PlayerColor.White, new  Coord(6,0)), null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, new Pawn(PlayerColor.Red, new Coord(1,7)), null, new Pawn(PlayerColor.Red, new Coord(3,7)), null, new Pawn(PlayerColor.Red, new Coord(5,7)), null, new Pawn(PlayerColor.Red, new  Coord(7,7))},
                {new Pawn(PlayerColor.Red, new Coord(0,6)), null, new Pawn(PlayerColor.Red, new Coord(2,6)), null, new Pawn(PlayerColor.Red, new Coord(4,6)), null, new Pawn(PlayerColor.Red, new  Coord(6,6)), null},
                {null, new Pawn(PlayerColor.Red, new Coord(1,5)), null, new Pawn(PlayerColor.Red, new Coord(3,5)), null, new Pawn(PlayerColor.Red, new Coord(5,5)), null, new Pawn(PlayerColor.Red, new  Coord(7,5))},
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
            if (ValidCoord(coord))
            {
                return Matrix[coord.X, coord.Y] != null;
            }
            else
            {
                return false;
            }
        }
        
        public Piece GetPiece(Coord originMove)
        {
            Piece piece = Matrix[originMove.X, originMove.Y];
            return piece;
        }
    }
}
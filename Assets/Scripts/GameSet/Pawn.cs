using System;
using System.Collections.Generic;
using System.Linq;
using GameSet;
using Players;
using UnityEngine;

namespace GameSet
{
    public class Pawn : Piece
    {

        public Pawn(PlayerColor owner, Coord currentCoord) : base(owner, currentCoord){}

        public override int EvaluateMove(Board board, Coord destination)
        {
            int xDist = CurrentCoord.X - destination.X;
            int yDist = CurrentCoord.Y - destination.Y;
            if (Mathf.Abs(xDist) == 2 && Mathf.Abs(yDist) == 2 &&
                board.Matrix[CurrentCoord.X + xDist / 2, CurrentCoord.Y + yDist / 2] != null)
            {
                return 1;
            }

            return 0;
        }

        public override List<Coord> AvailableMoves(Board board)
        {
            List<Coord> moves = new List<Coord>();
            //Basic moves
            if (Owner == PlayerColor.White)
            {
                if (board.ValidCoord(CurrentCoord.TopLeft))
                {
                    moves.Add(CurrentCoord.TopLeft);
                }
                if (board.ValidCoord(CurrentCoord.TopLeft))
                {
                    moves.Add(CurrentCoord.TopRight);
                }
            }

            if (Owner == PlayerColor.Red)
            {
                if (board.ValidCoord(CurrentCoord.BottomLeft))
                {
                    moves.Add(CurrentCoord.BottomLeft);
                }
                if (board.ValidCoord(CurrentCoord.BottomRight))
                {
                    moves.Add(CurrentCoord.BottomRight);
                }
            }
            
            //Eat moves
            if (board.ValidCoord(CurrentCoord.JumpTopLeft))
            {
                if(board.Matrix[CurrentCoord.TopLeft.X, CurrentCoord.TopLeft.Y]?.Owner != Owner &&
                   board.Matrix[CurrentCoord.JumpTopLeft.X, CurrentCoord.JumpTopLeft.Y] == null)moves.Add(CurrentCoord.JumpTopLeft);
            }
            if (board.ValidCoord(CurrentCoord.JumpTopRight))
            {
                if(board.Matrix[CurrentCoord.TopRight.X, CurrentCoord.TopRight.Y]?.Owner != Owner &&
                   board.Matrix[CurrentCoord.JumpTopRight.X, CurrentCoord.JumpTopRight.Y] == null) moves.Add(CurrentCoord.JumpTopRight);
            }
            if (board.ValidCoord(CurrentCoord.JumpBottomLeft))
            {
                if(board.Matrix[CurrentCoord.BottomLeft.X, CurrentCoord.BottomLeft.Y]?.Owner != Owner &&
                   board.Matrix[CurrentCoord.JumpBottomLeft.X, CurrentCoord.JumpBottomLeft.Y] == null) moves.Add(CurrentCoord.JumpBottomLeft);
            }

            if (board.ValidCoord(CurrentCoord.JumpBottomRight))
            {
                if(board.Matrix[CurrentCoord.BottomRight.X, CurrentCoord.BottomRight.Y]?.Owner != Owner &&
                   board.Matrix[CurrentCoord.JumpBottomRight.X, CurrentCoord.JumpBottomRight.Y] == null) moves.Add(CurrentCoord.JumpBottomRight);
            }

            return moves.ToList();
        }

        public override void ExecuteMove(Board board, Coord destination)
        {
            if(board.Matrix[destination.X, destination.Y] != null) throw new Exception("Occupied position: " + destination.X+","+destination.Y);
            board.Matrix[destination.X, destination.Y] = board.Matrix[CurrentCoord.X, CurrentCoord.Y];
            board.Matrix[CurrentCoord.X, CurrentCoord.Y] = null;
            CurrentCoord = destination;
        }

        public override object Clone()
        {
            return new Pawn(Owner, CurrentCoord);
        }
    }
}
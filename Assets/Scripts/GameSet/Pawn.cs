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
            if (Mathf.Abs(xDist) == 2 && Mathf.Abs(yDist) == 2)
            {
                return Mathf.Abs(xDist + yDist);
            }
    
            return Mathf.Abs(yDist- xDist);
        }

        public override List<Coord> AvailableMoves(Board board)
        {
            List<Coord> moves = new List<Coord>();
            switch (Owner)
            {
                case PlayerColor.White:
                {
                    //Basic moves
                    if (board.ValidCoord(CurrentCoord.TopLeft) && !board.OccupiedCoord(CurrentCoord.TopLeft))
                    {
                        moves.Add(CurrentCoord.TopLeft);
                    }
                    if (board.ValidCoord(CurrentCoord.TopRight) && !board.OccupiedCoord(CurrentCoord.TopRight))
                    {
                        moves.Add(CurrentCoord.TopRight);
                    }
                
                    //Eat moves
                    if (board.ValidCoord(CurrentCoord.JumpTopLeft))
                    {
                        if(board.Matrix[CurrentCoord.TopLeft.X, CurrentCoord.TopLeft.Y]?.Owner == PlayerColor.Red &&
                           board.Matrix[CurrentCoord.JumpTopLeft.X, CurrentCoord.JumpTopLeft.Y] == null)moves.Add(CurrentCoord.JumpTopLeft);
                    }
                    if (board.ValidCoord(CurrentCoord.JumpTopRight))
                    {
                        if(board.Matrix[CurrentCoord.TopRight.X, CurrentCoord.TopRight.Y]?.Owner == PlayerColor.Red &&
                           board.Matrix[CurrentCoord.JumpTopRight.X, CurrentCoord.JumpTopRight.Y] == null) moves.Add(CurrentCoord.JumpTopRight);
                    }

                    break;
                }
                case PlayerColor.Red:
                {
                    //Basic moves
                    if (board.ValidCoord(CurrentCoord.BottomLeft) && !board.OccupiedCoord(CurrentCoord.BottomLeft))
                    {
                        moves.Add(CurrentCoord.BottomLeft);
                    }
                    if (board.ValidCoord(CurrentCoord.BottomRight) && !board.OccupiedCoord(CurrentCoord.BottomRight))
                    {
                        moves.Add(CurrentCoord.BottomRight);
                    }
                
                    //Eat moves
                    if (board.ValidCoord(CurrentCoord.JumpBottomLeft))
                    {
                        if(board.Matrix[CurrentCoord.BottomLeft.X, CurrentCoord.BottomLeft.Y]?.Owner == PlayerColor.White &&
                           board.Matrix[CurrentCoord.JumpBottomLeft.X, CurrentCoord.JumpBottomLeft.Y] == null) moves.Add(CurrentCoord.JumpBottomLeft);
                    }

                    if (board.ValidCoord(CurrentCoord.JumpBottomRight))
                    {
                        if(board.Matrix[CurrentCoord.BottomRight.X, CurrentCoord.BottomRight.Y]?.Owner == PlayerColor.White &&
                           board.Matrix[CurrentCoord.JumpBottomRight.X, CurrentCoord.JumpBottomRight.Y] == null) moves.Add(CurrentCoord.JumpBottomRight);
                    }

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return moves;
        }

        public override void ExecuteMove(Board board, Coord destination)
        {
            //Check if position is occupied
            if(board.Matrix[destination.X, destination.Y] != null) throw new Exception("Occupied position: " + destination.X+","+destination.Y);

            //if Jump over enemy pawn destroy it
            int enemyX = (destination.X - CurrentCoord.X) / 2;
            int enemyY = (destination.Y - CurrentCoord.Y) / 2;
            enemyX += CurrentCoord.X;
            enemyY += CurrentCoord.Y;
            PlayerColor enemy = board.Matrix[enemyX, enemyY].Owner;
            Coord enemyCoord = new Coord(enemyX, enemyY);
            Piece enemyPiece = new Pawn(enemy, enemyCoord);

            //Move pawn
            board.Matrix[destination.X, destination.Y] = board.Matrix[CurrentCoord.X, CurrentCoord.Y];
            board.Matrix[CurrentCoord.X, CurrentCoord.Y] = null;
            CurrentCoord = destination;
            board.Matrix[enemyX, enemyY] = null;
            board.PlayerPieces(enemy).Remove(enemyPiece);
        }

        public override object Clone()
        {
            return new Pawn(Owner, CurrentCoord);
        }
    }
}
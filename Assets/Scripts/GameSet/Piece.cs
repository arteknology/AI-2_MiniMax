using System;
using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

namespace GameSet
{
    public abstract class Piece : ICloneable
    {
        public PlayerColor Owner;
        public Coord CurrentCoord;

        protected Piece(PlayerColor owner, Coord currentCoord)
        {
            Owner = owner;
            CurrentCoord = currentCoord;
        }

        public abstract List<Coord> AvailableMoves(Board board);
        public abstract int EvaluateMove(Board board, Coord destination);
        public abstract void ExecuteMove(Board board, Coord destination);
        public abstract object Clone();

    }
}

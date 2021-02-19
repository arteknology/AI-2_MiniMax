using System;
using System.Collections.Generic;
using System.Linq;
using GameSet;
using Players;
using UnityEngine;

namespace MiniMax
{
    public class AI
    {
        public PlayerColor PlayerColor;
        public Board Board;
        public int Depth = 2;
        
        private Dictionary<Node, int> _Results = new Dictionary<Node, int>();
        
        public AI(Board board, PlayerColor playerColor)
        {
            Board = board;
            PlayerColor = playerColor;
        }

        public void Calculate()
        {
            _Results.Clear();
            foreach (Piece piece in Board.AvailablePieces(PlayerColor))
            {
                foreach (Coord availableMove in piece.AvailableMoves(Board))
                {
                    Node node = new Node(Board, PlayerColor, piece.CurrentCoord, availableMove);
                    //USE AlphaBeta-MINIMAX
                    int result = AlphaBetaMiniMax(node, Depth, int.MinValue, int.MaxValue, false);
                    //USE MINIMAX
                    //int result = Minimax(node, Depth, false);
                    Debug.Log("From: "+piece.CurrentCoord.X +","+ piece.CurrentCoord.Y +" To: "+availableMove.X +","+availableMove.Y+" With Heuristic Value: "+result);
                    _Results.Add(node, result);
                }
            }
        }

        public void Play()
        {
            KeyValuePair<Node, int> first = _Results.OrderByDescending(pair => pair.Value).First();
            Board.GetPiece(first.Key.OriginMove).ExecuteMove(Board, first.Key.DestMove);
        }

        //MINIMAX
        private int Minimax(Node node, int depth, bool isMax)
        {
            if (depth == 0 && node.IsTerminal)
            {
                return node.HeuristicValue;
            }

            int value;
            
            if (isMax)
            {
                value = node.Childs.Aggregate(int.MinValue, (current, child) => Mathf.Max(current, Minimax(child, depth - 1, false)));
                return value + node.HeuristicValue;
            }
        
            else
            {
                value = node.Childs.Aggregate(int.MaxValue, (current, child) => Mathf.Min(current, Minimax(child, depth - 1, true)));
                return value + node.HeuristicValue;
            }
        }
        
        //AlphaBeta-MINIMAX
        private int AlphaBetaMiniMax(Node node, int depth, int alpha, int beta, bool isMax) {
            if (depth == 0 || node.IsTerminal) {
                return node.HeuristicValue;
            }
            int value;
            if (isMax) {
                value = int.MinValue;
                foreach (Node child in node.Childs) {
                    value = Mathf.Max(value, AlphaBetaMiniMax(child, depth - 1, alpha, beta, false));
                    if (value >= beta) return value + node.HeuristicValue;
                    alpha = Mathf.Max(alpha, value);
                }
            }
            else {
                value = int.MaxValue;
                foreach (Node child in node.Childs) {
                    value = Mathf.Min(value, AlphaBetaMiniMax(child, depth - 1, alpha, beta, true));
                    if (alpha >= value) return value + node.HeuristicValue;
                    beta = Mathf.Min(beta, value);
                }
            }
            return value + node.HeuristicValue;
        }
        
    }
}
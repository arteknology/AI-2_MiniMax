using System.Collections.Generic;
using System.Linq;
using GameSet;
using Players;

namespace MiniMax
{
    public class Node
    {
        public Board Board;
        public PlayerColor PlayerColor;
        public Coord OriginMove;
        public Coord DestMove;
        public int HeuristicValue;

        public Node(Board board, PlayerColor playerColor, Coord originMove, Coord destMove)
        {
            Board = (Board) board.Clone();
            PlayerColor = playerColor;
            OriginMove = originMove;
            DestMove = destMove;
            HeuristicValue = board.GetPiece(OriginMove).EvaluateMove(board, destMove);
            board.GetPiece(originMove).ExecuteMove(board, destMove);
        }

        public bool IsTerminal
        {
            get
            {
                return Board.AvailablePieces(PlayerColor).Sum(piece => piece.AvailableMoves(Board).Count) > 0;
            }
        }

        public List<Node> Childs
        {
            get
            {
                List<Node> childs = new List<Node>();
                foreach (Piece availablePiece in Board.AvailablePieces(PlayerColor))
                {
                    foreach (Coord availableMove in availablePiece.AvailableMoves(Board))
                    {
                        childs.Add(new Node(Board, PlayerColor, availablePiece.CurrentCoord, availableMove));
                    }
                }
                return childs;
            }
        }

    }
}

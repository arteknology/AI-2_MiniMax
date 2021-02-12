using UnityEngine;

namespace MiniMax
{
    public class MinMaxSimulator : MonoBehaviour {

        //Profondeur de l'arbre de simiulation
        [SerializeField]
        private int simulationDepth = 3;
    
        
        /*public int Minimax(Node node, int depth, bool isMax)
    {
        if (depth == 0 && node.IsTerminal)
        {
            return node.HeuristicValue;
        }

        int value;
        
        if (isMax)
        {
            value = int.MinValue;
                
            foreach (Node child in node.Childs)
            {
                value = Mathf.Max(value, Minimax(child, depth - 1, false);
            }
            return value;
        }
        
        else
        {
            value = int.MaxValue;

            foreach (Node child in node.Childs)
            {
                value = Mathf.Min(value, Minimax(child, depth - 1, true);
            }
            return value;
        }
    }*/
    }
}
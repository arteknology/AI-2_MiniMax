using System;
using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;
using GameSet;

public class GameManager : MonoBehaviour
{
    //Pieces prefabs
    public GameObject WhitePawn;
    public GameObject RedPawn;
    public GameObject WhiteLady;
    public GameObject RedLady;

    public List<Transform> PosList;
    public Transform[,] PhysicalMatrix;
    public Transform PiecesContent;
    public Board Board = new Board();

    private void Awake()
    {
        GenerateMatrix();
        Board.BoardSetup();
        //CreateAI();
        UpdatePhysicalBoard();
    }
    
    private void GenerateMatrix()
    {
        PhysicalMatrix = new Transform[(int) Mathf.Sqrt(PosList.Count), (int) Mathf.Sqrt(PosList.Count)];
        foreach (Transform transformPos in PosList)
        {
            int column = Convert.ToInt32(transformPos.name.Split('.')[0]);
            int row = Convert.ToInt32(transformPos.name.Split('.')[1]);
            PhysicalMatrix[row, column] = transformPos;

        }
    }
    
    private void CreateAI()
    {
        throw new NotImplementedException();
    }

    private void UpdatePhysicalBoard()
    {
        foreach (Transform child in PiecesContent)
            Destroy(child.gameObject);

        for (int i = 0; i < Board.Matrix.GetLength(0); i++)
        {
            for (int j = 0; j < Board.Matrix.GetLength(1); j++)
            {
                if(Board.Matrix[i,j] == null) continue;

                if (Board.Matrix[i, j].Owner == PlayerColor.White && Board.Matrix[i, j].GetType() == typeof(Pawn))
                    Instantiate(WhitePawn, PhysicalMatrix[i, j].position, Quaternion.identity, PiecesContent);
                if (Board.Matrix[i, j].Owner == PlayerColor.White && Board.Matrix[i, j].GetType() == typeof(Lady))
                    Instantiate(WhiteLady, PhysicalMatrix[i, j].position, Quaternion.identity, PiecesContent);
                if (Board.Matrix[i, j].Owner == PlayerColor.Red && Board.Matrix[i, j].GetType() == typeof(Pawn))
                    Instantiate(RedPawn, PhysicalMatrix[i, j].position, Quaternion.identity, PiecesContent);
                if (Board.Matrix[i, j].Owner == PlayerColor.Red && Board.Matrix[i, j].GetType() == typeof(Lady))
                    Instantiate(RedLady, PhysicalMatrix[i, j].position, Quaternion.identity, PiecesContent);
            }
        }
    }
}

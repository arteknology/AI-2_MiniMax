using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Players;
using UnityEngine;
using GameSet;
using MiniMax;

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
    public GameObject WhitePanel;
    public GameObject RedPanel;
    public Text WhiteScore;
    public Text RedScore;

    private AI _whiteAI;
    private AI _redAI;
    private bool _isWhiteTurn = true;
    private int _redPieces;
    private int _whitePieces;

    private void Awake()
    {
        WhitePanel.SetActive(false);
        RedPanel.SetActive(false);
        GenerateMatrix();
        Board.BoardSetup();
        CreateAI();
        UpdatePhysicalBoard();
        _whitePieces = Board.PlayerPieces(PlayerColor.White).Count;
        _redPieces = Board.PlayerPieces(PlayerColor.Red).Count;
    }

    private void Update()
    {
        if (WhiteWin())
        {
            WhitePanel.SetActive(true);
        }

        if (RedWin())
        {
            RedPanel.SetActive(true);
        }
        
        else if (Input.GetButtonDown("Fire1"))
        {
            
            if (_isWhiteTurn)
            {
                _whiteAI.Calculate();
                _whiteAI.Play();
                _isWhiteTurn = false;
                UpdatePhysicalBoard();
                UpdateScore();
            }
            else
            {
                _redAI.Calculate();
                _redAI.Play();
                _isWhiteTurn = true;
                UpdatePhysicalBoard();
                UpdateScore();
            }
        }
    }

    private bool WhiteWin()
    {
        return Board.PlayerPieces(PlayerColor.Red).Count == 0;
    }
    
    private bool RedWin()
    {
        return Board.PlayerPieces(PlayerColor.White).Count == 0;
    }

    private void UpdateScore()
    {

        int whiteScore = _redPieces - Board.PlayerPieces(PlayerColor.Red).Count;
        int redScore = _whitePieces - Board.PlayerPieces(PlayerColor.White).Count;
        WhiteScore.text = ""+whiteScore;
        RedScore.text = "" + redScore;

    }

    private void GenerateMatrix()
    {
        PhysicalMatrix = new Transform[(int) Mathf.Sqrt(PosList.Count), (int) Mathf.Sqrt(PosList.Count)];
        foreach (Transform transformPos in PosList)
        {
            int column = Convert.ToInt32(transformPos.name.Split('.')[0]);
            int row = Convert.ToInt32(transformPos.name.Split('.')[1]);
            PhysicalMatrix[column, row] = transformPos;

        }
    }
    
    private void CreateAI()
    {
        _whiteAI = new AI(Board, PlayerColor.White);
        _redAI = new AI(Board, PlayerColor.Red);
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

               if (Board.Matrix[i,j].Owner == PlayerColor.White && Board.Matrix[i,j].GetType() == typeof(Pawn))
                   Instantiate(WhitePawn, PhysicalMatrix[i,j].position, Quaternion.identity, PiecesContent);
               if (Board.Matrix[i, j].Owner == PlayerColor.White && Board.Matrix[i,j].GetType() == typeof(Lady))
                   Instantiate(WhiteLady, PhysicalMatrix[i,j].position, Quaternion.identity, PiecesContent);
               if (Board.Matrix[i, j].Owner == PlayerColor.Red && Board.Matrix[i,j].GetType() == typeof(Pawn))
                   Instantiate(RedPawn, PhysicalMatrix[i,j].position, Quaternion.identity, PiecesContent);
               if (Board.Matrix[i, j].Owner == PlayerColor.Red && Board.Matrix[i,j].GetType() == typeof(Lady))
                   Instantiate(RedLady, PhysicalMatrix[i,j].position, Quaternion.identity, PiecesContent);
           }
       }
    }
}
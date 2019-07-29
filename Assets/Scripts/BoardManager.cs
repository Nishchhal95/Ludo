using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance = null;

    public BoardArea[] boardAreas = new BoardArea[4];

    public int numberOfPieces = 4;
    public Piece piecePrefab;

    public List<PathPiece> pathPiecesList = new List<PathPiece>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupBoard(Player[] players)
    {
        for (int i = 0; i < players.Length; i++)
        {
            boardAreas[i].BoardColor = players[i].boardColor;
        }

        SpawnPieces(players);
    }

    private void SpawnPieces(Player[] players)
    {
        for (int i = 0; i < players.Length; i++)
        {
            for (int j = 0; j < numberOfPieces; j++)
            {
                players[i].pieces[j] = Instantiate(piecePrefab, players[i].boardArea.spawnPoints[j].position, Quaternion.identity);
                players[i].pieces[j].BoardColor = players[i].boardColor;
                players[i].pieces[j].currentPathPiece = players[i].boardArea.startPoint.GetComponent<PathPiece>();
            }
        }
    }
}

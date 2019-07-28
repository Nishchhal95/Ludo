using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance = null;

    public BoardColorData[] boardColorDatas;
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
        EventsManager.onColorsSet += SetupBoard;
    }

    private void OnDisable()
    {
        EventsManager.onColorsSet -= SetupBoard;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupBoard()
    {
        if(boardColorDatas != null && boardColorDatas.Length > 0)
        {
            for (int i = 0; i < boardColorDatas.Length; i++)
            {
                for (int j = 0; j < boardColorDatas[i].pieceHolders.Length; j++)
                {
                    Vector2 pos = boardColorDatas[i].pieceHolders[j].position;
                    Piece piece = Instantiate(piecePrefab, pos , Quaternion.identity);
                    piece.Color = boardColorDatas[i].Color;
                    piece.spawnPoint = boardColorDatas[i].pieceSpawnPoint;
                    piece.PlayerID = ColorManager.colorToIdDictionary[boardColorDatas[i].Color];
                }
            }
        }
    }
}

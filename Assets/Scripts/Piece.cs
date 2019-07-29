using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private SpriteRenderer pieceGFXRender;
    public Transform spawnPoint;

    public BoardColor BoardColor
    {
        get
        {
            return boardColor;
        }

        set
        {
            boardColor = value;
            UpdateColor();
        }
    }
    private BoardColor boardColor;

    public bool IsOpen { get { return isOpen; } set { isOpen = value; } }
    private bool isOpen = false;

    public int PlayerID { get { return playerID; } set { playerID = value; } }
    private int playerID;

    public PathPiece currentPathPiece;
    private int currentIndex;

    private bool move = false;
    private Queue<Vector3> positionToMoveList = new Queue<Vector3>();
    private float moveSpeed = 5;
    Vector3 targetPos;

    private void Awake()
    {
        pieceGFXRender = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        currentIndex = BoardManager.Instance.pathPiecesList.FindIndex(x => x == currentPathPiece);
    }

    void Update()
    {
        if(move)
        {
            if (transform.position == targetPos)
            {
                if(positionToMoveList.Count == 0)
                {
                    move = false;
                    Deselect();
                    EventsManager.onTurnComplete?.Invoke();
                    return;
                }

                targetPos = positionToMoveList.Dequeue();
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
        }
    }

    private void UpdateColor()
    {
        Color color = _GameManager.Instance.GetColorFromEnum(boardColor);

        if(color != Color.white)
        pieceGFXRender.color = color;
    }

    public void Select(int diceValue)
    {
        //pieceGFXRender.color = Color.black;
        transform.localScale = new Vector3(1.3f, 1.3f, 1);

        if(diceValue == 6 || diceValue == 1)
        {
            if (!IsOpen)
            {
                OpenPiece();
            }

            return;
        }

        if(diceValue != 6 && diceValue != 1)
        {
            Debug.Log("Not 1 or 6");
            if (isOpen)
            {
                Debug.Log("Moving " + diceValue + " units");
                MovePiece(diceValue);
            }
        }

    }

    public void Deselect()
    {
        //pieceGFXRender.color = color;
        transform.localScale = new Vector3(1f, 1f, 1);
    }

    private void OpenPiece()
    {
        if (!IsOpen)
        {
            IsOpen = true;

            Vector2 spawnPos = _GameManager.Instance.GetPieceSpawnPointFromColor(boardColor);

            if(spawnPos != new Vector2())
            {
                transform.position = spawnPos;
            }

            Deselect();
            EventsManager.onTurnComplete?.Invoke();
        }
    }

    private void MovePiece(int units)
    {
        for (int i = 0; i < units; i++)
        {
            currentIndex++;

            if (currentIndex > BoardManager.Instance.pathPiecesList.Count - 1)
            {
                currentIndex = 0;
            }

            positionToMoveList.Enqueue(BoardManager.Instance.pathPiecesList[currentIndex].transform.position);
        }

        targetPos = positionToMoveList.Dequeue();
        move = true;
    }
}

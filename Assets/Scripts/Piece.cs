using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private SpriteRenderer pieceGFXRender;
    public Transform spawnPoint;

    public Color Color
    {
        get
        {
            return color;
        }

        set
        {
            color = value;
            UpdateColor();
        }
    }
    private Color color;

    public bool IsOpen { get { return isOpen; } set { isOpen = value; } }
    private bool isOpen = false;

    public int PlayerID { get { return playerID; } set { playerID = value; } }
    private int playerID;

    private PathPiece currentPos;
    private int currentIndex;

    public int unitsToMove = 2;

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
        currentPos = spawnPoint.gameObject.GetComponent<PathPiece>();
        currentIndex = BoardManager.Instance.pathPiecesList.FindIndex(x => x == currentPos);
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
        pieceGFXRender.color = color;
    }

    public void Select()
    {
        //pieceGFXRender.color = Color.black;
        transform.localScale = new Vector3(1.3f, 1.3f, 1);
        if (!IsOpen)
        {
            OpenPiece();
        }

        else
        {
            unitsToMove = Dice.Instance.rollNumber;
            MovePiece(unitsToMove);
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
            transform.position = spawnPoint.position;
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
        //StartCoroutine(MovePieceRoutine(units));
    }

    private IEnumerator MovePieceRoutine(int units)
    {
        //if (currentIndex + units > BoardManager.Instance.pathPiecesList.Count - 1)
        //{
        //    currentIndex = (currentIndex + units) % BoardManager.Instance.pathPiecesList.Count;
        //}
        //else
        //{
        //    currentIndex += units;
        //}


        while (units > 0)
        {
            currentIndex++;
            units--;

            //transform.position = BoardManager.Instance.pathPiecesList[currentIndex].transform.position;
            yield return null;
        }
    }
}

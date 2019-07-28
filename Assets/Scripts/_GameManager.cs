using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour
{
    public Piece lastPiece = null;

    public int numberOfPlayers = 2;
    public int currentTurnID = 0;

    public int[] inGameIdArray;

    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        inGameIdArray = new int[numberOfPlayers];
    }

    private void OnEnable()
    {
        EventsManager.onPieceSelected += PieceSelected;
        EventsManager.onTurnComplete += SwitchTurn;
    }

    private void OnDisable()
    {
        EventsManager.onPieceSelected -= PieceSelected;
        EventsManager.onTurnComplete -= SwitchTurn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchTurn()
    {
        currentTurnID++;
        if(currentTurnID == numberOfPlayers)
        {
            currentTurnID = 0;
        }

        isMoving = false;
    }

    private void PieceSelected(Piece piece)
    {
        if (piece.PlayerID == currentTurnID && !isMoving)
        {
            isMoving = true;
            if (lastPiece != null)
            {
                lastPiece.Deselect();
            }

            piece.Select();

            lastPiece = piece;
        }
    }
}

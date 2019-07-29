using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour
{
    public static _GameManager Instance = null;

    public int currentTurn = 0;
    public int currentTurnID;
    public BoardColor currentTurnColor;

    public int currentDiceNumber;
    public bool diceRolled = false;

    public Player[] players;

    public ColorToEnum[] colorToEnums;

    public Piece lastPiece = null;

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
        currentTurnID = players[currentTurn].ID;
        currentTurnColor = players[currentTurn].boardColor;
        SetupBoard();
    }

    private void OnEnable()
    {
        EventsManager.onPieceSelected += PieceSelected;
        EventsManager.onTurnComplete += SwitchTurn;
        EventsManager.onDiceRollComplete += DiceRolled;
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

    private void SetupBoard()
    {
        BoardManager.Instance.SetupBoard(players);
    }

    public void RollDice()
    {
        Dice.Instance.roll = true;
    }

    public void SwitchTurn()
    {
        diceRolled = false;
        currentTurn++;

        if(currentTurn == players.Length)
        {
            currentTurn = 0;
        }

        currentTurnID = players[currentTurn].ID;
        currentTurnColor = players[currentTurn].boardColor;

    }

    private void PieceSelected(Piece piece)
    {
        if(lastPiece != null)
        {
            lastPiece.Deselect();
        }

        if(piece.BoardColor == currentTurnColor && diceRolled)
        {
            piece.Select(currentDiceNumber);
            diceRolled = false;
        }
    }

    private void DiceRolled(int diceNumber)
    {
        diceRolled = true;
        currentDiceNumber = diceNumber;

        int closeCount = 0;

        for (int i = 0; i < 4; i++)
        {
            if (!players[currentTurn].pieces[i].IsOpen)
            {
                closeCount++;
            }
        }

        if (closeCount == 4 && (diceNumber == 6 || diceNumber == 1))
        {
            players[currentTurn].pieces[0].Select(diceNumber);
        }

        Debug.Log("Dice Number " + diceNumber + " and Close Count " + closeCount);
        if(closeCount == 4 && diceNumber != 6 && diceNumber != 1)
        {
            SwitchTurn();
        }
        //Check Whose Turn it is

        //Let Player Select a Piece 

        //Move Piece

        //Switch Turn
    }

    public Color GetColorFromEnum(BoardColor boardColor)
    {
        for (int i = 0; i < colorToEnums.Length; i++)
        {
            if(colorToEnums[i].boardColor == boardColor)
            {
                return colorToEnums[i].color;
            }
        }

        return Color.white;
    }

    public Vector2 GetPieceSpawnPointFromColor(BoardColor boardColor)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if(players[i].boardColor == boardColor)
            {
                return players[i].boardArea.startPoint.position;
            }
        }

        return new Vector2();
    }

    public PathPiece GetSpawnPathPieceFromColor(BoardColor boardColor)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].boardColor == boardColor)
            {
                return players[i].boardArea.startPoint.GetComponent<PathPiece>();
            }
        }

        return null;
    }
}

public enum BoardColor
{
    Red,
    Green,
    Yellow,
    Blue,
    Purple,
    Pink,
    Orange
}

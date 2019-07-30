using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    public Text currentTurnText;

    private void Awake()
    {
        if (Instance == null)
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

        UpdateCanvas(currentTurnColor);
    }

    private void OnEnable()
    {
        EventsManager.onPieceSelected += PieceSelected;
        EventsManager.onTurnComplete += SwitchTurn;
        EventsManager.onDiceRollComplete += DiceRolled;
        EventsManager.onPieceOpen += PieceOpened;
    }

    private void OnDisable()
    {
        EventsManager.onPieceSelected -= PieceSelected;
        EventsManager.onTurnComplete -= SwitchTurn;
        EventsManager.onDiceRollComplete -= DiceRolled;
        EventsManager.onPieceOpen -= PieceOpened;
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
        Dice.Instance.Roll();
    }

    public void SwitchTurn()
    {
        diceRolled = false;
        if (currentDiceNumber != 6)
        {
            currentTurn++;

            if (currentTurn == players.Length)
            {
                currentTurn = 0;
            }

            currentTurnID = players[currentTurn].ID;
            currentTurnColor = players[currentTurn].boardColor;

            UpdateCanvas(currentTurnColor);
        }
    }

    private void UpdateCanvas(BoardColor boardColor)
    {
        currentTurnText.text = boardColor.ToString();
        currentTurnText.color = GetColorFromEnum(boardColor);
    }

    private void PieceSelected(Piece piece)
    {
        if (lastPiece != null)
        {
            lastPiece.Deselect();
        }

        if (piece.BoardColor == currentTurnColor && diceRolled)
        {
            piece.Select(currentDiceNumber);
            lastPiece = piece;
        }
    }

    private void DiceRolled(int diceNumber)
    {
        diceRolled = true;
        currentDiceNumber = diceNumber;

        //If No move Left then Switch Turn
        if(!IsMoveLeft(diceNumber))
        {
            SwitchTurn();
        }

        //Check Whose Turn it is

        //Let Player Select a Piece 

        //Move Piece

        //Switch Turn
    }

    private void PieceOpened()
    {
        players[currentTurn].numberOfOpenPieces++;
    }

    private bool IsMoveLeft(int diceNumber)
    {
        //Check If we all Pieces Closed
        if (players[currentTurn].numberOfOpenPieces == 0)
        {
            //If All Pieces Closed Then if we get an Open Number the
            if(IsOpenNumber(diceNumber))
            {
                //We Have an Open Number and opened a Piece because we have all pieces locked
                Debug.Log("Opened a Piece");
                return true;
            }

            else
            {
                //We have no pieces Open Plus there is no open Number on the dice
                //No moves left
                return false;
            }
        }

        else
        {
            //We have Move left
            return true;
        }


        return false;
    }

    public bool IsOpenNumber(int diceNumber)
    {
        if (diceNumber == 6 || diceNumber == 1)
        {
            //We Can Open A piece
            Debug.Log("Can Open a Piece");
            players[currentTurn].pieces[0].Select(diceNumber);

            //OtherWise Let the User Make a Move
            return true;
        }

        return false;
    }

    public Color GetColorFromEnum(BoardColor boardColor)
    {
        for (int i = 0; i < colorToEnums.Length; i++)
        {
            if (colorToEnums[i].boardColor == boardColor)
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
            if (players[i].boardColor == boardColor)
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

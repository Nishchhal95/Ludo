using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager
{
    public delegate void OnColorsSet();
    public static OnColorsSet onColorsSet;

    public delegate void OnPieceSelected(Piece piece);
    public static OnPieceSelected onPieceSelected;

    public delegate void OnTurnComplete();
    public static OnTurnComplete onTurnComplete;
}

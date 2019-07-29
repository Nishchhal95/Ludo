using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int ID;
    public BoardColor boardColor;
    public int slot;

    public Piece[] pieces = new Piece[4];
    public BoardArea boardArea;
}

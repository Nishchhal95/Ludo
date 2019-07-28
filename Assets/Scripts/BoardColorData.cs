using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardColorData : MonoBehaviour
{
    public List<PathPiece> colorPieces;
    public SpriteRenderer pieceHolderSprite;
    public Transform[] pieceHolders;
    public Transform pieceSpawnPoint;
    public Transform endPoint;

    public Color Color { get { return color; } set { color = value; SetColor(color); } }
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor(Color color)
    {
        for (int i = 0; i < colorPieces.Count; i++)
        {
            colorPieces[i].GetComponent<SpriteRenderer>().color = color;
        }

        pieceHolderSprite.color = color;

        pieceSpawnPoint.GetComponent<SpriteRenderer>().color = color;
        endPoint.GetComponent<SpriteRenderer>().color = color;
    }
}

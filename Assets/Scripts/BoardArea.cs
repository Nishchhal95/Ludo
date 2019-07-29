using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardArea : MonoBehaviour
{
    public SpriteRenderer baseImage;
    public List<PathPiece> colorPath = new List<PathPiece>();
    public Transform[] spawnPoints = new Transform[4];
    public Transform startPoint;

    private BoardColor boardColor;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateColor()
    {
        Color color = _GameManager.Instance.GetColorFromEnum(boardColor);

        if (color != Color.white)
        {
            baseImage.color = color;

            for (int i = 0; i < colorPath.Count; i++)
            {
                colorPath[i].GetComponent<SpriteRenderer>().color = color;
            }

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].GetComponent<SpriteRenderer>().color = color;
            }
        }

        startPoint.GetComponent<SpriteRenderer>().color = color;
    }
}

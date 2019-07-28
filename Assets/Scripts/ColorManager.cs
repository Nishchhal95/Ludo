using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public Color[] colors;
    public BoardColorData[] boardColorDatas;
    public int[] playerIdArray;

    public static Dictionary<Color, int> colorToIdDictionary = new Dictionary<Color, int>();

    // Start is called before the first frame updates
    void Start()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            colorToIdDictionary.Add(colors[i], playerIdArray[i]);
        }
        SetColors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetColors()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            boardColorDatas[i].Color = colors[i];
        }
        EventsManager.onColorsSet?.Invoke();
    }
}

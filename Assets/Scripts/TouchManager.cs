using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        TouchDetection();
    }

    private void TouchDetection()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit2D = Physics2D.Raycast(pos, Vector2.zero);

        if(hit2D.collider != null)
        {
            Piece piece = hit2D.collider.gameObject.GetComponent<Piece>();
            if (piece != null)
            {
                Debug.Log(hit2D.collider.gameObject.name);
                EventsManager.onPieceSelected?.Invoke(piece);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dot;
    public string color;
    public string title;
    private int xPos;
    private int yPos;
    void Start()
    {
        
    }



    public void showPath() {
        if (title.Equals("pawn")){
            Instantiate(dot, new Vector3(xPos, yPos + 1, 0), Quaternion.identity);
            Instantiate(dot, new Vector3(xPos, yPos + 2, 0), Quaternion.identity);
        }
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        xPos = (int)pos.getXPos();
        yPos = (int)pos.getYPos();
    }

    public void onMouseDown() {
        showPath();
    }
}

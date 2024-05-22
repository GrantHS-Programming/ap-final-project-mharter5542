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
    public GameObject board1;
    
    void Start()
    {
        xPos = (int)transform.position.x;
        yPos = (int)transform.position.y;
        Debug.Log(color + " " + title + ": " + xPos + ", " + yPos);
        
    }

    public int GetXPos() {return xPos;}
    public int GetYPos() {return yPos;}



    public void showPath() {
        if (title == "pawn"){
            if (color == "black") {
                Instantiate(dot, new Vector3(xPos, yPos + 1, 0), Quaternion.identity);
                Instantiate(dot, new Vector3(xPos, yPos + 2, 0), Quaternion.identity);
            }
            else {
                Instantiate(dot, new Vector3(xPos, yPos - 1, 0), Quaternion.identity);
                Instantiate(dot, new Vector3(xPos, yPos - 2, 0), Quaternion.identity);
            }
            
        
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        Debug.Log("clicked");
        board1.GetComponent<board>().path(xPos,yPos,title,color);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dot;
    public string color;
    private bool isWhite;
    public string title;
    private int xPos;
    private int yPos;
    public GameObject theBoard;
    public bool firstMove = true;
    void Start()
    {
        xPos = (int)transform.position.x;
        yPos = (int)transform.position.y;
        //Debug.Log(color + " " + title + ": " + xPos + ", " + yPos);
        if (yPos < 5) {color = "black";}
        else {color = "white";}
        dot = GameObject.FindWithTag("dot");
        theBoard = GameObject.FindWithTag("Board");
        if (color.Equals("white")) {isWhite = true;}
        else {isWhite = false;}
        
    }

    public int GetXPos() {return xPos;}
    public int GetYPos() {return yPos;}
    public string GetColor() {return color;}
    public bool isFirstMove() {return firstMove;}
    public void setIsFirstMove(bool value) {firstMove = value;}
    public string getTitle() {return title;}
    public bool GetIsWhite() {return isWhite;}


    // Update is called once per frame
    void Update()
    {
        xPos = (int)transform.position.x;
        yPos = (int)transform.position.y;
    }

    public string PrintInfo() {
        Debug.Log(color + " " + title + " at (" + xPos + ", " + yPos + ")");
        return color + " " + title + " at (" + xPos + ", " + yPos + ")";
    }
    public void ShowPath() {
        theBoard.GetComponent<Board>().Path(xPos,yPos,title,color, true);
    }
    public void ShowPathWithOtherPaths() {
        theBoard.GetComponent<Board>().Path(xPos,yPos,title,color, false);
    }

    void OnMouseDown() {
        if (theBoard.GetComponent<Board>().IsWhiteTurn() == isWhite && !theBoard.GetComponent<Board>().IsGameOver() && (!theBoard.GetComponent<Board>().getCheck() || title.Equals("king"))) {
            Debug.Log("clicked");
            theBoard.GetComponent<Board>().SetSelected(xPos,yPos);
            ShowPath();
        }
    }
}

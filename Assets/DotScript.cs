using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotScript : MonoBehaviour
{
    private GameObject piece;
    private GameObject board;
    private GameObject dot;
    public GameObject whiteKing;
    public GameObject blackKing;
    // Start is called before the first frame update
    void Start()
    {
        board = GameObject.FindWithTag("Board");
        
        
       
    }

    public void SetPiece(GameObject obj) {piece = obj;}
    public void setDot(GameObject obj) {
        dot = obj;
        int xPos = (int)dot.transform.position.x;
        int yPos = (int)dot.transform.position.y;
        if (xPos < 0 || xPos > 7 || yPos < 0 || yPos > 7) {
            Destroy(dot);
        }

        
    }

    public void IsOnKing(){
        board = GameObject.FindWithTag("Board");
        int xPos = (int)transform.position.x;
        int yPos = (int)transform.position.y;
        whiteKing = GameObject.FindWithTag("WhiteKing");
        blackKing = GameObject.FindWithTag("BlackKing");
        //whiteKing.GetComponent<PieceScript>().PrintInfo();
        //blackKing.GetComponent<PieceScript>().PrintInfo();
        if (((int)whiteKing.transform.position.x == xPos && (int)whiteKing.transform.position.y == yPos) || ((int)blackKing.transform.position.x == xPos && (int)blackKing.transform.position.y == yPos)) {
            board.GetComponent<Board>().setCheck(true);
            Debug.Log("Dot is on a king");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUp() {
        board.GetComponent<Board>().MovePiece(piece.GetComponent<PieceScript>().GetXPos(), piece.GetComponent<PieceScript>().GetYPos(), (int)transform.position.x, (int)transform.position.y);
        board.GetComponent<Board>().DestroyAllDots();
    }
}

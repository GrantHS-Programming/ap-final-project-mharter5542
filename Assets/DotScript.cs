using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotScript : MonoBehaviour
{
    private GameObject piece;
    private GameObject board;
    // Start is called before the first frame update
    void Start()
    {
        board = GameObject.FindWithTag("GameController");
    }

    public void SetPiece(GameObject obj) {piece = obj;}

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUp() {
        board.GetComponent<Board>().MovePiece(piece.GetComponent<PieceScript>().GetXPos(), piece.GetComponent<PieceScript>().GetYPos(), (int)transform.position.x, (int)transform.position.y);
        board.GetComponent<Board>().DestroyAllDots();
    }
}

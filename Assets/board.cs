using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class board : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dot;
    public GameObject[] whitePieces;
    public GameObject[] blackPieces;
    private int[,] spacesTaken = new int[8,8];

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 16; i++) {
            int x = whitePieces[i].GetComponent<PieceScript>().GetXPos();
        }
    }

    public void path(int x, int y, string title, string color) {

        if (title.Equals("pawn")) {
            if (color.Equals("black")) {
                Instantiate(dot, new Vector3(x, y + 1, 0), Quaternion.identity);
                Instantiate(dot, new Vector3(x, y + 2, 0), Quaternion.identity);
            }
            else {
                Instantiate(dot, new Vector3(x, y - 1, 0), Quaternion.identity);
                Instantiate(dot, new Vector3(x, y - 2, 0), Quaternion.identity);
            }
        }
        else if (title.Equals("tower")) {
            if (color.Equals("black")) {
                Instantiate(dot, new Vector3(x, y + 1, 0), Quaternion.identity);
                Instantiate(dot, new Vector3(x, y + 2, 0), Quaternion.identity);
            }
            else {
                Instantiate(dot, new Vector3(x, y - 1, 0), Quaternion.identity);
                Instantiate(dot, new Vector3(x, y - 2, 0), Quaternion.identity);
            }
        }

    }
}

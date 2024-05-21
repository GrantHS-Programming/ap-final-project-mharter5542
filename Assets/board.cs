using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class board : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dot;
    public GameObject[] pieces;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void path(int x, int y, string title, string color) {

        if (title == "pawn") {
            if (color == "black") {
                Instantiate(dot, new Vector3(x, y + 1, 0), Quaternion.identity);
                Instantiate(dot, new Vector3(x, y + 2, 0), Quaternion.identity);
            }
            else {
                Instantiate(dot, new Vector3(x, y - 1, 0), Quaternion.identity);
                Instantiate(dot, new Vector3(x, y - 2, 0), Quaternion.identity);
            }
        }
        else if (title == "tower") {
            if (color == "black") {
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

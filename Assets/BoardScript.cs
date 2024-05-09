using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lightSquare;
    public GameObject darkSquare;
    public GameObject[,] boardSpaces = new GameObject[8, 8];
    void Start()
    {
    
    
        int[] board1D = {0,1,0,1,0,1,0,1,1,0,1,0,1,0,1,0,0,1,0,1,0,1,0,1,1,0,1,0,1,0,1,0,0,1,0,1,0,1,0,1,1,0,1,0,1,0,1,0,0,1,0,1,0,1,0,1,1,0,1,0,1,0,1,0,};
    
        int index = 0;
        for (int row = 0; row < 8; row++) {
            for (int col = 0; col < 8; col++) {
                if (board1D[index] == 0) {
                    Instantiate(lightSquare, new Vector3(row, col, 0), Quaternion.identity);
                    Debug.Log("Created new light square");
                }
                else if (board1D[index] == 1) {
                    Instantiate(darkSquare, new Vector3(row, col, 0), Quaternion.identity);
                    Debug.Log("Created new dark square");
                }
                index++;//yuh
            
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

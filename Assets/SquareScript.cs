using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    private GameObject square;

    private int xPos;
    private int yPos;
    
    void Start()
    {
    
        xPos = (int)transform.position.x;
        yPos = (int)transform.position.y;
        Debug.Log(xPos + ", " + yPos);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

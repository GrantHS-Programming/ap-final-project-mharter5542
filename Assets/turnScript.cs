using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class turnScript : MonoBehaviour
{

    public GameObject board;

   
    public TextMeshProUGUI _turn;
    

    
    // Start is called before the first frame update
    void Start()
    {
        if (board.GetComponent<Board>().IsWhiteTurn()) {_turn.text = "White Turn";}
        else {_turn.text = "Black Turn";}
    }

    // Update is called once per frame
    void Update()
    {
        if (board.GetComponent<Board>().IsGameOver()) {
            if (board.GetComponent<Board>().getWinner().Equals("black")) {_turn.text = "Black Wins!";}
            else {_turn.text = "White Wins!";}
            
        }
        else if (board.GetComponent<Board>().IsWhiteTurn()) {_turn.text = "White Turn";}
        else  {_turn.text = "Black Turn";}
        /*if (board.GetComponent<Board>().getCheck() == true) {
            _turn.text = "Check, " + _turn.text;
        }*/

    }
}

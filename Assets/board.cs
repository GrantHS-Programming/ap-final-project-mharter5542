using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Board : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dot;
    public GameObject[] pieces = new GameObject[32];
    
    public static GameObject[,] spacesTaken = new GameObject[8,8];
    public static GameObject selected;
    public static List<GameObject> dots = new List<GameObject>();
    public bool whiteTurn = true;
    private int lastX = -1;
    private int lastY = -1;
    private bool isGameOver = false;
    private string winner = "none";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in pieces) {
            if (obj != null) {
                int x = obj.GetComponent<PieceScript>().GetXPos();
                int y = obj.GetComponent<PieceScript>().GetYPos();
                if (x >= 0 && x < 8 && y >= 0 && y < 8 && !(x == lastX && y == lastY)) {spacesTaken[x,y] = obj;}
            }
        }
        
    }

    public void gameOver(string color) {
        winner = color;
        isGameOver = true;
    }
    public bool IsGameOver() {return isGameOver;}
    public string getWinner() {return winner;}

    public void MovePiece(int startX, int startY, int newX, int newY) {
        if (startX >= 0 && startX < 8 && startY >= 0 && startY < 8) {
            selected = spacesTaken[startX,startY];
            if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8) {
                if (spacesTaken[newX,newY] != null) {
                    if (spacesTaken[newX,newY].GetComponent<PieceScript>().getTitle().Equals("king")) {
                        gameOver(spacesTaken[startX,startY].GetComponent<PieceScript>().GetColor());
                    }
                    Destroy(spacesTaken[newX,newY]);
                    
                }
                selected.transform.position = new Vector3(newX, newY, 0);
                selected.GetComponent<PieceScript>().setIsFirstMove(false);
                spacesTaken[newX,newY] = selected;
                spacesTaken[startX,startY] = null; 
            }
            spacesTaken[startX,startY] = null; 
            lastX = startX;
            lastY = startY;
            Debug.Log("moved");
        }
        
        whiteTurn = !whiteTurn;
    }

    public bool IsWhiteTurn() {return whiteTurn;}
    public void DestroyPiece(int x, int y) {
        GameObject obj = spacesTaken[x,y];
        
        Destroy(spacesTaken[x,y]);
        spacesTaken[x,y] = null;
        for (int i = 0; i < pieces.Length; i++) {
            if (pieces[i] == obj) {
                pieces[i] = null;
            }
        }
       
    }

    public static GameObject[,] GetSpacesTaken() {return spacesTaken;}
    public GameObject GetSelected() {return selected;}
    public void SetSelected(int x, int y) {
        if (x >= 0 && x < 8 && y >= 0 && y < 8) {
            selected = spacesTaken[x,y];
        }
    }

    

    public void Path(int x, int y, string title, string color) {
        DestroyAllDots();
        /*for (int n = 7; n >= 0; n--) {
            string row = "row " + n + ": ";
            for (int i = 7; i >= 0; i--) {
                GameObject obj = spacesTaken[n,i];
                if (obj != null) {row += "[x]";}
                else {row += "[ ]";}
            }
            Debug.Log(row);
        }*/
 
        if (x >= 0 && x < 8 && y >= 0 && y < 8) {
            GameObject piece = spacesTaken[x,y];
            if (piece != null) {
                GameObject obj;
                if (title.Equals("pawn")) {
                    if (color.Equals("black")) {
                        if (y < 7 && spacesTaken[x, y + 1] == null ) {
                            obj = Instantiate(dot, new Vector3(x, y + 1, 0), Quaternion.identity);
                            obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                            obj.GetComponent<DotScript>().setDot(obj);
                            dots.Add(obj);

                            if (piece.GetComponent<PieceScript>().isFirstMove() && y < 6 && spacesTaken[x,y+2] == null) {
                                obj = Instantiate(dot, new Vector3(x, y + 2, 0), Quaternion.identity);
                                obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                                obj.GetComponent<DotScript>().setDot(obj);
                                dots.Add(obj);
                            }
                        }
                        
                        if (y + 1 < 8 && x + 1 < 8 && spacesTaken[x+1,y+1] != null && spacesTaken[x+1,y+1].GetComponent<PieceScript>().GetColor().Equals("white")) {
                            obj = Instantiate(dot, new Vector3(x + 1, y + 1, 0), Quaternion.identity);
                            obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                            obj.GetComponent<DotScript>().setDot(obj);
                            dots.Add(obj);
                        }
                        if (y + 1 < 8 && x - 1 >= 0 && spacesTaken[x-1,y+1] != null && spacesTaken[x-1,y+1].GetComponent<PieceScript>().GetColor().Equals("white")) {
                            obj = Instantiate(dot, new Vector3(x - 1, y + 1, 0), Quaternion.identity);
                            obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                            obj.GetComponent<DotScript>().setDot(obj);
                            dots.Add(obj);
                        }
                        
                    }
                    else {
                        if (y > 0 && spacesTaken[x, y - 1] == null) {
                            obj = Instantiate(dot, new Vector3(x, y - 1, 0), Quaternion.identity);
                            obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                            obj.GetComponent<DotScript>().setDot(obj);
                            dots.Add(obj);
                    
                            if (piece.GetComponent<PieceScript>().isFirstMove() && y > 1 && spacesTaken[x,y-2] == null) {
                                obj = Instantiate(dot, new Vector3(x, y - 2, 0), Quaternion.identity);
                                obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                                obj.GetComponent<DotScript>().setDot(obj);
                                dots.Add(obj);
                            }
                        }
                        
                        if (y > 0 && x > 0 && spacesTaken[x-1,y-1] != null && spacesTaken[x-1,y-1].GetComponent<PieceScript>().GetColor().Equals("black")) {
                            obj = Instantiate(dot, new Vector3(x - 1, y - 1, 0), Quaternion.identity);
                            obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                            obj.GetComponent<DotScript>().setDot(obj);
                            dots.Add(obj);
                        }
                        if (y > 0 && x < 7 && spacesTaken[x+1,y-1] != null && spacesTaken[x+1,y-1].GetComponent<PieceScript>().GetColor().Equals("black")) {
                            obj = Instantiate(dot, new Vector3(x + 1, y - 1, 0), Quaternion.identity);
                            obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                            obj.GetComponent<DotScript>().setDot(obj);
                            dots.Add(obj);
                        }
                    }
                }
                if (title.Equals("tower") || title.Equals("queen")) {
            
                    bool blocked = false;
                    for (int i = y + 1; i < 8 && !blocked ; i++) {
                        if (spacesTaken[x,i] != null && spacesTaken[x,i].GetComponent<PieceScript>().GetColor().Equals(color)) {blocked = true;}
                        else {
                            obj = Instantiate(dot, new Vector3(x, i, 0), Quaternion.identity);
                            obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                            obj.GetComponent<DotScript>().setDot(obj);
                            dots.Add(obj);
                        }
                        if (spacesTaken[x,i] != null ) {blocked = true;}
                    }
                    blocked = false;
                    for (int i = y - 1; i >= 0 && !blocked ; i--) {
                        if (spacesTaken[x,i] != null && spacesTaken[x,i].GetComponent<PieceScript>().GetColor().Equals(color)) {blocked = true;}
                        else {
                            obj = Instantiate(dot, new Vector3(x, i, 0), Quaternion.identity);
                            obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                            obj.GetComponent<DotScript>().setDot(obj);
                            dots.Add(obj);
                        }
                        if (spacesTaken[x,i] != null ) {blocked = true;}
                    }
                    blocked = false;
                    for (int i = x - 1; i >= 0 && !blocked ; i--) {
                        if (spacesTaken[i,y] != null && spacesTaken[i,y].GetComponent<PieceScript>().GetColor().Equals(color)) {blocked = true;}
                        else {
                            obj = Instantiate(dot, new Vector3(i, y, 0), Quaternion.identity);
                            obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                            obj.GetComponent<DotScript>().setDot(obj);
                            dots.Add(obj);
                        }
                        if (spacesTaken[i,y] != null ) {blocked = true;}
                    }
                    blocked = false;
                    for (int i = x + 1; i < 8 && !blocked ; i++) {
                        if (spacesTaken[i,y] != null && spacesTaken[i,y].GetComponent<PieceScript>().GetColor().Equals(color)) {blocked = true;}
                        else {
                            obj = Instantiate(dot, new Vector3(i, y, 0), Quaternion.identity);
                            obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                            obj.GetComponent<DotScript>().setDot(obj);
                            dots.Add(obj);
                        }
                        if (spacesTaken[i,y] != null ) {blocked = true;}
                    }
                }
                if (title.Equals("bishop") || title.Equals("queen")) {
                    bool blocked = false;
                    int nextX = x + 1;
                    int nextY = y + 1;
                    while (!blocked && nextX < 8 && nextX >= 0 && nextY < 8 && nextY >= 0) {
                        if (spacesTaken[nextX,nextY] != null) {
                            if (!spacesTaken[nextX,nextY].GetComponent<PieceScript>().GetColor().Equals(color)) {
                                obj = Instantiate(dot, new Vector3(nextX, nextY, 0), Quaternion.identity);
                                obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                                obj.GetComponent<DotScript>().setDot(obj);
                                dots.Add(obj);
                            }
                            blocked = true;
                        }
                        else {
                            obj = Instantiate(dot, new Vector3(nextX, nextY, 0), Quaternion.identity);
                            obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                            obj.GetComponent<DotScript>().setDot(obj);
                            dots.Add(obj);
                        }
                        nextX++;
                        nextY++;
                    }
                    blocked = false;
                    nextX = x - 1;
                    nextY = y + 1;
                    while (!blocked && nextX < 8 && nextX >= 0 && nextY < 8 && nextY >= 0) {
                        if (spacesTaken[nextX,nextY] != null) {
                            if (!spacesTaken[nextX,nextY].GetComponent<PieceScript>().GetColor().Equals(color)) {
                                obj = Instantiate(dot, new Vector3(nextX, nextY, 0), Quaternion.identity);
                                obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                                obj.GetComponent<DotScript>().setDot(obj);
                                dots.Add(obj);
                            }
                            blocked = true;
                        }
                        else {
                            obj = Instantiate(dot, new Vector3(nextX, nextY, 0), Quaternion.identity);
                            obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                            obj.GetComponent<DotScript>().setDot(obj);
                            dots.Add(obj);
                        }
                        nextX--;
                        nextY++;
                    }
                    blocked = false;
                    nextX = x + 1;
                    nextY = y - 1;
                    while (!blocked && nextX < 8 && nextX >= 0 && nextY < 8 && nextY >= 0) {
                        if (spacesTaken[nextX,nextY] != null) {
                            if (!spacesTaken[nextX,nextY].GetComponent<PieceScript>().GetColor().Equals(color)) {
                                obj = Instantiate(dot, new Vector3(nextX, nextY, 0), Quaternion.identity);
                                obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                                obj.GetComponent<DotScript>().setDot(obj);
                                dots.Add(obj);
                            }
                            blocked = true;
                        }
                        else {
                            obj = Instantiate(dot, new Vector3(nextX, nextY, 0), Quaternion.identity);
                            obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                            obj.GetComponent<DotScript>().setDot(obj);
                            dots.Add(obj);
                        }
                        nextX++;
                        nextY--;
                    }
                    blocked = false;
                    nextX = x - 1;
                    nextY = y - 1;
                    while (!blocked && nextX < 8 && nextX >= 0 && nextY < 8 && nextY >= 0) {
                        if (spacesTaken[nextX,nextY] != null) {
                            if (!spacesTaken[nextX,nextY].GetComponent<PieceScript>().GetColor().Equals(color)) {
                                obj = Instantiate(dot, new Vector3(nextX, nextY, 0), Quaternion.identity);
                                obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                                obj.GetComponent<DotScript>().setDot(obj);
                                dots.Add(obj);
                            }
                            blocked = true;
                        }
                        else {
                            obj = Instantiate(dot, new Vector3(nextX, nextY, 0), Quaternion.identity);
                            obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                            obj.GetComponent<DotScript>().setDot(obj);
                            dots.Add(obj);
                        }
                        nextX--;
                        nextY--;
                    }
                }
                if (title.Equals("knight")) {
                    if (x + 1 < 8 && x + 1 >= 0 && y + 2 < 8 && y + 2 >= 0 && (spacesTaken[x+1, y+2] == null || !spacesTaken[x+1,y+2].GetComponent<PieceScript>().GetColor().Equals(color))){
                        obj = Instantiate(dot, new Vector3(x+1, y+2, 0), Quaternion.identity);
                        obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                        obj.GetComponent<DotScript>().setDot(obj);
                        dots.Add(obj);
                    }
                    if (x + 2 < 8 && x + 2 >= 0 && y + 1 < 8 && y + 1 >= 0 && (spacesTaken[x+2, y+1] == null || !spacesTaken[x+2,y+1].GetComponent<PieceScript>().GetColor().Equals(color))){
                        obj = Instantiate(dot, new Vector3(x+2, y+1, 0), Quaternion.identity);
                        obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                        obj.GetComponent<DotScript>().setDot(obj);
                        dots.Add(obj);
                    }
                    if (x - 1 < 8 && x - 1 >= 0 && y + 2 < 8 && y + 2 >= 0 && (spacesTaken[x-1, y+2] == null || !spacesTaken[x-1,y+2].GetComponent<PieceScript>().GetColor().Equals(color))){
                        obj = Instantiate(dot, new Vector3(x-1, y+2, 0), Quaternion.identity);
                        obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                        obj.GetComponent<DotScript>().setDot(obj);
                        dots.Add(obj);
                    }
                    if (x - 2 < 8 && x - 2 >= 0 && y + 1 < 8 && y + 1 >= 0 && (spacesTaken[x-2, y+1] == null || !spacesTaken[x-2,y+1].GetComponent<PieceScript>().GetColor().Equals(color))){
                        obj = Instantiate(dot, new Vector3(x-2, y+1, 0), Quaternion.identity);
                        obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                        obj.GetComponent<DotScript>().setDot(obj);
                        dots.Add(obj);
                    }
                    if (x + 1 < 8 && x + 1 >= 0 && y - 2 < 8 && y - 2 >= 0 && (spacesTaken[x+1, y-2] == null || !spacesTaken[x+1,y-2].GetComponent<PieceScript>().GetColor().Equals(color))){
                        obj = Instantiate(dot, new Vector3(x+1, y-2, 0), Quaternion.identity);
                        obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                        obj.GetComponent<DotScript>().setDot(obj);
                        dots.Add(obj);
                    }
                    if (x + 2 < 8 && x + 2 >= 0 && y - 1 < 8 && y - 1 >= 0 && (spacesTaken[x+2, y-1] == null || !spacesTaken[x+2,y-1].GetComponent<PieceScript>().GetColor().Equals(color))){
                        obj = Instantiate(dot, new Vector3(x+2, y-1, 0), Quaternion.identity);
                        obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                        obj.GetComponent<DotScript>().setDot(obj);
                        dots.Add(obj);
                    }
                    if (x - 1 < 8 && x - 1 >= 0 && y - 2 < 8 && y - 2 >= 0 && (spacesTaken[x-1, y-2] == null || !spacesTaken[x-1,y-2].GetComponent<PieceScript>().GetColor().Equals(color))){
                        obj = Instantiate(dot, new Vector3(x-1, y-2, 0), Quaternion.identity);
                        obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                        obj.GetComponent<DotScript>().setDot(obj);
                        dots.Add(obj);
                    }
                    if (x - 2 < 8 && x - 2 >= 0 && y - 1 < 8 && y - 1 >= 0 && (spacesTaken[x-2, y-1] == null || !spacesTaken[x-2,y-1].GetComponent<PieceScript>().GetColor().Equals(color))){
                        obj = Instantiate(dot, new Vector3(x-2, y-1, 0), Quaternion.identity);
                        obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                        obj.GetComponent<DotScript>().setDot(obj);
                        dots.Add(obj);
                    }
                }
                if (title.Equals("king")) {
                    for (int n = x - 1; n < x + 2; n++) {
                        for (int i = y - 1; i < y + 2; i++) {
                            if (!(n == x && i == y)) {
                                if (n < 8 && n >= 0 && i < 8 && i >= 0 && (spacesTaken[n, i] == null || !spacesTaken[n,i].GetComponent<PieceScript>().GetColor().Equals(color))){
                                    obj = Instantiate(dot, new Vector3(n, i, 0), Quaternion.identity);
                                    obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                                    obj.GetComponent<DotScript>().setDot(obj);
                                    dots.Add(obj);
                                }
                            }
                        }
                    }
                }

            }
            
            
        }

    }

    public void DestroyAllDots() {
        foreach (GameObject obj in dots) {
            if (obj != null) {Destroy(obj);}
        }
        dots.Clear();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Board : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dot;
    public GameObject[] pieces = new GameObject[32];
    
    public static GameObject[,] spacesTaken = new GameObject[8,8];
    public static GameObject selected;
    public static List<GameObject> dots = new List<GameObject>();
    public bool whiteTurn = true;

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
                if (x >= 0 && x < 8 && y >= 0 && y < 8) {spacesTaken[x,y] = obj;}
            }
        }
        
    }

    public void MovePiece(int startX, int startY, int newX, int newY) {
        if (startX >= 0 && startX < 8 && startY >= 0 && startY < 8) {
            selected = spacesTaken[startX,startY];
            if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8) {
                if (spacesTaken[newX,newY] != null) {
                    Destroy(spacesTaken[newX,newY]);
                }
                selected.transform.position = new Vector3(newX, newY, 0);
                selected.GetComponent<PieceScript>().setIsFirstMove(false);
                spacesTaken[newX,newY] = selected;
                spacesTaken[startX,startY] = null; 
            }
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
        if (x >= 0 && x < 8 && y >= 0 && y < 8) {
            GameObject piece = spacesTaken[x,y];
            if (piece != null) {
                GameObject obj;
                if (title.Equals("pawn")) {
                    if (color.Equals("black")) {
                        bool blocked = false;
                        if (y + 1 < 8 && spacesTaken[x, y + 1] != null) {
                            blocked = true;
                        }
                        if (y + 1 < 8 && !blocked) {
                            obj = Instantiate(dot, new Vector3(x, y + 1, 0), Quaternion.identity);
                            obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                            obj.GetComponent<DotScript>().setDot(obj);
                            dots.Add(obj);
                    
                            if (piece.GetComponent<PieceScript>().isFirstMove()) {
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
                        if (y - 1 >= 0 && spacesTaken[x,y-1] != null) {
                            obj = Instantiate(dot, new Vector3(x, y - 1, 0), Quaternion.identity);
                            obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                            obj.GetComponent<DotScript>().setDot(obj);
                            dots.Add(obj);
                            if (piece.GetComponent<PieceScript>().isFirstMove()) {
                                obj = Instantiate(dot, new Vector3(x, y - 2, 0), Quaternion.identity);
                                obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                                obj.GetComponent<DotScript>().setDot(obj);
                                dots.Add(obj);
                            }
                        }
                    }
                }
                else if (title.Equals("tower")) {
                    obj = Instantiate(dot, new Vector3(x, y + 1, 0), Quaternion.identity);
                    obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                    obj.GetComponent<DotScript>().setDot(obj);
                    dots.Add(obj);
                    bool blocked = false;
                    for (int i = y + 2; i < 8 && !blocked ; i++) {
                        if (spacesTaken[x,i] != null) {blocked = true;}
                        obj = Instantiate(dot, new Vector3(x, i, 0), Quaternion.identity);
                        obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                        obj.GetComponent<DotScript>().setDot(obj);
                        dots.Add(obj);
                    }
                    blocked = false;
                    for (int i = y - 1; i >= 0 && !blocked ; i--) {
                        if (spacesTaken[x,i] != null) {blocked = true;}
                        obj = Instantiate(dot, new Vector3(x, i, 0), Quaternion.identity);
                        obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                        obj.GetComponent<DotScript>().setDot(obj);
                        dots.Add(obj);
                    }
                    blocked = false;
                    for (int i = x - 1; i >= 0 && !blocked ; i--) {
                        if (spacesTaken[i,y] != null) {blocked = true;}
                        obj = Instantiate(dot, new Vector3(i, y, 0), Quaternion.identity);
                        obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                        obj.GetComponent<DotScript>().setDot(obj);
                        dots.Add(obj);
                    }
                    blocked = false;
                    for (int i = x + 1; i < 8 && !blocked ; i++) {
                        if (spacesTaken[i,y] != null) {blocked = true;}
                        obj = Instantiate(dot, new Vector3(i, y, 0), Quaternion.identity);
                        obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                        obj.GetComponent<DotScript>().setDot(obj);
                        dots.Add(obj);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Board : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dot;
    public static List<GameObject> pieces = new List<GameObject>();
    
    public static GameObject[,] spacesTaken = new GameObject[8,8];
    public static GameObject selected;
    public static List<GameObject> dots = new List<GameObject>();
    public bool whiteTurn = true;

    void Start()
    {
        pieces.Add();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in pieces) {
            int x = obj.GetComponent<PieceScript>().GetXPos();
            int y = obj.GetComponent<PieceScript>().GetYPos();
            if (x >= 0 && x < 8 && y >= 0 && y < 8) {spacesTaken[x,y] = obj;}
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
            
            if (title.Equals("pawn")) {
                if (color.Equals("black")) {
                    GameObject obj = Instantiate(dot, new Vector3(x, y + 1, 0), Quaternion.identity);
                    obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                    dots.Add(obj);
                    obj = Instantiate(dot, new Vector3(x, y + 2, 0), Quaternion.identity);
                    obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                    dots.Add(obj);
                }
                else {
                    GameObject obj = Instantiate(dot, new Vector3(x, y - 1, 0), Quaternion.identity);
                    obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                    dots.Add(obj);
                    obj = Instantiate(dot, new Vector3(x, y - 2, 0), Quaternion.identity);
                    obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                    dots.Add(obj);
                }
            }
            else if (title.Equals("tower")) {
                if (color.Equals("black")) {
                    GameObject obj = Instantiate(dot, new Vector3(x, y + 1, 0), Quaternion.identity);
                    obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                    dots.Add(obj);
                    obj = Instantiate(dot, new Vector3(x, y + 2, 0), Quaternion.identity);
                    obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                    dots.Add(obj);
                }
                else {
                    GameObject obj = Instantiate(dot, new Vector3(x, y - 1, 0), Quaternion.identity);
                    obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                    dots.Add(obj);
                    obj = Instantiate(dot, new Vector3(x, y - 2, 0), Quaternion.identity);
                    obj.GetComponent<DotScript>().SetPiece(spacesTaken[x,y]);
                    dots.Add(obj);
                }
            }
        }

    }

    public void DestroyAllDots() {
        foreach (GameObject obj in dots) {
            Destroy(obj);
        }
        dots.Clear();
    }

}

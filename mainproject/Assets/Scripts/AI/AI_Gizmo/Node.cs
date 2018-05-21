using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    // Values
    public bool free;           // if is solid or free space
    public Vector3 position;    // Position of the node
    public Node parent;         // parent of the node

    // position on map 
    public int XonMap;
    public int YonMap;

    // G and H values
    public int Gvalue;
    public int Hvalue;
    public int Fvalue
    {
        get
        {
            return Gvalue + Hvalue;
        }
    }

    // Constructor 
    public Node (bool Solid, Vector3 Postion, int xMap, int yMap)
    {
        free = Solid;
        position = Postion;
        XonMap = xMap;
        YonMap = yMap;
    }
}

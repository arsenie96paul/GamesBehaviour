using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Additional class used to create a map using Gizmos 
public class Map : MonoBehaviour {

    // Take position for the start and end points
    public Transform start;
    public Transform end;

    public LayerMask solidObj; // check solid 
    public Vector2 SizeMap;    // size of the map
    public List<Node> road;    // list with the created path
    Node[,] map;

    public float nodeDim;      // dimension of a node
    float nodeR;               // diameter of the node

    // x and y variable of the map dimension
    int Xsize;
    int Ysize;        

    // set up everything and create the map
    void Start()
    {
        nodeR = nodeDim * 2;
        Xsize = Mathf.RoundToInt(SizeMap.x / nodeR);
        Ysize = Mathf.RoundToInt(SizeMap.y / nodeR);

        map = new Node[Xsize, Ysize];

        // start point of the map
        Vector3 StartPoint = transform.position - Vector3.right * SizeMap.x / 2 - Vector3.up * SizeMap.y / 2;

        // create all the map
        for (int i = 0; i < Xsize; i++)
            for (int j = 0; j < Ysize; j++)
            {
                Vector3 points = StartPoint + Vector3.right * (i * nodeR + nodeDim) + Vector3.up * (j * nodeR + nodeDim);
                bool freeSpace = !(Physics.CheckSphere(points, nodeDim, solidObj));
                map[i, j] = new Node(freeSpace, points, i, j);
            }
    }

    // Draw method using Gizmos
    void OnDrawGizmos()
    {
        // draw gizmo cube
        Gizmos.DrawWireCube(transform.position, new Vector3(SizeMap.x, SizeMap.y, 1));

        if (map != null)
        {
            // calculate the position on the map of the start and end point
            Node startN = MapPos(start.position);
            Node endN = MapPos(end.position);

            // for each node in the map, colour everything in the desire way
            foreach (Node n in map)
            {
               // Gizmos.color = (n.walkable) ? Color.white : Color.red;

                if (n.free)
                {
                    Gizmos.color = Color.green;
                }
                if (!n.free)
                {
                    Gizmos.color = Color.black;
                }
                if (startN == n)
                {
                    Gizmos.color = Color.white;
                }
                if (endN == n)
                {
                    Gizmos.color = Color.black;
                }

                // Draw the path
                if (road != null)
                {
                    if (road.Contains(n))
                    {
                        Gizmos.color = Color.cyan;
                    }
                }

                Gizmos.DrawCube(n.position, Vector3.one * (nodeR - .1f));
            }
        }
    }

    // Check node around in order to find neigh
    public List<Node> FindNeighboursOnMap(Node node)
    {
        List<Node> neigh = new List<Node>();

        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                int posX = node.XonMap + i;
                int posY = node.YonMap + j;

                if (posX >= 0 && posX < Xsize && posY >= 0 && posY < Ysize)
                {
                    neigh.Add(map[posX, posY]);
                }
            }
        return neigh;
    }

    // transform the coordinates in position on map
    public Node MapPos(Vector3 posOnMap)
    {
        float percentX = (posOnMap.x + SizeMap.x / 2) / SizeMap.x;
        float percentY = (posOnMap.y + SizeMap.y / 2) / SizeMap.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((Xsize - 1) * percentX);
        int y = Mathf.RoundToInt((Ysize - 1) * percentY);

        return map[x, y];
    }

}
   


using System.Collections.Generic;
using UnityEngine;

public class AI_Algorithm : MonoBehaviour {

    // Position of the 2 main points
    public Transform start;
    public Transform end;
    Map map; // map component

    void Start()
    {
        // get the map component
        map = GetComponent<Map>();
    }

    // Fixed update function contain the a* algorithm
    void FixedUpdate()
    {
        // transform the positions
        Node NodeS = map.MapPos(start.position);
        Node NodeE = map.MapPos(end.position);

        // create open list and close list
        List<Node> openList = new List<Node>();
        HashSet<Node> closeList = new HashSet<Node>();

        // and the start point to the open list
        openList.Add(NodeS);
        float CountList = openList.Count;

        // while list not empty
        while (CountList > 0)
        { 
            Node node = openList[0];
            for (int x = 1; x < openList.Count; x++)
            {
                // check the f value
                if (openList[x].Fvalue < node.Fvalue || openList[x].Fvalue == node.Fvalue)
                {
                    if (openList[x].Hvalue < node.Hvalue)
                        node = openList[x];
                }
            }

            // remove the node from the open list and add it to the close list
            openList.Remove(node);
            closeList.Add(node);

            // find the neighbours
            List<Node> neighbours = new List<Node>();
            neighbours = map.FindNeighboursOnMap(node);

            // if current node is the ned node, create the path
            if (node == NodeE)
            {
                // create the path ( road )
                List<Node> road = new List<Node>();
                Node cNode = NodeE;

                while (cNode != NodeS)
                {
                    road.Add(cNode);
                    cNode = cNode.parent;
                }

                // Create the path in the way back
                road.Reverse();
                map.road = road;

                return;
            }

            foreach (Node neigh in neighbours)
            {
                // if in close list or if solid object skip
                if (!(neigh.free) || closeList.Contains(neigh))
                    continue;
                            
                int cost = node.Gvalue + CalculateDist(node, neigh);

                if (cost < neigh.Gvalue || !openList.Contains(neigh))
                {
                    neigh.Hvalue = CalculateDist(neigh, NodeE);
                    neigh.Gvalue = cost;                   
                    neigh.parent = node;

                    if (!openList.Contains(neigh))
                    {
                        openList.Add(neigh);
                    }
                }
            }
        }
    }

    // calculate distances (createde because the calculation needed multiple times)
    public int CalculateDist(Node first, Node second)
    {
        int dstX = Mathf.Abs(first.XonMap - second.XonMap);
        int dstY = Mathf.Abs(first.YonMap - second.YonMap);

        // got some random values in order to create the calculation
        if (dstX > dstY)
        {
            return 5 * dstY + 3 * (dstX - dstY);
        }
        else
        {
            return 5 * dstX + 3 * (dstY - dstX);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Properly A* algorithm, used for path finding
public class AI : MonoBehaviour {

    GameObject[] tree; // list of objects from the scene
    GameObject player; // player object

    // Start and end point
    Nodes startNode = null;
    Nodes endNode = null;
    Nodes[] allnodes;

    public int something;

    // Use this for initialization
    void Start () {

        // find all nodes in the scene 
        tree = GameObject.FindGameObjectsWithTag("Node");
    }

    // Update is called once per frame
    void FixedUpdate () {

        player = GameObject.FindGameObjectWithTag("Player");

        // Create list of all nodes
        Nodes[] treeBody = new Nodes[tree.Length];        

        // Get component
        for (int i = 0; i <tree.Length; i ++)
        {
            treeBody[i] = tree[i].GetComponent<Nodes>();
        }
        allnodes = treeBody;

        // take start and end point, add colours for each node
        foreach (Nodes n in treeBody)
        {
            if (n.Special == "start")
            {
                startNode = n;
                n.nodeC = Color.red;
            }
            if (n.Special == "end")
            {
                endNode = n;
                n.nodeC = Color.blue;
            }
            if (n.Special == "")
            {
                n.nodeC = Color.grey;
            }
            if (n.solid)
            {
                n.nodeC = Color.black;
            }
        }
        aStar(startNode, endNode, treeBody);
    }

    // Go back
    void trace (Nodes start, Nodes end)
    {
        List<Nodes> path = new List<Nodes>();
        Nodes current = end;

        while (current != startNode)
        {
            path.Add(current);
            current = current.parent;
        }
        path.Reverse();
    }

    // calculate the distance form a node to another
    float Distance (Nodes a, Nodes b)
    {
        float xdist = Mathf.Abs(a.gameObject.transform.position.x - b.gameObject.transform.position.x) ;
        float ydist = Mathf.Abs(a.gameObject.transform.position.y - b.gameObject.transform.position.y) ;

        if (xdist > ydist)
        {
            return xdist + 5 * (xdist - ydist);
        }

        return ydist + 5 * (ydist - xdist);
    }

    List<Nodes> checkNeigh(Nodes node)
    {
        List<Nodes> neighbour = new List<Nodes>(); // Create list of nodes around current node

        // neigboirs are the nodes at distance one on x and y axes 
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                float Ax = node.position.x + i;
                float Ay = node.position.y + j;

                // if exist a node in that position, add it to the neighbour list
                foreach (Nodes n in allnodes)
                {
                    // check if exist
                    if (n.position.x == Ax && n.position.y == Ay)
                    {
                        neighbour.Add(n); // add node
                    }
                }
                
            }

        return neighbour; // return list of neighbours
    }

    // A* Algorithm
    void aStar ( Nodes start, Nodes end, Nodes[] treeBody)
    {
        // Open and Close list of nodes
        List<Nodes> open = new List<Nodes>();
        List<Nodes> close = new List<Nodes>();

        open.Add(startNode);

        while (open.Count > 0)
        {
            Nodes currentNode = open[0];        // start node become the current node
            currentNode.nodeC = Color.green;    // current node will have colour green

            something = open.Count;
            // for each node
            for (int i = 0; i < open.Count; i++)
            {
                // check low cost
                if (open[i].total < currentNode.total || open[i].total == currentNode.total && open[i].Hvalue < currentNode.Hvalue)
                {
                    currentNode = open[i]; // move to the next node
                }
            }

            // remove current node from open list and add it to the close list
            open.Remove(currentNode);
            close.Add(currentNode);

            //if end, trace back
            if (currentNode == endNode)
            {
                //trace(startNode, endNode);
                return;
            }

            // go trough all neighbour
            foreach (Nodes neigh in checkNeigh(currentNode))
            {
                // if neighbour closed or solid skip it
                if (neigh.solid || close.Contains(neigh))
                {
                    continue;
                }

                // claculate distance
                float moveTo = currentNode.Gvalue + Distance(currentNode, neigh);

                if (moveTo < neigh.Gvalue || !open.Contains(neigh))
                {
                    neigh.Gvalue = moveTo;
                    neigh.Hvalue = Distance(neigh, endNode);
                    neigh.parent = currentNode;

                    if (!open.Contains(neigh))
                    {
                        open.Add(neigh);
                    }
                }
            }
        }
    }

}



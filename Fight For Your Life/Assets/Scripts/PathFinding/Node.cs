using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Node
{
    //It's current position
    public Vector3Int positionNode { get; set; }

    //It's parent node
    public Node parentNode;


    //G cost is distance from starting node
    public int G { get; set; }
    //H cost (Heuristic) is distance from end node
    public int H { get; set; }
    //F cost is G cost and H cost combined
    public int F { get { return G + H; } }
    

}

class NodeComparer : IComparer<Node>
{
    public int Compare(Node x, Node y)
    {
        if (x.F < y.F) return -1;
        if (x.F > y.F) return 1;
        return 0;
    }
}

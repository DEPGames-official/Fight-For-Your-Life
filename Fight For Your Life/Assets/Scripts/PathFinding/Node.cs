using System.Collections.Generic;
using UnityEngine;

public class Node
{
    //It's current position
    public Vector3Int positionNode { get; set; }

    //It's parent node
    public Node parentNode;


    //G cost is distance from starting node
    public float G { get; set; }
    //H cost (Heuristic) is distance from end node
    public float H { get; set; }
    //F cost is G cost and H cost combined
    public float F { get { return G + H; } }

    public string nodePosition { get; set; }
}



using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Node
{
    public Vector3Int positionNode { get; set; }

    //G cost is distance from starting node
    public int G { get; set; }
    //H cost (Heuristic) is distance from end node
    public int H { get; set; }
    //F cost is G cost and H cost combined
    public int F { get { return G + H; } }
    

}

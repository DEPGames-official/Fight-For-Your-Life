using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Node
{
    //G cost is distance from starting node
    int G;
    //H cost (Heuristic) is distance from end node
    int H;
    //F cost is G cost and H cost combined
    int F;

    Vector3Int positionNode;

    public Vector3Int SetPosition(Vector3Int position)
    {
        positionNode = position;
        return positionNode;
    }

    public void SetCosts(int GCost, int HCost, int FCost)
    {
        GCost = G;
        HCost = H;
        FCost = F;
    }


}

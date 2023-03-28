using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathGenerator
{
    GameObject start;
    GameObject end;

    List<Vector3> pathway = new List<Vector3>();
    public void GeneratePath()
    {
        
    }

    void GetGridPath()
    {
        Transform startTransform = start.transform;
        Transform endTransform = end.transform;

        Vector3Int startTransformInt = Vector3Int.FloorToInt(startTransform.position);
        Vector3Int endTransformInt = Vector3Int.FloorToInt(endTransform.position);

        

        
    }
}

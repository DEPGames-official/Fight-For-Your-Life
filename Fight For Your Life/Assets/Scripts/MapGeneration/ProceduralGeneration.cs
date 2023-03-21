using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField]
    Tilemap mainLand;
    public GameObject square;
    public TileBase grassTile;
    public float generationOffset;
    public Vector3 startPos, endPos;

    public float camHeight, camWidth;
    public Camera cam;

    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetCamArea();
        FillArea();
    }

    void GetCamArea()
    {
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;

        var rightBound = cam.transform.position.x + (camWidth / 2f);
        var leftBound = cam.transform.position.x - (camWidth / 2f);
        var upBound = cam.transform.position.y + (camHeight / 2f);
        var bottomBound = cam.transform.position.y - (camHeight / 2f);

        startPos = new Vector3(leftBound - generationOffset, upBound + generationOffset);
        endPos = new Vector3(rightBound + generationOffset, bottomBound - generationOffset);
    }

    void FillArea()
    {
        if (player.inputX != 0f || player.inputY != 0f)
        {
            Vector3Int cellPosStart = mainLand.WorldToCell(startPos);
            Vector3Int cellPosEnd = mainLand.WorldToCell(endPos);

            //Move x position first then go down 1 then set x to 0 up until the cellstartpos = cellendpos

            for (int x = cellPosStart.x; x < cellPosEnd.x; x++)
            {
                for (int y = cellPosStart.y; y > cellPosEnd.y; y--)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, cellPosStart.z);
                    if (!mainLand.HasTile(tilePosition))
                    {
                        mainLand.SetTile(tilePosition, grassTile);
                        print(tilePosition);
                    }

                }
            }
        }
        

        
        
    }

}

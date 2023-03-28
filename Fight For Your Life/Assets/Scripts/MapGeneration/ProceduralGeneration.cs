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
    //Min x range from player
    public int spawnXRange;
    //The random distance between min range and this range
    public int spawnXOffset;
    //The random distance between min range and this range
    public int spawnYOffset;
    //Min y range from player
    public int spawnYRange;
    public Vector3 spawnPosition;
    public TileBase grassTile;
    public float generationOffset;
    public Vector3 startPos, endPos;

    public float camHeight, camWidth;
    public Camera cam;

    public PlayerController player;
    PathGenerator pathGenerator = new PathGenerator();
    // Start is called before the first frame update
    void Start()
    {
        SpawnSquare();
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

    //Use square to simulate spawning boots to find to keep you afloat when the world gets flooded
    void SpawnSquare()
    {
        print("Nom?");
        int spawnX = Random.Range(spawnXRange, spawnXOffset);
        int spawnY = Random.Range(spawnYRange, spawnYOffset);

        print(spawnX);
        print(spawnY);

        spawnPosition = new Vector3(spawnX, spawnY, 0);

        GameObject.Instantiate(square, spawnPosition, Quaternion.identity);
        print(spawnPosition);
    }

    void FillArea()
    {
        if (player.inputX != 0f || player.inputY != 0f)
        {
            Vector3Int cellPosStart = mainLand.WorldToCell(startPos);
            Vector3Int cellPosEnd = mainLand.WorldToCell(endPos);

            //Move x position first then load all of y then move x 1 more up until the cellstartpos = cellendpos
            for (int x = cellPosStart.x; x < cellPosEnd.x; x++)
            {
                for (int y = cellPosStart.y; y > cellPosEnd.y; y--)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, cellPosStart.z);
                    if (!mainLand.HasTile(tilePosition))
                    {
                        mainLand.SetTile(tilePosition, grassTile);
                    }

                }
            }
        }
        

        
        
    }

}

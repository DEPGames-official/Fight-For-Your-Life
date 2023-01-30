using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Tilemaps;

public class AStarPathFinding : MonoBehaviour
{
    public Grid tileMapGrid;
    public GameObject player, enemy;

    public Tilemap mainWorldTilemap;
    public Tilemap tileMapBarriers;

    public Vector3Int distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        distanceToPlayer = tileMapGrid.WorldToCell(player.transform.position - enemy.transform.position);

        print(tileMapGrid.WorldToCell(player.transform.position));
        print(tileMapGrid.WorldToCell(enemy.transform.position));

        TileBase tileInfo = mainWorldTilemap.GetTile(tileMapGrid.WorldToCell(player.transform.position));

        print("ON TILE: " + tileInfo);

        print("Distane to player" + distanceToPlayer);
    }
}

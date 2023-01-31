using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStar : MonoBehaviour
{
    [SerializeField]
    //Start
    GameObject enemy;
    Transform enemyTransform;
    [SerializeField]
    Vector3Int startPos;


    [SerializeField]
    //Target
    GameObject player;
    Transform playerTransform;
    Vector3Int endPos;
    
    [SerializeField]
    Tilemap mainWorldTilemap;

    [SerializeField]
    Grid mainGrid;

    Node node = new Node();

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
        enemyTransform = enemy.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        startPos = mainGrid.WorldToCell(enemyTransform.position);
        endPos = mainGrid.WorldToCell(playerTransform.position);

        print(node.SetPosition(endPos));

        //print($"Distance is: {endPos - startPos}");
    }


    //Get top, bottom, left, right, top left, top right, bottom left, bottom right nodes
    void NodePositions()
    {
        
    }


}

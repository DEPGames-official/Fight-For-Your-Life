using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    Node startNode = new Node();

    List<Node> openList = new List<Node>();
    List<Node> closedList = new List<Node>();

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

        NodePositions();

        //print($"Distance is: {endPos - startPos}");
    }


    //Get top, bottom, left, right, top left, top right, bottom left, bottom right nodes
    void NodePositions()
    {
        startNode.positionNode = startPos;
        startNode.G = 0;
        startNode.H = GetManhattanDistance(startNode.positionNode, endPos);

        
        openList.Add(startNode);

        Node nodeWithLowestF = openList.OrderBy(Node => Node.F).First();

        print(nodeWithLowestF.F);

    }

    void LookAtNodesAround()
    {
        

        foreach(var node in openList)
        {
            

            var upNode = new Node
            {
                positionNode = node.positionNode + Vector3Int.up,
            };
            upNode.G = CalculateGCost(upNode.positionNode, startNode.positionNode);
            upNode.H = GetManhattanDistance(upNode.positionNode, endPos);

            openList.Add(upNode);

            var rightNode = new Node
            {
                positionNode = node.positionNode + Vector3Int.right
            };
            rightNode.G = CalculateGCost(rightNode.positionNode, startNode.positionNode);
            rightNode.H = GetManhattanDistance(rightNode.positionNode, endPos);

            openList.Add(rightNode);

            var downNode = new Node
            {
                positionNode = node.positionNode + Vector3Int.down
            };
            downNode.G = CalculateGCost(downNode.positionNode, startNode.positionNode);
            downNode.H = GetManhattanDistance(downNode.positionNode, endPos);

            openList.Add(downNode);

            var leftNode = new Node
            {
                positionNode = node.positionNode + Vector3Int.left
            };
            leftNode.G = CalculateGCost(leftNode.positionNode, startNode.positionNode);
            leftNode.H = GetManhattanDistance(leftNode.positionNode, endPos);

            openList.Add(leftNode);
        }
    }

    int GetManhattanDistance(Vector3Int currentNode, Vector3Int endNode)
    {
        int distanceX = Math.Abs(currentNode.x - endNode.x);
        int distanceY = Math.Abs(currentNode.y - endNode.y);

        //return distance from currentNode to endNode
        return distanceX + distanceY;
    }

    int CalculateGCost(Vector3Int currentNode, Vector3Int startNode)
    {
        int distanceX = Math.Abs(currentNode.x - startNode.x);
        int distanceY = Math.Abs(currentNode.y - startNode.y);

        return distanceX + distanceY;
    }


}

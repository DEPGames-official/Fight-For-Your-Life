using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStar : MonoBehaviour
{
    [SerializeField]
    //Start
    GameObject enemy;
    Transform enemyTransform;
    Vector3Int enemyPositionRounded;
    Rigidbody2D enemyRigidBody;
    public float enemySpeed;
    [SerializeField]
    EnemyController enemyController;
    [SerializeField]
    Vector3Int startPos;


    [SerializeField]
    //Target
    GameObject player;
    Transform playerTransform;
    [SerializeField]
    Vector3Int endPos;
    
    [SerializeField]
    Tilemap mainWorldTilemap;
    [SerializeField]
    TileBase nodeWithLowestCost;


    [SerializeField]
    Grid mainGrid;

    Node startNode = new Node();

    [SerializeField]
    //convert to priority queue
    //List<Node> openList = new List<Node>();

    Dictionary<string, Node> openDictionary = new Dictionary<string, Node>();
    //HashSet<Node> openList = new HashSet<Node>();
    [SerializeField]
    //List<Node> closedList = new List<Node>();

    Dictionary<string, Node> closedDictionary = new Dictionary<string, Node>();

    HashSet<Node> surroundingNodes = new HashSet<Node>();

    public bool processNodesBool;

    // Start is called before the first frame update
    void Start()
    {

        playerTransform = player.GetComponent<Transform>();
        enemyTransform = enemy.GetComponent<Transform>();

        startPos = mainGrid.WorldToCell(enemyTransform.position);
        endPos = mainGrid.WorldToCell(playerTransform.position);

        enemyRigidBody = enemy.GetComponent<Rigidbody2D>();

        NodePositions();

        print(openDictionary.Count);
        
    }

    // Update is called once per frame
    void Update()
    {

        MoveTargetToPosition();

        if (processNodesBool == true)
        {
            Stopwatch timeToExecute = new Stopwatch();
            timeToExecute.Start();
            processNodes();
            timeToExecute.Stop();

            print("It took:  " + timeToExecute.ElapsedMilliseconds);

            foreach (Node node in closedDictionary.Values)
            {
                mainWorldTilemap.SetTile(node.positionNode, nodeWithLowestCost);
            }

            
            
            
            
            //processNodesBool = false;
        }
    }


    //Get top, bottom, left, right, top left, top right, bottom left, bottom right nodes
    void NodePositions()
    {
       
        startNode.positionNode = startPos;
        startNode.G = 0;
        startNode.H = GetManhattanDistance(startNode.positionNode, endPos);
        //print($"Getting start and end positions and add startNode to openlist to start it: Starting node: {startNode.positionNode}");

        openDictionary.Clear();
        closedDictionary.Clear();
        surroundingNodes.Clear();

        openDictionary.Add(TotalDictionaryAmount.ToString() ,startNode);
        
    }

    void processNodes()
    {
        var minFValue = openDictionary.Values.Min(node => node.F);
        var nodesWithLowestFCost = openDictionary.Where(node => node.Value.F == minFValue).ToDictionary(node => node.Key, node => node.Value);

        foreach(KeyValuePair<string, Node> node in nodesWithLowestFCost)
        {
            //print(node.Value.positionNode);

            if (node.Value.positionNode == endPos)
            {
                //print($"Position found end: {node.Value.positionNode}");

                closedDictionary.Add(node.Key ,node.Value);

                RetracePath(node.Value);
                

                //Do stuff here to retrace position
            }
            else if (node.Value.positionNode != endPos)
            {
                LookAtNodesAround(node.Value);
                if (!closedDictionary.ContainsKey(node.Key))
                {
                    closedDictionary.Add(node.Key, node.Value);
                }
                
                openDictionary.Remove(node.Key);
                //processNodes();
            }

        }

        

    }

    List<Node> path = new List<Node>();
    void RetracePath(Node endNode)
    {
        Node current = endNode;
        while (current != startNode)
        {
            path.Add(current);
            current = current.parentNode;
        }

        path.Reverse();

        foreach (Node nodePosition in path)
        {
            //print(nodePosition.positionNode);
        }
    }

    
    void MoveTargetToPosition()
    {
        enemyPositionRounded = new Vector3Int(Mathf.RoundToInt(enemyTransform.position.x), Mathf.RoundToInt(enemyTransform.position.y), Mathf.RoundToInt(enemyTransform.position.z));
        if (path.Count > 0) 
        {
            if (enemyPositionRounded != path[0].positionNode)
            {
                Vector3 direction = (path[0].positionNode - enemyTransform.position).normalized;
                //print(direction);
                enemyTransform.Translate(direction * enemySpeed * Time.deltaTime);

                //enemyController.MoveEnemy(direction.x, direction.y, enemySpeed);


            }
            else if (enemyPositionRounded == path[0].positionNode)
            {
                path.Remove(path[0]);
            }
        }
        
    }

    public int TotalDictionaryAmount
    {
        get
        {
            return openDictionary.Count + closedDictionary.Count;
        }
    }


    //Add all of them to list then once DONE going through list THEN add it to Open List
    void LookAtNodesAround(Node focusNode)
    {
        

        var upNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.up,
            parentNode = focusNode
        };
        //upNode.G = CalculateGCost(upNode.positionNode, startNode.positionNode);
        //upNode.H = GetManhattanDistance(upNode.positionNode, endPos);

        surroundingNodes.Add(upNode);

        var rightNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.right,
            parentNode = focusNode
        };
        //rightNode.G = CalculateGCost(rightNode.positionNode, startNode.positionNode);
       //rightNode.H = GetManhattanDistance(rightNode.positionNode, endPos);

        surroundingNodes.Add(rightNode);

        var downNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.down,
            parentNode = focusNode
        };
        //downNode.G = CalculateGCost(downNode.positionNode, startNode.positionNode);
        //downNode.H = GetManhattanDistance(downNode.positionNode, endPos);

        surroundingNodes.Add(downNode);

        var leftNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.left,
            parentNode = focusNode
        };
        //leftNode.G = CalculateGCost(leftNode.positionNode, startNode.positionNode);
        //leftNode.H = GetManhattanDistance(leftNode.positionNode, endPos);

        surroundingNodes.Add(leftNode);

        //Sort out possible duplicate nodes
        HashSet<Node> nodesToRemove = new HashSet<Node>();

        foreach (var surroundNode in surroundingNodes)
        {
            //KeyValuePair<string, Node> duplicateNode = new KeyValuePair<string, Node>();
            KeyValuePair<string, Node> duplicateNode = new KeyValuePair<string, Node>();
            duplicateNode = openDictionary.Concat(closedDictionary).FirstOrDefault(node => node.Value.positionNode == surroundNode.positionNode);

            if (duplicateNode.Value != null)
            {
                //print($"Removed node: {duplicateNode.Key} ");
            }
            else
            {
                surroundNode.G = CalculateGCost(surroundNode.positionNode, startNode.positionNode);
                surroundNode.H = GetManhattanDistance(surroundNode.positionNode, endPos);

                if (!openDictionary.ContainsKey(TotalDictionaryAmount.ToString()))
                {
                    openDictionary.Add(TotalDictionaryAmount.ToString(), surroundNode);
                }
            }
            
        }
        surroundingNodes.Clear();

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

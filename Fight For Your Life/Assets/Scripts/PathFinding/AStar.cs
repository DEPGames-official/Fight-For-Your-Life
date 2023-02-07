using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Burst;
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
    [SerializeField]
    PlayerController playerController;
    Transform playerTransform;
    [SerializeField]
    Vector3Int endPos;

    [SerializeField]
    Tilemap mainWorldTilemap;
    [SerializeField]
    TileBase nodeWithLowestCost;


    [SerializeField]
    Grid mainGrid;

    Node startNode;

    [SerializeField]
    //convert to priority queue
    //List<Node> openList = new List<Node>();

    Dictionary<string, Node> openDictionary = new Dictionary<string, Node>();
    //HashSet<Node> openList = new HashSet<Node>();
    [SerializeField]
    //List<Node> closedList = new List<Node>();

    Dictionary<string, Node> closedDictionary = new Dictionary<string, Node>();

    HashSet<Node> surroundingNodes = new HashSet<Node>();

    public bool startAlgorithm;
    public bool tracedPath;

    // Start is called before the first frame update
    void Start()
    {

        playerTransform = player.GetComponent<Transform>();
        enemyTransform = enemy.GetComponent<Transform>();



        enemyRigidBody = enemy.GetComponent<Rigidbody2D>();




    }

    // Update is called once per frame
    void Update()
    {
        startPos = mainGrid.WorldToCell(enemyTransform.position);
        endPos = mainGrid.WorldToCell(playerTransform.position);

        //If openDictionary not empty then start processing
        if (openDictionary.Count != 0)
        {
            

            foreach (Node node in closedDictionary.Values)
            {
                mainWorldTilemap.SetTile(node.positionNode, nodeWithLowestCost);
            }
        }

        if (startAlgorithm == true)
        {
            RunAlgorithm();
            

            startAlgorithm = false;

        }

        if (openDictionary.Count != 0)
        {
            //Stopwatch timer = new Stopwatch();
            //timer.Start();

            processNodes();

            //timer.Stop();

            //print("Time to process (milliseconds): " + timer.ElapsedTicks);
        }
        





        MoveTargetToPosition();

    }

   
    //Get top, bottom, left, right, top left, top right, bottom left, bottom right nodes
    void SetStart()
    {
        openDictionary = new Dictionary<string, Node>();
        closedDictionary = new Dictionary<string, Node>();
        startNode = new Node
        {
            positionNode = startPos,
            G = 0
        };
        startNode.H = GetManhattanDistance(startNode.positionNode, endPos);

        openDictionary.Add(TotalDictionaryAmount.ToString(), startNode);

    }

    void RunAlgorithm()
    {
        SetStart();
        
    }

    void processNodes()
    {
        var minFValue = openDictionary.Values.Min(node => node.F);
        var nodesWithLowestFCost = openDictionary.Where(node => node.Value.F == minFValue).ToDictionary(node => node.Key, node => node.Value);

        foreach (KeyValuePair<string, Node> current in nodesWithLowestFCost)
        {
            if (current.Value.positionNode == endPos)
            {
                RetracePath(current.Value);
                openDictionary.Clear();
                startAlgorithm = true;
                break;
            }
            else if (current.Value.positionNode != endPos)
            {
                LookAtNodesAround(current.Value);
            }

            if (!closedDictionary.ContainsKey(current.Key))
            {
                closedDictionary.Add(current.Key, current.Value);
            }
            openDictionary.Remove(current.Key);


        }



    }

    List<Node> path = new List<Node>();
    void RetracePath(Node endNode)
    {
        if (!path.Contains(endNode))
        {
            Node current = endNode;
            while (current != startNode)
            {

                path.Add(current);
                current = current.parentNode;
            }

            path.Reverse();
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
                enemyTransform.Translate(direction * enemySpeed * Time.deltaTime);
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
        surroundingNodes = new HashSet<Node>();

        var upNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.up,
        };

        var upRightNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.up + Vector3Int.right,
            Square = true,
        };

        surroundingNodes.Add(upNode);
        surroundingNodes.Add(upRightNode);

        var rightNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.right,
        };

        var downRightNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.down + Vector3Int.right,
            Square = true,
        };


        surroundingNodes.Add(rightNode);
        surroundingNodes.Add(downRightNode);

        var downNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.down,
        };

        var downLeftNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.down + Vector3Int.left,
            Square = true,
        };

        

        surroundingNodes.Add(downNode);
        surroundingNodes.Add(downLeftNode);

        var leftNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.left,
        };

        var leftUpNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.left + Vector3Int.right,
            Square = true,
        };

        surroundingNodes.Add(leftNode);
        surroundingNodes.Add(leftUpNode);

        //Sort out possible duplicate nodes
        HashSet<Node> nodesToRemove = new HashSet<Node>();

        foreach (var surroundNode in surroundingNodes)
        {
            KeyValuePair<string, Node> duplicateNode = new KeyValuePair<string, Node>();
            //duplicateNode = openDictionary.Concat(closedDictionary).FirstOrDefault(node => node.Value.positionNode == surroundNode.positionNode);
            duplicateNode = openDictionary.Concat(closedDictionary).FirstOrDefault(node => node.Value.positionNode == surroundNode.positionNode);

            if (duplicateNode.Value == null)
            {
                surroundNode.G = CalculateGCost(surroundNode.positionNode, startNode.positionNode) * 10;
                surroundNode.H = GetManhattanDistance(surroundNode.positionNode, endPos) * 10;
                surroundNode.parentNode = focusNode;

                if (surroundNode.Square == true)
                {
                    surroundNode.G *= 1.4f;
                    surroundNode.H += 1.4f;
                }

                if (!openDictionary.ContainsKey(TotalDictionaryAmount.ToString()))
                {
                    openDictionary.Add(TotalDictionaryAmount.ToString(), surroundNode);
                }
            }
            /*else
            {
                surroundNode.G = CalculateGCost(surroundNode.positionNode, startNode.positionNode) * 10;
                surroundNode.H = GetManhattanDistance(surroundNode.positionNode, endPos) * 10;

                //Make sure start node ("0") doesn't get a parentNode
                if (duplicateNode.Key != "0")
                {
                    surroundNode.parentNode = focusNode;
                }

                if (surroundNode.Square == true)
                {
                    surroundNode.G *= 1.4f;
                    surroundNode.H += 1.4f;
                }

                if (!openDictionary.ContainsKey(duplicateNode.Key))
                {
                    openDictionary.Add(duplicateNode.Key, surroundNode);
                }
                if (closedDictionary.ContainsKey(duplicateNode.Key))
                {
                    closedDictionary.Remove(duplicateNode.Key);
                }

            }*/

        }

    }



    int GetManhattanDistance(Vector3Int currentNode, Vector3Int endNode)
    {
        int distanceX = Math.Abs(currentNode.x - endNode.x);
        int distanceY = Math.Abs(currentNode.y - endNode.y);

        //return distance from currentNode to endNode
        return distanceX + distanceY;
    }

    float CalculateGCost(Vector3Int currentNode, Vector3 startNode)
    {
        float distanceX;
        float distanceY;

        distanceX = Mathf.Abs(currentNode.x - startNode.x);
        distanceY = Mathf.Abs(currentNode.y - startNode.y);

        return distanceX + distanceY;
    }


}

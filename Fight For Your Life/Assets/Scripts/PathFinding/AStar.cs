using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;
//REWRITE
public class AStar : MonoBehaviour
{

    [SerializeField]
    //Gameobject going to target
    public GameObject seeker;
    public Transform seekerTransform;
    public float seekerSpeed;
    [SerializeField]
    Vector3Int seekerPos;


    [SerializeField]
    //Target of the seeker
    public GameObject target;
    public Transform targetTransform;
    [SerializeField]
    public Vector3Int targetPos;

    [SerializeField]
    public Tilemap mainWorldTilemap;
    [SerializeField]
    public TileBase nodeWithLowestCost;


    [SerializeField]
    public Grid mainGrid;

    Node startNode;

    [SerializeField]
    Dictionary<string, Node> openDictionary = new Dictionary<string, Node>();
    [SerializeField]
    Dictionary<string, Node> closedDictionary = new Dictionary<string, Node>();

    HashSet<Node> surroundingNodes = new HashSet<Node>();

    public List<Node> path = new List<Node>();

    //public bool startAlgorithm;
    //public bool tracedPath;

    public Stopwatch timeToRun = new Stopwatch();

    // Start is called before the first frame update
    void Start()
    {

        //targetTransform = target.GetComponent<Transform>();
        //seekerTransform = seeker.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {

        RunPathFind();

        

    }


    public void RunPathFind()
    {

        seekerPos = mainGrid.WorldToCell(seekerTransform.position);
        targetPos = mainGrid.WorldToCell(targetTransform.position);

        foreach (Node node in path)
        {
            mainWorldTilemap.SetTile(node.positionNode, nodeWithLowestCost);
        }

        timeToRun.Start();
        if (timeToRun.ElapsedMilliseconds > 500)
        {
            timeToRun.Stop();
            timeToRun.Reset();

            Stopwatch speed = new Stopwatch();
            speed.Start();
            
            SetStart();

            speed.Stop();
            print(speed.ElapsedTicks);
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
            positionNode = seekerPos,
            G = 0
        };
        startNode.H = GetDistance(startNode.positionNode, targetPos);

        openDictionary.Add(TotalDictionaryAmount.ToString(), startNode);

        processNodes();
    }

    void processNodes()
    {
        var minFValue = openDictionary.Values.Min(node => node.F);
        var nodesWithLowestFCost = openDictionary.Where(node => node.Value.F == minFValue).OrderBy(x => x.Value.H).ToDictionary(node => node.Key, node => node.Value);


        foreach (KeyValuePair<string, Node> current in nodesWithLowestFCost)
        {

            if (!closedDictionary.ContainsKey(current.Key))
            {
                closedDictionary.Add(current.Key, current.Value);
            }
            openDictionary.Remove(current.Key);

            if (current.Value.positionNode == targetPos)
            {
                RetracePath(current.Value);
                openDictionary.Clear();
                //startAlgorithm = true;
                return;
            }
            else if (current.Value.positionNode != targetPos)
            {
                LookAtNodesAround(current.Value);
            }




        }
        processNodes();



    }

    void RetracePath(Node endNode)
    {
        path = new List<Node>();
        if (!path.Contains(endNode))
        {
            Node current = endNode;
            while (current != startNode)
            {
                //print("Path " + current.positionNode + " " + current.nodePosition);
                //print(path.Count);
                path.Add(current);
                current = current.parentNode;
            }

            path.Reverse();
        }

    }


    void MoveTargetToPosition()
    {
        //seekerPositionRounded = new Vector3Int(Mathf.RoundToInt(seekerTransform.position.x), Mathf.RoundToInt(seekerTransform.position.y), Mathf.RoundToInt(seekerTransform.position.z));
        if (path.Count > 0)
        {
            if (seekerPos != path[0].positionNode)
            {
                Vector3 direction = (path[0].positionNode - seekerTransform.position).normalized;
                seekerTransform.Translate(direction * seekerSpeed * Time.deltaTime);
            }
            else if (seekerPos == path[0].positionNode)
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
            nodePosition = "UpNode",
        };

        var upRightNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.up + Vector3Int.right,
            nodePosition = "UpRightNode",
        };

        surroundingNodes.Add(upNode);
        surroundingNodes.Add(upRightNode);

        var rightNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.right,
            nodePosition = "RightNode",
        };

        var downRightNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.down + Vector3Int.right,
            nodePosition = "DownRightNode",
        };

        surroundingNodes.Add(rightNode);
        surroundingNodes.Add(downRightNode);

        var downNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.down,
            nodePosition = "DownNode",
        };

        var downLeftNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.down + Vector3Int.left,
            nodePosition = "DownLeftNode",
        };



        surroundingNodes.Add(downNode);
        surroundingNodes.Add(downLeftNode);

        var leftNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.left,
            nodePosition = "LeftNode",
        };

        var leftUpNode = new Node
        {
            positionNode = focusNode.positionNode + Vector3Int.left + Vector3Int.up,
            nodePosition = "LeftUpNode",
        };

        surroundingNodes.Add(leftNode);
        surroundingNodes.Add(leftUpNode);

        //Sort out possible duplicate nodes
        HashSet<Node> nodesToRemove = new HashSet<Node>();

        foreach (var surroundNode in surroundingNodes)
        {

            //print(surroundNode.F);


            KeyValuePair<string, Node> isClosed = new KeyValuePair<string, Node>();
            KeyValuePair<string, Node> isOpen = new KeyValuePair<string, Node>();
            //duplicateNode = openDictionary.Concat(closedDictionary).FirstOrDefault(node => node.Value.positionNode == surroundNode.positionNode);
            //duplicateNode = openDictionary.Concat(closedDictionary).FirstOrDefault(node => node.Value.positionNode == surroundNode.positionNode);
            isClosed = closedDictionary.FirstOrDefault(node => node.Value.positionNode == surroundNode.positionNode);
            isOpen = openDictionary.FirstOrDefault(node => node.Value.positionNode == surroundNode.positionNode);

            if (isClosed.Value != null)
            {
                continue;
            }

            if (isOpen.Value != null)
            {
                Node afterNode = new Node();
                afterNode.G = isOpen.Value.G;
                afterNode.H = isOpen.Value.H;
                afterNode.parentNode = isOpen.Value.parentNode;
                afterNode.positionNode = isOpen.Value.positionNode;
                afterNode.nodePosition = isOpen.Value.nodePosition;

                afterNode.G = GetDistance(afterNode.positionNode, startNode.positionNode);
                afterNode.H = GetDistance(afterNode.positionNode, targetPos);

                //print("afternode g = " + afterNode.G + " " + "isopen g = " + isOpen.Value.G);

                if (afterNode.G < isOpen.Value.G)
                {
                    var openKey = isOpen.Key;

                    openDictionary.Remove(openKey);
                    openDictionary.Add(openKey, afterNode);

                    print(openKey + " " + " was swapped out");
                }
                continue;
            }
            else if(!openDictionary.ContainsKey(TotalDictionaryAmount.ToString()))
            {
                surroundNode.G = GetDistance(surroundNode.positionNode, startNode.positionNode);
                surroundNode.H = GetDistance(surroundNode.positionNode, targetPos);
                surroundNode.parentNode = focusNode;

               
                openDictionary.Add(TotalDictionaryAmount.ToString(), surroundNode);
                
                
            }

        }

    }

    //returns if the first node has a bigger value
    bool ComparePathDistance(Node firstNode, Node secondNode)
    {
        if (firstNode.G > secondNode.G)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    int GetDistance(Vector3Int currentNode, Vector3Int endNode)
    {
        int distanceX = Math.Abs(currentNode.x - endNode.x);
        int distanceY = Math.Abs(currentNode.y - endNode.y);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        else
        {
            return 14 * distanceX + 10 * (distanceY - distanceX);
        }
    }

    /*float CalculateGCost(Vector3Int currentNode, Vector3Int startNode)
    {
        float distanceX;
        float distanceY;

        distanceX = Mathf.Abs(currentNode.x - startNode.x);
        distanceY = Mathf.Abs(currentNode.y - startNode.y);

        return distanceX + distanceY * 10;
    }*/


}


//JUST REWRITE ITTTTTTTT
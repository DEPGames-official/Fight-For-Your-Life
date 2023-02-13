using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarGameObjectManager : MonoBehaviour
{
    public List<GameObject> seeker = new List<GameObject>();
    public List<Transform> seekerTransform = new List<Transform>();
    //List<Vector3Int> seekerPos = new List<Vector3Int>();

    public GameObject target;
    public Transform targetTransform;
    //List<Vector3Int> targetPos = new List<Vector3Int>();

    public AStar aStarAlgorithm;

    public Grid mainGrid;

    public Tilemap mainWorldTilemap;
    public TileBase nodeWithLowestCost;

    public List<AStar> aStarList = new List<AStar>();

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < seeker.Count; i++)
        {
            aStarAlgorithm = new AStar
            {
                mainGrid = mainGrid,
                mainWorldTilemap = mainWorldTilemap,
                nodeWithLowestCost = nodeWithLowestCost,

                target = target,
                targetTransform = targetTransform,

                seeker = seeker[i],
                seekerTransform = seekerTransform[i]
            };

            aStarList.Add(aStarAlgorithm);

            //print(i);
        }

        /*aStarAlgorithm.mainGrid = mainGrid;
        aStarAlgorithm.mainWorldTilemap = mainWorldTilemap;
        aStarAlgorithm.nodeWithLowestCost = nodeWithLowestCost;

        aStarAlgorithm.target = target;
        aStarAlgorithm.targetTransform = targetTransform;

        aStarAlgorithm.seeker = seeker[0];
        aStarAlgorithm.seekerTransform = seekerTransform[0];*/
    }

    

    // Update is called once per frame
    void Update()
    {
        
        //aStarAlgorithm.RunPathFind();
        Run();
    }

    void Run()
    {
        for(int i = 0; i < aStarList.Count; i++)
        {
            aStarList[i].RunPathFind();
        }
       
        
    }
}

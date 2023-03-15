using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> enemyPrefabType = new List<GameObject>();

    public Vector3 spawnPosition;
    float spawnOffsetAmount;
    public Vector3 spawnPositionOffset;
    public Quaternion spawnRotation;

    public int spawnAmount;
    public int spawnCount;

    [SerializeField]
    XPBarController xpController;
    
    public float xSpawnPos;
    float xSpawnPosRand;
    
    public float ySpawnPos;
    float ySpawnPosRand;
    
    public float zSpawnPos;
    float zSpawnPosRand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*xSpawnPosRand = Random.Range(0, 11);
        xSpawnPos = xSpawnPosRand / 10;
        spawnPosition = new Vector3(xSpawnPos, 1f + spawnPositionOffset.y, 0f);
        spawnOffset = Random.Range(5, 11);
        spawnPositionOffset.y = spawnOffset / 10;
        Vector3 v3Pos = Camera.main.ViewportToWorldPoint(spawnPosition);
        enemyPrefabType[0].transform.position = v3Pos;*/


        SpawnEnemy();
    }

    //level + (2*level)
    void SpawnEnemy()
    {

        if (GetGameobjectsOnLayer(8).Count <= 0)
        {
            int xplevel = xpController.GetLevel();
            spawnAmount = SpawnAmount(xplevel);

            for (int i = 0; i < enemyPrefabType.Count; i++)
            {
                EnemyController enemyPrefab = enemyPrefabType[i].GetComponent<EnemyController>();

                if (enemyPrefab.difficultyLevel < xplevel && xplevel < enemyPrefab.difficultyLevel * 2)
                {
                    var amountToTakeOff = SpawnAmount(enemyPrefab.difficultyLevel);
                    print(amountToTakeOff);
                    spawnAmount -= amountToTakeOff;
                    for (int x = 0; x < amountToTakeOff; x++)
                    {
                        SpawnSide();

                        Vector3 v3Pos = Camera.main.ViewportToWorldPoint(spawnPosition);
                        var enemyToSpawn = enemyPrefabType[i];
                        GameObject newEnemy = Instantiate(enemyToSpawn, v3Pos, Quaternion.identity);
                    }
                }
                if (enemyPrefab.difficultyLevel >= xplevel && spawnAmount >= 0)
                {
                    for (int x = spawnAmount; x > 0; x--)
                    {
                        SpawnSide();

                        Vector3 v3Pos = Camera.main.ViewportToWorldPoint(spawnPosition);
                        var enemyToSpawn = enemyPrefabType[i];
                        GameObject newEnemy = Instantiate(enemyToSpawn, v3Pos, Quaternion.identity);
                        spawnAmount--;
                    }
                }
                
            }

            
        }

    }

    int SpawnAmount(int level)
    {
        int spawnAmount = level + (2 * level);
        return spawnAmount;
    }

    void SpawnSide()
    {
        SpawnRandom(0, 11);

        switch (Random.Range(1, 5))
        {
            case 1:
                spawnPosition = new Vector3(xSpawnPos, 1f + spawnPositionOffset.y, 0f);

                print("up");
                break;
            case 2:
                spawnPosition = new Vector3(1f + spawnPositionOffset.x, ySpawnPos, 0f);

                print("right");
                break;
            case 3:
                spawnPosition = new Vector3(0f - spawnPositionOffset.x, ySpawnPos, 0f);

                print("left");
                break;
            case 4:
                spawnPosition = new Vector3(xSpawnPos, 0f - spawnPositionOffset.y, 0f);

                print("down");
                break;
        }
    }
    void SpawnRandom(int minInclusive, int maxExclusive)
    {
        SpawnOffset(5, 11);

        xSpawnPosRand = Random.Range(0, 11);
        xSpawnPos = xSpawnPosRand / 10;

        ySpawnPosRand = Random.Range(0, 11);
        ySpawnPos = ySpawnPosRand / 10;

    }


    void SpawnOffset(int minInclusive, int maxExclusive)
    {
        spawnOffsetAmount = Random.Range(minInclusive, maxExclusive);

        spawnPositionOffset.x = spawnOffsetAmount / 10;
        spawnPositionOffset.y = spawnOffsetAmount / 10;
        
    }

    List<GameObject> GetGameobjectsOnLayer(int layer)
    {
        GameObject[] sceneGameObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
        List<GameObject> gameObjectsInLayer = new List<GameObject>();

        foreach (GameObject gameobject in sceneGameObjects)
        {
            if (gameobject.layer == layer)
            {
                gameObjectsInLayer.Add(gameobject);
            }
        }
        return gameObjectsInLayer;
    }

}

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
    public int totalSpawnCount;

    [SerializeField]
    XPBarController xpController;
    
    public float xSpawnPos;
    float xSpawnPosRand;
    
    public float ySpawnPos;
    float ySpawnPosRand;
    
    public float zSpawnPos;
    float zSpawnPosRand;

    System.Random spawnerRandom = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetGameobjectsOnLayer(8).Count <= 0)
            SpawnEnemies();
    }

    //level + (2*level)
    void SpawnEnemies()
    {
        int xplevel = xpController.GetLevel();
        spawnAmount = SpawnAmount(xplevel);
        spawnCount = spawnAmount;

        for (int i = 0; i < enemyPrefabType.Count; i++)
        {
            EnemyController enemyPrefab = enemyPrefabType[i].GetComponent<EnemyController>();

            if (enemyPrefab.difficultyLevel < xplevel && xplevel < enemyPrefab.difficultyLevel * 2)
            {
                //Amount of enemies to spawn that is of the lower level calculated based on it's level
                var amountOffset = SpawnAmount(enemyPrefab.difficultyLevel);
                spawnAmount -= amountOffset;
                for (int x = 0; x < amountOffset; x++)
                {
                    SpawnEnemy(enemyPrefabType[i]);
                }
            }
            if (enemyPrefab.difficultyLevel >= xplevel && spawnAmount > 0)
            {
                for (int x = spawnAmount; x > 0; x--)
                {
                    SpawnEnemy(enemyPrefabType[i]);
                    spawnAmount--;
                }
            }
                
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        SpawnSide();

        Vector3 v3Pos = Camera.main.ViewportToWorldPoint(spawnPosition);
        var enemyToSpawn = enemy;
        GameObject newEnemy = Instantiate(enemyToSpawn, v3Pos, Quaternion.identity);

        totalSpawnCount++;
    }

    int SpawnAmount(int level)
    {
        int spawnAmount = level + (2 * level);
        return spawnAmount;
    }

    void SpawnSide()
    {
        SpawnRandom(0, 11);

        switch (spawnerRandom.Next(1, 5))
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

        xSpawnPosRand = spawnerRandom.Next(0, 11);
        xSpawnPos = xSpawnPosRand / 10;

        ySpawnPosRand = spawnerRandom.Next(0, 11);
        ySpawnPos = ySpawnPosRand / 10;

    }


    void SpawnOffset(int minInclusive, int maxExclusive)
    {
        spawnOffsetAmount = spawnerRandom.Next(minInclusive, maxExclusive);

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

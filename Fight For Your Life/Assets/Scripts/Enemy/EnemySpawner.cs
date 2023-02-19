using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> enemyPrefabType = new List<GameObject>();

    public Vector3 spawnPosition;
    public Quaternion spawnRotation;

    public int spawnAmount;
    public int spawnCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                var enemyToSpawn = enemyPrefabType[0];
                GameObject newEnemy = Instantiate(enemyToSpawn);
                newEnemy.transform.GetChild(0).transform.localPosition = spawnPosition;
                spawnCount++;
            }
            print(spawnCount);
           
        }

    }
}

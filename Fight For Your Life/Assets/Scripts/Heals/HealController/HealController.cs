using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealController : MonoBehaviour
{
    [SerializeField]
    GameObject redHeart16Prefab;
    public float chancePercent = 15;
    public int probability;
    public int randomChance;
    System.Random randomProbability = new System.Random();


    //Chance = 100/percent
    //Spawn with a specified chance amount
    public void spawnRedHeart16(Vector3 position, Quaternion rotation)
    {
        //Rounds off and finds the 1 in chance
        probability = (int)Mathf.Round(100 / chancePercent);
        //Chooses a number between 0 and the 1 in chance
        //randomChance = UnityEngine.Random.Range(0, probability);
        randomChance = randomProbability.Next(probability);


        if (randomChance == 0f)
        {
            Instantiate(redHeart16Prefab, position, rotation);
            print(redHeart16Prefab.activeSelf);
        }
    }
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealController : MonoBehaviour
{
    [SerializeField]
    GameObject redHeart16Prefab; 

    public void spawnRedHeart16(Vector3 position, Quaternion rotation)
    {
        Instantiate(redHeart16Prefab, position, rotation);
        print(redHeart16Prefab.activeSelf);
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

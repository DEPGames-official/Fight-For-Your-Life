using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 50f;
    public float currHealth = 50f;

    public GameObject enemyParent;

    // Start is called before the first frame update
    void Start()
    {   
        enemyParent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (currHealth <= 0f)
        {
            enemyParent.SetActive(false);
        }
    }
}

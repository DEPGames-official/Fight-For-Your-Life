using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHeart16 : MonoBehaviour
{
    [SerializeField]
    GameObject playerToHealPrefab;
    [SerializeField]
    float healAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerToHealPrefab.tag)
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            playerHealth.currHealth += healAmount;

            gameObject.SetActive(false);
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

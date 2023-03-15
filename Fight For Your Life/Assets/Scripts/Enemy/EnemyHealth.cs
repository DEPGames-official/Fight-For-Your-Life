using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 50f;
    public float currHealth = 50f;

    [SerializeField]
    HealController healController;
    [SerializeField]
    XPBarController xpController;


    // Start is called before the first frame update
    void Start()
    {
        healController = GameObject.FindGameObjectWithTag("HealController").GetComponent<HealController>();
        xpController = GameObject.FindGameObjectWithTag("XPBar").GetComponent<XPBarController>();

        if (currHealth <= maxHealth)
        {
            currHealth = maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currHealth <= 0f)
        {
            if (gameObject.tag == "EnemyImpRed")
            {
                healController.spawnRedHeart16(transform.position, Quaternion.identity);
                xpController.AddXP(150f);
            }

            gameObject.SetActive(false);
            
        }
    }
}

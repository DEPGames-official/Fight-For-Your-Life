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
        //Do the following in case each individual enemy dies
        if (currHealth <= 0f)
        {
            switch (gameObject.tag)
            {
                case "EnemyImpRed":
                    healController.spawnRedHeart16(transform.position, Quaternion.identity);
                    xpController.AddXP(150f);
                    break;
                case "EnemyGoblin":
                    healController.spawnRedHeart16(transform.position, Quaternion.identity);
                    xpController.AddXP(300f);
                    break;
                case "EnemyNightBorne":
                    healController.spawnRedHeart16(transform.position, Quaternion.identity);
                    xpController.AddXP(500f);
                    break;
                case "EnemySkeleton":
                    healController.spawnRedHeart16(transform.position, Quaternion.identity);
                    xpController.AddXP(750f);
                    break;
                case "EnemyGolem":
                    healController.spawnRedHeart16(transform.position, Quaternion.identity);
                    xpController.AddXP(1000f);
                    break;
            }
            gameObject.SetActive(false);
            
        }
    }
}

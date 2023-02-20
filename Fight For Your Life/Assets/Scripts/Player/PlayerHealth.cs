using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currHealth = 100f;

    HealthBar playerHealthBar = new HealthBar();
    [SerializeField]
    Slider healthBarSlider;

    void Start()
    {
        playerHealthBar.healthSlider = healthBarSlider;
        playerHealthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (currHealth < 0f)
        {
            currHealth = 0f;
        }
        else if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
        playerHealthBar.SetHealth(currHealth);
    }
}

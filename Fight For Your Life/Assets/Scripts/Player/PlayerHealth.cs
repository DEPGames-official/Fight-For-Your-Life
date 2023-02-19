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
        playerHealthBar.slider = healthBarSlider;
        playerHealthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (currHealth < 0f)
        {
            currHealth = 0f;
        }
        playerHealthBar.SetHealth(currHealth);
    }
}

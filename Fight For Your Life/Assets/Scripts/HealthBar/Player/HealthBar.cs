using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar
{
    public Slider healthSlider;

    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }
}

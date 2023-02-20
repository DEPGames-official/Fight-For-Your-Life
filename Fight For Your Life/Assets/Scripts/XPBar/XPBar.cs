using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPBar
{
    public Slider xpSlider;
    public TextMeshProUGUI xpTextMeshPro;

    public void SetMaxXP(float xp)
    {
        xpSlider.maxValue = xp;
    }

    public void SetXP(float xp)
    {
        xpSlider.value = xp;
    }

    public void AddXP(float xp)
    {
        xpSlider.value += xp;
    }

    public void SetXPLevel(int level)
    {
        xpTextMeshPro.text = level.ToString();
    }

    public void AddXPLevel(int level)
    {
        int nextlevel = Int32.Parse(xpTextMeshPro.text) + level;
        xpTextMeshPro.text = nextlevel.ToString();
    }

}

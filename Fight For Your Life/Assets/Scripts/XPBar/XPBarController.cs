using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPBarController : MonoBehaviour
{
    [SerializeField]
    Slider xpSlider;
    [SerializeField]
    TextMeshProUGUI xpTextMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        SetXP(0f);
        SetXPLevel(1);
    }

    void Update()
    {
        AddXPLevel(1);
    }

    public void AddXP(float xp)
    {
        xpSlider.value += xp;
    }

    void SetXP(float xp)
    {
        xpSlider.value = xp;
    }

    void SetMaxXP(float xp)
    {
        xpSlider.maxValue = xp;
    }

    void SetXPLevel(int level)
    {
        xpTextMeshPro.text = level.ToString();
    }

    void AddXPLevel(int level)
    {
        if (xpSlider.value == xpSlider.maxValue)
        {
            int nextlevel = Int32.Parse(xpTextMeshPro.text) + level;
            xpTextMeshPro.text = nextlevel.ToString();
            SetXP(0f);
        }
    }

  


}

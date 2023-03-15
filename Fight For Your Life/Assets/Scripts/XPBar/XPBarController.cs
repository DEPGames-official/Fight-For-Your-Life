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

    public int GetLevel()
    {
        int level = Int32.Parse(xpTextMeshPro.text);
        return level;
    }

    void AddXPLevel(int level)
    {
        if (xpSlider.value == xpSlider.maxValue)
        {
            int nextlevel = Int32.Parse(xpTextMeshPro.text) + level;
            xpTextMeshPro.text = nextlevel.ToString();

            float xpRequired = nextlevel * 100f * 1.25f;

            SetMaxXP(xpRequired);
            SetXP(0f);
        }
    }

  


}

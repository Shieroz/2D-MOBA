using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public Text text;
    public HeroController controller;

    // Update is called once per frame
    void Update()
    {
        text.text = "HP: " + controller.stats[UnitStats.Stats.HPCur] + "\nMP: " + controller.stats[UnitStats.Stats.MPCur];
    }
}

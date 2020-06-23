using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStatsDisplay : MonoBehaviour
{
    public Image HPgauge, MPgauge;
    public Text Lvl, IDText;
    public string HeroName, ID;
    public float HP, MP;
    public HeroController controller;
    private HeroStats stats;
    private Vector3 displacementWithParent;
    // Start is called before the first frame update
    void Start()
    {
        HeroName = controller.HeroName;
        ID = controller.ID;
        stats = controller.stats;
        displacementWithParent = new Vector3(0f, 0f, transform.position.z * 2);
        UpdateStatsDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatsDisplay();
    }

    void LateUpdate()
    {
        MaintainPosition();
    }

    //Maintain the UI position relative with the hero
    void MaintainPosition()
    {
        transform.Rotate(Vector3.up, Vector3.SignedAngle(transform.forward, Vector3.forward, Vector3.up));
    }

    void UpdateStatsDisplay()
    {
        HP = stats[UnitStats.Stats.HPCur] / stats[UnitStats.Stats.HP];
        MP = stats[UnitStats.Stats.MPCur] / stats[UnitStats.Stats.MP];
        Lvl.text = ((float)stats[UnitStats.Stats.Lvl]).ToString();
        IDText.text = ID;
        HPgauge.fillAmount = 1f - HP;
        MPgauge.fillAmount = 1f - MP;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class HeroController : MonoBehaviour
{
    public string HeroName, ID;
    public HeroStats stats;
    public NavMeshAgent agent;
    public AIActionCenter actionCenter;

    public HeroSkills skills;

    void Awake()
    {
        stats = new HeroStats(HeroName);
    }

    // Update is called once per frame
    void Update()
    {
        Regen();
        if (actionCenter.keyboard.GetKey(KeyCode.Q))
            UseSkillQ();
    }

    void Regen()
    {
        //Regen HP & MP
        ChangeHP(stats[UnitStats.Stats.MPRegen] * Time.deltaTime);
        ChangeMP(stats[UnitStats.Stats.MPRegen] * Time.deltaTime);
    }

    void UseSkillQ()
    {
        skills.UseSkillQ();
    }

    void UseSkillW()
    {
        skills.UseSkillW();
    }

    void UseSkillE()
    {
        skills.UseSkillE();
    }

    void UseUltimate()
    {
        skills.UseUltimate();
    }

    public void ChangeMP(float mp)
    {
        stats[UnitStats.Stats.MPCur] = Mathf.Clamp(stats[UnitStats.Stats.MPCur] + mp, 0f, stats[UnitStats.Stats.MP]);
    }

    //Returns true if HP drops to 0 (the hero dies)
    public bool ChangeHP(float hp)
    {
        stats[UnitStats.Stats.HPCur] = Mathf.Clamp(stats[UnitStats.Stats.HPCur] + hp, 0f, stats[UnitStats.Stats.HP]);
        if (stats[UnitStats.Stats.HPCur] == 0f)
        {
            stats[UnitStats.Stats.HPCur] = stats[UnitStats.Stats.HP]; //Respawn
            actionCenter.mouse.CenterMouse();
            Vector2 mousePos = actionCenter.mouse.position;
            transform.position = new Vector3(mousePos.x, 1.2f, mousePos.y);
            actionCenter.deaths++;
            return true;
        }
        return false;
    }

    //Returns true if the enemy dies
    public bool DealDamage(HeroController enemy, int dmg)
    {
        return enemy.TakeDamage(dmg);
    }

    //Returns true if the hero dies
    public bool TakeDamage(float dmg)
    {
        return ChangeHP(-dmg);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Neutral")
        {
            TakeDamage(100f);
        }
    }
}

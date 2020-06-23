using System;
using System.Collections;
using UnityEngine;

public class UnitStats
{
    //This class provides basic stats for all units
    public enum Stats
    {
        Lvl, HP, HPCur, HPRegen, MP, MPCur, MPRegen, moveSpd, turnRate, atkSpd, atkT,
        Arm, Atk, MResist, PResist, SResist, SpellAmp, PrimAttr, projSpd, Exp,
        Agi, Str, Int, AgiGain, StrGain, IntGain, vsnD, vsnN, atkRange
    }
    public static int StatsNum = 32;
    public string name;
    public Hashtable _stats;
}

public class HeroStats : UnitStats
{
    public Hashtable stats;
    public Skill[] skills;

    public float this[Enum stat]
    {
        get
        {
            return (float)stats[stat];
        }
        set
        {
            stats[stat] = (float)value;
        }
    }

    public HeroStats(string name)
    {
        this.name = name;
        string[] lines = System.IO.File.ReadAllLines(/*Application.persistentDataPath*/ "C:/Users/nghe1/Desktop/" + name + ".txt");
        _stats = new Hashtable();

        int lineI = 0;
        //Populate _stats
        for (int i = 0; i < StatsNum; i++)
        {
            string[] words = lines[lineI].Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            _stats[Enum.Parse(typeof(Stats), words[0])] = float.Parse(words[1]);
            lineI++;
        }

        skills = new Skill[4];
        //Populate skills
        for (int i = 0; i < 4; i++)
        {
            lineI++;
            skills[i] = new Skill(4);
            skills[i].name = lines[lineI++];
            int j = 0;
            foreach (string s in lines[lineI++].Split('/'))
                skills[i].MPCost[j++] = int.Parse(s);
            j = 0;
            foreach (string s in lines[lineI++].Split('/'))
                skills[i].Damage[j++] = int.Parse(s);
            j = 0;
            foreach (string s in lines[lineI++].Split('/'))
                skills[i].Cooldown[j++] = int.Parse(s);
        }

        //Populate stats
        stats = new Hashtable();
        foreach (Stats a in Enum.GetValues(typeof(Stats)))
            stats[a] = (float)_stats[a];
        AdjustStats();
    }

    public void AdjustStats()
    {
        stats[Stats.HP] = (float)_stats[Stats.HP] + (float)_stats[Stats.Str] * 20f;
        stats[Stats.HPCur] = (float)stats[Stats.HP];
        stats[Stats.HPRegen] = (float)_stats[Stats.HPRegen] + (float)_stats[Stats.Str] * 0.1f;
        stats[Stats.MP] = (float)_stats[Stats.MP] + (float)_stats[Stats.Int] * 12f;
        stats[Stats.MPCur] = (float)stats[Stats.MP];
        stats[Stats.MPRegen] = (float)_stats[Stats.MPRegen] + (float)_stats[Stats.Int] * 0.05f;
        stats[Stats.Arm] = (float)_stats[Stats.Arm] + (float)_stats[Stats.Agi] * 0.16f;
        stats[Stats.atkSpd] = (float)_stats[Stats.atkSpd] + (float)_stats[Stats.Agi] * 1f;
        switch (stats[Stats.PrimAttr])
        {
            case 0f:
                stats[Stats.Atk] = (float)_stats[Stats.Atk] + (float)_stats[Stats.Agi] * 1f;
                break;
            case 1f:
                stats[Stats.Atk] = (float)_stats[Stats.Atk] + (float)_stats[Stats.Str] * 1f;
                break;
            case 2f:
                stats[Stats.Atk] = (float)_stats[Stats.Atk] + (float)_stats[Stats.Int] * 1f;
                break;
        }
    }

    public class Skill
    {
        public string name;
        public int[] MPCost;
        public int[] Damage;
        public int[] Cooldown;

        public Skill(int skillNum)
        {
            MPCost = new int[skillNum];
            Damage = new int[skillNum];
            Cooldown = new int[skillNum];
        }
    }
}
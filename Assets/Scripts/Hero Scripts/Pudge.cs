using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pudge : HeroSkills
{
    public GameObject MeatHookPrefab;
    public HeroController controller;
    private HeroStats stats;

    public int[] skillLvls;
    public int[] maxSkillLvls;
    private int skillNum = 4;

    private bool[] skillInProgress;

    private void Start()
    {
        stats = controller.stats;
        skillLvls = new int[skillNum];
        skillInProgress = new bool[skillNum];
        maxSkillLvls = new int[] { 4, 4, 4, 3 };
    }

    IEnumerator ThrowHook()
    {
        skillInProgress[0] = true;
        GameObject MeatHook = Instantiate(MeatHookPrefab, MeatHookPrefab.transform.position, MeatHookPrefab.transform.rotation);
        MeatHook.SetActive(true);
        MeatHook hook = MeatHook.GetComponent<MeatHook>();
        hook.self = gameObject;
        float hookSpeed = 20f;

        Vector2 mouseP = controller.actionCenter.mouse.position;
        Vector3 targetLocation = new Vector3(mouseP.x, 0f, mouseP.y);
        targetLocation.y = MeatHook.transform.position.y;
        Vector3 moveDir = (targetLocation - MeatHook.transform.position).normalized;

        float dTravelled = 0f;
        float distance = (targetLocation - MeatHook.transform.position).magnitude;
        MeatHook.transform.LookAt(new Vector3(targetLocation.x, MeatHook.transform.position.y, targetLocation.z));
        //Fly to target
        while (dTravelled < distance)
        {
            if (!hook.hit)
            {
                MeatHook.transform.position += moveDir * hookSpeed * Time.deltaTime;
                dTravelled += hookSpeed * Time.deltaTime;
            }
            else
            {
                break;
            }
            yield return null;
        }

        if (hook.hit && hook.tag != controller.tag)
        {
            //controller.DealDamage(hook.hitObject.GetComponent<HeroController>(), stats.skills[0].Damage[skillLvls[0]]);
        }

        //Return to hero
        targetLocation = transform.position;
        dTravelled = 0f;
        distance = (targetLocation - MeatHook.transform.position).magnitude;
        moveDir = (targetLocation - MeatHook.transform.position).normalized;
        MeatHook.transform.LookAt(MeatHook.transform.position - moveDir);
        while (dTravelled < distance)
        {
            MeatHook.transform.position += moveDir * hookSpeed * Time.deltaTime;
            dTravelled += hookSpeed * Time.deltaTime;
            yield return null;
        }
        Destroy(MeatHook);
        skillInProgress[0] = false;
    }

    public override void UseSkillQ()
    {
        controller.ChangeMP(-stats.skills[0].MPCost[skillLvls[0]]);
        if (!skillInProgress[0])
            StartCoroutine(ThrowHook());
    }

    public override void UseSkillW()
    {

    }

    public override void UseSkillE()
    {

    }

    public override void UseUltimate()
    {

    }

    public void LvlUpSkill (int skill)
    {
        if (skill >= 0 && skill < 4 && skillLvls[skill] < maxSkillLvls[skill])
            skillLvls[skill]++;
    }
}

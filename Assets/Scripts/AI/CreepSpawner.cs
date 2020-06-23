using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepSpawner : MonoBehaviour
{
    public GameObject creepPrefab;
    public AIActionCenter actionCenter;
    private GameObject creep;
    private CreepController creepC;
    // Start is called before the first frame update
    void Start()
    {
        SpawnCreep();
    }

    // Update is called once per frame
    void Update()
    {
        if (creep != null && creepC.hit)
        {
            Destroy(creep);
            actionCenter.kills++;
            SpawnCreep();
        }
    }

    void SpawnCreep()
    {
        Vector2 randMousePos = actionCenter.mouse.GetRandomPos();
        Vector3 newSpawnPos = new Vector3(randMousePos.x, 1.2f, randMousePos.y);
        //Spawn creep away from the player
        while ((newSpawnPos - actionCenter.transform.position).magnitude < 1f)
        {
            randMousePos = actionCenter.mouse.GetRandomPos();
            newSpawnPos = new Vector3(randMousePos.x, 1.2f, randMousePos.y);
        }
        creep = Instantiate(creepPrefab, newSpawnPos, Quaternion.identity);
        creepC = creep.GetComponent<CreepController>();
        actionCenter.creep = creep.transform;
    }
}

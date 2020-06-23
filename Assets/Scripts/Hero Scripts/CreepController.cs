using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepController : MonoBehaviour
{
    private static float timer = 1f, force = 5f;
    private float cooldown;
    private Vector3 moveDir;
    private Rigidbody rb;
    public bool hit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cooldown = timer;
        hit = false;
    }

    void Update()
    {
        if (cooldown < 0f)
        {
            cooldown = timer;
            NewDir();
            rb.AddForce(moveDir * force);
        }
        cooldown -= Time.deltaTime;
    }

    void NewDir()
    {
        moveDir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Skill" || collision.gameObject.tag == "Radiant")
            hit = true;
    }
}

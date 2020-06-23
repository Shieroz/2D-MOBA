using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public AIActionCenter actionCenter;

    void Update()
    {
        if (actionCenter.mouse.GetMouseButton(1))
        {
            agent.SetDestination(new Vector3(actionCenter.mouse.position.x, 0f, actionCenter.mouse.position.y));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatHook : MonoBehaviour
{
    public BoxCollider col;
    public bool hit;
    public GameObject hitObject;
    public GameObject self;

    private void Start()
    {
        hit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != self && other.gameObject.layer != LayerMask.NameToLayer("Terrain"))
        {
            hit = true;
            Debug.Log(other.name + "  " + other.tag);
        }
        hitObject = other.gameObject;
    }
}

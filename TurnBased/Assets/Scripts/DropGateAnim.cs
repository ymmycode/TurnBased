using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGateAnim : MonoBehaviour
{

    [SerializeField]GameObject gateDrop;
    public bool isNear = false;
    Animator gateDropAnim;


    // Start is called before the first frame update
    void Start()
    {
        gateDropAnim = gateDrop.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            isNear = true;
            gateDropAnim.SetTrigger("isTrue");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            isNear = false;
            gateDropAnim.SetTrigger("isExit");
        }
    }
}

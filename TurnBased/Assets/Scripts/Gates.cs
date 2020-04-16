using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{

    public GameObject theGates;
    Animator gateAnimation;

    private void Start()
    {
        gateAnimation = theGates.GetComponent<Animator>();
    }

    public void OpenGate()
    {
        Debug.Log("Open");
        gateAnimation.Play("Open");
    }
}

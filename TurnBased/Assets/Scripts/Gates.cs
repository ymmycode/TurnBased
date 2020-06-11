using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{

    public GameObject theGates;
    Animator gateAnimation;
    public string animationName;

    private void Start()
    {
        gateAnimation = theGates.GetComponent<Animator>();
    }

    public void OpenGate()
    {
        gateAnimation.Play(animationName);
    }
}

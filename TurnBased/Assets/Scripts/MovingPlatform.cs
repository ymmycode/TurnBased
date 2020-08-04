using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject hero;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hero"))
        {
            hero.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            hero.transform.parent = null;
        }
    }

}

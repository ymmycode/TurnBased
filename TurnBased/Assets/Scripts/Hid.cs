using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hid : MonoBehaviour
{
    [SerializeField]GameObject wallHid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            wallHid.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            wallHid.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TheProgram : MonoBehaviour
{
    public bool isNearThisComputer = false;
    public GameObject heroItSelf;
    public HUD hud;
    public GameObject cameraMovementStop;
    public KeyCode interactKey;
    public UnityEvent interactAction;
        private void Update()
    {
        if(isNearThisComputer == true)
        {   
            //Debug.Log(this.name);
            LaunchFlowchartPuzzle();
        }
    }

    private void LaunchFlowchartPuzzle()
    {
        if (Input.GetKeyDown(interactKey))
        {
            heroItSelf.GetComponent<MovementMech>().enabled = false;
            cameraMovementStop.GetComponent<PlayerFollow>().enabled = false;

            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;

            hud.CloseMessagePanel("");
            interactAction.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Hero"))
        {
            isNearThisComputer = true; 
            //Debug.Log(this.name);
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Hero"))
        {
            isNearThisComputer = false;
        }
    }
}

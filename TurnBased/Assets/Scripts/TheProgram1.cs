using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TheProgram1 : MonoBehaviour
{
    public bool isNearThisComputer = false;
    public GameObject heroItSelf;
    public HUD hud;
    public GameObject cameraMovementStop;
    public KeyCode interactKey;
    public UnityEvent interactAction;

        private void Update()
    {
        if(!isNearThisComputer)
        {   
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
            hud.OpenMessagePanel("");
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Hero"))
        {
            isNearThisComputer = true;
            hud.CloseMessagePanel("");
        }
    }
}

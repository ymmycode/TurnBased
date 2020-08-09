using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialPanel, thisPanel;
    public PlayerFollow playerFollow;
    public MovementMech heroMov;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hero"))
        {
            //show tutorial panel
            playerFollow.enabled = false;
            heroMov.enabled = false;
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            tutorialPanel.SetActive(true);
        }
    }

    public void BackButtonTutorial()
    {
        //destroy tutorial panel
        playerFollow.enabled = true;
        heroMov.enabled = true;
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        Destroy(thisPanel);
    }
}

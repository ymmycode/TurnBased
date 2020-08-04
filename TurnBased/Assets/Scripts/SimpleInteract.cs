using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleInteract : MonoBehaviour
{
    public GameObject heroItSelf;
    public Button processButton;
    public GameObject gateToOpen;
    public GameObject cameraToMove;

    private void Start()
    {
        processButton.interactable = true;
        processButton.enabled = true;
        //processButton.GetComponentInChildren<Image>().color = new Color32(125,124,125,255);
    }

    private void Update()
    {
        
    }

    public void OpenGate()
    {
        ClosePanel();
        Invoke("GateOpening", .7f);
        cameraToMove.GetComponent<PlayerFollow>().enabled = true;//enabling camera follow
        heroItSelf.GetComponent<MovementMech>().enabled = true;//enabling movemet mechanic
        Invoke("DisablePuzzle", 2f);
    }

    void DisablePuzzle()
    {
        this.gameObject.SetActive(false);
    }

    void GateOpening()
    {
        gateToOpen.GetComponent<Gates>().OpenGate();
    }


    public void LaunchPanel()
    {
        GetComponent<Animator>().SetBool("isOpen", true);
        GetComponent<Animator>().SetTrigger("isSpinGlow");
    }

    public void ClosePanel()
    {
        GetComponent<Animator>().SetBool("isOpen", false);
        //LeanTween.scale(gameObject, new Vector3(0,0,0), 1f);
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }
    void DestroyThisObject()
    {
        Destroy(this);
    }

    public void BackButton()
    {
        ClosePanel();
        Invoke("Follow", 1f);
        heroItSelf.GetComponent<MovementMech>().enabled = true;
    }

    void Follow()
    {
        cameraToMove.GetComponent<PlayerFollow>().enabled = true;
    }

}

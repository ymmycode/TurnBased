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

    Vector2 keyStartPos, gateStartPos;

    bool keyCorrect, gateCorrect = false;

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
        GetComponent<Animator>().Play("LaunchAnim0");
    }

    public void ClosePanel()
    {
        GetComponent<Animator>().Play("CloseAnim");
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
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

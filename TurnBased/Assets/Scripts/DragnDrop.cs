﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragnDrop : MonoBehaviour
{
    public GameObject heroItSelf;
    public GameObject key,gate, keyBlack, gateBlack;

    public Button processButton;
    public GameObject gateToOpen;
    public GameObject cameraToMove;

    Vector2 keyStartPos, gateStartPos;

    bool keyCorrect, gateCorrect = false;

    private void Start()
    {
        processButton.interactable = false;
        processButton.enabled = false;
        processButton.GetComponentInChildren<Image>().color = new Color32(125,124,125,255);

        keyStartPos = key.transform.position;
        gateStartPos = gate.transform.position;
    }

    private void Update()
    {
        if (keyCorrect && gateCorrect)
        {
            processButton.interactable = true;
            processButton.enabled = true;
            processButton.GetComponentInChildren<Image>().color = new Color32(255, 0, 2, 255);
        }
    }

    public void DragKey()
    {
        key.transform.position = Input.mousePosition;
    }

    public void DragGate()
    {
        gate.transform.position = Input.mousePosition;
    }

    public void DropKey()
    {
        float distance = Vector3.Distance(key.transform.position, keyBlack.transform.position);
        if (distance < 50)
        {
            key.transform.position = keyBlack.transform.position;
            keyCorrect = true;
        }
        else 
        {
            key.transform.position = keyStartPos;
        }
    }

    public void DropGate()
    {
        float distance = Vector3.Distance(gate.transform.position, gateBlack.transform.position);
        if (distance < 50)
        {
            gate.transform.position = gateBlack.transform.position;
            gateCorrect = true;
        }
        else
        {
            gate.transform.position = gateStartPos;
        }
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

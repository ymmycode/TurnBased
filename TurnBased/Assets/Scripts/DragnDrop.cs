using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DragnDrop : MonoBehaviour
{
    public GameObject heroItSelf;
    public GameObject 
        key,gate, 
        keyBlack, gateBlack,
        key1, 
        keyBlack1, 
        key2, 
        keyBlack2, 
        platform1, platformCorrect1, 
        platform2, platformCorrect2;

    public GameObject[] number, numberCorrect ;

    public Button processButton;
    public GameObject gateToOpen;
    public GameObject cameraToMove;

    public GameObject hintPanel;
    public GameObject algosPanel , hintButton, problemButton, title;
    public GameObject textFeedBack,textFeedBack1;
    
    Vector2 
        keyStartPos, gateStartPos,
        keyStartPos1,
        keyStartPos2,
        platform1InitPos, platform2InitPos,
        n1,n2,n3,n4,n5,n6,n7,n8,n9,n0;

    public bool keyCorrect, gateCorrect = false;
    public bool numbersCorrect, platFormCorrect;

    private enum Colors 
        {
            GREEN,
            BLUE,
            EMPTY
        }

    Colors keyColor;

    Color key1InitColor, key2InitColor, key3InitColor, gateInitColor;

    private void Start()
    {
        keyColor = Colors.EMPTY;
        algosPanel.SetActive(false);
        hintPanel.SetActive(false);
        processButton.interactable = false;
        processButton.enabled = false;
        processButton.GetComponentInChildren<Image>().color = new Color32(125, 124, 125, 255);
        ColorInit();
        PuzzleObjectInitPotiion();

    }

    

    private void Update()
    {
        if (keyCorrect && gateCorrect && numbersCorrect && platFormCorrect)
        {
            processButton.interactable = true;
            processButton.enabled = true;
            processButton.GetComponentInChildren<Image>().color = new Color32(255, 0, 2, 255);

            textFeedBack.SetActive(true);
        }

        switch (keyColor)
        {
            case Colors.GREEN:
                textFeedBack.SetActive(false);
                textFeedBack1.SetActive(true);
                break;

            case Colors.BLUE:
                textFeedBack.SetActive(true);
                textFeedBack1.SetActive(false);
                break;

            default:
                break;
        }
    }


    void ColorInit()
    {
        key1InitColor = key.GetComponent<Image>().color;
        key2InitColor = key1.GetComponent<Image>().color;
        key3InitColor = key2.GetComponent<Image>().color;
        gateInitColor = gate.GetComponent<Image>().color;
    }
    void PuzzleObjectInitPotiion()
    {
        keyStartPos = key.transform.position;
        keyStartPos1 = key1.transform.position;
        keyStartPos2 = key2.transform.position;
        gateStartPos = gate.transform.position;
        platform1InitPos = platform1.transform.position;
        platform2InitPos = platform2.transform.position;
        n0 = number[0].transform.position;
        n1 = number[1].transform.position;
        n2 = number[2].transform.position;
        n3 = number[3].transform.position;
        n4 = number[4].transform.position;
        n5 = number[5].transform.position;
        n6 = number[6].transform.position;
        n7 = number[7].transform.position;
        n8 = number[8].transform.position;
        n9 = number[9].transform.position;
    }

    public void DragKey()
    {
        key.transform.position = Input.mousePosition;
    }
    public void DragKey1()
    {
        key1.transform.position = Input.mousePosition;
    }
    public void DragKey2()
    {
        key2.transform.position = Input.mousePosition;
    }
    public void DragGate()
    {
        gate.transform.position = Input.mousePosition;
    }
    public void DragPlat1()
    {
        platform1.transform.position = Input.mousePosition;
    }
    public void DragPlat2()
    {
        platform2.transform.position = Input.mousePosition;
    }
    public void DragNumberZero()
    {
        number[0].transform.position = Input.mousePosition;
    }
    public void DragNumberOne()
    {
        number[1].transform.position = Input.mousePosition;
    }
    public void DragNumberTwo()
    {
        number[2].transform.position = Input.mousePosition;
    }
    public void DragNumberThree()
    {
        number[3].transform.position = Input.mousePosition;
    }
    public void DragNumberFour()
    {
        number[4].transform.position = Input.mousePosition;
    }
    public void DragNumberFive()
    {
        number[5].transform.position = Input.mousePosition;
    }
    public void DragNumberSix()
    {
        number[6].transform.position = Input.mousePosition;
    }
    public void DragNumberSeven()
    {
        number[7].transform.position = Input.mousePosition;
    }
    public void DragNumberEight()
    {
        number[8].transform.position = Input.mousePosition;
    }
    public void DragNumberNine()
    {
        number[9].transform.position = Input.mousePosition;
    }



    public void DropKey()
    {
        float distance = Vector3.Distance(key.transform.position, keyBlack.transform.position);
        if (distance < 50)
        {
            key.transform.position = keyBlack.transform.position;
            keyCorrect = true;
            key.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            keyBlack.GetComponent<Image>().color = key1InitColor;
        }
        else 
        {
            key.transform.position = keyStartPos;
        }
    }
    public void DropKey1()
    {
        float distance = Vector3.Distance(key1.transform.position, keyBlack1.transform.position);
        if (distance < 50)
        {
            key1.transform.position = keyBlack1.transform.position;
            keyCorrect = true;
            keyColor = Colors.BLUE;
            key1.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            keyBlack1.GetComponent<Image>().color = key2InitColor;
        }
        else
        {
            key1.transform.position = keyStartPos1;
        }
    }
    public void DropKey2()
    {
        float distance = Vector3.Distance(key2.transform.position, keyBlack2.transform.position);
        if (distance < 50)
        {
            key2.transform.position = keyBlack2.transform.position;
            keyCorrect = true;
            keyColor = Colors.GREEN;
            key2.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            keyBlack2.GetComponent<Image>().color = key3InitColor;
        }
        else
        {
            key2.transform.position = keyStartPos2;
        }
    }
    public void DropGate()
    {
        float distance = Vector3.Distance(gate.transform.position, gateBlack.transform.position);
        if (distance < 50)
        {
            gate.transform.position = gateBlack.transform.position;
            gateCorrect = true;
            gate.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            gateBlack.GetComponent<Image>().color = gateInitColor;
        }
        else
        {
            gate.transform.position = gateStartPos;
        }
    }
    public void DropPlat1()
    {
        float distance = Vector3.Distance(platform1.transform.position, platformCorrect1.transform.position);
        if (distance < 50)
        {
            platform1.transform.position = platformCorrect1.transform.position;
            platFormCorrect = true;
            platform1.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            platformCorrect1.GetComponent<Image>().color = new Color32(202, 162, 255, 255);
        }
        else
        {
            platform1.transform.position = platform1InitPos;
        }
    }
    public void DropPlat2()
    {
        float distance = Vector3.Distance(platform2.transform.position, platformCorrect2.transform.position);
        if (distance < 50)
        {
            platform2.transform.position = platformCorrect2.transform.position;
            platFormCorrect = true;
            platform2.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            platformCorrect2.GetComponent<Image>().color = new Color32(127, 42, 241, 255);
        }
        else
        {
            platform2.transform.position = platform2InitPos;
        }
    }
    public void DropNumberZero()
    {
        float distance = Vector3.Distance(number[0].transform.position, numberCorrect[0].transform.position);
        if (distance < 50)
        {
            number[0].transform.position = numberCorrect[0].transform.position;
            numbersCorrect = true;
            number[0].GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            number[0].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 0, 0, 0);
            numberCorrect[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            numberCorrect[0].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            number[0].transform.position = n0;
        }
    }
    public void DropNumberOne()
    {
        float distance = Vector3.Distance(number[1].transform.position, numberCorrect[1].transform.position);
        if (distance < 50)
        {
            number[1].transform.position = numberCorrect[1].transform.position;
            numbersCorrect = true;
            number[1].GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            number[1].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 0, 0, 0);
            numberCorrect[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            numberCorrect[1].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            number[1].transform.position = n1;
        }
    }
    public void DropNumberTwo()
    {
        float distance = Vector3.Distance(number[2].transform.position, numberCorrect[2].transform.position);
        if (distance < 50)
        {
            number[2].transform.position = numberCorrect[2].transform.position;
            numbersCorrect = true;
            number[2].GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            number[2].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 0, 0, 0);
            numberCorrect[2].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            numberCorrect[2].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255,255,255,255);
        }
        else
        {
            number[2].transform.position = n2;
        }
    }
    public void DropNumberThree()
    {
        float distance = Vector3.Distance(number[3].transform.position, numberCorrect[3].transform.position);
        if (distance < 50)
        {
            number[3].transform.position = numberCorrect[3].transform.position;
            numbersCorrect = true;
            number[3].GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            number[3].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 0, 0, 0);
            numberCorrect[3].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            numberCorrect[3].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            number[3].transform.position = n3;
        }
    }
    public void DropNumberFour()
    {
        float distance = Vector3.Distance(number[4].transform.position, numberCorrect[4].transform.position);
        if (distance < 50)
        {
            number[4].transform.position = numberCorrect[4].transform.position;
            numbersCorrect = true;
            number[4].GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            number[4].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 0, 0, 0);
            numberCorrect[4].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            numberCorrect[4].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            number[4].transform.position = n4;
        }
    }
    public void DropNumberFive()
    {
        float distance = Vector3.Distance(number[5].transform.position, numberCorrect[5].transform.position);
        if (distance < 50)
        {
            number[5].transform.position = numberCorrect[5].transform.position;
            numbersCorrect = true;
            number[5].GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            number[5].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 0, 0, 0);
            numberCorrect[5].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            numberCorrect[5].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            number[5].transform.position = n5;
        }
    }
    public void DropNumberSix()
    {
        float distance = Vector3.Distance(number[6].transform.position, numberCorrect[6].transform.position);
        if (distance < 50)
        {
            number[6].transform.position = numberCorrect[6].transform.position;
            numbersCorrect = true;
            number[6].GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            number[6].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 0, 0, 0);
            numberCorrect[6].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            numberCorrect[6].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            number[6].transform.position = n6;
        }
    }
    public void DropNumberSeven()
    {
        float distance = Vector3.Distance(number[7].transform.position, numberCorrect[7].transform.position);
        if (distance < 50)
        {
            number[7].transform.position = numberCorrect[7].transform.position;
            numbersCorrect = true;
            number[7].GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            number[7].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 0, 0, 0);
            numberCorrect[7].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            numberCorrect[7].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            number[7].transform.position = n7;
        }
    }
    public void DropNumberEight()
    {
        float distance = Vector3.Distance(number[8].transform.position, numberCorrect[8].transform.position);
        if (distance < 50)
        {
            number[8].transform.position = numberCorrect[8].transform.position;
            numbersCorrect = true;
            number[8].GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            number[8].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 0, 0, 0);
            numberCorrect[8].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            numberCorrect[8].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            number[8].transform.position = n8;
        }
    }
    public void DropNumberNine()
    {
        float distance = Vector3.Distance(number[9].transform.position, numberCorrect[9].transform.position);
        if (distance < 50)
        {
            number[9].transform.position = numberCorrect[9].transform.position;
            numbersCorrect = true;
            number[9].GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            number[9].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 0, 0, 0);
            numberCorrect[9].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            numberCorrect[9].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            number[9].transform.position = n9;
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
        algosPanel.SetActive(false);
        hintPanel.SetActive(false);
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

    public void OpenHintPanel()
    {
        hintPanel.SetActive(true);
        hintButton.SetActive(false);
        title.SetActive(false);
        problemButton.SetActive(false);

    }

    public void CloseHintPanel()
    {
        hintPanel.SetActive(false);
        hintButton.SetActive(true);
        title.SetActive(true);
        problemButton.SetActive(true);
    }

    public void OpenAlgosPanel()
    {
        algosPanel.SetActive(true);
        hintButton.SetActive(false);
        title.SetActive(false);
        problemButton.SetActive(false);
    }

    public void CloseAlgosPanel()
    {
        algosPanel.SetActive(false);
        hintButton.SetActive(true);
        title.SetActive(true);
        problemButton.SetActive(true);
    }

}

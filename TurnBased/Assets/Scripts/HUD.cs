using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{

    public GameObject messagePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMessagePanel(string Text)
    { 
        messagePanel.SetActive(true);
    }

    public void CloseMessagePanel(string Text)
    {
        messagePanel.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{

    public TMP_InputField inputUsername;
    public TMP_InputField inputPassword;
    public TMP_Text username, popUpMessage;
    public GameObject popUpPanel, 
            syncPanel, 
            mainMenuPanel, syncButton,
            panelInfo;
    public Animator buttonSync, formPanel;
    public Button loginButton;
    public Slider volumeSlider;
    public string loginMessageNew, userID;
    Web web;
    // Start is called before the first frame update
    void Start()
    {

        loginButton.onClick.AddListener(() => 
        {
            //start get userid
            StartCoroutine(Main.Instance.Web.GetUserID(
                inputUsername.GetComponent<TMP_InputField>().text));

            StartCoroutine(Main.Instance.Web.Login(
                inputUsername.GetComponent<TMP_InputField>().text, 
                inputPassword.GetComponent<TMP_InputField>().text));
            
            username.text = "Hello, " + inputUsername.GetComponent<TMP_InputField>().text;
            
            Invoke("ShowMessage",2f);
            Invoke("CheckAndCreateEntries",2f);
            Invoke("UpdateLastPlay",3f);
            Invoke("ClearField",3f);
            Invoke("setVolumeValue",3f);
        });
    }

    private void Update()
    {
        userID = Main.Instance.Web.userID;
        Debug.Log(userID);
    }

    void CheckAndCreateEntries()
    {
        StartCoroutine(Main.Instance.Web.SavedData(userID));
        StartCoroutine(Main.Instance.Web.SavedOption(userID));

        StartCoroutine(Main.Instance.Web.GetVolume(userID));
    }
    void setVolumeValue()
    {
        volumeSlider.value = Main.Instance.Web.savedVolumeValue;
    }

    void UpdateLastPlay()
    {
        StartCoroutine(Main.Instance.Web.UpdateDate(userID));
        StartCoroutine(Main.Instance.Web.UserLastScene(userID));
    }

    void ShowMessage()
    {
        popUpMessage.text = loginMessageNew;
        popUpPanel.GetComponent<Image>().raycastTarget = true;
        popUpPanel.GetComponent<Animator>().SetTrigger("isOpen");
    }

    public void ClearField()
    {
        inputUsername.GetComponent<TMP_InputField>().text = "";
        inputPassword.GetComponent<TMP_InputField>().text = "";
    }

    public void OkayLoginButton()
    {
        if(loginMessageNew == "Login Success")
        {
            popUpPanel.GetComponent<Animator>().SetTrigger("isClose");
            //do button sync disable, enable panel profile(info)
            formPanel.SetTrigger("closeLogin");
            buttonSync.SetTrigger("closeLogin");
            Invoke("BackToMenu",2f);
        }
        else
        {
            username.text = "";
            popUpPanel.GetComponent<Animator>().SetTrigger("isClose");
            popUpPanel.GetComponent<Image>().raycastTarget = false;
        }
    }
    
    void BackToMenu()
    {
        syncButton.SetActive(false);
        syncPanel.SetActive(false);
        panelInfo.SetActive(true);
        mainMenuPanel.SetActive(true);
    }


}

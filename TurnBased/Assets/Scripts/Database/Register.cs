using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Register : MonoBehaviour
{
    public TMP_InputField inputUsername;
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;
    public TMP_Text popUpMessage;
    public GameObject popUpPanel;
    public Button registerButton;

    // Start is called before the first frame update

    public string regMessage;
    void Start()
    {

        registerButton.onClick.AddListener(() => 
        {
            StartCoroutine(Main.Instance.Web.Register(
                inputUsername.GetComponent<TMP_InputField>().text, 
                inputPassword.GetComponent<TMP_InputField>().text,
                inputEmail.GetComponent<TMP_InputField>().text));

            Invoke("ShowMessage",0.5f);
            Invoke("ClearField",0.5f);
        });
    }

    void ShowMessage()
    {
        popUpMessage.text = regMessage;
        popUpPanel.GetComponent<Image>().raycastTarget = true;
        popUpPanel.GetComponent<Animator>().SetTrigger("isOpen");
    }
    public void ClearField()
    {
        inputUsername.GetComponent<TMP_InputField>().text = "";
        inputPassword.GetComponent<TMP_InputField>().text = "";
        inputEmail.GetComponent<TMP_InputField>().text = "";
    }

    public void OkayRegisterOkay()
    {
        //do close popup panel
        popUpPanel.GetComponent<Animator>().SetTrigger("isClose");
        popUpPanel.GetComponent<Image>().raycastTarget = false;
    }
}

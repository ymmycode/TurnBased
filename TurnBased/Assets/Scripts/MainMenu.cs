using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;

public class MainMenu : MonoBehaviour
{

    public GameObject optionPanel,
        syncPanel,
        mainMenu,
        formPanel, buttonForm, backButtonSync,
        syncButton, panelNameInfo, userButton, userSyncedData, blockButton;
    public Login login;
    public Register register;
    public PlayableDirector transition;
    public string lastPlayedScene;

    private void Update()
    {
        lastPlayedScene = Main.Instance.Web.lastScene;
    }
    public void PlayGame()
    {
        transition.Play();
        Invoke("GameScene",4.99f);
    }
    void GameScene()
    {
        if(lastPlayedScene == "")
        {
            SceneManager.LoadScene("C1");
        }
        else if(lastPlayedScene != "")
        {
            SceneManager.LoadScene(lastPlayedScene);
        }
    }

    public void SaveSyncOption()
    {
        //do courutine to upload setting
    }
    
    public void OpenOption()
    {
        mainMenu.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void BackOption()
    {
        optionPanel.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OpenSync()
    {
        mainMenu.SetActive(false);
        syncPanel.SetActive(true);
    }

    public void BackSync()
    {
        syncPanel.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoginOpenAnim()
    {
        backButtonSync.GetComponent<Animator>().SetTrigger("isOpen");
        formPanel.GetComponent<Animator>().SetTrigger("openLogin");
        buttonForm.GetComponent<Animator>().SetTrigger("loginClick");
    }

    public void RegisOpenAnim()
    {
        backButtonSync.GetComponent<Animator>().SetTrigger("isOpen");
        formPanel.GetComponent<Animator>().SetTrigger("openRegis");
        buttonForm.GetComponent<Animator>().SetTrigger("regisClick");
    }
    public void LoginCloseAnim()
    {
        login.ClearField();
        backButtonSync.GetComponent<Animator>().SetTrigger("isClose");
        formPanel.GetComponent<Animator>().SetTrigger("closeLogin");
        buttonForm.GetComponent<Animator>().SetTrigger("closeLogin");
    }

    public void RegisCloseAnim()
    {
        register.ClearField();
        backButtonSync.GetComponent<Animator>().SetTrigger("isClose");
        formPanel.GetComponent<Animator>().SetTrigger("closeRegis");
        buttonForm.GetComponent<Animator>().SetTrigger("closeRegis");
    }

    public void CloseProfilePanel()
    {
        blockButton.GetComponent<Image>().raycastTarget = true;
        userSyncedData.GetComponent<Animator>().SetTrigger("isClose");
        Invoke("CloseUserProfile",4f);
    }

    void CloseUserProfile()
    {
        userSyncedData.SetActive(false);
        userButton.SetActive(true);
        mainMenu.SetActive(true);
    }

    public void OpenProfilePanel()
    {
        blockButton.GetComponent<Image>().raycastTarget = false;
        mainMenu.SetActive(false);
        userButton.SetActive(false);
        userSyncedData.SetActive(true);
        userSyncedData.GetComponent<Animator>().SetTrigger("isOpen");
    }

}

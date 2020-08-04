using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class PauseGame : MonoBehaviour
{
    static PauseGame instance;
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public PlayerFollow playerFollow;
    public GameObject heroItSelf, popUp;
    GameObject data;

/*
    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
*/

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {Resume();}
            else
            {
                    Pause();
                    if(gameIsPaused == true){}
            }
        }
    }

    public void Resume()
    {
        heroItSelf.GetComponent<MovementMech>().enabled = true;
        playerFollow.enabled = true;
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        heroItSelf.GetComponent<MovementMech>().enabled = false;
        playerFollow.enabled = false;
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Main Menu with Database");
        Destroy(this.gameObject);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SaveSceneToDatabase()
    {
        StartCoroutine(Main.Instance.Web.UpdateLastScene());
        Invoke("SavedPopUp",0.5f);
    }

    void SavedPopUp()
    {
        popUp.SetActive(true);
        popUp.GetComponentInChildren<TMP_Text>().SetText(Main.Instance.Web.saveMessage);
        Invoke("ClosePopUp",1f);
    }

    void ClosePopUp(){popUp.SetActive(false);}

    public void ResetScene()
    { 
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavedValue : MonoBehaviour
{
    static SavedValue instance;
    public string userID, userName, lastScene, lastPlayed, activeScene;

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
    private void Update()
    {
        userID = Main.Instance.Web.userID;
        userName = Main.Instance.Web.userNameNew;
        lastScene = Main.Instance.Web.lastScene;
        lastPlayed = Main.Instance.Web.nowDate;
        GettingActiveScene();
    }

    void GettingActiveScene()
    {
        activeScene = SceneManager.GetActiveScene().name;
    }
}

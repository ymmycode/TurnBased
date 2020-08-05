using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    static Music instance;
    public string activeScene;

    void Awake() 
    {
        if(instance == null )
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else if(instance != this )
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        activeScene = SceneManager.GetActiveScene().name;
        if(activeScene == "Main Menu with Database" || activeScene == "C1TBC" ) 
        {Destroy(this.gameObject);}
    }
}

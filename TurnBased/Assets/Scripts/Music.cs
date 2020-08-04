using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    static Music instance;

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

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Main Menu with Database")
        {Destroy(this.gameObject);}
    }
}

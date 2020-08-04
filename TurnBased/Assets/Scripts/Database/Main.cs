using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;
    public Web Web;
    public SavedValue SavedValue;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Web = GetComponent<Web>();
        SavedValue = GetComponent<SavedValue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

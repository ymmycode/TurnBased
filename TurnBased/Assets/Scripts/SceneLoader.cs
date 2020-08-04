using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int nextScene;
    public float waitTime;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndChangeScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitAndChangeScene()
    {
        
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(nextScene);
    }
}

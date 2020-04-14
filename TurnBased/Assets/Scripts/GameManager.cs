using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager gameInstance;

    //hero gameobject
    public GameObject heroCharacter;

    //hero position
    public Vector3 newHeroPosition;
    public Vector3 previousHeroPosition;

    //scene
    public string sceneToLoad;
    public string lastScene;//battle

    private void Awake()
    {
        //checking this game instance is already existing
        if (gameInstance == null)
        {
            //if not exist, set to this
            gameInstance = this;
        }
        else if (gameInstance != this) //if exist but not this specific instance
        {
            //destroy
            Destroy(gameObject);
        }

        //set this game object to be indestructible
        DontDestroyOnLoad(gameObject);

        if (!GameObject.Find("Hero"))
        {
            GameObject hero = 
                Instantiate(heroCharacter, newHeroPosition, Quaternion.identity) as GameObject;
            hero.name = "Hero";
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);    
    }
}

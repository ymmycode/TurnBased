using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager gameInstance;

    public RegionData currentRegion;
    
    //public List<RegionData> regions = new List<RegionData>();
    
    //hero gameobject
    public GameObject heroCharacter;

    //hero position
    public Vector3 newHeroPosition;
    public Vector3 previousHeroPosition;

    //scene
    public string sceneToLoad;
    public string lastScene;//battle

    //logic
    public bool isWalking = false;
    public bool canGetEncounter = false;
    public bool gettingAttacked = false;

    //game state machine
    public enum GameStates
    {
        WORLD_STATE,
        TOWN_STATE,
        BATTLE_STATE,
        IDLE_STATE
    }
    public GameStates gameState;
    public int enemyAmount;

    //enemy deployment
    public List<GameObject> enemiesToBattle = new List<GameObject>();

    //spawn pointn
    public string nextSpawnPoint;

    private void Update()
    {
        switch (gameState)
        {
            case (GameStates.WORLD_STATE):
                if (isWalking)
                {
                    RandomEncounter();
                }

                if (gettingAttacked)
                {
                    gameState = GameStates.BATTLE_STATE;
                }
                break;

            case (GameStates.TOWN_STATE):

                break;

            case (GameStates.BATTLE_STATE):
                //load battle scene
                StartBattle();

                //game state idling, waiting battle end
                gameState = GameStates.IDLE_STATE;
                break;

            case (GameStates.IDLE_STATE):
                //idle state
                break;
        }
    }

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

    public void LoadSceneAfterBattle()
    {
        SceneManager.LoadScene(lastScene);
    }

    void RandomEncounter()
    {
        if (isWalking && canGetEncounter)
        {
            if (Random.Range(0,1000) < 10)
            {
                //Debug.Log("getting attacked by enemy");
                gettingAttacked = true;
            }
        }
    }

    void StartBattle()
    {
        //setting amount of enemies 
        enemyAmount = Random.Range(1, currentRegion.maxAmountEnemies + 1);

        //which enemies are we going encountered
        for (int i = 0; i < enemyAmount; i++)
        {
            enemiesToBattle.Add(currentRegion.possibleEnemies[Random.Range(0, currentRegion.possibleEnemies.Count)]);
            
        }

        //hero latest position
        previousHeroPosition = GameObject.Find("Hero").gameObject.transform.position;

        //storing to next position
        newHeroPosition = previousHeroPosition;

        //previous
        lastScene = SceneManager.GetActiveScene().name;

        //load the level
        SceneManager.LoadScene(currentRegion.battleScene);

        //reset
        isWalking = false;
        gettingAttacked = false;
        canGetEncounter = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{
    //this class is managing state of battle gameplay system

    public enum PerformAction 
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION
    }

    public PerformAction battleStates;

    //storing data message 
    public List<HandleTurn> performList = new List<HandleTurn>();
    public List<GameObject> heroesInBattle = new List<GameObject>();
    public List<GameObject> enemyInBattle = new List<GameObject>();

    //
    public enum HeroGUI
    { 
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        DONE
    }
    public HeroGUI heroInput;

    public List<GameObject> heroesToManage = new List<GameObject>();
    HandleTurn heroChoise;

    public GameObject enemyButton;
    
    //button parrent
    public Transform spacer;

    //
    public GameObject attackPanel;//action panel
    public GameObject enemySelectPanel;//selected targert pannel
    

    // Start is called before the first frame update
    void Start()
    {
        battleStates = PerformAction.WAIT;
        enemyInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy")); //how many Enemy is in the game
        heroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero")); //how many Hero is in the game

        attackPanel.SetActive(false);
        enemySelectPanel.SetActive(false);
        


        EnemyButton();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(battleStates);
        switch (battleStates)
        {
            case (PerformAction.WAIT):
                if (performList.Count > 0)
                {
                    battleStates = PerformAction.TAKEACTION;
                }
                break;

            case (PerformAction.TAKEACTION):
                //get attacker and the name of the attacker
                GameObject performer = performList[0].attacksGameObject;//target list

                if (performList[0].type == "Enemy")//who is going to this animation
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                    //checking is currently attacked hero is in battle
                    for ( int i = 0; i < heroesInBattle.Count; i++)
                    {
                        //checking attacker target same with hero in battle
                        if (performList[0].attackersTarget == heroesInBattle[i])
                        {
                            ESM.heroToAttack = performList[0].attackersTarget;
                            ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                            break;
                        }
                        else 
                        {
                            //prevent attack on dead hero
                            performList[0].attackersTarget = heroesInBattle[Random.Range(0, heroesInBattle.Count)];
                            ESM.heroToAttack = performList[0].attackersTarget;
                            ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                        }
                    }
                }
                
                if (performList[0].type == "Hero")
                {
                    HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
                    HSM.enemyToAttack = performList[0].attackersTarget;
                    HSM.currentState = HeroStateMachine.TurnState.ACTION;
                }

                battleStates = PerformAction.PERFORMACTION;

                break;

            case (PerformAction.PERFORMACTION):
                //idle
                break;    
        }
        switch (heroInput)
        {
            case (HeroGUI.ACTIVATE):
                if (heroesToManage.Count > 0)
                {
                    heroesToManage[0].transform.Find("Selector").gameObject.SetActive(true);
                    heroChoise = new HandleTurn();

                    attackPanel.SetActive(true);
                    heroInput = HeroGUI.WAITING;//set to waiting after activated

                }
                break;

            case (HeroGUI.WAITING):

                break;

            case (HeroGUI.DONE):
                HeroInputDone();
                break;

        }
    }

    public void CollectActionInformation(HandleTurn input) //require handleturn input
    {
        performList.Add(input);
    }

    void EnemyButton()
    {
        foreach (GameObject enemy in enemyInBattle)
        {
            GameObject newButton = Instantiate(enemyButton) as GameObject;//populate button with enemies object
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

            EnemyStateMachine currentEnemy = enemy.GetComponent<EnemyStateMachine>();
            
            Text buttonText = newButton.GetComponentInChildren<Text>();
            buttonText.text = currentEnemy.enemy.theName;//set button text to enemy nname

            button.enemyPrefab = enemy;
            newButton.transform.SetParent(spacer, false);
        }
    }

    public void AttackButton()
    {
        heroChoise.attacker = heroesToManage[0].name; //storing the name of attacker
        heroChoise.attacksGameObject = heroesToManage[0];//storing attacker game object
        heroChoise.type = "Hero"; //set the type to hero

        attackPanel.SetActive(false);
        enemySelectPanel.SetActive(true);
    }

    public void EnemySelection(GameObject choosenEnemy)
    {
        heroChoise.attackersTarget = choosenEnemy;//selected target
        heroInput = HeroGUI.DONE;
    }

    public void HeroInputDone()
    {
        performList.Add(heroChoise);
        enemySelectPanel.SetActive(false);
        heroesToManage[0].transform.Find("Selector").gameObject.SetActive(false);
        heroesToManage.RemoveAt(0);
        heroInput = HeroGUI.ACTIVATE;
    }
}

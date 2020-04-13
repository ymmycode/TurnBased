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
        PERFORMACTION,
        CHECKLIFE,
        WIN,
        LOSE
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

    //panel
    public GameObject attackPanel;//action panel
    public GameObject enemySelectPanel;//selected targert pannel
    public GameObject magicPanel;//magic panel
    
    //action of heroes
    public Transform actionLayout;
    public Transform magicLayout;
    public GameObject actionButton;
    public GameObject magicButton;
    private List<GameObject> attackButtons = new List<GameObject>();

    //enemy button
    private List<GameObject> enemyButtons = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        battleStates = PerformAction.WAIT;
        enemyInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy")); //how many Enemy is in the game
        heroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero")); //how many Hero is in the game

        attackPanel.SetActive(false);
        enemySelectPanel.SetActive(false);
        magicPanel.SetActive(false);

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

            case (PerformAction.CHECKLIFE):
                if (heroesInBattle.Count < 1)
                {
                    //lose the battle
                    battleStates = PerformAction.LOSE;
                }
                else if (enemyInBattle.Count < 1)
                {
                    //win the battle
                    battleStates = PerformAction.WIN;
                }
                else 
                {
                    ClearAttackButton();
                    heroInput = HeroGUI.ACTIVATE;
                }
                break;

            case (PerformAction.WIN):
                
                break;

            case (PerformAction.LOSE):
                
                break;
        }

        //hero panel input
        switch (heroInput)
        {
            case (HeroGUI.ACTIVATE):
                if (heroesToManage.Count > 0)
                {
                    heroesToManage[0].transform.Find("Selector").gameObject.SetActive(true);
                    heroChoise = new HandleTurn();



                    attackPanel.SetActive(true);
                    CreateButton();

                    heroInput = HeroGUI.WAITING;//set to waiting after activa
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

    public void EnemyButton()
    {
        //cleanup
        foreach (GameObject enemyButton in enemyButtons)
        {
            Destroy(enemyButton);
        }
        enemyButtons.Clear();

        //create button
        foreach (GameObject enemy in enemyInBattle)
        {
            GameObject newButton = Instantiate(enemyButton) as GameObject;//populate button with enemies object
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

            EnemyStateMachine currentEnemy = enemy.GetComponent<EnemyStateMachine>();
            
            Text buttonText = newButton.GetComponentInChildren<Text>();
            buttonText.text = currentEnemy.enemy.theName;//set button text to enemy nname

            button.enemyPrefab = enemy;
            newButton.transform.SetParent(spacer, false);
            enemyButtons.Add(newButton);
        }
    }

    public void AttackButton()
    {
        heroChoise.attacker = heroesToManage[0].name; //storing the name of attacker
        heroChoise.attacksGameObject = heroesToManage[0];//storing attacker game object
        heroChoise.type = "Hero"; //set the type to hero

        heroChoise.choosenAttack =
            heroesToManage[0].GetComponent<HeroStateMachine>().baseHero.attacks[0];

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
        ClearAttackButton();

        heroesToManage[0].transform.Find("Selector").gameObject.SetActive(false);
        heroesToManage.RemoveAt(0);
        heroInput = HeroGUI.ACTIVATE;
    }

    void ClearAttackButton()
    {
        enemySelectPanel.SetActive(false);
        attackPanel.SetActive(false);
        magicPanel.SetActive(false);

        //delete button after done input
        foreach (GameObject attackButton in attackButtons)
        {
            Destroy(attackButton);
        }
        attackButtons.Clear();

    }

    void CreateButton()
    {
        GameObject attackButton = Instantiate(actionButton) as GameObject;
        Text attackButtonText = attackButton.transform.Find("Text").gameObject.GetComponent<Text>();
        attackButtonText.text = "Attack";
        attackButton.GetComponent<Button>().onClick.AddListener(() => AttackButton());
        attackButton.transform.SetParent(actionLayout, false);
        attackButtons.Add(attackButton);


        GameObject magicButton = Instantiate(actionButton) as GameObject;
        Text magicButtonText = magicButton.transform.Find("Text").gameObject.GetComponent<Text>();
        magicButtonText.text = "Magic";
        magicButton.GetComponent<Button>().onClick.AddListener(() => MagicSpellsPanel());
        magicButton.transform.SetParent(actionLayout, false);
        attackButtons.Add(magicButton);

        //check if is there any magic skill in that current heroes
        var magicOnCurrentHeroes = heroesToManage[0].GetComponent<HeroStateMachine>().baseHero.magicAttacks;
        if (heroesToManage[0].GetComponent<HeroStateMachine>().baseHero.magicAttacks.Count > 0) //only perform the next action
        {
            foreach (BaseAttack magic in heroesToManage[0].GetComponent<HeroStateMachine>().baseHero.magicAttacks)
            {
                
                //create button
                GameObject magicSpellButton = Instantiate(magicButton) as GameObject;
                Text spellButtonText = magicSpellButton.transform.Find("Text").gameObject.GetComponent<Text>();
                spellButtonText.text = magic.attackName;
   
                MagicAttackButton MAB = magicSpellButton.GetComponent<MagicAttackButton>();
                MAB.magicSpellToPerform = magic;
                Debug.Log(MAB.magicSpellToPerform);
                Debug.Log(magic);

                
                Debug.Log(MAB.magicSpellToPerform);


                magicSpellButton.transform.SetParent(magicLayout, false);
                attackButtons.Add(magicSpellButton);
            }
        }
        else
        {
            magicButton.GetComponent<Button>().interactable = false;
        }
    }

    public void TargetMagicAttack(BaseAttack targetMagic)
    {
        heroChoise.attacker = heroesToManage[0].name; //storing the name of attacker
        heroChoise.attacksGameObject = heroesToManage[0];//storing attacker game object
        heroChoise.type = "Hero"; //set the type to hero

        heroChoise.choosenAttack = targetMagic;
        magicPanel.SetActive(false);
        enemySelectPanel.SetActive(true);
    }

    public void MagicSpellsPanel()
    {
        enemySelectPanel.SetActive(false);
        attackPanel.SetActive(false);
        magicPanel.SetActive(true);
    }
}

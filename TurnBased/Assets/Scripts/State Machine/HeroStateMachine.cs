using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour
{
    BattleStateMachine BSM;
    
    //using base hero class
    public BaseHero baseHero;

    //hero state
    public enum TurnState
    { 
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTION,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    //for progress bar
    float currentCooldown = 0f;
    float maxCooldown = 5f;
    float calculateCooldown;
    public Image progressBar;

    public GameObject selector;

    public GameObject enemyToAttack;
    private bool actionStarted = false;
    private Vector3 startPosition;
    float animationSpeed = 10f;
    
    //deadlogic
    public bool isAlive = true;

    //hero stat panel
    private HeroStatPanel statsPanel;
    public GameObject heroNewPanel;
    public Transform heroPanelSpacer;

    // Start is called before the first frame update
    void Start()
    {
        //find spacer or layout
        //set the panel
        CreateHeroPanel();

        startPosition = transform.position;
        //try get creative later on, use this to modifiy currennt cooldown
        //and lets call it "luck" or "stamina" status variable 
        currentCooldown = Random.Range(0, 2f);
        
        selector.SetActive(false);
        BSM = GameObject.Find("Battle Manager").GetComponent<BattleStateMachine>();

        //starting state
        currentState = TurnState.PROCESSING;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentState);
        switch (currentState)
        {
            case (TurnState.PROCESSING) :
                UpdateProgressBar();
                break;

            case (TurnState.ADDTOLIST):
                BSM.heroesToManage.Add(this.gameObject);
                currentState = TurnState.WAITING;
                break;

            case (TurnState.WAITING):
                //wait for input
                break;

            case (TurnState.ACTION):
                StartCoroutine(TimeForAction());
                break;

            case (TurnState.DEAD):
                if (!isAlive)
                {

                }
                else 
                {
                    //change tag
                    this.gameObject.tag = "Dead Hero";

                    //disable oncoming attack
                    BSM.heroesInBattle.Remove(this.gameObject);

                    //not managable 
                    BSM.heroesToManage.Remove(this.gameObject);

                    //disactivate the slector
                    selector.SetActive(false);

                    //reset GUI
                    BSM.attackPanel.SetActive(false);
                    BSM.enemySelectPanel.SetActive(false);

                    if (BSM.heroesInBattle.Count > 0)
                    {
                        //remove from the list
                        for (int i = 0; i < BSM.performList.Count; i++)
                        {
                            if (BSM.performList[i].attacksGameObject == this.gameObject)
                            {
                                BSM.performList.Remove(BSM.performList[i]);//remover
                            }

                            if (BSM.performList[i].attackersTarget == this.gameObject)
                            {
                                BSM.performList[i].attackersTarget =
                                    BSM.heroesInBattle[Random.Range(0, BSM.heroesInBattle.Count)];
                            }
                        }
                    }

                    //Channge this color gameobject or play adead aanimation
                    this.gameObject.GetComponentInChildren<MeshRenderer>().material.color =
                        new Color32(105,105,105,255);

                    BSM.battleStates = BattleStateMachine.PerformAction.CHECKLIFE;
                    //reset hero input
                    isAlive = false;
                }
                break;

        }
    }

    void UpdateProgressBar()
    {
        currentCooldown = currentCooldown + Time.deltaTime;
        calculateCooldown = currentCooldown / maxCooldown;
        progressBar.transform.localScale =
            new Vector3(
                Mathf.Clamp(calculateCooldown, 0, 1),
                progressBar.transform.localScale.y,
                progressBar.transform.localScale.z
                );

        if (currentCooldown >= maxCooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }
    IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        //enemy attack animation start
        Vector3 enemyPosition = new Vector3(
            enemyToAttack.transform.position.x - 1.5f,
            enemyToAttack.transform.position.y,
            enemyToAttack.transform.position.z
            );

        //while enemy moving toward hero, do nothin
        while (MoveTowardsEnemy(enemyPosition)) { yield return null; }

        //wait
        yield return new WaitForSeconds(0.5f);

        //attack, do some damage
        DoDamage();

        //enemy init postiion animation start
        Vector3 firstPosition = startPosition;
        while (MoveToStartPosition(firstPosition)) { yield return null; }

        //remove this performfrom BSM list
        BSM.performList.RemoveAt(0); // delete at this state, so next state can add

        //rest BSM to wait
        BSM.battleStates = BattleStateMachine.PerformAction.WAIT;

        //end coroutine
        actionStarted = false;
        //reset enemy state
        currentCooldown = 0f;
        currentState = TurnState.PROCESSING;

    }

    bool MoveTowardsEnemy(Vector3 target)
    {
        return target !=
            (transform.position =
                Vector3.MoveTowards(transform.position,
                    target,
                    animationSpeed * Time.deltaTime));
    }

    bool MoveToStartPosition(Vector3 target)
    {
        return target !=
            (transform.position =
                Vector3.MoveTowards(transform.position,
                    target,
                    animationSpeed * Time.deltaTime));
    }

    public void TakeDamageFromEnemy(float getDamageAmount)
    {
        baseHero.currentHP -= getDamageAmount;
        if (baseHero.currentHP <= 0)
        {
            baseHero.currentHP = 0;
            currentState = TurnState.DEAD;
        }
        UpdateHeroPanel();
    }

    void DoDamage()
    {
        float calculateDamage = baseHero.currentATK + BSM.performList[0].choosenAttack.attackDamage;
        enemyToAttack.GetComponent<EnemyStateMachine>().TakeDamageFromHero(calculateDamage);
    }

    void CreateHeroPanel()
    {

        heroNewPanel = Instantiate(heroNewPanel) as GameObject;
        statsPanel = heroNewPanel.GetComponent<HeroStatPanel>();
        statsPanel.heroName.text = baseHero.theName;
        statsPanel.heroHP.text = "HP : " + baseHero.currentHP.ToString();
        statsPanel.heroMP.text = "MP : " + baseHero.currentMP.ToString();

        progressBar = statsPanel.progressBar;
        heroNewPanel.transform.SetParent(heroPanelSpacer, false);

    }

    //update and refresh hero stats panel
    void UpdateHeroPanel()
    {
        statsPanel.heroHP.text = "HP : " + baseHero.currentHP.ToString();
        statsPanel.heroMP.text = "MP : " + baseHero.currentMP.ToString();
    }
}

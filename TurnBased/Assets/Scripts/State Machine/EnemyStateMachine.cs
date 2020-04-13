using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    BattleStateMachine BSM;
    public BaseEnemy enemy;

    //Enemy state
    public enum TurnState
    {
        PROCESSING,
        CHOOSEACT,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    //for progress bar
    float currentCooldown = 0f;
    float maxCooldown = 5f;

    //this gameobject position
    Vector3 startPosition;

    //action logic
    bool actionStarted = false;
    public GameObject heroToAttack;
    float animationSpeed = 10f;

    public GameObject selector;

    //dead logic
    private bool isAlive = true;


    // Start is called before the first frame update
    void Start()
    {
        selector.SetActive(false);
        currentState = TurnState.PROCESSING;
        BSM = GameObject.Find("Battle Manager").GetComponent<BattleStateMachine>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentState);
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                UpdateProgressBar();

                break;

            case (TurnState.CHOOSEACT):
                ChooseAction();
                currentState = TurnState.WAITING;
                break;

            case (TurnState.WAITING):
                //idle
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
                    //change tag of enemy
                    this.gameObject.tag = "Dead Enemy";

                    //cannot be attacked
                    BSM.enemyInBattle.Remove(this.gameObject);

                    //disable selector
                    selector.SetActive(false);

                    //remove all hero enemy input
                    for (int i = 0; i < BSM.performList.Count; i++)
                    {
                        if (BSM.performList[i].attacksGameObject == this.gameObject)
                        {
                            BSM.performList.Remove(BSM.performList[i]); 
                        }
                    }

                    //change the color or dead animation
                    this.gameObject.GetComponentInChildren<MeshRenderer>().material.color = 
                        new Color32(105, 105, 105, 255);//gray

                    //set to dead
                    isAlive = false;

                    //reset or refresh enemy button
                    BSM.EnemyButton();

                    //Cheking condition
                    BSM.battleStates = BattleStateMachine.PerformAction.CHECKLIFE;
                }

                break;

        }
    }

    void UpdateProgressBar()
    {
        currentCooldown = currentCooldown + Time.deltaTime;

        if (currentCooldown >= maxCooldown)
        {
            currentState = TurnState.CHOOSEACT;
        }
    }

    void ChooseAction()
    {
        HandleTurn myAttack = new HandleTurn();
        myAttack.attacker = enemy.theName;
        myAttack.type = "Enemy";
        myAttack.attacksGameObject = this.gameObject;
        myAttack.attackersTarget = BSM.heroesInBattle[Random.Range(0, BSM.heroesInBattle.Count)];

        int num = Random.Range(0, enemy.attacks.Count);
        myAttack.choosenAttack = enemy.attacks[num];
        //Debug.Log(this.gameObject + "has choosen" 
        //    + myAttack.choosenAttack.attackName +
        //   ", damage output" + myAttack.choosenAttack.attackDamage);

        BSM.CollectActionInformation(myAttack);
    }

    IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;

        //enemy attack animation start
        Vector3 heroPosition = new Vector3(
            heroToAttack.transform.position.x + 1.5f,
            heroToAttack.transform.position.y,
            heroToAttack.transform.position.z
            );
        
        //while enemy moving toward hero, do nothin
        while (MoveTowardsEnemy(heroPosition)){yield return null;}

        //wait
        yield return new WaitForSeconds(0.5f);

        //attack, do some damage
        DoDamage();

        //enemy init postiion animation start
        Vector3 firstPosition = startPosition;
        while(MoveToStartPosition(firstPosition)){ yield return null; }

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
                    animationSpeed * Time.deltaTime ));
    }

    bool MoveToStartPosition(Vector3 target)
    {
        return target !=
            (transform.position =
                Vector3.MoveTowards(transform.position,
                    target,
                    animationSpeed * Time.deltaTime));
    }

    void DoDamage()
    {
        float currentAttack = enemy.currentATK;
        float choosenAttack = BSM.performList[0].choosenAttack.attackDamage;
        float calculatedDamage = currentAttack + choosenAttack;
        heroToAttack.GetComponent<HeroStateMachine>().TakeDamageFromEnemy(calculatedDamage);
    }

    public void TakeDamageFromHero(float getDamageAmount)
    {
        enemy.currentHP -= getDamageAmount;
        if ( enemy.currentHP <= 0)
        {
            enemy.currentHP = 0;
            currentState = TurnState.DEAD;
        }
    }
}

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


    // Start is called before the first frame update
    void Start()
    {
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
        myAttack.attacker = enemy.name;
        myAttack.type = "Enemy";
        myAttack.attacksGameObject = this.gameObject;
        myAttack.attackersTarget = BSM.heroesInBattle[Random.Range(0, BSM.heroesInBattle.Count)];
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
}

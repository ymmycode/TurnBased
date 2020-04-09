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
    [SerializeField] Image progressBar;

    public GameObject selector;

    public GameObject enemyToAttack;
    private bool actionStarted = false;
    private Vector3 startPosition;
    float animationSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
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
}

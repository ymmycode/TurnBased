using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
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
                
                break;

            case (TurnState.WAITING):
                
                break;

            case (TurnState.SELECTION):
                
                break;

            case (TurnState.ACTION):
                
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
}

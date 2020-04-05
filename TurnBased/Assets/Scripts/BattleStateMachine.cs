using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    // Start is called before the first frame update
    void Start()
    {
        battleStates = PerformAction.WAIT;
        enemyInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy")); //how many Enemy is in the game
        heroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero")); //how many Hero is in the game
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

                break;

            case (PerformAction.PERFORMACTION):

                break;
        }
    }

    public void CollectActionInformation(HandleTurn input) //require handleturn input
    {
        performList.Add(input);
    }
}

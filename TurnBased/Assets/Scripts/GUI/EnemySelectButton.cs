using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject enemyPrefab;
    private bool showSelector;

    public void SelectEnemy() 
    {
        GameObject.Find("Battle Manager").GetComponent<BattleStateMachine>().EnemySelection(enemyPrefab);//save input
    }

    public void HideSelector()
    {
        enemyPrefab.transform.Find("Selector").gameObject.SetActive(false);
    }

    public void ShowSelector()
    {
        enemyPrefab.transform.Find("Selector").gameObject.SetActive(true);
    }
}

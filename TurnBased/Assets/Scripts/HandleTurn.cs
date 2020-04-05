using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandleTurn
{
    public string attacker; //name of attacker
    public string type; //object type
    public GameObject attacksGameObject; //can contain hero or ennemy state machine, and doing right script
    public GameObject attackersTarget; //who is going to be attacked

    //


}

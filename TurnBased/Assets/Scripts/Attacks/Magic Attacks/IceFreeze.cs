using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFreeze : BaseAttack
{
    public IceFreeze()
    {
        attackName = "Freeze";
        attackDescription = "Froze the enemy, so they had cold later on";
        attackDamage = 10f;
        attackCost = 5f;

    }
}

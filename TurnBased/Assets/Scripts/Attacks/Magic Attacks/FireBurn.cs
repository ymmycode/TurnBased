using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBurn : BaseAttack
{
    public FireBurn()
    {
        attackName = "Burn";
        attackDescription = "burning enemies with fire, it's efective";
        attackDamage = 20f;
        attackCost = 10f;
    }
}

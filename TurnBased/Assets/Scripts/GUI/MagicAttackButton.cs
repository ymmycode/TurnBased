using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttackButton : MonoBehaviour
{
    public BaseAttack magicSpellToPerform;

    public void CastSpellMagic()
    {
        GameObject.Find("Battle Manager").GetComponent<BattleStateMachine>().TargetMagicAttack(magicSpellToPerform);
    }
}

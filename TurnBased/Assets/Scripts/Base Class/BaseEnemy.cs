using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemy : BaseClass
{
    //Enemy Status
    public enum Type
    { 
        GRASS,
        FIRE,
        WATER,
        ELECTRIC
    };

    public enum Rarity
    {
        COMMON, 
        UNCOMMON,
        RARE,
        SUPERRARE
    };


    public Type enemyType;
    public Rarity rarity;
}

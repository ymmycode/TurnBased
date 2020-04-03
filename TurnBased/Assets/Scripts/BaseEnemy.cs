using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemy
{

    //Enemy Status

    public string name;

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

    public float baseHP;
    public float currentHP;

    public float baseMP;
    public float currentMP;

    public float baseATK;
    public float currentATK;
    public float baseDEF;
    public float currentDEF;


}

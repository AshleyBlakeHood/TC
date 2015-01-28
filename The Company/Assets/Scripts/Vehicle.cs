using UnityEngine;
using System.Collections;

[System.Serializable]
public class Vehicle
{
    public string type = "";
    public float age = 1;
    public float range = 1;
    //Limitation Variable
    public float armour = 1;
    public int noise = 1;
    public int agentCapacity = 4;
    public float price = 1;

    public Vehicle[] upgrades; //0 = Base Level, 1 = Level 2 etc.
    public int fearFactor = 1;

    public int storageUnits = 10;
    public float rarity = 70;


}

//Design
//Fear Factor
//Storage Requirements
//Rarity
//Weapons
//Would have own stats
//Difficulty of operation
//Morale
//Region of Origin
//Speed

using UnityEngine;
using System.Collections;

[System.Serializable]
public class Armour
{
    public enum ArmourLocation { Hands, Arms, Torso, Head, Legs, Feet };

    //Details
    string name = "Armour";
    string material = "Material";
    ArmourLocation armourLocation = ArmourLocation.Torso;
    string regionOfOrigin = "TC";

    //Gameplay
    int resistanceFactor = 1;
    int storageCapacity = 1;
    float weight = 1;
    string size = "Small";
    public float price = 250;
    int fearFactor = 1;
    int moraleBoost = 1;
    float rarity = 80;
    int storageUnits = 1;
    Armour[] upgrades;

    //Spyy Stats
    int noise = 1;
    bool concealable = false;
}

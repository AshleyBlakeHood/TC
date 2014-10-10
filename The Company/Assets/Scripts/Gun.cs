using UnityEngine;
using System.Collections;

[System.Serializable]
public class Gun
{
	//Weapony Stats
	public int damage = 1;
	public int accuracy = 1;
	public int range = 100;
	public int clipSize = 7;
	public float fireRate = 1.2f;
	public float reloadTime = 3.4f;

	//Details
	public string name = "Gun";
	public float weight = 1.7f;
	public string size = "Small";
	public float price = 250;
	public int fearFactor = 1;

	//Spyy Stats
	public int noise = 7; //0 - 10.
	public bool concealable = true;

	//Gameplay
	public int moraleBoost = 1;
	public float rarity = 70; //Higher is less rare.
	public string regionOfOrigin = "Earth";
	public float age;
	public int storageUnits = 1;
	public Gun[] upgrades; //0 = Base Level, 1 = Level 2 etc.

	//Inside Game
	public Texture2D image;

	public Gun (string nam, int dam, int acc, int iRange, int cSize, float fRate, float rTime, float wight, string sze, float iPrice, int fFactor, int iNoise, bool conceal, int mBoost, float iRarity,
	            string region, float iAge, int storage, Gun[] iUpgrades)
	{
		name = nam;
		damage = dam;
		acc = accuracy;
		range = iRange;
		clipSize = cSize;
		fireRate = fRate;
		reloadTime = rTime;
		weight = wight;
		size = sze;
		price = iPrice;
		fearFactor = fFactor;
		noise = iNoise;
		concealable = conceal;
		moraleBoost = mBoost;
		rarity = iRarity;
		regionOfOrigin = region;
		age = iAge;
		storageUnits = storage;
		upgrades = iUpgrades;
	}
}
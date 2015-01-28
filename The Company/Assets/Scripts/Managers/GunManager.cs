using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunManager : MonoBehaviour
{
	public TextAsset gunList;
	public TextAsset[] externalGunList; //Needed for mods.

	public Gun[] allGuns;

	// Use this for initialization
	void Start ()
	{
		ReadInGunData (gunList, false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void ReadInGunData(TextAsset file, bool append)
	{
        VeCSV gunCSV = new VeCSV(file.text, VeCSV.HeaderDirection.Horizontal);

		//Initalize gun array.
        allGuns = new Gun[gunCSV.subValueCount - 1];

		//Pull out the data using the parsed indexes.
        for (int i = 1; i < gunCSV.subValueCount; i++)
		{
            string name = gunCSV.Get("Name", i);
            string damage = gunCSV.Get("Damage", i);
            string accuracy = gunCSV.Get("Accuracy", i);
            string range = gunCSV.Get("Range (m)", i);
            string clipSize = gunCSV.Get("Clip Size", i);
            string fireRate = gunCSV.Get("Fire Rate (RPM)", i);
            string reloadTime = gunCSV.Get("Reload Time", i);
            string weight = gunCSV.Get("Weight (g)", i);
            string size = gunCSV.Get("Size", i);
            string price = gunCSV.Get("Price", i);
            string fearFactor = gunCSV.Get("Fear Factor", i);
            string noise = gunCSV.Get("Noise", i);
            string concealable = gunCSV.Get("Concealable", i);
            string moraleBoost = gunCSV.Get("Morale Boost", i);
            string rarity = gunCSV.Get("Rarity", i);
            string region = gunCSV.Get("Region of Origin", i);
            string storage = gunCSV.Get("Storage Units", i);

            allGuns[i - 1] = new Gun(name, int.Parse(damage), int.Parse(accuracy), int.Parse(range), int.Parse(clipSize), float.Parse(fireRate), float.Parse(reloadTime), float.Parse(weight), size, float.Parse(price), int.Parse(fearFactor), int.Parse(noise),
                                BoolParse(concealable), int.Parse(moraleBoost), float.Parse(rarity), region, 0, int.Parse(storage));
		}
	}

	public bool BoolParse(string s)
	{
		string[] trues = {"1", "TRUE", "T", "Y"};
		string[] falses = {"0", "FALSE", "F", "N"};

		//Convert to upper to limit the comparisons required.
		s = s.ToUpper ();

		for (int i = 0; i < trues.Length; i++)
		{
			if (s == trues[i])
				return true;
			else if (s == falses[i])
				return false;
		}

		Debug.LogError ("Could not parse a bool from string. False was returned, behaviour may be unexpected!", this);
		return false;
	}
}

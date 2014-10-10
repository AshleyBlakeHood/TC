using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunManager : MonoBehaviour
{
	public TextAsset gunList;
	public TextAsset[] externalGunList; //Needed for mods.

	public Gun[] allGuns;
	public List<Gun> gunMarketGuns = new List<Gun>();
	public List<Gun> blackMarketGuns = new List<Gun>();
	public List<Gun> diplomaticMarketGuns = new List<Gun>();

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
		//Split by lines and trim empty lines.
		string[] lines = gunList.text.Trim (System.Environment.NewLine.ToCharArray ()).Split (System.Environment.NewLine.ToCharArray ());

		//Initalize gun array.
		allGuns = new Gun[lines.Length - 1];

		//In case modders move things around or we do this will get the indexs where the columns are in the .csv.
		string[] indexFinder = lines[0].Split (',');

		int name = -1;
		int damage = -1;
		int accuracy = -1;
		int range = -1;
		int clipSize = -1;
		int fireRate = -1;
		int reloadTime = -1;
		int weight = -1;
		int size = -1;
		int price = -1;
		int fearFactor = -1;
		int noise = -1;
		int concealable = -1;
		int moraleBoost = -1;
		int rarity = -1;
		int region = -1;
		int storage = -1;
		int upgradeNumber = -1;
		int gunMarket = -1;
		int diplomaticMarket = -1;
		int blackMarket = -1;

		//Scan through columns to find the respective indexs.
		for (int i = 0; i < indexFinder.Length; i++)
		{
			switch (indexFinder[i])
			{
			case "Name":
				name = i;
				break;
			case "Damage":
				damage = i;
				break;
			case "Accuracy":
				accuracy = i;
				break;
			case "Range (m)":
				range = i;
				break;
			case "Clip Size":
				clipSize = i;
				break;
			case "Fire Rate (RPM)":
				fireRate = i;
				break;
			case "Reload Time":
				reloadTime = i;
				break;
			case "Weight (g)":
				weight = i;
				break;
			case "Size":
				size = i;
				break;
			case "Price":
				price = i;
				break;
			case "Fear Factor":
				fearFactor = i;
				break;
			case "Noise":
				noise = i;
				break;
			case "Concealable":
				concealable = i;
				break;
			case "Morale Boost":
				moraleBoost = i;
				break;
			case "Rarity":
				rarity = i;
				break;
			case "Region of Origin":
				region = i;
				break;
			case "Storage Units":
				storage = i;
				break;
			case "Upgrade Number":
				upgradeNumber = i;
				break;
			case "Gun Market":
				gunMarket = i;
				break;
			case "Diplomatic Market":
				diplomaticMarket = i;
				break;
			case "Black Market":
				blackMarket = i;
				break;
			}
		}

		//Pull out the data using the parsed indexes.
		for (int i = 1; i < lines.Length; i++)
		{
			string[] ls = lines[i].Split (',');
			
			if (ls.Length < 20)
				continue;
			
			allGuns[i - 1] = new Gun(ls[name], int.Parse (ls[damage]), int.Parse(ls[accuracy]), int.Parse (ls[range]), int.Parse (ls[clipSize]), float.Parse(ls[fireRate]), float.Parse(ls[reloadTime]), float.Parse(ls[weight]), ls[size], float.Parse(ls[price]), int.Parse (ls[fearFactor]), int.Parse (ls[noise]), 
			                     BoolParse(ls[concealable]), int.Parse (ls[moraleBoost]), float.Parse(ls[rarity]), ls[region], 0, int.Parse (ls[storage]), null);

			//Market Checks
			if (BoolParse (ls[gunMarket]))
			    gunMarketGuns.Add (allGuns[i - 1]);

			if (BoolParse (ls[diplomaticMarket]))
				diplomaticMarketGuns.Add (allGuns[i - 1]);

			if (BoolParse (ls[blackMarket]))
				blackMarketGuns.Add (allGuns[i - 1]);
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

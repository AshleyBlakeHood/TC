﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlackMarket : MonoBehaviour
{
	GUIManager guiManager;
	GunManager gunManager;

	private Gun[] guns = new Gun[4];
	
	PlayerData player;
	
	// Use this for initialization
	void Start ()
	{
		guns [0] = new Gun ("Rocket Laucnher", 12, 56, 175, 50, 2.23f, 4, 12, "Large", 250, 1, 46, false, 0, 60, "USA", 1, 2, null);
		guns [1] = new Gun ("Fat Man", 12, 56, 175, 50, 2.23f, 4, 12, "Large", 250, 1, 46, false, 0, 60, "USA", 1, 2, null);
		guns [2] = new Gun ("Poison Dart Pistol", 12, 56, 175, 50, 2.23f, 4, 12, "Small", 250, 1, 46, true, 0, 60, "USA", 1, 2, null);
		guns [3] = new Gun ("C4", 12, 56, 175, 50, 2.23f, 4, 12, "Small", 250, 1, 46, true, 0, 60, "USA", 1, 2, null);

		guiManager = GameObject.FindGameObjectWithTag ("GUI Manager").GetComponent<GUIManager>();
		gunManager = GameObject.FindGameObjectWithTag ("Gun Manager").GetComponent<GunManager>();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerData> ();
		
		SelectGunsForSale ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void SelectGunsForSale()
	{
		//!!!!!!
		//Needs reprogramming by changing ItemWeightHolder to a template class.
		for (int gunsIndex = 0; gunsIndex < guns.Length; gunsIndex++)
		{
			ItemWeightHolder[] gunWeights = new ItemWeightHolder[gunManager.blackMarketGuns.Count];
			
			for (int i = 0; i < gunWeights.Length; i++)
			{
				gunWeights[i] = new ItemWeightHolder(gunManager.blackMarketGuns[i].name, gunManager.blackMarketGuns[i].rarity);
			}
			
			string selectedGun = SelectionEngine.GetItem (gunWeights);
			
			for (int i = 0; i < gunManager.blackMarketGuns.Count; i++)
			{
				if (gunManager.blackMarketGuns[i].name == selectedGun)
					guns[gunsIndex] = gunManager.blackMarketGuns[i];
			}
		}
	}

	void OnMouseDown()
	{
		guiManager.blackMarketGUI.SetActive (true);

		UpdateGUI ();
	}

	public void UpdateGUI()
	{
		RectTransform[] childTransforms = guiManager.blackMarketGUI.GetComponentsInChildren<RectTransform> ();
		
		foreach (RectTransform child in childTransforms)
		{
			Gun gun1 = guns[0];
			Gun gun2 = guns[1];
			Gun gun3 = guns[2];
			Gun gun4 = guns[3];
			
			//Name
			if (child.name == "Text 1")
				child.GetComponent<Text>().text = guns[0].name;
			
			if (child.name == "Text 2")
				child.GetComponent<Text>().text = guns[1].name;
			
			if (child.name == "Text 3")
				child.GetComponent<Text>().text = guns[2].name;
			
			if (child.name == "Text 4")
				child.GetComponent<Text>().text = guns[3].name;
			
			//Price
			if (child.name == "Text 1 - Price")
				child.GetComponent<Text>().text = "£" + guns[0].price;
			
			if (child.name == "Text 2 - Price")
				child.GetComponent<Text>().text = "£" + guns[1].price;
			
			if (child.name == "Text 3 - Price")
				child.GetComponent<Text>().text = "£" + guns[2].price;
			
			if (child.name == "Text 4 - Price")
				child.GetComponent<Text>().text = "£" + guns[3].price;
			
			//Button Event
			if (child.name == "Button 1")
				child.GetComponent<Button>().onClick.AddListener(delegate{AddEquipmentToPlayer(gun1);});
			
			if (child.name == "Button 2")
				child.GetComponent<Button>().onClick.AddListener(delegate{AddEquipmentToPlayer(gun2);});
			
			if (child.name == "Button 3")
				child.GetComponent<Button>().onClick.AddListener(delegate{AddEquipmentToPlayer(gun3);});
			
			if (child.name == "Button 4")
				child.GetComponent<Button>().onClick.AddListener(delegate{AddEquipmentToPlayer(gun4);});
		}
	}

	void AddEquipmentToPlayer(Gun gun)
	{
		player.AddEquipment (gun);
	}
}

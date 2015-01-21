using UnityEngine;
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
			ItemWeightHolder[] gunWeights = new ItemWeightHolder[gunManager.allGuns.Length];
			
			for (int i = 0; i < gunWeights.Length; i++)
			{
                gunWeights[i] = new ItemWeightHolder(gunManager.allGuns[i].name, gunManager.allGuns[i].rarity);
			}
			
			string selectedGun = SelectionEngine.GetItem (gunWeights);

            for (int i = 0; i < gunManager.allGuns.Length; i++)
			{
                if (gunManager.allGuns[i].name == selectedGun)
				{
					Gun gun = gunManager.allGuns[i];
					guns[gunsIndex] = new Gun(gun.name, gun.damage, gun.accuracy, gun.range, gun.clipSize, gun.fireRate, gun.reloadTime, gun.weight, gun.size, gun.price, gun.fearFactor,
					                          gun.noise, gun.concealable, gun.moraleBoost, gun.rarity, gun.regionOfOrigin, gun.age, gun.storageUnits);

					guns[gunsIndex].accuracy += System.Convert.ToInt32 (guns[gunsIndex].accuracy * Random.Range (-(guns[gunsIndex].accuracy * 0.1f), guns[gunsIndex].accuracy * 0.1f));
					guns[gunsIndex].clipSize += System.Convert.ToInt32 (guns[gunsIndex].clipSize * Random.Range (-(guns[gunsIndex].clipSize * 0.1f), guns[gunsIndex].clipSize * 0.1f));
					guns[gunsIndex].damage += System.Convert.ToInt32 (guns[gunsIndex].damage * Random.Range (-(guns[gunsIndex].damage * 0.1f), guns[gunsIndex].damage * 0.1f));
					guns[gunsIndex].fireRate += guns[gunsIndex].fireRate * Random.Range (-(guns[gunsIndex].fireRate * 0.1f), guns[gunsIndex].fireRate * 0.1f);
					guns[gunsIndex].range += System.Convert.ToInt32 (guns[gunsIndex].range * Random.Range (-(guns[gunsIndex].range * 0.1f), guns[gunsIndex].range * 0.1f));
					guns[gunsIndex].reloadTime += guns[gunsIndex].reloadTime * Random.Range (-(guns[gunsIndex].reloadTime * 0.1f), guns[gunsIndex].reloadTime * 0.1f);
					guns[gunsIndex].weight += guns[gunsIndex].weight * Random.Range (-(guns[gunsIndex].weight * 0.1f), guns[gunsIndex].weight * 0.1f);

					guns[gunsIndex].name += "+";

					break;
				}
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

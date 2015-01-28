using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GunMarket : MonoBehaviour
{
	GUIManager guiManager;
	GunManager gunManager;

	public Gun[] guns = new Gun[3];

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
                    guns[gunsIndex] = gunManager.allGuns[i];
			}
		}
	}

	void OnMouseDown()
	{
		guiManager.gunMarketGUI.SetActive (true);
		ScrollableList sl = guiManager.gunMarketGUI.GetComponentInChildren<ScrollableList> ();

		sl.ClearList ();
		sl.CreateList (guns.Length, 1);

		for (int i = 0; i < guns.Length; i++)
		{
			Gun gun = guns[i];
			sl.elements[i].transform.FindChild("Text").GetComponent<Text>().text = guns[i].name;
			sl.elements[i].transform.FindChild("Text - Price").GetComponent<Text>().text = "£" + guns[i].price;
			sl.elements[i].transform.FindChild("Button").GetComponent<Button>().onClick.AddListener(delegate{AddEquipmentToPlayer(gun);});
		}
	}

	void AddEquipmentToPlayer(Gun gun)
	{
		player.AddEquipment (gun);
	}
}

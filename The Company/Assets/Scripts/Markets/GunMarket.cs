using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GunMarket : MonoBehaviour
{
	GUIManager guiManager;
	public Gun[] guns = new Gun[3];

	PlayerData player;

	// Use this for initialization
	void Start ()
	{
		guns [0] = new Gun ("P90", 12, 56, 175, 50, 2.23f, 4, 12, "Medium", 250, 1, 46, false, 0, 60, "USA", 1, 2, null);
		guns [1] = new Gun ("AK-47", 12, 56, 175, 50, 2.23f, 4, 12, "Medium", 250, 1, 46, false, 0, 60, "USA", 1, 2, null);
		guns [2] = new Gun ("9MM Pisotl", 12, 56, 175, 50, 2.23f, 4, 12, "Small", 250, 1, 46, true, 0, 60, "USA", 1, 2, null);

		guiManager = GameObject.FindGameObjectWithTag ("GUI Manager").GetComponent<GUIManager>();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerData> ();
	}
	
	// Update is called once per frame
	void Update () {
	
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
			sl.elements[i].transform.FindChild("Button").GetComponent<Button>().onClick.AddListener(delegate{AddEquipmentToPlayer(gun);});
		}
	}

	void AddEquipmentToPlayer(Gun gun)
	{
		player.AddEquipment (gun);
	}
}

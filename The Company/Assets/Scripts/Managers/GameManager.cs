using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public GameObject notifyObject;

	public GameObject dynamicObjectHolder;

	List<RepData> repListComplete = new List<RepData>();
	public List<PlayerData> allPlayers = new List<PlayerData>();
	RepManager rm;
    List<string> regionNames = new List<string>();
    string saveName, mapName;

	public static string dataFolder
	{
		get
		{
			return Application.dataPath;
		}
	}

	// Use this for initialization
	void Start ()
	{
		if (dynamicObjectHolder == null)
		{
			if (GameObject.Find ("Dynamic Object Holder") != null)
				dynamicObjectHolder = GameObject.Find ("Dynamic Object Holder");
			else
				dynamicObjectHolder = new GameObject("Dynamic Object Holder");
		}

        saveName = "TEST";
        mapName = "EarthContinents";
		populatePlayerList();
		try
		{
			rm = GameObject.Find("RepManager").GetComponent<RepManager>();
            rm.SetSaveNames(saveName, mapName);
            rm.InitRepSystem();
            regionNames = rm.GetRegions();
            rm.GetRegionWeightingsByDifficulty(0.5);
			//mm.InitMissionCreator();

		}
		catch
		{
			Debug.Log("No Rep Manager");
		}

		if (PlayerPrefs.GetInt ("Colours Set") == 1)
			Camera.main.backgroundColor = new Color (PlayerPrefs.GetFloat ("Background ColourR"), PlayerPrefs.GetFloat ("Background ColourG"), PlayerPrefs.GetFloat ("Background ColourB"));
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void NotifyPlayerOfSpawn(Vector3 position, Color color)
	{
		GameObject notify = Instantiate (notifyObject, position, Quaternion.identity) as GameObject;
		notify.GetComponent<SpriteRenderer> ().color = color;
		notify.name = "Notify Object";
	}

	public void populatePlayerList()
	{
		//This will be where we create the player objects
		GameObject[] tempPlayers = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject p in tempPlayers)
		{
			allPlayers.Add(p.GetComponent<PlayerData>());
		}
		
	}

    public List<string> GetRegionNames()
    {
        return regionNames;
    }

	public static void LoadMainScene()
	{
		Application.LoadLevel ("Main Scene");
	}
}

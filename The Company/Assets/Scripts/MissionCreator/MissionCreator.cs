using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionCreator : MonoBehaviour {

	public GameObject missionPrefab;
	TextAsset missionInfo;
	public TextAsset missionWords;
	private MissionModel[] data;
	private string[] locationdata;

	// Use this for initialization
	void Start () {
        missionInfo = Resources.Load("Mission Info") as TextAsset;
		/*GetMissionInfo ();
		GetMissionName ();
		GameObject mission = new GameObject ("Mission");
		Mission mObject = mission.AddComponent<Mission> ();
		mObject.data = CreateNewMission();*/
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			CreateNewMission();
		}
	}
	public void InitMissionCreator(string[] locations)
	{
		locationdata = locations;
		GetMissionInfo ();
		GetMissionName ();
		GameObject mission = new GameObject ("Mission");
		Mission mObject = mission.AddComponent<Mission> ();
		mObject.data = CreateNewMission();
	}
	private void GetMissionInfo()
	{
		string[] lines = missionInfo.text.Trim ('\n').Split ('\n');
		data = new MissionModel[lines.Length];

		for (int i = 0; i < lines.Length; i++)
		{
			string[] lineSplit = lines[i].Split (',');
			
			if (lineSplit.Length < 7)
				continue;
			
			data[i] = new MissionModel(lineSplit[0], lineSplit[1], lineSplit[2], lineSplit[3], lineSplit[4], lineSplit[5], lineSplit[6]);
		}

	}
	private void GetLocationData()
	{

	}
	private void GetMissionName()
	{
		string[] lines = missionWords.text.Trim ('\n').Split ('\n');

		for (int i = 0; i<lines.Length; i++) 
		{
			string [] lineSplit = lines[i].Split(',');

			if(lineSplit.Length < 3)
				continue;
		}
	}
	public MissionData CreateNewMission()
	{
		Random.seed = Random.seed;
		int seed = Random.seed;

		Debug.Log (seed);

		int row = Random.Range (0, data.Length);
		string type = data [row].type;
		int timeGiven = int.Parse (data [row].timeGiven);
		int objectives = int.Parse (data [row].objectives);
		int minEquipment = int.Parse (data [row].minEquipment);
		int maxEquipment = int.Parse (data [row].maxEquipment);
		int minAgents = int.Parse (data [row].minAgents);
		int maxAgents = int.Parse (data [row].maxAgents);
		string continent = "Europe";
		string location = "Brussels";

		int agents = Random.Range (minAgents, maxAgents + 1);
		int equipment = Random.Range (minEquipment, maxEquipment + 1);
		Debug.Log ("Mission type:  " + type);
		Debug.Log ("Time Given:  " + timeGiven + " hours");
		Debug.Log ("Number of Objectives:  " + objectives);
		Debug.Log ("Amount of Equipment allowed:  " + equipment);
		Debug.Log ("Amount of Agents allowed:  " + agents);
		Debug.Log ("Continent:  " + continent);
		Debug.Log ("Location:  " + location);


		return new MissionData (type, timeGiven, objectives, equipment, agents, continent, location);
	}
}
public class MissionModel
{
	public string type = "Bomb Defusal";
	public string timeGiven = "24";
	public string objectives = "4";
	public string minEquipment = "2";
	public string maxEquipment = "5";
	public string minAgents = "1";
	public string maxAgents = "6";
	
	public MissionModel(string typ, string time, string obj, string minE, string maxE, string minA, string maxA)
	{
		type = typ;
		timeGiven = time;
		objectives = obj;
		minEquipment = minE;
		maxEquipment = maxE;
		minAgents = minA;
		maxAgents = maxA;
	}
}

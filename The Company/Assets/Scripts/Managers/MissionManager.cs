using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour {

	public GameObject missionPrefab;
	private TextAsset missionInfo;
	private TextAsset missionWords;
	private TextAsset locationData;
	private TextAsset personTargetNames;
	private TextAsset placeTargetNames;
	private MissionModel[] data;
	private Target[] pTargets;
	private Target[] lTargets;
	private string[] locations;
	private MissionName[] missionNames;
	//private List<string> locationdata;
	//public static List<GameObject> activeMissions;

	private int missionID = 0;
	// Use this for initialization
	void Start () {
        missionInfo = Resources.Load("Mission Info") as TextAsset;
		missionWords = Resources.Load ("Mission Words") as TextAsset;
		locationData = Resources.Load ("EarthContinents") as TextAsset;
		personTargetNames = Resources.Load ("Person Targets") as TextAsset;
		placeTargetNames = Resources.Load ("Place Targets") as TextAsset;
		InitMissionCreator ();
		//GameObject mission = new GameObject ("Mission");
		//Mission mObject = mission.AddComponent<Mission> ();
		//mObject.data = CreateNewMission();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			CreateNewMission();
		}
	}
	public void InitMissionCreator()
	{
		//Link to the GameManager
		//locationdata = locations;
		GetMissionInfo ();
		GetMissionName ();
		GetLocationData ();
		GetPersonTargetName ();
		GetPlaceTargetName ();

	}
	private void GetMissionInfo()
	{
		string[] lines = missionInfo.text.Trim ('\n').Split ('\n');
		data = new MissionModel[lines.Length];

		for (int i = 0; i < lines.Length; i++)
		{
			string[] lineSplit = lines[i].Split (',');
			
			if (lineSplit.Length < 8)
				continue;
			
			data[i] = new MissionModel(lineSplit[0], lineSplit[7], lineSplit[2], lineSplit[3], lineSplit[4], lineSplit[5], lineSplit[6], lineSplit[1]);
		}

	}
	private void GetLocationData()
	{
		string[] lines = locationData.text.Trim ('\n').Split ('\n');
		
		locations = new string[lines.Length];
		
		for (int i = 0; i < lines.Length; i++)
		{
			string[] lineSplit = lines[i].Split (',');
			
			if (lineSplit.Length < 1)
				continue;
			
			locations[i] = lineSplit[0];
		}
	}
	private void GetMissionName()
	{
		string[] lines = missionWords.text.Trim ('\n').Split ('\n');
		missionNames = new MissionName[lines.Length]; 
		for (int i = 0; i<lines.Length; i++) 
		{
			string[] lineSplit = lines[i].Split (',');

			missionNames[i] = new MissionName(lineSplit);
		}
	}
	private void GetPersonTargetName()
	{
		string[] lines = personTargetNames.text.Trim ('\n').Split ('\n');
		pTargets = new Target[lines.Length];
		for (int i = 0; i < lines.Length; i++)
		{
			string[] lineSplit = lines[i].Split (',');
			
			if (lineSplit.Length < 2)
				continue;
			
			pTargets[i] = new Target(lineSplit[0],lineSplit[1]);
		}
	}
	private void GetPlaceTargetName()
	{
		string[] lines = placeTargetNames.text.Trim ('\n').Split ('\n');
		lTargets = new Target[lines.Length];
		for (int i = 0; i < lines.Length; i++)
		{
			string[] lineSplit = lines[i].Split (',');
			
			if (lineSplit.Length < 2)
				continue;
			
			lTargets[i] = new Target(lineSplit[0],lineSplit[1]);
		}
	}
	private string MissionTitleCreator(int row)
	{
		string title = data [row].missionTitle;
		for(int i = 0; i<missionNames.Length;i++)
		{
			if (missionNames [i].missionName [0] == title) 
			{
				int maxName = missionNames[i].missionName.Length;
				maxName = Random.Range(1,maxName);
				title = missionNames[i].missionName[maxName];
			}
		}
		return title;
	}
	public void CreateNewMission()
	{
		Random.seed = Random.seed;
		int seed = Random.seed;

		Debug.Log (seed);
		int row = Random.Range (0, data.Length);
		string type = data [row].type;
		string title = MissionTitleCreator (row);
		int timeGiven = int.Parse (data [row].timeGiven);
		int objectives = int.Parse (data [row].objectives);
		int minEquipment = int.Parse (data [row].minEquipment);
		int maxEquipment = int.Parse (data [row].maxEquipment);
		int minAgents = int.Parse (data [row].minAgents);
		int maxAgents = int.Parse (data [row].maxAgents);
		string continent = locations[Random.Range (0,locations.Length)];
		string forename = pTargets [Random.Range (0, pTargets.Length)].forename;
		string surname = pTargets [Random.Range (0, pTargets.Length)].surname;
		string fTarget = lTargets [Random.Range (0, lTargets.Length)].forename;
		string sTarget = lTargets [Random.Range (0, lTargets.Length)].surname;

		if (row == 1 || row == 4 || row == 8) 
		{
			title = title + " " + forename + " " + surname + " in " + continent;
		}
		if (row == 6 || row == 7 || row == 9 || row == 10)
		{
			title = title + " " + fTarget +  " " + sTarget + " in " + continent;
		}
		if (row == 0 || row == 3) 
		{
			title = title + " " + forename + "'s " + "hideout in " + continent;
		}
		if (row == 2)
		{
			title = title + " the robbery of " + fTarget + " " + sTarget + " bank in " + continent;
		}
		if(row == 5)
		{
			title = title + " bomb in " + continent;
		}
		int agents = Random.Range (minAgents, maxAgents + 1);
		int equipment = Random.Range (minEquipment, maxEquipment + 1);

		MissionData temp = new MissionData (type, timeGiven, objectives, equipment, agents, continent, missionID, title);
		GameObject mission = new GameObject ("Mission");
		Mission mObject = mission.AddComponent<Mission> ();
		mObject.data = temp;
		missionID++;
		//activeMissions.Add (mission);
	}
	public void RemoveActiveMission(int ID)
	{

		//activeMissions.RemoveAt (0);
		Debug.Log ("Get wrekt");
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
	public string missionTitle = "[KILL]";
	
	public MissionModel(string typ, string time, string obj, string minE, string maxE, string minA, string maxA, string title)
	{
		type = typ;
		timeGiven = time;
		objectives = obj;
		minEquipment = minE;
		maxEquipment = maxE;
		minAgents = minA;
		maxAgents = maxA;
		missionTitle = title;
	}
}
public class MissionName
{
	public string[] missionName;
	public MissionName (string[] name)
	{
		missionName = name;
	}
}
public class Target
{
	public string forename;
	public string surname;
	public Target(string fName, string sName)
	{
		forename = fName;
		surname = sName;
	}
}

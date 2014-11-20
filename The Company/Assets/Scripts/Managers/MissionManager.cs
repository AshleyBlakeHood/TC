using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour {

	public GameObject missionPrefab;
	private TextAsset missionInfo;
	private TextAsset missionWords;
	private TextAsset locationData;
	private TextAsset personTargetNames;
	private MissionModel[] data;
	private PersonTarget[] pTargets;
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
		//Manipulate locationdata[]
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

			if(lineSplit[i] == null)
			{
				lineSplit[i] = "j";
			}
			missionNames[i] = new MissionName(lineSplit);
		}
	}
	private void GetPersonTargetName()
	{
		string[] lines = personTargetNames.text.Trim ('\n').Split ('\n');
		pTargets = new PersonTarget[lines.Length];
		for (int i = 0; i < lines.Length; i++)
		{
			string[] lineSplit = lines[i].Split (',');
			
			if (lineSplit.Length < 2)
				continue;
			
			pTargets[i] = new PersonTarget(lineSplit[0],lineSplit[1]);
		}
	}
	private string MissionTitleCreator(int row)
	{
		string title = data [8].missionTitle;
		for(int i = 0; i<missionNames.Length;i++)
		{
			if (missionNames [i].missionName [0] == title) 
			{
				int maxName = missionNames[i].missionName.Length;
				Debug.Log ("YAY");
				bool emptyName = true;
				maxName = Random.Range(1,maxName);
				title = missionNames[i].missionName[maxName];
				/*while(emptyName = true)
				{

					Debug.Log (title);
					if(title != null)
					{
						emptyName = false;
					}
				}*/
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
		string location = "Brussels";
		string forename = pTargets [Random.Range (0, pTargets.Length)].forename;
		string surname = pTargets [Random.Range (0, pTargets.Length)].surname;
		if (row == 1 || row == 4 || row == 8) 
		{
			title = title + " " + forename + " " + surname + " in " + location;
		}
		if (row == 3 || row == 4 || row == 6 || row == 7 || row == 9 || row == 10)
		{
			//title = title + lTargets;
		}
		int agents = Random.Range (minAgents, maxAgents + 1);
		int equipment = Random.Range (minEquipment, maxEquipment + 1);

		MissionData temp = new MissionData (type, timeGiven, objectives, equipment, agents, continent, location, missionID, title);
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
public class PersonTarget
{
	public string forename;
	public string surname;
	public PersonTarget(string fName, string sName)
	{
		forename = fName;
		surname = sName;
	}
}

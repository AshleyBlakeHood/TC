using UnityEngine;
using System.Collections;

public class Mission : MonoBehaviour {
	
	public MissionData data;
	private int time;
	//MissionManager mm;
	//MissionManager mm = new MissionManager();
	// Use this for initialization
	void Start () {
		string type = "";
		type = data.type;
		time = data.timeGiven;
		time *= 100;
	}
	
	// Update is called once per frame
	void Update () {
		time--;
		data.timeGiven = time;
		if (time == 0) 
		{
			MissionRemover();
		}
		//timeout code in here for each mission
		//Link back to mission manager when it times out, mission manager will have  a function that will remove it from the active missions list and destroy it
	}
	public void MissionRemover()
	{
		//mm.RemoveActiveMission (data.missionID);
		//mm.activeMissions.RemoveAt(data.missionID);
		Debug.Log ("Got here");
		//MissionManager.activeMissions.RemoveAt (data.missionID);
		/*GameObject mm = GameObject.Find ("MissionManager");
		MissionManager missionM = mm.GetComponent<MissionManager> ();
		missionM.activeMissions.RemoveAt (data.missionID);*/
		Debug.Log ("Mission Deleted");
		Destroy(gameObject);
	}
}

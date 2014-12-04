﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Mission : MonoBehaviour {
	
	public MissionData data;
	private int time;
	//MissionManager mm;
	MissionManager mm = new MissionManager();
	// Use this for initialization
	void Start () {
		string type = "";
		type = data.type;
		time = data.timeGiven;
		time *= 300;
	}
	
	// Update is called once per frame
	void Update () {
		//time--;
		data.timeGiven = time;
		if (time == 0) 
		{
			MissionRemover();
		}
	}
	public void MissionRemover()
	{
		mm = GameObject.Find ("MissionManager").GetComponent<MissionManager>();
        mm.RemoveActiveMission(gameObject);
		Debug.Log ("Mission Deleted");
		Destroy(gameObject);
	}
}

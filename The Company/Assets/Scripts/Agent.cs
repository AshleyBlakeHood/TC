using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : MonoBehaviour
{
	public AgentData data;

	//Agent Usable?
	bool inTraining = false;
	bool inMission = false;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void SetAgentMissionStatus(bool iInMission)
	{
		inMission = iInMission;
	}

	public void SetAgentTrainingStatus(bool iInTraining)
	{
		inTraining = iInTraining;
	}

	public bool IsAgentUsable()
	{
		if (inMission || inTraining)
			return false;
		else
			return true;
	}
}

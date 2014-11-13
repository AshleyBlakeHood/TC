using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour
{
	public AgentData data;

	//Movement Stuff
	Vector3 startPos = Vector3.zero;
	float startTime = 0;
	float journeyLength = 0;

	Vector3 destination = Vector3.zero;

	//Agent Usable?
	bool inTraining = false;
	bool inMission = false;

	// Use this for initialization
	void Start ()
	{
		destination = new Vector3 (Random.Range (-8f, 8f), Random.Range (-6f, 6f), 0);

		StartMove ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//DEBUG TESTING STUFF
		float distanceCovered = (Time.time - startTime) * 0.1f;
		float fracJourney = distanceCovered / journeyLength;

		transform.position = Vector3.Lerp (startPos, destination, fracJourney);

		if (Vector3.Distance (transform.position, destination) < 0.1f)
		{
			destination = new Vector3 (Random.Range (-8f, 8f), Random.Range (-6f, 6f), 0);

			StartMove ();
		}

		//Really bad programming, just for testing - DO NOT LEAVE IN!
		GetComponent<LineRenderer>().SetColors (new Color (1, 0.65F, 0), new Color (1, 0.65F, 0));
		GetComponent<LineRenderer>().SetPosition (0, transform.position);
		GetComponent<LineRenderer>().SetPosition (1, destination);
	}

	private void StartMove()
	{
		startPos = transform.position;

		startTime = Time.time;
		journeyLength = Vector3.Distance (startPos, destination);
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

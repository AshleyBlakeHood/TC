using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour
{
	public AgentCreator ac;

	public List<Vector2> officeLocations;
	public List<Vector2> safehouseLocations;

	public List<Agent> agents;

	public List<Gun> equipment = new List<Gun>();
	public string playerName = "";

	public List<RepData> repList = new List<RepData>();

	// Use this for initialization
	void Start ()
	{
		AddAgent (ac.CreateNewAgent ());
		AddAgent (ac.CreateNewAgent ());
		AddAgent (ac.CreateNewAgent ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.A))
			AddAgent (ac.CreateNewAgent ());
	}

	public void AddAgent(AgentData a)
	{
		GameObject agent = Instantiate (ac.agentPrefab, new Vector3(-0.08f, 3.06f, 0), Quaternion.identity) as GameObject;
		agent.GetComponent<Agent> ().data = a;

		agents.Add (agent.GetComponent<Agent>());
	}

	public void AddEquipment(Gun g)
	{
		equipment.Add (g);
	}
}

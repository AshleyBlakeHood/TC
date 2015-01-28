using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour
{
	public AgentCreator ac;

	public List<Vector2> officeLocations;
	public List<Vector2> safehouseLocations;

	public List<Gun> equipment = new List<Gun>();
	public string playerName = "";

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void AddEquipment(Gun g)
	{
		equipment.Add (g);
	}
}

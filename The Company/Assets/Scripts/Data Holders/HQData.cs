using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class HQData : MonoBehaviour 
{
	public int id = 0;
	public List<int> agentList = new List<int>();
	public List<int> equipmentList = new List<int>();
	public List<int> vehiclesList = new List<int>();

	public HQData ()
	{
		id = 0;
	}
	public HQData (int fId, int iAgent, int iEquipment, int iVehicles)
	{
		id = fId;
		agentList.Add (iAgent);
		equipmentList.Add (iEquipment);
		vehiclesList.Add (iVehicles);
	}


	public void addAgent(int agentID)
	{
		agentList.Add (agentID);
	}
	public void addItem(int itemID)
	{
		equipmentList.Add (itemID);
	}
	public void addVehicle(int vehicleID)
	{
		vehiclesList.Add (vehicleID);
	}


	public bool removeAgent(int agentID)
	{
		bool found = false;
		int answer = searchList (agentID, agentList);
		if (answer != -1) {
			found = true;
		}
		return found;
	}
	public bool removeItem(int itemID)
	{
		bool found = false;
		int answer = searchList (itemID, equipmentList);
		if (answer != -1) {
			found = true;
		}
		return found;
	}
	public bool removeVehicle(int vehicleID)
	{
		bool found = false;
		int answer = searchList (vehicleID, vehiclesList);
		if (answer != -1) {
			found = true;
		}
		return found;
	}
	public int searchList(int id, List<int> idList)
	{
		int temp = -1;
		foreach(int cID in idList)
		{
			if(cID == id)
			{
				temp = cID;
			}
		}
		return temp;
	}
}


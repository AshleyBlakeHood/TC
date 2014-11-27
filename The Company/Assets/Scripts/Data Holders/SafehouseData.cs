using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SafehouseData : MonoBehaviour 
{
	public int id = 0;
	public List<int> agentList = new List<int>();
	public List<int> equipmentList = new List<int>();
	public List<int> vehiclesList = new List<int>();

	public SafehouseData ()
	{
		id = 0;
	}
	public SafehouseData (int fId, int iAgent, int iEquipment, int iVehicles)
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


	public void removeAgent(int agentID)
	{
		agentList = searchAndDestroy(agentID, agentList);
	}
	public void removeItem(int itemID)
	{
		equipmentList = searchAndDestroy(itemID, equipmentList);
	}
	public void removeVehicle(int vehicleID)
	{
		vehiclesList = searchAndDestroy(vehicleID, vehiclesList);
	}
	public List<int> searchAndDestroy(int id, List<int> idList)
	{
		bool found = false;
		int place = -1;
		foreach(int cID in idList)
		{
			if(idList[cID] == id)
			{
				found = true;
				place = cID;
			}
		}
		if(found = true)
		{
			idList.RemoveAt(place);
		}
		return idList;
	}
}


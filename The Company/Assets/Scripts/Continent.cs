using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// The Continent class holds the headquarters located on the continent
/// </summary>
public class Continent : MonoBehaviour
{
	//Attributes
	public List<int> hqList = new List<int>();
	//The areaID gives the continent an id. This is used in retrieving the correct continent
	public string areaID;

	/// <summary>
	/// Adds the ID of the headquarters to the continent class's list of headquarters. This is useful in tracking where the headquarters are located
	/// </summary>
	/// <param name="hq">Hq.</param>
	public void addAreaHQ(int hq)
	{
		hqList.Add (hq);
	}
}

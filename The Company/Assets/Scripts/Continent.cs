using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Continent : MonoBehaviour
{
	public List<int> hqList = new List<int>();

	void Start () {

	}

	public void addAreaHQ(int hq)
	{
		hqList.Add (hq);
	}
}

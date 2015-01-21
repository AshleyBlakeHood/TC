using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Continent : MonoBehaviour
{
	public List<int> hqList = new List<int>();

	public Sprite fineSprite;
	public string areaID;

	void Start () {
		fineSprite = GetComponent<SpriteRenderer>().sprite;
	}

	void Update()
	{

	}

	public void addAreaHQ(int hq)
	{
		hqList.Add (hq);
	}

	public void loadDeadzone()
	{

	}

}

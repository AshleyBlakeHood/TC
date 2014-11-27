using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Continent : MonoBehaviour
{
	public List<int> hqList = new List<int>();
	public bool deadZone = false;
	public Sprite deadSprite;
	public Sprite fineSprite;
	public string areaID;

	void Start () {
		fineSprite = GetComponent<SpriteRenderer>().sprite;
	}

	void Update()
	{
		int test = Random.Range (0, 100);
		if (test == 69) {
			switchZoneMode();
		}
	}

	public void addAreaHQ(int hq)
	{
		hqList.Add (hq);
	}

	public void loadDeadzone()
	{

	}
	public void switchZoneMode()
	{
		deadZone = !deadZone;
		if (deadZone == true) {
			GetComponent<SpriteRenderer> ().sprite = deadSprite;
		} 
		else {
			GetComponent<SpriteRenderer> ().sprite = fineSprite;
		}
	}

}

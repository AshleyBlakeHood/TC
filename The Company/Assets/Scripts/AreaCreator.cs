using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaCreator : MonoBehaviour {
	public TextAsset areaFile;
	public Continent contIns;
	public GameObject tempArea;
	public GameObject areaPlacement;
	public Sprite tempSprite;
	private Vector3 areaPos = Vector3.zero;

	public GameObject[] areas; 
	
	// Use this for initialization
	void Start () {
		areas = Resources.LoadAll<GameObject>("AreaPrefabs");
		ReadInAreas ();
		foreach (var x in areas) {
			Debug.Log ("Quack: " + x.gameObject.name);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ReadInAreas()
	{
		string[] dataFromFile = areaFile.text.Trim ('\n').Split ('\n');

		for (int i = 0; i < dataFromFile.Length; i++)
		{
			string[] dataLineSplit = dataFromFile[i].Split (',');
			if (dataLineSplit.Length < 4)
			{
				continue;
			}
			instantiateAreas(dataLineSplit);
		}
	}

	public void instantiateAreas(string[] data){
		Debug.Log (data[0]);

		foreach (var curArea in areas) {
			if(data[0] == curArea.gameObject.name)
			{
				tempArea = curArea.gameObject;
			}
		}
		
		float xLoc = System.Convert.ToSingle (data [1]);
		float yLoc = System.Convert.ToSingle (data [2]);
		float zLoc = System.Convert.ToSingle (data [3]);
		areaPos = new Vector3(xLoc, yLoc, zLoc);
		areaPlacement = Instantiate (tempArea, areaPos, Quaternion.identity) as GameObject;
		areaPlacement.GetComponent<Continent> ().areaID = data[0];

	}
}

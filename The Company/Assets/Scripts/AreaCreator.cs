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

	public Sprite[] areaResources;
	public GameObject[] areas;

	// Use this for initialization
	void Start () {
		areaResources = Resources.LoadAll<Sprite>("Sprites");
		areas = new GameObject[areaResources.Length];

		ReadInAreas ();
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
			instantiateAreas(dataLineSplit, i);
		}
	}

	public void instantiateAreas(string[] data, int index)
	{
		Sprite selectedSprite = new Sprite();

		foreach (Sprite curArea in areaResources)
		{
			if(data[0] == curArea.name)
			{
				selectedSprite = curArea;
			}
		}
		
		float xLoc = System.Convert.ToSingle (data [1]);
		float yLoc = System.Convert.ToSingle (data [2]);
		float zLoc = System.Convert.ToSingle (data [3]);
		areaPos = new Vector3(xLoc, yLoc, zLoc);

		areaPlacement = new GameObject (data [0]);
		areaPlacement.transform.position = areaPos;

		SpriteRenderer sr = areaPlacement.AddComponent<SpriteRenderer> ();
		sr.sprite = selectedSprite;
		sr.color = new Color (0.35294117647f, 0.50196078431f, 0.73725490196f);
		Debug.Log ("Specific Setting of Continent Colour Happens Here!");

		areaPlacement.AddComponent<Continent> ();
		areaPlacement.AddComponent<PolygonCollider2D> ();

		areas [index] = areaPlacement;

		areaPlacement.GetComponent<Continent> ().areaID = data[0];

	}
}

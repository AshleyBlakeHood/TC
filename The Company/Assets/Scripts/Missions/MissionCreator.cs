using UnityEngine;
using System.Collections;

public class MissionCreator : MonoBehaviour
{
    AreaCreator areaCreator;
    GameManager gameManager;

    VeCSV missionWeightings;
	VeCSV missionNames;

	public GameObject missionObject;

	// Use this for initialization
	void Start ()
    {
		//Find objects in scene.
        gameManager = GameObject.FindObjectOfType<GameManager>();
        areaCreator = GameObject.FindObjectOfType<AreaCreator>();

		//Load in the mission weightings.
	    string missionWeightingsCSV = (Resources.Load("Mission Weightings") as TextAsset).ToString();
        missionWeightings = new VeCSV(missionWeightingsCSV, VeCSV.HeaderDirection.Horizontal);

		//Load in the possible mission names based on mission type.
		string missionNamesCSV = (Resources.Load("Mission Names") as TextAsset).ToString();
		missionNames = new VeCSV(missionNamesCSV, VeCSV.HeaderDirection.Horizontal);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

	public MissionData CreateMission()
    {
        //Type,Min Amount of Agents,Max Amount of Agents,Min Time Limit (Minutes),Max Time Limit (Minutes),Min Monetary Reward,Max Monetary Reward
        int typeSelectedIndex = Random.Range(1, missionWeightings.subValueCount);

		//Parse the mission details and create random missions from them.
        string type = missionWeightings.Get("Type", typeSelectedIndex);
        int amountOfAgents = Random.Range(int.Parse(missionWeightings.Get("Min Amount of Agents", typeSelectedIndex)), int.Parse(missionWeightings.Get("Max Amount of Agents", typeSelectedIndex)));
        float timeLimit = Random.Range(int.Parse(missionWeightings.Get("Min Time Limit (Minutes)", typeSelectedIndex)), int.Parse(missionWeightings.Get("Max Time Limit (Minutes)", typeSelectedIndex))) * 1000;
        int monetaryReward = Random.Range(int.Parse(missionWeightings.Get("Min Monetary Reward", typeSelectedIndex)), int.Parse(missionWeightings.Get("Max Monetary Reward", typeSelectedIndex)));
        Continent location = areaCreator.areas[Random.Range(0, areaCreator.areas.Length)].GetComponent<Continent>();

        int[] skillValues = new int[11];

		//Randomly assign skill requirements and then base them on the amount of agents.
        for (int i = 0; i < skillValues.Length; i++)
        {
            skillValues[i] = Random.Range(1, 100) * amountOfAgents;
        }

        SkillsHolder skills = new SkillsHolder(skillValues[0], skillValues[1], skillValues[2], skillValues[3], skillValues[4], skillValues[5], skillValues[6], skillValues[7], skillValues[8], skillValues[9], skillValues[10]);

		//Get a mission name based on the created details.
		string missionName = PopulateMissionNameWithRandomValues(missionNames.Get (type, Random.Range (1, missionNames.subValueCount)));

		//Return a new mission data object.
		return new MissionData(missionName, skills, monetaryReward, location, type, amountOfAgents, timeLimit, "Client Name");
    }

	private string PopulateMissionNameWithRandomValues(string missionName)
	{
		string currentKey = "";
		bool addingToKey = false;

		//Loop through all characters looking for keys ([KEY]).
		for (int i = 0; i < missionName.Length; i++)
		{
			if (missionName[i] == '[')
				addingToKey = true;
			else if (missionName[i] == ']')
			{
				string currentKeyCSV = (Resources.Load (currentKey) as TextAsset).ToString ();
				VeCSV possibleValues = new VeCSV(currentKeyCSV, VeCSV.HeaderDirection.Horizontal);

				missionName = missionName.Replace ("[" + currentKey + "]", possibleValues.Get (0, Random.Range (0, possibleValues.subValueCount)));

				addingToKey = false;
				currentKey = "";
			}
			else if (addingToKey)
				currentKey += missionName[i];
		}

		return missionName;
	}

	public void PlaceMissionInWorld(MissionData missionData)
	{
		//Cache the sprite and position.
		Sprite continentSprite = missionData.location.GetComponent<SpriteRenderer> ().sprite;
		Vector3 continentPos = missionData.location.transform.position;

		RaycastHit2D continentHit;
		bool hitContinent = false;

		int tries = 0;

		//Continually look for the location continent through the use of raycasts.
		while (!hitContinent)
		{
			float x = Random.Range (-continentSprite.bounds.extents.x, continentSprite.bounds.extents.x);
			float y = Random.Range (-continentSprite.bounds.extents.y, continentSprite.bounds.extents.y);

			Ray findContinentRay = new Ray(new Vector3(continentPos.x + x, continentPos.y + y, transform.position.z -10), Vector3.forward);
			continentHit = Physics2D.GetRayIntersection(findContinentRay);

			if (continentHit.collider != null)
			{
				if (continentHit.collider.name.Replace ("(Clone)", "") == missionData.location.name)
				{
					hitContinent = true;
					GameObject g = Instantiate (missionObject, new Vector3(continentPos.x + x, continentPos.y + y, 0), Quaternion.identity) as GameObject;
					g.name = "Mission: " + missionData.missionName;
					g.AddComponent<MissionObject>().missionData = missionData;
				}
			}

			tries++;

			if (tries > 50)
			{
				Debug.Log ("Fail");
				hitContinent = true;
			}
		}
	}

    void OnGUI()
    {
		//For debug purposes.
        if (GUI.Button(new Rect(10, 10, 100, 100), "Test Mission"))
			PlaceMissionInWorld (CreateMission ());
    }
}

using UnityEngine;
using System.Collections;
using System;

public class MissionObject : MonoBehaviour
{
	GameManager gameManager;
    GUIManager guiManager;
    TimeManager timeManager;
    MissionCreator missionCreator;

	public MissionData missionData;

    public DateTime missionStartTime { get; private set; }
    public DateTime missionEndTime { get; private set; }

	// Use this for initialization
	void Start ()
    {
		gameManager = FindObjectOfType<GameManager>();
        guiManager = FindObjectOfType<GUIManager>();
        timeManager = FindObjectOfType<TimeManager>();
        missionCreator = FindObjectOfType<MissionCreator>();

        missionCreator.spawnedMissions.Add(this);

        missionStartTime = timeManager.currentDT;
        missionEndTime = missionStartTime.AddSeconds(missionData.timeLimit);

		//Notify Player
		gameManager.NotifyPlayerOfSpawn (transform.position, Color.blue);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (timeManager.currentDT > missionEndTime)
            DeleteMission();
	}

    public void DeleteMission()
    {
        missionCreator.spawnedMissions.Remove(this);
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        guiManager.GetComponent<GUIM_Mission>().SetMissionData(missionData, this);
        guiManager.GetComponent<GUIM_Mission>().ShowMission();
    }

    public void CompleteMission (Player plr)
    {
        CashManager cm = GameObject.FindObjectOfType<CashManager>();
        cm.investment((double)missionData.monetaryReward);

        if (missionData.itemRewardName == "Agents")
        {
            AgentCreator ac = GameObject.FindObjectOfType<AgentCreator>();
            SafehouseData shd = GameObject.FindObjectOfType<SafehouseData>();
            for (int i = 0; i != missionData.itemRewardAmount; i++)
            {
                shd.officeAgents.Add(ac.CreateNewAgent());
            }
        }
        else if (missionData.itemRewardName.Contains("(Gun)"))
        {

        }
        else if (missionData.itemRewardName.Contains("(Armour)"))
        {

        }
        else if (missionData.itemRewardName.Contains("(Equipment)"))
        {

        }
        else if (missionData.itemRewardName.Contains("(Vehicle)"))
        {

        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GUIM_Mission : MonoBehaviour
{
    GUIManager guiManager;
    TimeManager timeManager;

    private MissionData missionData;
    private MissionObject missionObject;

	// Use this for initialization
	void Start ()
    {
        guiManager = GameObject.FindObjectOfType<GUIManager>();
        timeManager = GameObject.FindObjectOfType<TimeManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (guiManager.missionViewGUI.activeInHierarchy && missionObject != null)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds((missionObject.missionEndTime - timeManager.currentDT).TotalSeconds);

            guiManager.missionViewGUI.transform.FindChild("Text - Time Left").GetComponent<Text>().text = "Time Left: " + String.Format("{0}:{1:00}:{2:00}", (int)timeSpan.TotalHours, (int)timeSpan.Minutes, (int)timeSpan.Seconds);
        }
	}

    public void SetMissionData(MissionData missionData, MissionObject missionObject)
    {
        this.missionData = missionData;
        this.missionObject = missionObject;
    }

    public void ShowMission()
    {
        guiManager.missionViewGUI.SetActive(true);
        UpdateGUI();
    }

    public void UpdateGUI()
    {
        guiManager.missionViewGUI.transform.FindChild("Text - Mission Name").GetComponent<Text>().text = missionData.missionName;
        guiManager.missionViewGUI.transform.FindChild("Text - Mission Location").GetComponent<Text>().text = "Location: " + missionData.location.name;
        guiManager.missionViewGUI.transform.FindChild("Text - Mission Type").GetComponent<Text>().text = "Type: " + missionData.type;

        TimeSpan timeSpan = TimeSpan.FromSeconds(missionData.timeLimit);

        guiManager.missionViewGUI.transform.FindChild("Text - Time Left").GetComponent<Text>().text = "Time Left: " + String.Format("{0}:{1:00}:{2:00}", (int)timeSpan.TotalHours, (int)timeSpan.Minutes, (int)timeSpan.Seconds);
        guiManager.missionViewGUI.transform.FindChild("Text - Client").GetComponent<Text>().text = "Client: " + missionData.cilentName;
        guiManager.missionViewGUI.transform.FindChild("Text - Total Agents").GetComponent<Text>().text = "Max Agents: " + missionData.amountOfAgents.ToString();
    }
}

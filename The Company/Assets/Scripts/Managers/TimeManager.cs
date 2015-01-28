using UnityEngine;
using System.Collections;
using System;

public class TimeManager : MonoBehaviour
{
	public GUIM_Time timeGUI;

	public DateTime currentDT;
	float startTime = 0;

	// Use this for initialization
	void Start ()
	{
		currentDT = System.DateTime.Now;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1))
			ChangeGameSpeed(1);

		if (Input.GetKeyDown (KeyCode.Alpha2))
			ChangeGameSpeed(2);

		if (Input.GetKeyDown (KeyCode.Alpha3))
			ChangeGameSpeed(3);

		currentDT = currentDT.AddMinutes (Time.deltaTime * 2);

		//Update GUI
		timeGUI.lblDate.text = currentDT.ToString ("dd/MM/yyyy");
		timeGUI.lblTime.text = currentDT.ToString ("HH:mm tt");
	}

	public void ChangeGameSpeed(int speedSetting)
	{
		timeGUI.speed1Button.interactable = true;
		timeGUI.speed2Button.interactable = true;
		timeGUI.speed3Button.interactable = true;

		switch (speedSetting)
		{
		case 1:
			timeGUI.speed1Button.interactable = false;
			Time.timeScale = 1;
			break;
		case 2:
			timeGUI.speed2Button.interactable = false;
			Time.timeScale = 3;
			break;
		case 3:
			timeGUI.speed3Button.interactable = false;
			Time.timeScale = 7;
			break;

		}
	}
}
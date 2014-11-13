﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class MissionData {

	public string type = "Bomb Defusal";
	public int timeGiven = 24;
	public int objectives = 5;
	public int equipment = 6;
	public int agents = 3;
	public string continent = "Europe";
	public string location = "Brussels";

	public MissionData(string typ, int time, int obj, int equip, int agt, string cont, string locat)
	{
		type = typ;
		timeGiven = time;
		objectives = obj;
		equipment = equip;
		agents = agt;
		continent = cont;
		location = locat;
	}
}
using UnityEngine;
using System.Collections;

[System.Serializable]
public class AgentData
{
	public string foreame = "Agent";
	public string surname = "Agentson";
	public string codename = "Agent 1";
	public string[] aliases = {"Spy", "Operative"};

	public string gender = "?";

	public string dayOfBirth = "1";
	public string monthOfBirth = "1";
	public string yearOfBirth = "1970";

	public string height = "6'0\"";

	public string eyes = "Brown";
	public string hair = "Black";

	public PrimaryStats stats = new PrimaryStats(10, 10, 10);
	public SkillsHolder skills = new SkillsHolder(100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100);

	public float mapX = 0;
	public float mapY = 0;

	public float wage = 0;

	public AgentData (string fName, string sName, string cName, string[] aNames, string gend, string dob, string mob, string yob, string iHeight, string iEyes, string iHair, PrimaryStats iStats, SkillsHolder iSkills)
	{
		foreame = fName;
		surname = sName;
		codename = cName;
		aliases = aNames;

		gender = gend;

		dayOfBirth = dob;
		monthOfBirth = mob;
		yearOfBirth = yob;

		height = iHeight;

		eyes = iEyes;
		hair = iHair;

		stats = iStats;
		skills = iSkills;

		CalculateWage ();
	}

	private void CalculateWage()
	{
		wage = skills.deception + skills.firearms + skills.firstaid + skills.hacking + skills.investigation + skills.lockpick + skills.perception + skills.persuasion + skills.steal + skills.stealth + skills.unarmed;
		wage = (wage * 10) / 11;
		wage += ((stats.agility + stats.charisma + stats.intelligence) * 100) / 3;

		wage = Mathf.Round (wage);
	}
}

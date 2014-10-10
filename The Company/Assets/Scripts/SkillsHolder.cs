using UnityEngine;
using System.Collections;

public class SkillsHolder
{
	public int stealth;
	public int steal;
	public int firearms;
	public int unarmed;
	public int firstaid;
	public int lockpick;
	public int hacking;
	public int perception;
	public int investigation;
	public int deception;
	public int persuasion;

	public SkillsHolder(int iStealth, int iSteal, int iFire, int iUnarm, int ifirst, int iLock, int iHack, int iPer, int iInvest, int iDecep, int iPersu)
	{
		stealth = iStealth;
		steal = iSteal;
		firearms = iFire;
		unarmed = iUnarm;
		firstaid = ifirst;
		lockpick = iLock;
		hacking = iHack;
		perception = iPer;
		investigation = iInvest;
		deception = iDecep;
		persuasion = iPersu;
	}
}

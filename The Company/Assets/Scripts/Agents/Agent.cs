using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : MonoBehaviour
{
	public enum Skill { Stealth, Steal, Firearms, Unarmed, FirstAid, LockPick, Hacking, Perception, Investigation, Deception, Persuasion };

	public AgentData data;

	//Agent Usable?
	bool inTraining = false;
	bool inMission = false;

	private float timeUntilTrained = 0;
	private Skill trainingSkill = Skill.Stealth;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (inTraining)
		{
			if (timeUntilTrained > Time.time)
			{
				SetAgentTrainingStatus(false);
				ChangeAgentSkill(trainingSkill, Random.Range (0, 10));
			}
		}
	}

	public void ChangeAgentSkill(Skill skill, int amount)
	{
		switch (skill)
		{
		case Skill.Stealth:
			data.skills.stealth = ValidateSkillChange (data.skills.stealth, amount);
			break;
		case Skill.Steal:
			data.skills.steal = ValidateSkillChange (data.skills.steal, amount);
			break;
		case Skill.Firearms:
			data.skills.firearms = ValidateSkillChange (data.skills.firearms, amount);
			break;
		case Skill.Unarmed:
			data.skills.unarmed = ValidateSkillChange (data.skills.unarmed, amount);
			break;
		case Skill.FirstAid:
			data.skills.firstaid = ValidateSkillChange (data.skills.firstaid, amount);
			break;
		case Skill.LockPick:
			data.skills.lockpick = ValidateSkillChange (data.skills.lockpick, amount);
			break;
		case Skill.Hacking:
			data.skills.hacking = ValidateSkillChange (data.skills.hacking, amount);
			break;
		case Skill.Perception:
			data.skills.perception = ValidateSkillChange (data.skills.perception, amount);
			break;
		case Skill.Investigation:
			data.skills.investigation = ValidateSkillChange (data.skills.investigation, amount);
			break;
		case Skill.Deception:
			data.skills.deception = ValidateSkillChange (data.skills.deception, amount);
			break;
		case Skill.Persuasion:
			data.skills.persuasion = ValidateSkillChange (data.skills.persuasion, amount);
			break;
		}
	}

	private int ValidateSkillChange(int currentSkillAmount, int change)
	{
		currentSkillAmount += change;

		if (currentSkillAmount > 100)
			return 100;
		else if (currentSkillAmount < 0)
			return 0;
		else
			return currentSkillAmount;
	}

	public void TrainAgent(Skill skill)
	{
		switch (skill)
		{
		case Skill.Stealth:
			trainingSkill = Skill.Stealth;
			break;
		case Skill.Steal:
			trainingSkill = Skill.Steal;
			break;
		case Skill.Firearms:
			trainingSkill = Skill.Firearms;
			break;
		case Skill.Unarmed:
			trainingSkill = Skill.Unarmed;
			break;
		case Skill.FirstAid:
			trainingSkill = Skill.FirstAid;
			break;
		case Skill.LockPick:
			trainingSkill = Skill.LockPick;
			break;
		case Skill.Hacking:
			trainingSkill = Skill.Hacking;
			break;
		case Skill.Perception:
			trainingSkill = Skill.Perception;
			break;
		case Skill.Investigation:
			trainingSkill = Skill.Investigation;
			break;
		case Skill.Deception:
			trainingSkill = Skill.Deception;
			break;
		case Skill.Persuasion:
			trainingSkill = Skill.Persuasion;
			break;
		}
	}

	public void SetAgentMissionStatus(bool iInMission)
	{
		inMission = iInMission;
	}

	public void SetAgentTrainingStatus(bool iInTraining)
	{
		inTraining = iInTraining;
	}

	public bool IsAgentUsable()
	{
		if (inMission || inTraining)
			return false;
		else
			return true;
	}
}

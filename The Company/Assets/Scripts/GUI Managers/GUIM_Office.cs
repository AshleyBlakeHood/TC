using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GUIM_Office : MonoBehaviour
{
    GUIManager guiManager;

    public GameObject agentCanvas;

    public SafehouseData safehouse;

	private enum Purpose {ShowAgentsInOffice, ShowAgentsToHire}

	List<AgentData> agentsForHire = new List<AgentData> ();

	// Use this for initialization
	void Start ()
    {
        guiManager = GameObject.FindObjectOfType<GUIManager>();

		AgentCreator ac = GameObject.FindObjectOfType<AgentCreator>();

		for (int i = 0; i < Random.Range(1, 6); i++)
		{
			agentsForHire.Add (ac.CreateNewAgent ());
		}
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

	/// <summary>
	/// Opens the agents canvas and updates the date so that the player can see their agents.
	/// </summary>
	/// <param name="safehouse">Safehouse.</param>
    public void ShowAgents(SafehouseData safehouse)
    {
		this.safehouse = safehouse;

        agentCanvas.SetActive(true);
        
		UpdateOfficeList ();
    }

	/// <summary>
	/// Will show the agents already associated with the object without passing a new set of agents to the object.
	/// </summary>
    public void ShowAgents()
    {
        agentCanvas.SetActive(true);

		UpdateOfficeList ();
    }

	/// <summary>
	/// Updates the agents that are currently in the office associated with this object.
	/// </summary>
	private void UpdateOfficeList()
	{
		ScrollableList sl = agentCanvas.GetComponentInChildren<ScrollableList>();
		
		sl.ClearList();
		
		if (safehouse.officeAgents.Count > 0)
		{
			sl.CreateList(safehouse.officeAgents.Count, 1);
			
			for (int i = 0; i < safehouse.officeAgents.Count; i++)
			{
				SetAgentBoxData(sl.elements[i].transform, safehouse.officeAgents[i], Purpose.ShowAgentsInOffice);
			}
		}
	}

	/// <summary>
	/// Updates the list of agents available for hire. This is global due to the nature of this object.
	/// </summary>
	public void UpdateHireList()
	{
		ScrollableList sl = agentCanvas.GetComponentInChildren<ScrollableList>();
		
		sl.ClearList();

		if (agentsForHire.Count > 0)
		{
			sl.CreateList(agentsForHire.Count, 1);
			
			for (int i = 0; i < agentsForHire.Count; i++)
			{
				SetAgentBoxData(sl.elements[i].transform, agentsForHire[i], Purpose.ShowAgentsToHire);
			}
		}
	}

	/// <summary>
	/// Updates the fields and assigns button method calls for the listbox prefab for an agent.
	/// </summary>
	/// <param name="boxTranform">Box tranform.</param>
	/// <param name="data">Data.</param>
	/// <param name="purpose">Purpose.</param>
	private void SetAgentBoxData(Transform boxTranform, AgentData data, Purpose purpose)
	{
		//Attributes
		boxTranform.FindChild("Text - NameField").GetComponent<Text>().text = data.foreame + " " + data.surname;
		boxTranform.FindChild("Text - CodenameField").GetComponent<Text>().text = data.codename;
		boxTranform.FindChild("Text - Known AliasesField").GetComponent<Text>().text = string.Join(", ", data.aliases);
		boxTranform.FindChild("Text - GenderField").GetComponent<Text>().text = data.gender;
		boxTranform.FindChild("Text - DOBField").GetComponent<Text>().text = data.dayOfBirth + "/" + data.monthOfBirth + "/" + data.yearOfBirth;
		boxTranform.FindChild("Text - HeightField").GetComponent<Text>().text = data.height;
		boxTranform.FindChild("Text - EyesField").GetComponent<Text>().text = data.eyes;
		boxTranform.FindChild("Text - HairField").GetComponent<Text>().text = data.hair;
		
		//Primary Stats
		boxTranform.FindChild("Primary Stats").FindChild("Text - CharismaField").GetComponent<Text>().text = data.stats.charisma.ToString();
		boxTranform.FindChild("Primary Stats").FindChild("Text - IntelligenceField").GetComponent<Text>().text = data.stats.intelligence.ToString();
		boxTranform.FindChild("Primary Stats").FindChild("Text - AgilityField").GetComponent<Text>().text = data.stats.agility.ToString();
		
		//Skills
		boxTranform.FindChild("Skills").FindChild("Text - StealthField").GetComponent<Text>().text = data.skills.stealth.ToString();
		boxTranform.FindChild("Skills").FindChild("Text - StealField").GetComponent<Text>().text = data.skills.steal.ToString();
		boxTranform.FindChild("Skills").FindChild("Text - FirearmsField").GetComponent<Text>().text = data.skills.firearms.ToString();
		boxTranform.FindChild("Skills").FindChild("Text - UnarmedField").GetComponent<Text>().text = data.skills.unarmed.ToString();
		boxTranform.FindChild("Skills").FindChild("Text - First AidField").GetComponent<Text>().text = data.skills.firstaid.ToString();
		boxTranform.FindChild("Skills").FindChild("Text - Lock PickField").GetComponent<Text>().text = data.skills.lockpick.ToString();
		boxTranform.FindChild("Skills").FindChild("Text - HackingField").GetComponent<Text>().text = data.skills.hacking.ToString();
		boxTranform.FindChild("Skills").FindChild("Text - PerceptionField").GetComponent<Text>().text = data.skills.perception.ToString();
		boxTranform.FindChild("Skills").FindChild("Text - InvestigationField").GetComponent<Text>().text = data.skills.investigation.ToString();
		boxTranform.FindChild("Skills").FindChild("Text - DeceptionField").GetComponent<Text>().text = data.skills.deception.ToString();
		boxTranform.FindChild("Skills").FindChild("Text - PersuasionField").GetComponent<Text>().text = data.skills.persuasion.ToString();

		switch (purpose)
		{
		case Purpose.ShowAgentsInOffice:
			boxTranform.FindChild("Button - Hire Agent").gameObject.SetActive (false);

			boxTranform.FindChild("Button - Burn Agent").GetComponent<Button>().onClick.AddListener(delegate{BurnAgent(data);});
			break;
		case Purpose.ShowAgentsToHire:
			boxTranform.FindChild("Button - Burn Agent").gameObject.SetActive (false);
			boxTranform.FindChild("Button - Train Agent").gameObject.SetActive (false);

			boxTranform.FindChild("Button - Hire Agent").GetComponent<Button>().onClick.AddListener(delegate{HireAgent(data);});
			break;
		}
	}

	/// <summary>
	/// SDisplay the agents that are available for hire to the user.
	/// </summary>
	public void ShowAgentsToHire()
	{
		AgentCreator ac = GameObject.FindObjectOfType<AgentCreator>();

		agentCanvas.SetActive(true);
		ScrollableList sl = agentCanvas.GetComponentInChildren<ScrollableList>();
		
		sl.ClearList();
		sl.CreateList(agentsForHire.Count, 1);
		
		for (int i = 0; i < agentsForHire.Count; i++)
		{
			SetAgentBoxData(sl.elements[i].transform, agentsForHire[i], Purpose.ShowAgentsToHire);
		}
	}

	/// <summary>
	/// Hire the passed agent and associate the agent with the office currently associated with this object.
	/// </summary>
	/// <param name="agent">Agent.</param>
	public void HireAgent(AgentData agent)
	{
		safehouse.officeAgents.Add (agent);

		agentsForHire.Remove (agent);

		UpdateHireList ();

		FindObjectOfType<CashManager> ().bills (agent.wage);
	}

	/// <summary>
	/// Remove the passed agent from the game.
	/// </summary>
	/// <param name="agent">Agent.</param>
	public void BurnAgent(AgentData agent)
	{
		FindObjectOfType<CashManager> ().bills (-agent.wage);
		FindObjectOfType<Player> ().ChangeGlobalReputation (-1);

		safehouse.officeAgents.Remove (agent);

		UpdateOfficeList ();
	}
}

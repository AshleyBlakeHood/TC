using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIM_Office : MonoBehaviour
{
    GUIManager guiManager;

    public GameObject agentCanvas;

    public AgentData[] agents;

	// Use this for initialization
	void Start ()
    {
        guiManager = GameObject.FindObjectOfType<GUIManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void ShowAgents(AgentData[] agents)
    {
        this.agents = agents;

        agentCanvas.SetActive(true);
        ScrollableList sl = agentCanvas.GetComponentInChildren<ScrollableList>();

        sl.ClearList();
        sl.CreateList(agents.Length, 1);

        for (int i = 0; i < agents.Length; i++)
        {
            //Attributes
            sl.elements[i].transform.FindChild("Text - NameField").GetComponent<Text>().text = agents[i].foreame + " " + agents[i].surname;
            sl.elements[i].transform.FindChild("Text - CodenameField").GetComponent<Text>().text = agents[i].codename;
            sl.elements[i].transform.FindChild("Text - Known AliasesField").GetComponent<Text>().text = string.Join(", ", agents[i].aliases);
            sl.elements[i].transform.FindChild("Text - GenderField").GetComponent<Text>().text = agents[i].gender;
            sl.elements[i].transform.FindChild("Text - DOBField").GetComponent<Text>().text = agents[i].dayOfBirth + "/" + agents[i].monthOfBirth + "/" + agents[i].yearOfBirth;
            sl.elements[i].transform.FindChild("Text - HeightField").GetComponent<Text>().text = agents[i].height;
            sl.elements[i].transform.FindChild("Text - EyesField").GetComponent<Text>().text = agents[i].eyes;
            sl.elements[i].transform.FindChild("Text - HairField").GetComponent<Text>().text = agents[i].hair;

            //Primary Stats
            sl.elements[i].transform.FindChild("Primary Stats").FindChild("Text - CharismaField").GetComponent<Text>().text = agents[i].stats.charisma.ToString();
            sl.elements[i].transform.FindChild("Primary Stats").FindChild("Text - IntelligenceField").GetComponent<Text>().text = agents[i].stats.intelligence.ToString();
            sl.elements[i].transform.FindChild("Primary Stats").FindChild("Text - AgilityField").GetComponent<Text>().text = agents[i].stats.agility.ToString();

            //Skills
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - StealthField").GetComponent<Text>().text = agents[i].skills.stealth.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - StealField").GetComponent<Text>().text = agents[i].skills.steal.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - FirearmsField").GetComponent<Text>().text = agents[i].skills.firearms.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - UnarmedField").GetComponent<Text>().text = agents[i].skills.unarmed.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - First AidField").GetComponent<Text>().text = agents[i].skills.firstaid.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - Lock PickField").GetComponent<Text>().text = agents[i].skills.lockpick.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - HackingField").GetComponent<Text>().text = agents[i].skills.hacking.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - PerceptionField").GetComponent<Text>().text = agents[i].skills.perception.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - InvestigationField").GetComponent<Text>().text = agents[i].skills.investigation.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - DeceptionField").GetComponent<Text>().text = agents[i].skills.deception.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - PersuasionField").GetComponent<Text>().text = agents[i].skills.persuasion.ToString();
        }
    }

    public void ShowAgents()
    {
        agentCanvas.SetActive(true);
        ScrollableList sl = agentCanvas.GetComponentInChildren<ScrollableList>();

        sl.ClearList();
        sl.CreateList(agents.Length, 1);

        for (int i = 0; i < agents.Length; i++)
        {
            //Attributes
            sl.elements[i].transform.FindChild("Text - NameField").GetComponent<Text>().text = agents[i].foreame + " " + agents[i].surname;
            sl.elements[i].transform.FindChild("Text - CodenameField").GetComponent<Text>().text = agents[i].codename;
            sl.elements[i].transform.FindChild("Text - Known AliasesField").GetComponent<Text>().text = string.Join(", ", agents[i].aliases);
            sl.elements[i].transform.FindChild("Text - GenderField").GetComponent<Text>().text = agents[i].gender;
            sl.elements[i].transform.FindChild("Text - DOBField").GetComponent<Text>().text = agents[i].dayOfBirth + "/" + agents[i].monthOfBirth + "/" + agents[i].yearOfBirth;
            sl.elements[i].transform.FindChild("Text - HeightField").GetComponent<Text>().text = agents[i].height;
            sl.elements[i].transform.FindChild("Text - EyesField").GetComponent<Text>().text = agents[i].eyes;
            sl.elements[i].transform.FindChild("Text - HairField").GetComponent<Text>().text = agents[i].hair;

            //Primary Stats
            sl.elements[i].transform.FindChild("Primary Stats").FindChild("Text - CharismaField").GetComponent<Text>().text = agents[i].stats.charisma.ToString();
            sl.elements[i].transform.FindChild("Primary Stats").FindChild("Text - IntelligenceField").GetComponent<Text>().text = agents[i].stats.intelligence.ToString();
            sl.elements[i].transform.FindChild("Primary Stats").FindChild("Text - AgilityField").GetComponent<Text>().text = agents[i].stats.agility.ToString();

            //Skills
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - StealthField").GetComponent<Text>().text = agents[i].skills.stealth.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - StealField").GetComponent<Text>().text = agents[i].skills.steal.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - FirearmsField").GetComponent<Text>().text = agents[i].skills.firearms.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - UnarmedField").GetComponent<Text>().text = agents[i].skills.unarmed.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - First AidField").GetComponent<Text>().text = agents[i].skills.firstaid.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - Lock PickField").GetComponent<Text>().text = agents[i].skills.lockpick.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - HackingField").GetComponent<Text>().text = agents[i].skills.hacking.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - PerceptionField").GetComponent<Text>().text = agents[i].skills.perception.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - InvestigationField").GetComponent<Text>().text = agents[i].skills.investigation.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - DeceptionField").GetComponent<Text>().text = agents[i].skills.deception.ToString();
            sl.elements[i].transform.FindChild("Skills").FindChild("Text - PersuasionField").GetComponent<Text>().text = agents[i].skills.persuasion.ToString();
        }
    }
}

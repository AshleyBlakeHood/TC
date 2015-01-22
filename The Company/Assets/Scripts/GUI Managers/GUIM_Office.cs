using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIM_Office : MonoBehaviour
{
    GUIManager guiManager;

    public GameObject agentCanvas;

	// Use this for initialization
	void Start ()
    {
        guiManager = GameObject.FindObjectOfType<GUIManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void ShowAgents()
    {
        agentCanvas.SetActive(true);
        AgentCreator ac = GameObject.FindObjectOfType<AgentCreator>();
        

        ScrollableList sl = agentCanvas.GetComponentInChildren<ScrollableList>();

        sl.ClearList();
        sl.CreateList(5, 1);

        for (int i = 0; i < 5; i++)
        {
            AgentData data = ac.CreateNewAgent();

            sl.elements[i].transform.FindChild("Text - NameField").GetComponent<Text>().text = data.foreame + " " + data.surname;
            sl.elements[i].transform.FindChild("Text - CodenameField").GetComponent<Text>().text = data.codename;
            sl.elements[i].transform.FindChild("Text - Known AliasesField").GetComponent<Text>().text = string.Join(", ", data.aliases);
            sl.elements[i].transform.FindChild("Text - GenderField").GetComponent<Text>().text = data.gender;
            sl.elements[i].transform.FindChild("Text - DOBField").GetComponent<Text>().text = data.dayOfBirth + "/" + data.monthOfBirth + "/" + data.yearOfBirth;
            sl.elements[i].transform.FindChild("Text - HeightField").GetComponent<Text>().text = data.height;
            sl.elements[i].transform.FindChild("Text - EyesField").GetComponent<Text>().text = data.eyes;
            sl.elements[i].transform.FindChild("Text - HairField").GetComponent<Text>().text = data.hair;
        }
    }
}

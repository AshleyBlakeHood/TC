using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIM_TopBar : ObserverMonoBehaviour
{
	public Canvas topBarCanvas;

	private CashManager subject;
	private Player subjectPlayer;

	// Use this for initialization
	void Start ()
	{
		subject = FindObjectOfType<Player> ().GetComponent<CashManager> ();
		subjectPlayer = FindObjectOfType<Player> ();

		subject.Attach (this);
		subjectPlayer.Attach (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void UpdateObserver()
	{
		topBarCanvas.transform.FindChild ("Text - Money").GetComponent<Text> ().text = "Money: £" + subject.currentHeld;
		topBarCanvas.transform.FindChild ("Text - Bills").GetComponent<Text> ().text = "Bills: £" + subject.billsAmount;

		topBarCanvas.transform.FindChild ("Text - Reputation").GetComponent<Text> ().text = "Reputation: " + subjectPlayer.GetGlobalReputation ();
	}

	public void EnableBillsText()
	{
		topBarCanvas.transform.FindChild ("Text - Bills").GetComponent<Text> ().enabled = true;
	}

	public void DisableBillsText()
	{
		topBarCanvas.transform.FindChild ("Text - Bills").GetComponent<Text> ().enabled = false;
	}
}

using UnityEngine;
using System.Collections;

public class BankManager : MonoBehaviour {

    bool runOnce = false;
    double interestRate = 0.2, currentHeld = 0;

    public double current { get { return currentHeld; } }

    TimeManager tm;

    System.DateTime holdingTime, theTime;



    public void investment(double payIn, string equation)
    {
        if (equation == "Add")
        {
            currentHeld = +payIn;
        }
        else if (equation == "Subtract")
        {
            currentHeld = +payIn;
        }
    }

    void timeAdd()
    {
        theTime = theTime.AddHours(2); // Sets the interest add delay
    }

    void interest()
    {
        if (runOnce == false)
        {
            theTime = tm.currentDT;

            timeAdd();

            runOnce = true;
        }


        holdingTime = tm.currentDT;

        if (holdingTime.CompareTo(theTime).ToString() == "1" || holdingTime.CompareTo(theTime).ToString() == "0")
        {
            Debug.Log("outputted");

            timeAdd();

            currentHeld += currentHeld * interestRate;
        }
    }

	// Use this for initialization
	void Start () {

        tm = GameObject.Find("Time Manager").GetComponent<TimeManager>();
	
	}
	
	// Update is called once per frame
	void Update () {

        interest();

	}
}

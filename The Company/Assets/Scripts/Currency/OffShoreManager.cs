using UnityEngine;
using System.Collections;

public class money_OffShore : MonoBehaviour {

    int setUpCost = 10;
    bool runOnce = false;
    double interestRate = 0.2, currentHeld = 0;

    public double current { get { return currentHeld; } }
    public double getSetUp { get { return setUpCost; } }

    TimeManager tm;

    System.DateTime holdingTime, theTime;

    // write holder value to be taken out at the end of each month



    public void investment(double payIn, string equation)
    {
        if (equation == "Add")
        {
            currentHeld =+ payIn;
        }
        else if (equation == "Subtract")
        {
            currentHeld =+ payIn;
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
            theTime = tm.currentDT; // sets time from Time manager

            timeAdd();

            runOnce = true; // So that this section isnt run again to reset time
        }


        holdingTime = tm.currentDT; // Updates holdingtime so that checks are the same always

        if (holdingTime.CompareTo(theTime).ToString() == "1" || holdingTime.CompareTo(theTime).ToString() == "0")
        {
            timeAdd();

            currentHeld += currentHeld * interestRate; // Adds interest rate to account
        }
    }


	// Use this for initialization
    void Start()
    {

        tm = GameObject.Find("Time Manager").GetComponent<TimeManager>();

    }


	
	// Update is called once per frame
	void Update () 
    {

        interest();

	}
}

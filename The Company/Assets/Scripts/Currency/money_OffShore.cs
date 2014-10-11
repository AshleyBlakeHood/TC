using UnityEngine;
using System.Collections;

public class money_OffShore : MonoBehaviour {

    int setUpCost = 10, runOnce = 0;
    double interestRate = 0.9;

    double currentHeld = 1;

    public double current { get { return currentHeld; } }

    TimeManager tm;

    System.DateTime holdingTime, theTime;



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


	// Use this for initialization
    void Start()
    {

        tm = GameObject.Find("Time Manager").GetComponent<TimeManager>();

        

        //Debug.Log(tm.currentDT.ToString("HH:mm tt"));
    }


	
	// Update is called once per frame
	void Update () 
    {
        if (runOnce == 0)
        {
            theTime = tm.currentDT;

            theTime = theTime.AddHours(2);

            runOnce++;
        }


        holdingTime = tm.currentDT;

        //Debug.Log(holdingTime.CompareTo(theTime).ToString());

        //Debug.Log(tm.currentDT.ToString("HH:mm tt") + theTime.ToString("HH:mm tt") + holdingTime.CompareTo(theTime).ToString() + "    " + currentHeld);

        if (holdingTime.CompareTo(theTime).ToString() == "1" || holdingTime.CompareTo(theTime).ToString() == "0")
        {
            Debug.Log("outputted");

            theTime = theTime.AddHours(2);

            currentHeld += currentHeld * interestRate;
        }

	}
}

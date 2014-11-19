using UnityEngine;
using System.Collections;

public class BankManager : MonoBehaviour {

    bool runOnceInterest = false, runOnceBills = false;
    double interestRate = 0.2, currentHeld = 0, billsAmount = 0;

    public double current { get { return currentHeld; } }

    TimeManager tm;

    System.DateTime holdingTimeInterest, holdingTimeBills, theTimeBills, theTimeInterest;



    public void investment(double payIn)
    {
        currentHeld = +payIn;
    }

    public void bills(double amount)
    {
        billsAmount += amount;
    }

    void timeAddInterest()
    {
        theTimeInterest = theTimeInterest.AddHours(2); // Sets the interest add delay
    }

    void timeAddBills()
    {
        theTimeBills = theTimeBills.AddMonths(1);   
    }

    void interest()
    {
        if (runOnceInterest == false)
        {
            theTimeInterest = tm.currentDT;

            timeAddInterest();

            runOnceInterest = true;
        }


        holdingTimeInterest = tm.currentDT;

        if (holdingTimeInterest.CompareTo(theTimeInterest).ToString() == "1" || holdingTimeInterest.CompareTo(theTimeInterest).ToString() == "0")
        {
            //Debug.Log("outputted");

            timeAddInterest();

            currentHeld += currentHeld * interestRate;
        }
    }

    void billReductor()
    {
        if (runOnceBills== false)
        {
            theTimeBills = tm.currentDT;

            timeAddBills();

            runOnceBills = true;
        }

        holdingTimeBills = tm.currentDT;

        if (holdingTimeBills.CompareTo(theTimeBills).ToString() == "1" || holdingTimeBills.CompareTo(theTimeBills).ToString() == "0")
        {
            //Debug.Log("outputted");

            timeAddBills();

            currentHeld -= billsAmount;
        }
    }

	// Use this for initialization
	void Start () {

        tm = GameObject.Find("Time Manager").GetComponent<TimeManager>();
        
        StartCoroutine(wait());
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator wait() // Runs methods every 10 seconds
    {
        while (true)
        {
            yield return new WaitForSeconds(10.0f);
            //interest();
            billReductor(); 
            //Debug.Log("run");
        }

    }
}

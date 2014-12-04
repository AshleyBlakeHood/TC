using UnityEngine;
using System.Collections;

public class BankManager : MonoBehaviour {

    bool runOnceInterest = false, runOnceBills = false;
    double interestRate = 0.2, currentHeld = 0, billsAmount = 0;

    public double current { get { return currentHeld; } }

    TimeManager tm;

    System.DateTime holdingTimeInterest, holdingTimeBills, theTimeBills, theTimeInterest;


    // Method for money to be passed to the players account
    public void investment(double payIn)
    {
        currentHeld = +payIn;
    }

    // Method for taking money out of players account each month
    public void bills(double amount)
    {
        billsAmount += amount;
    }

    // Sets the time interval for adding interest to the players account
    void timeAddInterest()
    {
        theTimeInterest = theTimeInterest.AddHours(2); // Sets the interest add delay
    }

    // Sets the time interval for subtracting bills from the players account
    void timeAddBills()
    {
        theTimeBills = theTimeBills.AddMonths(1);   
    }

    // Checks if interest time interval has been passed
    // Adds the interest to the account
    void interest()
    {
        if (runOnceInterest == false)
        {
            theTimeInterest = tm.currentDT;

            // Adds time interval to checking time
            timeAddInterest();

            runOnceInterest = true;
        }

        // Holds the current time in a variable so that it does not change while checking
        holdingTimeInterest = tm.currentDT;

        // Checks if the current time is equal to the time interval or if it is more then
        if (holdingTimeInterest.CompareTo(theTimeInterest).ToString() == "1" || holdingTimeInterest.CompareTo(theTimeInterest).ToString() == "0")
        {
            // Adds time interval to checking time
            timeAddInterest();

            currentHeld += currentHeld * interestRate;
        }
    }

    void billReductor()
    {
        if (runOnceBills== false)
        {
            theTimeBills = tm.currentDT;

            // Adds time interval to checking time
            timeAddBills();

            runOnceBills = true;
        }

        // Holds the current time in a variable so that it does not change while checking
        holdingTimeBills = tm.currentDT;

        // Checks if the current time is equal to the time interval or if it is more then
        if (holdingTimeBills.CompareTo(theTimeBills).ToString() == "1" || holdingTimeBills.CompareTo(theTimeBills).ToString() == "0")
        {
            // Adds time interval to checking time
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
        }

    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoanManager : MonoBehaviour {

    public TextAsset loadList;

    public Loan[] allLoans;
    public Loan[] activeLoans; //Holds loans that are currently active
    public List<Loan> loanList = new List<Loan>();

    private Loan[] loans = new Loan[4];

    bool runOnce = false;

    TimeManager tm;
    OffShoreManager osm;
    BankManager bm;
    CashManager cm;

    System.DateTime holdingTime, theTimeWeek, theTimeMonth;

	// Use this for initialization
	void Start () {
        readInData();

        tm = GameObject.Find("Time Manager").GetComponent<TimeManager>();
        osm = GameObject.Find("Off Shore Manager").GetComponent<OffShoreManager>();
        bm = GameObject.Find("Bank Manager").GetComponent<BankManager>();
        cm = GameObject.Find("Cash Manager").GetComponent<CashManager>();

        StartCoroutine(wait());
	}

    void readInData()
    {
        //Split by lines and trim empty lines
        string[] lines = loadList.text.Trim(System.Environment.NewLine.ToCharArray()).Split(System.Environment.NewLine.ToCharArray());

        //Initialise loan array
        allLoans = new Loan[lines.Length - 1];

        //In case modders move around or we do this will get the indexs where the columns are in the .csv
        string[] indexFinder = lines[0].Split(',');

        int loanName = -1;
        int loanAmount = -1;
        int interestRate = -1;
        int weekLength = -1;
        int accountAim = -1;
        int appearenceWeighting = -1;

        //Scan through columns to find the respective indexs.
        for (int i = 0; i < indexFinder.Length; i++)
        {
            switch (indexFinder[i])
            {
                case "Name":
                    loanName = i;
                    break;
                case "Amount":
                    loanAmount = i;
                    break;
                case "Interest Rate":
                    interestRate = i;
                    break;
                case "Length":
                    weekLength = i;
                    break;
                case "Account Aim":
                    accountAim = i;
                    break;
                case "Weighting":
                    appearenceWeighting = i;
                    break;
            }
        }

        // public Gun[] allGuns;  public List<Gun> blackMarketGuns = new List<Gun>();
        //Pull out the data using the parsed indexes.
        for (int i = 1; i < lines.Length; i++)
        {
            string[] ls = lines[i].Split(',');

            if (ls.Length < 6)
                continue;

            allLoans[i - 1] = new Loan(ls[loanName], RandomPicker(ls[loanAmount]), RandomPicker(ls[interestRate]), RandomPickerInt(ls[weekLength]), ls[accountAim], float.Parse(ls[appearenceWeighting]));

            loanList.Add(allLoans[i - 1]);

        }
    }

    public float RandomPicker(string input)
    {
        return (Random.Range(float.Parse(input.Substring(0, input.IndexOf("-"))), 
                             float.Parse(input.Substring(input.IndexOf("-"), input.Length - 1))));
    }

    public int RandomPickerInt(string input)
    {
        return (Random.Range(int.Parse(input.Substring(0, input.IndexOf("-"))), 
                             int.Parse(input.Substring(input.IndexOf("-"), input.Length - 1))));
    }

    public bool BoolParse(string s)
    {
        string[] trues = { "1", "TRUE", "T", "Y" };
        string[] falses = { "0", "FALSE", "F", "N" };

        //Convert to upper to limit the comparisons required.
        s = s.ToUpper();

        for (int i = 0; i < trues.Length; i++)
        {
            if (s == trues[i])
                return true;
            else if (s == falses[i])
                return false;
        }

        Debug.LogError("Could not parse a bool from string. False was returned, behaviour may be unexpected!", this);
        return false;
    }

    private void selectLoans()
    {
        //!!!!!!
        //Needs reprogramming by changing ItemWeightHolder to a template class.
        // public Gun[] allGuns;  public List<Gun> blackMarketGuns = new List<Gun>();
        for (int loansIndex = 0; loansIndex < loans.Length; loansIndex++)
        {
            ItemWeightHolder[] loanWeights = new ItemWeightHolder[loanList.Count];

            for (int i = 0; i < loanWeights.Length; i++)
            {
                loanWeights[i] = new ItemWeightHolder(loanList[i].loanName, loanList[i].appearenceWeighting);
            }

            string selectedLoan = SelectionEngine.GetItem(loanWeights);

            for (int i = 0; i < loanList.Count; i++)
            {
                if (loanList[i].loanName == selectedLoan)
                    loans[loansIndex] = loanList[i];
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void timeAddWeek()
    {
        theTimeWeek = theTimeWeek.AddHours(2); // Sets the interest add delay
    }

    void timeAddMonth()
    {
        theTimeMonth = theTimeMonth.AddHours(2); // Sets the interest add delay
    }

    void interestPayer() // Adds the interest at the end of each month
    {
        for (int loanCounter = 0; loanCounter < activeLoans.Length; loanCounter++) // Rolls through all the active loans
        {
            activeLoans[loanCounter].paymentLeft = activeLoans[loanCounter].paymentLeft * activeLoans[loanCounter].interestRate; // Times the remaining payments of the loan by its interest rate every time the method is run
        }
    }

    public Loan[] removeEntry(Loan[] array, Loan entry)
    {
        // Use a temporary array
        Loan[] tmp = new Loan[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            // Only add the ones that are NOT the unwanted entry to tmp
            if (array[i] != entry)
            {
                tmp[i] = array[i];
            }
        }
        // Replace filter by the temporary array
        array = tmp;

        return array;
    } 

    void timeChecker()
    {
        if (runOnce == false)
        {
            theTimeWeek = tm.currentDT;

            timeAddWeek();

            theTimeMonth = tm.currentDT;

            timeAddMonth();

            runOnce = true;
        }


        holdingTime = tm.currentDT;

        if (holdingTime.CompareTo(theTimeWeek).ToString() == "1" || holdingTime.CompareTo(theTimeWeek).ToString() == "0") // Checks for weeks
        {
            //Debug.Log("outputted");

            for (int loanCounter = 0; loanCounter < activeLoans.Length; loanCounter++) // Rolls through all the active loans
            {
                if (activeLoans[loanCounter].accountAim == "Cash")
                {
                    if ((activeLoans[loanCounter].paymentLeft - activeLoans[loanCounter].weeklyPayments) < 0)
                    {
                        cm.bills(activeLoans[loanCounter].paymentLeft);

                        activeLoans = removeEntry(activeLoans, activeLoans[loanCounter]);
                    }
                    else if ((activeLoans[loanCounter].paymentLeft - activeLoans[loanCounter].weeklyPayments) > 0)
                    {
                        cm.bills(activeLoans[loanCounter].weeklyPayments);

                        activeLoans[loanCounter].paymentLeft = activeLoans[loanCounter].paymentLeft - activeLoans[loanCounter].weeklyPayments;
                    }
                }
                else if (activeLoans[loanCounter].accountAim == "Bank")
                {
                    if ((activeLoans[loanCounter].paymentLeft - activeLoans[loanCounter].weeklyPayments) < 0)
                    {
                        bm.bills(activeLoans[loanCounter].paymentLeft);

                        activeLoans = removeEntry(activeLoans, activeLoans[loanCounter]);
                    }
                    else if ((activeLoans[loanCounter].paymentLeft - activeLoans[loanCounter].weeklyPayments) > 0)
                    {
                        bm.bills(activeLoans[loanCounter].weeklyPayments);

                        activeLoans[loanCounter].paymentLeft = activeLoans[loanCounter].paymentLeft - activeLoans[loanCounter].weeklyPayments;
                    }
                }
                else if (activeLoans[loanCounter].accountAim == "Offshore")
                {
                    if ((activeLoans[loanCounter].paymentLeft - activeLoans[loanCounter].weeklyPayments) < 0)
                    {
                        osm.bills(activeLoans[loanCounter].paymentLeft);

                        activeLoans = removeEntry(activeLoans, activeLoans[loanCounter]);
                    }
                    else if ((activeLoans[loanCounter].paymentLeft - activeLoans[loanCounter].weeklyPayments) > 0)
                    {
                        osm.bills(activeLoans[loanCounter].weeklyPayments);

                        activeLoans[loanCounter].paymentLeft = activeLoans[loanCounter].paymentLeft - activeLoans[loanCounter].weeklyPayments;
                    }
                }
            }

            

            timeAddWeek();

        }

        if (holdingTime.CompareTo(theTimeMonth).ToString() == "1" || holdingTime.CompareTo(theTimeMonth).ToString() == "0") // Checks for months
        {
            //Debug.Log("outputted");

            interestPayer();

            timeAddMonth();

        }
    }

    IEnumerator wait() // Runs methods every 10 seconds
    {
        while (true)
        {
            yield return new WaitForSeconds(15.0f);
            timeChecker();
            //Debug.Log("run");
        }

    }
}

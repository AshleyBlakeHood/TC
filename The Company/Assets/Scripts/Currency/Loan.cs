using UnityEngine;
using System.Collections;

public class Loan
{

    //Loan Stats
    public string loanName;
    public double loanAmount;
    public double interestRate;
    public double weeklyPayments;
    public int weekLength;
    public string accountAim;
    public float appearenceWeighting;
    public double paymentLeft; 

    public Loan(string nameS, double Amount, double Rate, int Length, string Account, float Weighting)
    {
        loanName = nameS;
        loanAmount = Amount;
        interestRate = Rate;
        weeklyPayments = loanAmount / weeklyPayments;
        weekLength = Length;
        accountAim = Account;
        appearenceWeighting = Weighting;
        paymentLeft = Amount;
    }

}

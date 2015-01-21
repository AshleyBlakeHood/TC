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
    public float appearenceWeighting;
    public double paymentLeft; 

    public Loan(string nameS, double Amount, double Rate, int Length, float Weighting)
    {
        loanName = nameS;
        loanAmount = Amount;
        interestRate = Rate;
        weeklyPayments = loanAmount / weeklyPayments;
        weekLength = Length;
        appearenceWeighting = Weighting;
        paymentLeft = Amount;
    }

}

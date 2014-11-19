using UnityEngine;
using System.Collections;

public class CashManager : MonoBehaviour {

    double currentHeld = 0;

    public double current { get { return currentHeld; } }


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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

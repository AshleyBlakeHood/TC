using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : SubjectMonobehaviour
{
	public CashManager cashManager;

    private float maxReputation = 100;
    private float minimumReputation = 0;

    private float globalReputation = 0;

	// Use this for initialization
	void Start ()
	{
		cashManager = GetComponent<CashManager> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

    /// <summary>
    /// Get the current global reputation of the player.
    /// </summary>
    /// <returns></returns>
    public float GetGlobalReputation()
    {
        return globalReputation;
    }

    /// <summary>
    /// Set the global reputation of the player without an increase or decrease.
    /// </summary>
    /// <param name="newGlobalReputation"></param>
    public void SetGlobalReputation(float newGlobalReputation)
    {
        globalReputation = newGlobalReputation;

		Notify ();
    }

    /// <summary>
    /// Increase or decrease the global reputation of the player.
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeGlobalReputation(float amount)
    {
        globalReputation += amount;

        if (globalReputation > maxReputation)
		{
            globalReputation = maxReputation;

			Debug.Log ("Reputation WIN!");
		}

        if (globalReputation < minimumReputation)
		{
            globalReputation = minimumReputation;

			Debug.Log ("Reputation LOSE");
		}

		Notify ();
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Airport : MonoBehaviour
{
    public Continent hostContinent;

    public Airport[] blacklistedContinents;

    public List<AirportDistanceObject> connectedAirports = new List<AirportDistanceObject>();

	// Use this for initialization
	void Start ()
    {
        Continent parent = transform.parent.GetComponent<Continent>();

        if (parent != null && hostContinent == null)
            hostContinent = parent;

	    foreach (Airport a in GameObject.FindObjectsOfType<Airport>())
        {
            if (!ArrayContains(a, blacklistedContinents) && a != this)
                connectedAirports.Add(new AirportDistanceObject(a, Vector3.Distance(transform.position, a.transform.position)));
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    private bool ArrayContains(Airport comparer, Airport[] continentArray)
    {
        foreach (Airport bc in continentArray)
        {
            return true;
        }

        return false;
    }

    void OnDrawGizmosSelected ()
    {
        foreach (AirportDistanceObject a in connectedAirports)
        {
            Gizmos.DrawLine(transform.position, a.airport.transform.position);
            Gizmos.color = Color.cyan;
        }
    }
}

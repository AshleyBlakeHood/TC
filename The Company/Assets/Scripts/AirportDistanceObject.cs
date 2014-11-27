using UnityEngine;
using System.Collections;

[System.Serializable]
public class AirportDistanceObject
{
    public Airport airport;
    public float distance;

	public AirportDistanceObject(Airport iAirport, float iDistance)
    {
        airport = iAirport;
        distance = iDistance;
    }
}

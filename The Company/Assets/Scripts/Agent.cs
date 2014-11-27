using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : MonoBehaviour
{
	public AgentData data;

	//Movement Stuff
	Vector3 startPos = Vector3.zero;
	float startTime = 0;
	float journeyLength = 0;

    public List<Vector2> path = new List<Vector2>();

	//Agent Usable?
	bool inTraining = false;
	bool inMission = false;

	// Use this for initialization
	void Start ()
	{
        //path.Add(new Vector2(Random.Range(-8f, 8f), Random.Range(-6f, 6f)));

		//StartMove ();
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetMouseButtonDown(1))
        {
            Airport[] airports = GameObject.FindObjectsOfType<Airport>();

            Ray findContinentRay = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z -10), Vector3.forward);
            RaycastHit2D continentHit = Physics2D.GetRayIntersection(findContinentRay);

            Ray destiantionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D destinationHit = Physics2D.GetRayIntersection(destiantionRay, Mathf.Infinity);

            if (destinationHit.collider != null && continentHit.collider != null)
            {
                if (destinationHit.collider == continentHit.collider)
                {
                    path.Clear();
                    
                    path.Add(destinationHit.point);
                    StartMove();
                }
                else
                {
                    path = PathToAirport(continentHit.transform.GetComponent<Continent>(), transform.position, GetClosestAirport(destinationHit.point, airports), airports);
                    path.Add(destinationHit.point);
                    StartMove();
                }
            }
        }

        if (path.Count > 0)
        {
            //DEBUG TESTING STUFF
            float distanceCovered = (Time.time - startTime) * 0.1f;
            float fracJourney = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(startPos, path[0], fracJourney);

            if (Vector2.Distance(transform.position, path[0]) < 0.1f)
            {
                path.RemoveAt(0);
                
                if (path.Count > 0)
                {
                    StartMove();
                }
            }

            if (path.Count > 0)
            {
                //Really bad programming, just for testing - DO NOT LEAVE IN!
                GetComponent<LineRenderer>().SetColors(new Color(1, 0.65F, 0), new Color(1, 0.65F, 0));
                GetComponent<LineRenderer>().SetPosition(0, transform.position);
                GetComponent<LineRenderer>().SetPosition(1, path[0]);
            }
        }
	}

	private void StartMove()
	{
		startPos = transform.position;

		startTime = Time.time;
        journeyLength = Vector3.Distance(startPos, path[0]);
	}

	public void SetAgentMissionStatus(bool iInMission)
	{
		inMission = iInMission;
	}

	public void SetAgentTrainingStatus(bool iInTraining)
	{
		inTraining = iInTraining;
	}

	public bool IsAgentUsable()
	{
		if (inMission || inTraining)
			return false;
		else
			return true;
	}

    public Airport GetClosestAirport(Vector3 location, Airport[] airports)
    {
        //Debug.Log(location);

        Airport closest = airports[0];

        for (int i = 0; i < airports.Length; i++)
        {
            //Debug.Log(airports[i].transform.parent.name);
            if (Vector2.Distance(location, airports[i].transform.position) < Vector2.Distance(location, closest.transform.position))
            {
                closest = airports[i];
                //Debug.Log(airports[i].transform.parent.name);
            }
        }

        return closest;
    }

    public List<Vector2> PathToAirport(Continent startContinent, Vector3 startPosition, Airport destination, Airport[] airports)
    {
        List<Vector2> output = new List<Vector2>();

        //Find closest airport.
        output.Add(GetClosestAirport(startPosition, startContinent.transform.GetComponentsInChildren<Airport>()).transform.position);

        //Find destination airport.
        for (int i = 0; i < airports.Length; i++)
        {
            if (airports[i] == destination)
            {
                output.Add(airports[i].transform.position);
                break;
            }
        }

        return output;
    }
}

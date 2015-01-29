using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// The Continent class holds the headquarters located on the continent
/// </summary>

public class Continent : MonoBehaviour
{
	//Attributes
	public List<int> hqList = new List<int>();
	//The areaID gives the continent an id. This is used in retrieving the correct continent
	public string areaID;

    private Sprite continentSprite;

    void Awake()
    {
        continentSprite = GetComponent<SpriteRenderer>().sprite;
    }

	/// <summary>
	/// Adds the ID of the headquarters to the continent class's list of headquarters. This is useful in tracking where the headquarters are located
	/// </summary>
	/// <param name="hq">Hq.</param>
	public void addAreaHQ(int hq)
	{
		hqList.Add (hq);
	}

    public Vector2 GetRandomPointOnContinent()
    {
        RaycastHit2D continentHit;
        bool hitContinent = false;

        int tries = 0;

        //Continually look for the location continent through the use of raycasts.
        while (!hitContinent)
        {
            float x = Random.Range(-continentSprite.bounds.extents.x, continentSprite.bounds.extents.x);
            float y = Random.Range(-continentSprite.bounds.extents.y, continentSprite.bounds.extents.y);

            Ray findContinentRay = new Ray(new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z - 10), Vector3.forward);
            continentHit = Physics2D.GetRayIntersection(findContinentRay);

            if (continentHit.collider != null)
            {
                if (continentHit.collider.gameObject.GetComponent<Continent>() != null)
                {
                    if (continentHit.collider.gameObject.GetComponent<Continent>() == this)
						return new Vector2(transform.position.x + x, transform.position.y + y);
                }
            }

            //tries++;

            if (tries > 50)
            {
                hitContinent = true;
            }
        }

		return transform.position;
    }
}

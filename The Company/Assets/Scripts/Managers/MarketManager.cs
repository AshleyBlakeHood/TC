using UnityEngine;
using System.Collections;

public class MarketManager : MonoBehaviour
{
    AreaCreator areaCreator;

    public GameObject marketPrefab;
    public GameObject blackMarketPrefab;

	// Use this for initialization
	void Start ()
    {
		areaCreator = FindObjectOfType<AreaCreator> ();

        for (int i = 0; i < areaCreator.areas.Length; i++)
        {
            Continent continent = areaCreator.areas[i].GetComponent<Continent>();

			Vector2 marketSpawnPosition = continent.GetRandomPointOnContinent();
			Vector2 blackMarketSpawnPosition = continent.GetRandomPointOnContinent();
            
			if (marketSpawnPosition == null || blackMarketSpawnPosition == null)
			{
				i--;
				continue;
			}

			GameObject market = Instantiate(marketPrefab, new Vector3(marketSpawnPosition.x, marketSpawnPosition.y, -0.01f), Quaternion.identity) as GameObject;
            market.name = continent.name + " Market";

			GameObject blackMarket = Instantiate(blackMarketPrefab, new Vector3(blackMarketSpawnPosition.x, blackMarketSpawnPosition.y, -0.01f), Quaternion.identity) as GameObject;
            market.name = continent.name + " Black Market";
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}

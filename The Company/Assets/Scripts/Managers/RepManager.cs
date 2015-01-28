using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class RepManager : MonoBehaviour {
	
	List<string> regionNames;
	string[,] regionWeightingTable;
	
	public List<RepData> fullRepList = new List<RepData>();
	
	private string mapName, saveName, weightingTableName;
	protected GameManager gm;

	
	/*
	
	Load all rep into the fullreplist with rep at 50 by default - done as far as default, always there data
	create method that will notify the game manager when the list is populated - done
	create method that will send new object to game manager when it is added to the list - done
	create method to look up regionWeightingTable with your name, their name and return weight -- done
	method to change individual weightings
	method to change all weightings at once
	method to remove from the replist - done
	method to notify when a object is removed from the replist - done
	method to update CSV -- done
	method to save and load repData
	*/
	
	// Use this for initialization
	void Start () {
		try
		{
			gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		}
		catch
		{
			Debug.Log("No Game Manager");
		}
        /*
		//0.75 Expert, 0.66 Hard, 0.50 Medium, 0.33 Easy, 0.20 Noob
		GetRegionWeightingsByDifficulty(0.20);
        WriteRegionWeightingsToCSV(weightingTableName);
		//PopulateRepList();
        regionWeightingTable = UpdateRegionWeightingTableFromCSV(weightingTableName);
        WriteRegionWeightingsToCSV(weightingTableName);
		GetRegionWeightingBetween("Europe", "Russia");*/
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void InitRepSystem()
    {
        regionNames = GetRegionNames();
        InitRegionWeightingTable();
        PopulateRepList();
    }
	
	void InitRegionWeightingTable()
	{
		//Populates the weighting table
		regionWeightingTable = new string[regionNames.Count+2,regionNames.Count+2];
		
		for(int x = 0; x<=regionNames.Count; x++)
		{
			for (int y = 0; y<=regionNames.Count; y++)
			{
				//Populates the table with blank strings
				regionWeightingTable[x,y] = "";
			}
		}
		
		for(int i = 1; i <= regionNames.Count; i++)
		{
			//adds names to the table
			regionWeightingTable[0,i] = regionNames[i-1].Trim(System.Environment.NewLine.ToCharArray());
			regionWeightingTable[i,0] = regionNames[i-1].Trim(System.Environment.NewLine.ToCharArray());
		}
	}
	
	public void GetRandomRegionWeightings(double difficultyMod)
	{	
		//randomly generates weightings, difficulty mod is only used to make sure there aren't all negative or all positive
		//Very rudimentary
		for(int y=1; y<=regionNames.Count; y++)
		{
			int previousWeighting = Random.Range(-10,10);
			for (int x = 1; x<=regionNames.Count; x++)
			{
				int newWeighting;
				if(previousWeighting >= difficultyMod)
				{
					//do a negative
					newWeighting = Random.Range(-10,0);
					previousWeighting = newWeighting;
					regionWeightingTable[x,y] = newWeighting.ToString();
				}
				else
				{
					//do a positive
					newWeighting = Random.Range(0,10);
					previousWeighting = newWeighting;
					regionWeightingTable[x,y] = newWeighting.ToString();
				}
				
				if(x==y)
				{
					newWeighting=0;				
					regionWeightingTable[x,y] = newWeighting.ToString();
				}
				
			}
		}
        WriteRegionWeightingsToCSV(weightingTableName);
	}
	
	public void GetRegionWeightingsByDifficulty(double difficultyMod)
	{
		//Uses the difficulty modifier as a percentage to get mostly negative or mostly positive numbers
		//0.2 would be easy, 0.9 would be impossible, quick and dirty but it works
		for(int y=1; y<=regionNames.Count; y++)
		{
			int negative = 0, positive = 0;
			double negCap = regionNames.Count*difficultyMod; 
			double posCap = regionNames.Count - (regionNames.Count*difficultyMod);
			int upperBounds = 10, lowerBounds = -10;
			for (int x = 1; x<=regionNames.Count; x++)
			{
				int newWeighting;
				if(negative >= negCap)
				{
					lowerBounds = -4;
				}
				else if(positive >= posCap)
				{
					upperBounds = 4;
				}
				
				newWeighting = Random.Range(lowerBounds,upperBounds);
				regionWeightingTable[x,y] = newWeighting.ToString();
				
				if(x==y)
				{
					newWeighting=0;				
					regionWeightingTable[x,y] = newWeighting.ToString();
				}
				if(newWeighting >= 0)
				{
					positive++;
				}
				else
				{
					negative++;	
				}
			}
		}
        WriteRegionWeightingsToCSV(weightingTableName);
	}
	
	List<string> GetRegionNames()
	{	
		//Just loads the names from the continent names CSV for the given map
		TextAsset mapData = Resources.Load(mapName) as TextAsset;
		string[] dataLines = mapData.text.Trim('\n').Split('\n');
		List<string> tempRN = new List<string>();
		
		foreach(string s in dataLines)
		{
			string[] temp = s.Split(',');
			tempRN.Add(temp[0].Trim(System.Environment.NewLine.ToCharArray()));
		}
		return tempRN;
	}
	
	void WriteRegionWeightingsToCSV(string fileName)
	{
		//Writes all the data in region weighting table to a csv that can be loaded back in at runtime
		//It is recommended to check if the file already exists at run time to prevent accidental overwriting
		StringBuilder sb = new StringBuilder();
		for(int i = 0; i < regionNames.Count+1; i++)
		{
			string[] tempStrings = new string[regionNames.Count+1];
			for(int j = 0; j< regionNames.Count+1; j++)
			{
				tempStrings[j] = regionWeightingTable[i,j];
			}
			sb.AppendLine(string.Join(",", tempStrings));
		}
		File.WriteAllText(Application.persistentDataPath + Path.DirectorySeparatorChar + fileName + ".csv", sb.ToString());
	}
	
	string[,] UpdateRegionWeightingTableFromCSV(string fileName)
	{
		//Reads in the csv data from the given filename and returns a 2d string array that could be used to refresh the weightings table
		//Or loading the data at the start of play
		string[] csvData = File.ReadAllLines(Application.persistentDataPath + Path.DirectorySeparatorChar + fileName + ".csv");
		List<string[]> splitData = new List<string[]>();
		string[,] tempTable = new string[regionNames.Count+1,regionNames.Count+1];
		int i = 0;
		foreach(string s in csvData)
		{
			splitData.Add(csvData[i].Split(','));
			i++;
		}
		for(int y=0; y < regionNames.Count+1; y++)
		{
			
			for (int x=0; x< regionNames.Count+1; x++)
			{
				
				tempTable[x,y] = splitData[x][y];
			}
		}
		
		return tempTable;
	}
	
	string GetRegionWeightingBetween(string you, string them)
	{
		//Gets the x and y positions of you and them and then gets the region weighting based upon that
        string tempWeighting = "";
        int[] positions = GetWeightingXY(you, them);
        tempWeighting = regionWeightingTable[positions[0], positions[1]];
        return tempWeighting;
	}

    void SetRegionWeightingBetween(string you, string them, int amount)
    {
        //Gets the x and y positions of you and them and then gets the region weighting based upon that
		string tempWeighting = "";
        int[] positions = GetWeightingXY(you, them);
		tempWeighting = regionWeightingTable[positions[0],positions[1]];
        tempWeighting += amount;
        regionWeightingTable[positions[0], positions[1]] = tempWeighting;
        WriteRegionWeightingsToCSV(weightingTableName);
    }

    int[] GetWeightingXY(string you, string them)
    {
        int[] temp = new int[2];
        for (int i = 0; i < regionNames.Count + 1; i++)
        {
            if (regionWeightingTable[i, 0] == them)
            {
                temp[0] = i;
            }
        }

        for (int i = 0; i < regionNames.Count + 1; i++)
        {
            if (regionWeightingTable[0, i] == you)
            {
                temp[1] = i;
            }
        }
        return temp;
    }
	
	public void PopulateRepList()
	{
		//Loads the full rep list with all the rep data objects to be sent to the game manager for use in player class
		foreach(string s in regionNames)
		{
			RepData temp = new RepData();
			temp.setRep(50);
			temp.setName(s);
			fullRepList.Add(temp);
		}
		
		foreach(PlayerData p in gm.allPlayers)
		{
			RepData temp = new RepData();
			temp.setRep(50);
			temp.setName(p.playerName);
			fullRepList.Add(temp);
		}
		
		RepData bmRep = new RepData();
		bmRep.setRep(0);
		bmRep.setName("Black Market");
		fullRepList.Add(bmRep);
	}
	
	public void RemoveRepObjectByName(string name)
	{
		//Searches for the object with the given name and removes it from the internal replist then sends it to gm to be removed from the players replists
		RepData temp = new RepData();
		foreach(RepData r in fullRepList)
		{
			if(r.getName() == name)
			temp = r;
		}
		fullRepList.Remove(temp);
	}
	
	public void AddNewObjectToList(RepData newRep)
	{
		//Adds a new rep object to the list and then sends that object to the gm to be added to the players rep lists
		Debug.Log(newRep.getName());
		fullRepList.Add(newRep);
	}

    public List<string> GetRegions()
    {
        return regionNames;
    }

    public void SetSaveNames(string save, string map)
    {
        saveName = save;
        mapName = map;
        weightingTableName = saveName + mapName + "WeightingTable"; 
    }
}

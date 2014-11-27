using UnityEngine;
using System.Collections;

public class OfficeManager : MonoBehaviour {
	public TimeManager tiemMen;
	public OfficesData office;

	private int wasteMod = 21;
	private int coffeeMod = 42;
	private int printMod = 441;
	private int deskMod = 1050;
	private int matliniMod = 147;


	// Use this for initialization
	void Start () {
		office.waste = 0;
		office.coffeeCups = 0;
		office.lostAgents = 0; 
		office.printerInk = 0; 
		office.deskPops = 0;
		office.matliniShaken = 0;

	}
	
	// Update is called once per frame
	void Update () {
		updateStats ();
	}
	public void updateStats(){
		if (rollChance (wasteMod) == 1) {
			office.waste++;
		}
		if (rollChance (coffeeMod) == 1) {
			office.coffeeCups++;
		}
		if (rollChance (printMod) == 1) {
			office.printerInk++;
		}
		if (rollChance (deskMod) == 1) {
			office.deskPops++;
		}
		if (rollChance (matliniMod) == 1) {
			office.matliniShaken++;
		}
	}

	public int rollChance(int mod){
		int intTime = (int) Time.timeScale;
		int adjusted = mod / intTime;
		return Random.Range (0, adjusted);
	}

}

//public int waste;
//public int coffeeCups; 
//public int lostAgents; 
//public int printerInk; 
//public int deskPops;
//public int matliniShaken;

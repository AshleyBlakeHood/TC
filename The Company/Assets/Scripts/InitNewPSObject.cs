using UnityEngine;
using System.Collections;

public class InitNewPSObject : MonoBehaviour {

	public string name;
	public int rep;
	// Use this for initialization
	void Start () {
		RepData temp = new RepData();
		temp.setName(name);
		temp.setRep(rep);
		GameObject.Find("RepManager").GetComponent<RepManager>().AddNewObjectToList(temp);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

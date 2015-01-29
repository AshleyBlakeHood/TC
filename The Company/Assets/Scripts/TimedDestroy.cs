using UnityEngine;
using System.Collections;

public class TimedDestroy : MonoBehaviour
{
	public float secondsToLive = 1f;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (StartDestroy());
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	IEnumerator StartDestroy()
	{
		yield return new WaitForSeconds(secondsToLive);
		Destroy (gameObject);
	}
}

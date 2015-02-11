using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class SubjectMonobehaviour : MonoBehaviour
{
	private List<ObserverMonoBehaviour> observers = new List<ObserverMonoBehaviour>();

	public void Attach(ObserverMonoBehaviour observer)
	{
		observers.Add (observer);
	}

	public void Detach(ObserverMonoBehaviour observer)
	{
		observers.Remove (observer);
	}

	public void Notify()
	{
		foreach (ObserverMonoBehaviour o in observers)
		{
			o.UpdateObserver();
		}
	}
}

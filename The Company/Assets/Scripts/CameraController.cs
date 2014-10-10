using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float maxZoom = -8.25f;
	public float minZoom = -0.5f;

	bool movingCamera = false;
	Vector3 previousMousePos = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Get the amount the player has turned the scroll wheel and add it to the current z value of the camera.
		float z = camera.transform.position.z + (Input.GetAxis ("Mouse ScrollWheel") * 2.5f);

		//If over limit, set to limit.
		if (z > minZoom)
			z = minZoom;
		else if (z < maxZoom)
			z = maxZoom;

		//Apply the new zoom level.
		camera.transform.position = new Vector3 (camera.transform.position.x, camera.transform.position.y, z);

		//Check if the mouse position has changed and if the camera should be moving.
		if (Input.mousePosition != previousMousePos && movingCamera)
		{
			Vector3 camPos = camera.transform.position;

			//Use the difference between the mouse positions to move the camera.
			camera.transform.position = new Vector3(camPos.x - ((Input.mousePosition.x - previousMousePos.x) / 100), camPos.y - ((Input.mousePosition.y - previousMousePos.y) / 100), camPos.z);
			previousMousePos = Input.mousePosition;
		}

		if (Input.GetMouseButtonDown (2))
		{
			//On first press set moving camera to true and update the last position.
			movingCamera = true;
			previousMousePos = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp (2))
			movingCamera = false;
	}
}
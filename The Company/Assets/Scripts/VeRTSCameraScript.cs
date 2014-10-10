using UnityEngine;
using System.Collections;

public class VeRTSCameraScript : MonoBehaviour {
	
	
	public float cameraFOV, moveSpeed, scrollSpeed, maxZoom, minZoom;
	public GameObject camera;
	
	Quaternion rotSet;
	Vector3 posSet;
	float mouseXPos, rotateSpeed;
	// Use this for initialization
	void Start () {	
		camera.transform.LookAt(this.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		inputHandler();
		setFOV();
	}
	
	void inputHandler()
	{
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			moveRight();
		}
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			moveLeft();
		}
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			moveUp();
		}
		if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			moveDown();
		}
		if(Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			zoomIn();
		}
		if(Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			zoomOut();
		}
		if(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.Mouse2))
		{
			if(Input.mousePosition.y < 150)
			{
				lowerCamera();
			}
			if(Input.mousePosition.y > Screen.height - 150)
			{
				raiseCamera();
			}
			if(Input.mousePosition.x < 150)
			{
				orbitLeft();
			}
			if(Input.mousePosition.x > Screen.width - 150)
			{
				orbitRight();
			}
		}
	}
	
	public void moveRight()
	{
		Debug.Log("UNITY SUCKS DONKEY BALLS");
		this.transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
		//posSet.x += Time.deltaTime * moveSpeed;
	}
	
	public void moveLeft()
	{
		this.transform.Translate(Vector3.left * (Time.deltaTime * moveSpeed));
		//posSet.x -= moveSpeed;
	}
	
	public void moveUp()
	{
		this.transform.Translate(Vector3.forward * (Time.deltaTime * moveSpeed));
	}
	
	public void moveDown()
	{
		this.transform.Translate(Vector3.back * (Time.deltaTime * moveSpeed));
	}
	
	public void raiseCamera()
	{
		if(camera.transform.rotation.eulerAngles.x < 75)
		camera.transform.Translate(Vector3.up * (Time.deltaTime * moveSpeed));
		
		camera.transform.LookAt(this.transform.position);
	}
	
	public void lowerCamera()
	{
		if(camera.transform.position.y > 0.5)
		camera.transform.Translate(Vector3.down * (Time.deltaTime * moveSpeed));
		
		camera.transform.LookAt(this.transform.position);
	}
	
	public void zoomIn()
	{
		if(camera.transform.position.y > maxZoom)
		camera.transform.Translate(Vector3.forward * (Time.deltaTime * scrollSpeed));
	}
	
	public void zoomOut()
	{
		if(camera.transform.position.y < minZoom)
		camera.transform.Translate(Vector3.back * (Time.deltaTime * scrollSpeed));
	}
	
	public float setRotSpeed()
	{
		float rotSpeed;
		float screenCenter = Screen.width/2;
		
		rotSpeed = (screenCenter - mouseXPos) * 0.01f;
		
		return rotSpeed;
	}
	
	public void orbitRight()
	{
		mouseXPos = Input.mousePosition.x;
		rotateSpeed = setRotSpeed();
		if (mouseXPos > (Screen.width/2 + 50))
		{
			Quaternion blehbleh = new Quaternion();
			Vector3 bleh = new Vector3(0, transform.rotation.eulerAngles.y + rotateSpeed, 0);
			blehbleh.eulerAngles = bleh;
			this.transform.rotation = blehbleh;
		}
	}
	
	public void orbitLeft()
	{
		mouseXPos = Input.mousePosition.x;
		rotateSpeed = setRotSpeed();
		if (mouseXPos < (Screen.width/2 - 50))
		{
			Quaternion blehbleh = new Quaternion();
			Vector3 bleh = new Vector3(0, transform.rotation.eulerAngles.y + rotateSpeed, 0);
			blehbleh.eulerAngles = bleh;
			this.transform.rotation = blehbleh;
		}	
	}
	
	public void setFOV()
	{
		camera.camera.fieldOfView = cameraFOV;
	}
}

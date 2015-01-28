using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// The Base Spawner class will take an game object passed to it, instantiate the object at the location of the mouse cursor and follow the mouse until the player 
/// chooses a location. Once the player pressing the left mouse button down, the object will be left at the last location of the cursor. The class also contains checking
/// to see if the location is valid.
/// </summary>
public class BaseSpawner : MonoBehaviour {
	//Attributes
	//Instances of other classes
	public GameObject HQObject;
	public GameObject SHObject;
	public SafehouseData houseData;
	public Continent cont;

	//Holds a game object while it is being dragged around the screen
	private GameObject draggingHQ;
	//Once a HQ is placed, it will be placed in this list for future reference
	private List<GameObject> headquarters;

	//Holds the locations of the mouse in a 2D and 3D vector
	private Transform hqSpawner;
	private Vector3 mouseToWorldPos = Vector3.zero;
	private Vector2 mouseToWorldPos2D = Vector2.zero;
	private Collider ownCollider;

	private bool hasPlaced;
	private bool hqPlaced;
	private bool dragging = false;
	//Increment for an unique ID for each HQ
	private int baseNumber;


	void Start () {
		houseData = new SafehouseData ();
		headquarters = new List<GameObject>();
		cont = new Continent ();
		ownCollider = transform.root.gameObject.GetComponent<Collider> ();
		//Creates and obtains the current instances of other classes
	}


	void Update ()
	{
		//If the J key is pressed down, it will instantiate a safehouse 
		if (Input.GetKeyDown (KeyCode.J))
		{
			if (dragging != true)
			{
				SetItem(SHObject);
				dragging = true;
			}
		}

		//If the H key is pressed down, it will instaniate a head quarters 
		if (Input.GetKeyDown (KeyCode.H))
		{
			if (dragging != true)
			{
				SetItem(HQObject);
				dragging = true;
			}
		}
		//These two if statements set the dragging to true which means the game object is instantiated and following the mouse 
		if (dragging)
		{
			//Gets the co-ordinates of the mouse location
			Vector3 m = Input.mousePosition;
			m = new Vector3(m.x,m.y,-Camera.main.transform.position.z);
			Vector3 p = Camera.main.ScreenToWorldPoint(m);
			mouseToWorldPos = new Vector3(p.x,p.y,0);
			mouseToWorldPos2D = new Vector2(p.x,p.y);
			//Sets the game object's co-ordinates to the co-ordinates of the mouse cursor
			draggingHQ.transform.position = mouseToWorldPos;
			draggingHQ.collider2D.enabled = false;
			//disables the game object's collider so the raycasts which check for a continent will not hit the game object first.
			if (Input.GetMouseButton(0)){
				//Once the player has chosen a location
				//Masks the layer so only the continents are able to be hit by a raycast. This will ensure a headquarters is placed upon land
				int layerMask = 1 << LayerMask.NameToLayer("Continents");
				RaycastHit2D hit = Physics2D.Raycast(mouseToWorldPos2D,mouseToWorldPos2D, 0, layerMask);
				if (hit.collider != null) {
					//The raycast has hit land so now cast another ray to check if another headquarters, market etc is located in that position
					RaycastHit2D checkCollison = Physics2D.Raycast(mouseToWorldPos2D,mouseToWorldPos2D);
					if (checkCollison.collider.name == hit.collider.name)
					{
<<<<<<< HEAD
						if (hit.collider.GetComponent<Continent>().deadZone == false)
						{
							Debug.Log("Hit object: " + hit.collider.gameObject.name);
							Debug.Log ("Meow");
							dragging = false;
							baseNumber++;
							draggingHQ.GetComponent<SafehouseData>().id = baseNumber;
							hit.collider.gameObject.GetComponent<Continent>().addAreaHQ(baseNumber);
							headquarters.Add (draggingHQ);
							draggingHQ.collider2D.enabled = true;
                            draggingHQ.transform.position = new Vector3(draggingHQ.transform.position.x, draggingHQ.transform.position.y, -0.01f);
						}
						else{
							Debug.Log("DeadZone hit");
						}
=======
						//If the names of the two game objects hit by the raycasts equal the same when the space underneath the game object is clear 
						Debug.Log("Hit object: " + hit.collider.gameObject.name);
						dragging = false;
						baseNumber++;
						draggingHQ.GetComponent<SafehouseData>().id = baseNumber;
						//Gives the game object an identifier
						hit.collider.gameObject.GetComponent<Continent>().addAreaHQ(baseNumber);
						headquarters.Add (draggingHQ);
						//Adds the gameobject to a list
						draggingHQ.collider2D.enabled = true;
						//Turns the collider back on 
>>>>>>> origin/Petes-Twig
					}
				}
				else
				{
					Debug.Log("Miss");
				}
			}
		}
	}

	/// <summary>
	/// SetItem will instantiate the game object into the game at the location of the mouse cursor
	/// </summary>
	/// <param name="b">The blue component.</param>
	public void SetItem(GameObject b)
	{
		draggingHQ = Instantiate (b, mouseToWorldPos, Quaternion.identity) as GameObject;
		//The Game Object has not been placed so it will follow the cursor until the user clicks again
		hasPlaced = false;
	}

}
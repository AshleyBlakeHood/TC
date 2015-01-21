using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseSpawner : MonoBehaviour {
	public GameObject HQObject;
	public GameObject SHObject;
	public SafehouseData houseData;
	public Continent cont;
	//Create a dragging prefab too. Otherwwise when a HQ comes into existence as drag it will start doing things. From there we can do the placement checks too.
	private GameObject draggingHQ;
	private List<GameObject> headquarters;

	private Transform hqSpawner;
	private Vector3 mouseToWorldPos = Vector3.zero;
	private Vector2 mouseToWorldPos2D = Vector2.zero;
	private Collider ownCollider;

	private bool hasPlaced;
	private bool hqPlaced;
	private bool dragging = false;
	private int baseNumber;
	// Use this for initialization
	void Start () {
		houseData = new SafehouseData ();
		headquarters = new List<GameObject>();
		cont = new Continent ();
		ownCollider = transform.root.gameObject.GetComponent<Collider> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.J))
		{
			if (dragging != true)
			{
				SetItem(SHObject);
				dragging = true;
			}
		}
		//Give things name not letters!
		if (Input.GetKeyDown (KeyCode.H))
		{
			if (dragging != true)
			{
				SetItem(HQObject);
				dragging = true;
			}
		}
		if (dragging)
		{
			Vector3 m = Input.mousePosition;
			m = new Vector3(m.x,m.y,-Camera.main.transform.position.z);
			Vector3 p = Camera.main.ScreenToWorldPoint(m);
			mouseToWorldPos = new Vector3(p.x,p.y,0);
			mouseToWorldPos2D = new Vector2(p.x,p.y);
			draggingHQ.transform.position = mouseToWorldPos;
			draggingHQ.collider2D.enabled = false;
			if (Input.GetMouseButton(0)){
				int layerMask = 1 << LayerMask.NameToLayer("Continents");
				Debug.Log(layerMask);
				RaycastHit2D hit = Physics2D.Raycast(mouseToWorldPos2D,mouseToWorldPos2D, 0, layerMask);
				if (hit.collider != null) {
					RaycastHit2D checkCollison = Physics2D.Raycast(mouseToWorldPos2D,mouseToWorldPos2D);
					if (checkCollison.collider.name == hit.collider.name)
					{
						Debug.Log("Hit object: " + hit.collider.gameObject.name);
						Debug.Log ("Meow");
						dragging = false;
						baseNumber++;
						draggingHQ.GetComponent<SafehouseData>().id = baseNumber;
						hit.collider.gameObject.GetComponent<Continent>().addAreaHQ(baseNumber);
						headquarters.Add (draggingHQ);
						draggingHQ.collider2D.enabled = true;
					}
				}
				else
				{
					Debug.Log("Miss");
				}
			}
		}
	}

	public void SetItem(GameObject b)
	{
		draggingHQ = Instantiate (b, mouseToWorldPos, Quaternion.identity) as GameObject;

		hasPlaced = false;
		//hqSpawner = ((GameObject)Instantiate (b)).transform;
	}

}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScrollableList : MonoBehaviour
{
    public GameObject itemPrefab;
    //public int itemCount = 10, columnCount = 1;

	public Scrollbar[] scrollbarsToSetToZero;
	public GameObject[] elements;

    void Start()
    {
    }

	public void CreateList(int items, int columns)
	{
		//Set element storage to size of items and columns.
		elements = new GameObject[items * columns];

		RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform>();
		RectTransform containerRectTransform = gameObject.GetComponent<RectTransform>();
		
		//calculate the width and height of each child item.
		float width = containerRectTransform.rect.width / columns;
		float ratio = width / rowRectTransform.rect.width;
		float height = rowRectTransform.rect.height * ratio;
		int rowCount = items / columns;
		if (items % rowCount > 0)
			rowCount++;
		
		//adjust the height of the container so that it will just barely fit all its children
		float scrollHeight = height * rowCount;
		containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
		containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);
		
		int j = 0;
		for (int i = 0; i < items; i++)
		{
			//this is used instead of a double for loop because itemCount may not fit perfectly into the rows/columns
			if (i % columns == 0)
				j++;
			
			//create a new item, name it, and set the parent
			GameObject newItem = Instantiate(itemPrefab) as GameObject;
			newItem.name = gameObject.name + " Item at (" + i + "," + j + ")";
			newItem.transform.parent = gameObject.transform;

			//Add new item to list of elements.
			elements[i] = newItem;

			//move and size the new item
			RectTransform rectTransform = newItem.GetComponent<RectTransform>();
			
			float x = -containerRectTransform.rect.width / 2 + width * (i % columns);
			float y = containerRectTransform.rect.height / 2 - height * j;
			rectTransform.offsetMin = new Vector2(x, y);
			
			x = rectTransform.offsetMin.x + width;
			y = rectTransform.offsetMin.y + height;
			rectTransform.offsetMax = new Vector2(x, y);
		}
		
		//Sets all included scrollbars to zero instead of to 50%.
		foreach (Scrollbar scrollbar in scrollbarsToSetToZero)
		{
			scrollbar.value = 1;
		}
	}

	public void ClearList()
	{
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			Destroy (gameObject.transform.GetChild (i).gameObject);
		}
//		while (gameObject.transform.childCount > 0)
//		{
//			Destroy (gameObject.transform.GetChild (0).gameObject);
//		}
	}
}

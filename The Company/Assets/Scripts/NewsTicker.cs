using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NewsTicker : MonoBehaviour
{
	private List<NewsTickerItem> newsItems = new List<NewsTickerItem>();
	
	public GameObject newstickerCanvas;
	private Text ticker;

	public float speed = 30;

	// Use this for initialization
	void Start ()
	{
		ticker = newstickerCanvas.GetComponentInChildren<Text> ();

		newsItems.Add (new NewsTickerItem ("1/2 Life III Project Cancelled", true));
		newsItems.Add (new NewsTickerItem ("News Headline 2", true));
		newsItems.Add (new NewsTickerItem ("Everything is Awesome", true));

		ticker.text = newsItems[0].headline;
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void FixedUpdate ()
	{
		ticker.transform.position = new Vector2 (ticker.transform.position.x - (speed * Time.deltaTime), ticker.transform.position.y);

		if (ticker.rectTransform.position.x * 2 < -ticker.rectTransform.rect.width)
		{
			//Update Item
			ticker.rectTransform.position = new Vector2 (ticker.rectTransform.rect.width * 1.5f, ticker.transform.position.y);

			int item = Random.Range (0, newsItems.Count);
			ticker.text = newsItems[item].headline;

			if (!newsItems[item].repeat)
				newsItems.RemoveAt (newsItems[item]);
		}
	}


}

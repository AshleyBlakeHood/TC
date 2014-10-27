using UnityEngine;
using System.Collections;

public class NewsTickerItem
{
	public string headline = "Unitialised Headline";
	public int timesToShow = 1;
	public bool repeat = false;

	public NewsTickerItem(string iHeadline, bool iRepeat)
	{
		headline = iHeadline;
		repeat = iRepeat;
	}
}

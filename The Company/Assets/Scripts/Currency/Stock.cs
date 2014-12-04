using UnityEngine;
using System.Collections;

public class Stock 
{

	//Stock Stats
    public string stockName;
    public double stockPrice;
    public int stockAmount;

    public Stock(string name, double price, int amount)
    {
        stockName = name;
        stockPrice = price;
        stockAmount = amount;
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StockManager : MonoBehaviour {

    public TextAsset loadList;

    public Stock[] allStock;
    public List<Stock> stockList = new List<Stock>();
    public List<Stock> personalList = new List<Stock>();

    BankManager bm;

	// Use this for initialization
	void Start () {

        bm = GameObject.Find("Bank Manager").GetComponent<BankManager>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void readInData()
    {
        //Split by lines and trim empty lines
        string[] lines = loadList.text.Trim(System.Environment.NewLine.ToCharArray()).Split(System.Environment.NewLine.ToCharArray());

        //Initialise loan array
        allStock = new Stock[lines.Length - 1];

        //In case modders move around or we do this will get the indexs where the columns are in the .csv
        string[] indexFinder = lines[0].Split(',');

        int stockName = -1;
        int stockPrice = -1;
        int stockAmount = -1;

        //Scan through columns to find the respective indexs.
        for (int i = 0; i < indexFinder.Length; i++)
        {
            switch (indexFinder[i])
            {
                case "Name":
                    stockName = i;
                    break;
                case "Price":
                    stockPrice = i;
                    break;
                case "Amount":
                    stockAmount = i;
                    break;
            }
        }

        //Pull out the data using the parsed indexes.
        for (int i = 1; i < lines.Length; i++)
        {
            string[] ls = lines[i].Split(',');

            if (ls.Length < 3)
                continue;

            allStock[i - 1] = new Stock(ls[stockName], double.Parse(ls[stockPrice]), int.Parse(ls[stockAmount]));

            stockList.Add(allStock[i - 1]);

        }
    }

    public int getOwnedStock(string stock)
    {
        // Cycles through players stock list checking each against passed string
        foreach (Stock holder in personalList)
        {
            if (holder.stockName == stock)
            {
                return holder.stockAmount;
            }
        }

        return (0);

    }

    void buyStocks(string stock, int amount)
    {
        Stock holder = null, holderPersonal = null;
        int stockIndex = -1;

        // Searches all stock list for passed stock name
        // If found places stock in temp string to get price at that time
        // Saves index of stock for later saving to all stock list
        for (int i = 0; i < allStock.Length; i++)
        {
            if (allStock[i].stockName == stock)
            {
                holder = allStock[i];
                stockIndex = i;
            }
        }

        // Makes sure the stock passed is real
        if (holder != null)
        {
            // Makes sure the stock asked for has enough stock to sell
            if (allStock[stockIndex].stockAmount > amount)
            {
                // Passes cost to bank manager to subtract from bank account
                bm.bills(amount * holder.stockPrice);

                bool found = false;

                // Checks if player already owns stock of that name
                // Either adding stock amount to players existing stock
                // Or making a new item in list for the stock name
                for (int i = 0; i < personalList.Count; i++)
                {
                    if (stock == personalList[i].stockName)
                    {
                        personalList[i].stockAmount =+ amount;

                        allStock[stockIndex].stockAmount = -amount;

                        found = true;
                    }
                }

                if (found == false)
                {
                    holderPersonal = new Stock(stock,0,amount);

                    personalList.Add(holderPersonal);

                    allStock[stockIndex].stockAmount = -amount;
                }
            }
        }
        else
        {
            Debug.LogError("No stock found");
        }


    }

    void sellingStock(string stock, int amount)
    {
        Stock holder = null;
        int stockIndex = -1, personalIndex = -1;
        bool found = false;

        // Searches all stock list for passed stock name
        // If found places stock in temp string to get price at that time
        // Saves index of stock for later saving to all stock list
        for (int i = 0; i < allStock.Length; i++)
        {
            if (allStock[i].stockName == stock)
            {
                holder = allStock[i];
                stockIndex = i;
            }
        }

        // Checks to make sure player actually owns stock with the passed name
        for (int i = 0; i < personalList.Count; i++)
        {
            if (stock == personalList[i].stockName)
            {
                found = true;
                personalIndex = i;
            }
        }

        // Subtracts stock from players list
        if (found == true)
        {
            personalList[personalIndex].stockAmount -= amount;

            bm.investment(allStock[stockIndex].stockPrice * amount);
        }
        else
        {
            Debug.Log("No Stock found");
        }

    }


}

using UnityEngine;
using System.Collections;

public class SelectionEngine
{
	public static string GetItem(ItemWeightHolder[] holder)
	{
		float sum = 0;

		//Get Sum from Weights
		foreach (ItemWeightHolder i in holder)
		{
			sum += i.weight;
		}

		//Variable to exit while loop.
		bool valOut = false;

		//Amount to pass while adding the weights of items together.
		float requiredAmount = Random.Range (0, sum);

		//Ongoing total.
		float total = 0;

		//While the value hasn't been passed, continue.
		while (valOut == false)
		{
			//Loop through all items in order.
			for (int i = 0; i < holder.Length; i++)
			{
				//Add the current weight to total.
				total += holder[i].weight;

				//If the total is greater than the required amount, return.
				if (total > requiredAmount)
				{
					//Set value to exit while loop and return the item.
					valOut = true;
					return holder[i].item;
				}
			}
		}

		//Fallback - Should never be executed.
		return "";
	}
}

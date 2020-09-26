using UnityEngine;
using System;

public class DataTranslator : MonoBehaviour {

	private static string HEALTH_SYMBOL = "[HEALTH]";
	private static string COINS_SYMBOL = "[COINS]";

	public static string ValuesToData (int health, int coins)
	{
		return HEALTH_SYMBOL + health + "/" + COINS_SYMBOL + coins;
	}

	public static int DataToHealth (string data)
	{
		return int.Parse (DataToValue(data, HEALTH_SYMBOL));
    }

	public static int DataToCoins (string data)
	{
		return int.Parse(DataToValue(data, COINS_SYMBOL));
	}

	private static string DataToValue (string data, string symbol)
	{
		string[] pieces = data.Split('/');
		foreach (string piece in pieces)
		{
			if (piece.StartsWith(symbol))
			{
				return piece.Substring(symbol.Length);
			}
		}

		Debug.LogError(symbol + " not found in " + data);
		return "";
	}

}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public Text health;
	public Text coinsCount;

	// Use this for initialization
	void Start () {
		if (UserAccountManager.IsLoggedIn)
			UserAccountManager.instance.GetData(OnReceivedData);
	}

	void OnReceivedData (string data)
	{
		if (health == null || coinsCount == null)
			return;

		health.text = DataTranslator.DataToHealth(data).ToString() + " HEALTH";
		coinsCount.text = DataTranslator.DataToCoins(data).ToString() + " COINS";
	}
	
}
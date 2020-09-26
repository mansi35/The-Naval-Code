using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class PlayerScore : MonoBehaviour {
	int lastHealth = 100;
	int lastCoins = 0;
	
	Player player;

	void Start ()
	{
		player = GetComponent<Player>();
		StartCoroutine(SyncScoreLoop());
	}

	void OnDestroy ()
	{
		if (player != null)
			SyncNow();
	}

	IEnumerator SyncScoreLoop ()
	{
		while (true)
		{
			yield return new WaitForSeconds(5f);

			SyncNow();
		}
	}

	void SyncNow ()
	{
		if (UserAccountManager.IsLoggedIn)
		{
			UserAccountManager.instance.GetData(OnDataRecieved);
		}
	}

	void OnDataRecieved(string data)
	{
		if (player.currentHealth >= lastHealth && player.currentCoins <= lastCoins)
			return;

		// int healthSinceLast = lastHealth - player.currentHealth;
		// int coinsSinceLast = player.currentCoins - lastCoins;

		// int myhealth = DataTranslator.DataToHealth(data);
		// int mycoins = DataTranslator.DataToCoins(data);

		// int newHealth = myhealth - healthSinceLast;
        int newHealth = player.currentHealth;
		int newCoins = player.currentCoins;
        
		string newData = DataTranslator.ValuesToData(newHealth, newCoins);
		Debug.Log("Syncing: " + newData);

		lastHealth = player.currentHealth;
		lastCoins = player.currentCoins;

		UserAccountManager.instance.SendData(newData);
	}

}
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreboardItem : MonoBehaviour {

	[SerializeField]
	Text usernameText;

	[SerializeField]
	Text healthText;

	[SerializeField]
	Text coinsText;

	public void Setup (string username, int health, int coins)
	{
		usernameText.text = username;
		healthText.text = "Health: " + health;
		coinsText.text = "Coins: " + coins;
	}

}

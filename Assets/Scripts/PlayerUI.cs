using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	[SerializeField]
	RectTransform healthBarFill;

	[SerializeField]
	GameObject pauseMenu;

	[SerializeField]
	GameObject questionPanel;

	[SerializeField]
	Text coinText;

	// [SerializeField]
	// GameObject manager;

	[SerializeField]
	GameObject scoreboard;

	private Player player;
	private PlayerController controller;

	public void SetPlayer (Player _player)
	{
		player = _player;
		controller = player.GetComponent<PlayerController>();
	}

	void Start ()
	{
		PauseGame.IsOn = false;
	}

	void Update ()
	{
		SetHealthAmount(player.GetHealthPct());
		SetCoinsAmount(player.currentCoins);

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TogglePauseMenu();
		}

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			scoreboard.SetActive(true);
        } else if (Input.GetKeyUp(KeyCode.Tab))
		{
			scoreboard.SetActive(false);
        }
	}

	public void TogglePauseMenu ()
	{
		pauseMenu.SetActive(!pauseMenu.activeSelf);
		PauseGame.IsOn = pauseMenu.activeSelf;
    }

	public void ActivateQuestion ()
	{
		questionPanel.SetActive(true);
	}

	void SetHealthAmount (float _amount)
	{
		healthBarFill.localScale = new Vector3(1f, _amount, 1f);
	}

	void SetCoinsAmount (int _amount)
	{
		coinText.text = _amount.ToString();
	}

}
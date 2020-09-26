using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class display : MonoBehaviour {

	public Text[] highscoreFields;
	scoreboard highscoresManager;

	void Start() {
		for (int i = 0; i < highscoreFields.Length; i ++) {
			highscoreFields[i].text = i+1 + ". Fetching...";
		}

				
		highscoresManager = GetComponent<scoreboard>();
		StartCoroutine("RefreshHighscores");
	}
	
    public void OnHighscoresDownloaded(Highscore[] highscoresList) {
		for (int i = 0; i < highscoreFields.Length; i ++) {
			highscoreFields[i].text = i+1 + ". ";
			if (i < highscoresList.Length) {
				highscoreFields[i].text += highscoresList[i].username + " - " + highscoresList[i].score;
			}
		}
	}
	
	IEnumerator RefreshHighscores() {
		while (true) {
			highscoresManager.DownloadHighscores();
			yield return new WaitForSeconds(30);
		}
	}
}
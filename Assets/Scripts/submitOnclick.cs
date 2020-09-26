using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class submitOnclick : MonoBehaviour
{
  public Button btnClick;
  public GameObject Question;
  public InputField inputUser;

  public QuestionManager game;
  public bool quesStatus;

  private Player player;

  public void SetPlayerPrefab (Player _player)
	{
		player = _player;
	}

  // private void Start()
  // {
  //   btnClick.onClick.AddListener(GetInputOnClickHandler);
  // }

  public void GetInputOnClickHandler()
  {
    //Debug.Log(GetType(inputUser.text.ToString()));

    string myans =inputUser.text.ToString();

    string correctAns =  game.correctAns;
    checkAns( myans, correctAns);
    Question.SetActive(false);

  }   

  public void checkAns( string myans , string correctAns)
  {
    if( myans != correctAns)
    {
      quesStatus = false;
      player.GetComponent<Player>().CmdSetDamage(10);
    }
    else
    {
      quesStatus = true;
      player.GetComponent<Player>().CmdSetCoins(10);
    }
  }

  // [Command]
  // void CmdTakeDamage (int _amount)
  // {
  //   player.GetComponent<Player>().RpcTakeDamage(_amount);
  // }
}

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

  private void Start()
  {
    btnClick.onClick.AddListener(GetInputOnClickHandler);
  }

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
      Debug.Log("False");
    }
    else
    {
      Debug.Log("True");
    }
  }
}

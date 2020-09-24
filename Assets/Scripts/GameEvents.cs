using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName  = "Quiz/new Game Events")]

public class GameEvents : ScriptableObject
{
   public delegate void UpdateQuestionUICallback(Question question);
   public UpdateQuestionUICallback UpdateQuestionUI;
}

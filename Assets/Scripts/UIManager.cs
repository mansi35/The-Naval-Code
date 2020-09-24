using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

[Serializable]
public struct UIElements {
    [SerializeField] TextMeshProUGUI questionInfoTextObject;
    public TextMeshProUGUI QuestionInfoTextObject { get { return questionInfoTextObject;}}

    


}

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameEvents events;

    [SerializeField] UIElements uIElements;

    void OnEnable () {
        events.UpdateQuestionUI += UpdateQuestionUI;
    }
    void OnDisable () {
        events.UpdateQuestionUI -= UpdateQuestionUI;
    }

    void UpdateQuestionUI (Question question) {
        uIElements.QuestionInfoTextObject.text = question.Info;
        

    }

}

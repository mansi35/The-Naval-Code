using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    Question[] _questions = null;
    public Question[] Questions { get { return _questions;} }

    [SerializeField] GameEvents events = null;

    private List<int> FinishedQuestions = new List<int>();
    private int currentQuestion = 0;

    public int color;
    public string  correctAns;

    void Start() 
    {
        LoadQuestions();
        foreach (var question in Questions) {
            Debug.Log(question.Info);
        }
        Display();
    }

    public void Display() 
    {
        var question = GetRandomQuestion();
        correctAns = question.Ans;

        if(events.UpdateQuestionUI != null) {
           events.UpdateQuestionUI(question);
        }
        else {
           Debug.LogWarning("oops something went wrong.try again");
        }  
    }

    Question GetRandomQuestion () 
    {
        var randomIndex  = GetRandomQuestionIndex();
        currentQuestion = randomIndex;
        return Questions[currentQuestion];
    }

    public int GetRandomQuestionIndex() 
    {
        var random = 0;
        if (FinishedQuestions.Count < Questions.Length) {
            do{
               random = UnityEngine.Random.Range(0, Questions.Length);

            }while (FinishedQuestions.Contains(random) );
           
        }
        return random;
    }

    public void LoadQuestions() 
    {
        if(color == 0) {
            Object[] objs = Resources.LoadAll("hard", typeof(Question));
            _questions = new Question[objs.Length];
            for(int i  = 0; i < objs.Length; i++) {
                _questions[i]  = (Question)objs[i];
            }
        }
        else if (color == 2) {
         
            Object[] objs = Resources.LoadAll("easy", typeof(Question));
            _questions = new Question[objs.Length];
        
        
            for(int i  = 0; i < objs.Length; i++) {
                _questions[i]  = (Question)objs[i];
            }
        }
        else if (color == 1) {
            
            Object[] objs = Resources.LoadAll("medium", typeof(Question));
            _questions = new Question[objs.Length];
            
            
            for(int i  = 0; i < objs.Length; i++) {
                _questions[i]  = (Question)objs[i];   
            }
        }
        else {
            Debug.LogWarning("opps something went wrong.try again");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[CreateAssetMenu(menuName  = "Quiz/new Question")]

public class Question : ScriptableObject {
    [SerializeField] private string _info = string.Empty;
    public string Info { get { return _info;} }

    [SerializeField] private string _ans = string.Empty;
    public string Ans { get { return _ans;} }
}


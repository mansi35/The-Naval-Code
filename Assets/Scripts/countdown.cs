using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdown : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private float mainTimer;
    [SerializeField] private GameObject Question;
    private float timer;
    private bool canCount = true;
    private bool doOnce = false;

    void OnEnable()
    {
        timer = mainTimer;
    }

    void Update()
    {
        if (timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            uiText.text = timer.ToString("F");
        }

        if (timer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            uiText.text = "0.00";
            timer = 0.0f;
            Question.SetActive(false);
        }
    }
}

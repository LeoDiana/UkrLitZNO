using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour {

    public AutorQuiz autorQ;
    public CompositionQuiz compositionQ;
    public Settings settings;

    public Text text;
    public Text WhatToDoText;
    public Button[] buttons;

    public Slider slider;
    public Button comp;
    float delay;

    public int rightAns;

    int QuestionDistribution()
    {
        int rez = Random.Range(1, 13);

        if (settings.EnableQuestions[rez])
            return rez;
        else
            return QuestionDistribution();
    }

    string ShowWhatToDo(int q)
    {
        switch(q)
        {
            case 1: return "Справжнє ім'я";
            case 2: return "Напрям";
            case 3: return "Угрупування";
            case 4: return "Про кого\nце висловлення?";
            case 5: return "Автор твору";
            case 6: return "Напрям твору";
            case 7: return "Жанр твору";
            case 8: return "Який твір присвячено:";
            case 9: return "Місце подій";
            case 10: return "Встановіть відповідність\nміж персонажем і твором";
            case 11: return "Встановіть відповідність\nміж персонажами і твором";
            case 12: return "Встановіть відповідність\nміж персонажами одного твору";
            default: return "";
        }
    }

    void NewQuestion()
    {
        comp.enabled = false;
        comp.GetComponentInChildren<Text>().text = "";

        if(settings.EnabledMarks>0)
        {
            int q = QuestionDistribution();
            WhatToDoText.text = ShowWhatToDo(q);

            if (q <= 4)
                autorQ.NewQuestion(q);
            else
                compositionQ.NewQuestion(q);
        }
        else
        {
            WhatToDoText.text = "";
            text.text = "Виберіть бажаний тип завдань в налаштуваннях";
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].GetComponentInChildren<Text>().text = "";
            return;
        }
    }

    public void ButtonClick(int id)
    {
        settings.themeCon.ButtonsColor(rightAns, id);

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(delay);
        NewQuestion();
    }

    public void sliderVal()
    {
        int v = Mathf.CeilToInt(slider.value);
        switch (v)
        {
            case 0: { delay = 0.25f; break; }
            case 1: { delay = 0.5f; break; }
            case 2: { delay = 0.75f; break; }
            case 3: { delay = 1.0f; break; }
            case 4: { delay = 1.25f; break; }
            case 5: { delay = 1.5f; break; }
            case 6: { delay = 2.0f; break; }
            case 7: { delay = 2.5f; break; }
            case 8: { delay = 3.0f; break; }
        }
    }

    void Start()
    {
        sliderVal();
        NewQuestion();    
    }
}

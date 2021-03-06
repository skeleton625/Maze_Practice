﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuUI, FinishUI;
    [SerializeField]
    private GameObject TimerUI;
    [SerializeField]
    private InputField Row, Column;
    [SerializeField]
    private Text Minute, Seconds;
    [SerializeField]
    private Text FinMin, FinSec;
    [SerializeField]
    private FinishCollider FinishPoint;

    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }

    public int GetInputFieldRow()
    {
        int _row;
        if (int.TryParse(Row.text, out _row))
            return _row;
        else
            return 0;
    }

    public int GetInputFieldCol()
    {
        int _col;
        if (int.TryParse(Row.text, out _col))
            return _col;
        else
            return 0;
    }

    public IEnumerator StartTimerCoroutine()
    {
        int min = 0;
        double sec = 0;

        FinishPoint = GameObject.Find("FinishPoint").GetComponent<FinishCollider>();

        MenuUI.SetActive(false);
        TimerUI.SetActive(true);

        while(true)
        {
            sec += Time.deltaTime;
            if(sec >= 60)
            {
                sec = 0;
                ++min;
                Minute.text = min.ToString();
            }

            if (FinishPoint.IsFinish)
                break;

            Seconds.text = string.Format("{0:0}", sec);
            yield return null;
        }

        FinMin.text = Minute.text;
        FinSec.text = Seconds.text;
        FinishUI.SetActive(true);
    }
}

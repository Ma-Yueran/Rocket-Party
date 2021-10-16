using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float startTime = 300;
    private float time;
    [SerializeField]
    private Text timerText;

    private void Start()
    {
        time = startTime;
    }

    private void Update()
    {
        if (time <= 0)
        {
            time = 0;
            return;
        }

        time -= Time.deltaTime;

        timerText.text = Mathf.CeilToInt(time) + " Secs Left!";
    }
}

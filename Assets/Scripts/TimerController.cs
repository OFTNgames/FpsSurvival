using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerController : MonoBehaviour
{
    public static TimerController instance;
    public GameObject gameOverPanel;
    public Text timeCounter;

    private TimeSpan timePlaying;
    private bool timerGoing;
    private float elapsedTime;

    private void Awake()
    {
        instance = this;
    }
          
    void Start()
    {
        timeCounter.text = "Time: 00:00.00";
        timerGoing = false;
    }


    //called from elsewhere, i.e. another script, should be game manager  
    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;
        Debug.Log("Timer Start");

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
        string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss':'ff");
        gameOverPanel.transform.Find("FinalTime").GetComponent<Text>().text = timePlayingStr;

    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss':'ff");
            timeCounter.text = timePlayingStr;

            yield return null;

        }
    }
}

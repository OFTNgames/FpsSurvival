using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillcounterController : MonoBehaviour
{
    public static KillcounterController instance;
    public GameObject gameOverPanel;
    public Text killCounter;
    private int numberkilled;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        string killCountStr = "Score: " + numberkilled.ToString("0000");
        killCounter.text = killCountStr;
        numberkilled = 0;

    }

    //Call from elsewhere

    //use
    //KillcounterController.instance.IncreaseKillCount();
    public void IncreaseKillCount()
    {
        numberkilled +=1;
        string killCountStr = "Score: " + numberkilled.ToString("0000");
        killCounter.text = killCountStr;
    }
    
    public void PrintEndGameScore()
    {
        string killCountStr = "Score: " + numberkilled.ToString("0000");
        gameOverPanel.transform.Find("FinalKillCount").GetComponent<Text>().text = killCountStr;
    }
}

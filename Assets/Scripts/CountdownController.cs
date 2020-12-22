using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;

    private void Start()
    {
        StartCoroutine(CountdowntoStart()); 
    }

    IEnumerator CountdowntoStart()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--; 
        }

        countdownDisplay.text = "START!";
        GameManager.instance.BeginGame();

        yield return new WaitForSeconds(1f);
        countdownDisplay.gameObject.SetActive(false);
        
    }

}

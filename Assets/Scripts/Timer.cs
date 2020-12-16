using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{

    public Text timerText;
    public float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 10f;
    }

    void SwapColor(float roundTime)
    {
        if (MatchHandler.state == MatchState.FP_TURN)
        {
            timerText.color = Color.red;
        }
        else if (MatchHandler.state == MatchState.SP_TURN)
        {
            timerText.color = Color.blue;
        }
        currentTime = roundTime;
    }


    // Update is called once per frame
    void Update()
    {
        if (MatchHandler.state == MatchState.FP_TURN || MatchHandler.state == MatchState.SP_TURN)
        {
            currentTime -= Time.deltaTime;
            timerText.text = Mathf.CeilToInt(currentTime).ToString();
        }
    }
}

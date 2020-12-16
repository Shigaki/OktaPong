using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsPanel : MonoBehaviour
{

    public Text winnerText;
    public Text scoreText;

    public void SetWinner(int p1score, int p2score)
    {
        if (p1score > p2score)
        {
            winnerText.color = Color.red;
            scoreText.color = Color.red;
            winnerText.text = "Player1 Wins";
        }
        else
        {
            winnerText.color = Color.blue;
            scoreText.color = Color.blue;
            winnerText.text = "Player2 Wins";
        }
        scoreText.text = p1score.ToString() + " : " + p2score.ToString();
    }
}

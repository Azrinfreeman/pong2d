using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class ScoreP : MonoBehaviour
{
    public int playerInt;

    public int score;

    public int currentScore;

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        if (playerInt == 0)
        {
            score = PlayerPrefs.GetInt("scorePlayer1");
        }
        else
        {
            score = PlayerPrefs.GetInt("scorePlayer2");
        }
        text = transform
            .GetChild(0)
            .transform.GetChild(0)
            .transform.GetChild(0)
            .transform.GetComponent<TextMeshProUGUI>();

        SetText();
    }

    public void SetTotal()
    {
        score += currentScore;
    }

    public void SetText()
    {
        text.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update() { }
}

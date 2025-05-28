using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class ScoreP : MonoBehaviour
{
    public int playerInt;

    public int score;

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

    public void SetText()
    {
        text.text = score.ToString();
    }

    // Update is called once per frame
    void Update() { }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinnerController : MonoBehaviour
{
    public int player1;
    public int player2;

    public TextMeshProUGUI winnerNameText;
    public Image flagPlayer;

    public AudioSource yay;

    // Start is called before the first frame update
    void Start()
    {
        player1 = Managers.Score.playerScore;
        player2 = Managers.Score.aiScore;

        if (player1 > player2)
        {
            winnerNameText.text = "Player 1";
            flagPlayer.sprite = GameObject
                .Find("FlagP1")
                .transform.GetChild(0)
                .transform.GetChild(0)
                .GetComponent<Image>()
                .sprite;
        }
        else if (player2 > player1)
        {
            winnerNameText.text = "Player 2";
            flagPlayer.sprite = GameObject
                .Find("FlagP2")
                .transform.GetChild(0)
                .transform.GetChild(0)
                .GetComponent<Image>()
                .sprite;
        }
        else if (player2 == player1)
        {
            winnerNameText.text = "It's a draw";
            flagPlayer.GetComponent<Transform>().gameObject.SetActive(false);
        }

        if (!yay.isPlaying)
        {
            yay.Play();
        }
    }

    // Update is called once per frame
    void Update() { }
}

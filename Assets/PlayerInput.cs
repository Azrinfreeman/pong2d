using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public int playerInt;
    public Transform PlayerNameBtn;
    public TextMeshProUGUI textNameDisplay;

    public InputField textName;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Player2").GetComponent<Paddle>().owner == PaddleOwner.AI)
        {
            GameObject
                .Find("PlayerName2")
                .transform.GetChild(0)
                .GetComponent<TextMeshProUGUI>()
                .text = "Ali Bot";
        }
    }

    // Update is called once per frame
    void Update() { }

    public void EnterName()
    {
        if (!textName.text.Equals(""))
        {
            PlayerPrefs.SetString("player" + (playerInt + 1), textName.text);
            textNameDisplay.text = textName.text;
            PlayerNameBtn.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            transform.gameObject.SetActive(false);

            if (playerInt == 0)
            {
                Managers.UI.inGameUI.isReadyPlayerName1 = true;
                //disable the score if the player is an ai
                if (GameObject.Find("Player2").GetComponent<Paddle>().owner == PaddleOwner.AI)
                {
                    GameObject.Find("ScoreP2").transform.gameObject.SetActive(false);
                }
            }
            else if (playerInt == 1)
            {
                Managers.UI.inGameUI.isReadyPlayerName2 = true;
            }

            if (Managers.UI.inGameUI.isReadyPlayerName1 && Managers.UI.inGameUI.isReadyPlayerName2)
            {
                Managers.UI.inGameUI.levelChoices.transform.gameObject.SetActive(true);
                Managers.UI.inGameUI.PlayerInputBtn1.transform.gameObject.SetActive(false);
                Managers.UI.inGameUI.PlayerInputBtn2.transform.gameObject.SetActive(false);
            }
        }
    }
}

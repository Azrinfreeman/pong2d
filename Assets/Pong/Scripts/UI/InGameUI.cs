﻿//  /*********************************************************************************
//   *********************************************************************************
//   *********************************************************************************
//   * Produced by Skard Games										                  *
//   * Facebook: https://goo.gl/5YSrKw											      *
//   * Contact me: https://goo.gl/y5awt4								              *
//   * Developed by Cavit Baturalp Gürdin: https://tr.linkedin.com/in/baturalpgurdin *
//   *********************************************************************************
//   *********************************************************************************
//   *********************************************************************************/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Text info;
    public Text score;
    public Button gameBackButton;

    [HideInInspector]
    public Color infoInitColor;

    [HideInInspector]
    public Color scoreInitColor;

    public Text textPlayer;

    public Text textAi;

    void Start()
    {
        infoInitColor = info.color;
        scoreInitColor = score.color;
    }

    public void UpdateScore()
    {
        score.text = Managers.Score.playerScore + "-" + Managers.Score.aiScore;
        textPlayer.text = Managers.Score.aiScore.ToString();
        textAi.text = Managers.Score.playerScore.ToString();
    }

    public void GameInfo(string txt)
    {
        info.text = txt;
    }

    public void GameBackButtonClicked()
    {
        Managers.Audio.PlayClickSound();
        Managers.UI.ActivateUI(Menus.MAIN);
        Managers.Game.SetState(typeof(MenuState));
        Managers.Match.SaveMatch();
        Managers.Match.PlayButton.GetComponent<Transform>().gameObject.SetActive(false);
    }

    public void SetInfoText(string text, bool isEnabled)
    {
        Managers.UI.inGameUI.info.enabled = isEnabled;
        Managers.UI.inGameUI.info.text = text;

        if (!isEnabled)
            info.color = infoInitColor;
    }
}

//  /*********************************************************************************
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
    public Transform PlayerInputBtn1;
    public Transform PlayerInputBtn2;
    public Text info;
    public Text score;
    public Button gameBackButton;

    [HideInInspector]
    public Color infoInitColor;

    [HideInInspector]
    public Color scoreInitColor;

    public Text textPlayer;

    public Text textAi;
    public Button PlayButton;
    public Transform levelChoices;

    public Transform playerPanel;

    public Transform questionPanel;
    public Transform winnerPanel;

    public Transform stopButton;
    public bool firstTimePlay;
    public bool isReadyPlayerName1;
    public bool isReadyPlayerName2;

    void Start()
    {
        infoInitColor = info.color;
        scoreInitColor = score.color;
    }

    public void UpdateScore()
    {
        score.text = Managers.Score.aiScore + "-" + Managers.Score.playerScore;
        textPlayer.text = Managers.Score.playerScore.ToString();
        textAi.text = Managers.Score.aiScore.ToString();
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
        PlayButton.GetComponent<Transform>().gameObject.SetActive(false);
        Managers.UI.inGameUI.PlayButton.GetComponent<Transform>().gameObject.SetActive(false);
        Managers.UI.inGameUI.playerPanel.gameObject.SetActive(false);
    }

    public void SetInfoText(string text, bool isEnabled)
    {
        Managers.UI.inGameUI.info.enabled = isEnabled;
        Managers.UI.inGameUI.info.text = text;

        if (!isEnabled)
            info.color = infoInitColor;
    }
}

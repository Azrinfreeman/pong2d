using System.Collections;
using System.Text.RegularExpressions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class KickOffState : _StatesBase
{
    private int countdown;
    private Vector2 _ballVelocity;

    #region implemented abstract members of _StatesBase

    public override void OnActivate()
    {
        Debug.Log("<color=green>KickOff State</color> OnActive");
        Managers.Game.isGameActive = true;

        countdown = 4;
        Managers.Input.isActive = false;
        Managers.UI.inGameUI.gameBackButton.gameObject.SetActive(true);

        if (!Managers.Game.isGameActive)
            Managers.Match.Reset();
        else
        {
            _ballVelocity = Managers.Match.ball.ballBody.linearVelocity;
            Managers.Match.ball.ballBody.linearVelocity = Vector2.zero;
        }

        Managers.UI.inGameUI.playerPanel.gameObject.SetActive(true);
        if (Managers.Match.isAiMatch)
        {
            Managers.UI.inGameUI.PlayerInputBtn2.gameObject.SetActive(false);
        }

        Managers.UI.inGameUI.PlayButton.GetComponent<Transform>().gameObject.SetActive(true);
        Managers.UI.inGameUI.PlayButton.GetComponent<Button>().onClick.RemoveAllListeners();
        Managers
            .UI.inGameUI.PlayButton.GetComponent<Button>()
            .onClick.AddListener(() => CountDown());

        Managers.UI.inGameUI.PlayButton.GetComponent<Transform>().gameObject.SetActive(false);

        if (Managers.UI.inGameUI.firstTimePlay)
        {
            Managers.UI.inGameUI.PlayButton.GetComponent<Transform>().gameObject.SetActive(true);
        }
        //level choices
        if (!Managers.UI.inGameUI.firstTimePlay)
        {
            Managers.UI.inGameUI.firstTimePlay = true;
            if (Managers.UI.inGameUI.isReadyPlayerName1 && Managers.UI.inGameUI.isReadyPlayerName2)
            {
                Managers.UI.inGameUI.levelChoices.transform.gameObject.SetActive(true);
            }
            else
            {
                Managers.UI.inGameUI.levelChoices.transform.gameObject.SetActive(false);
            }
        }

        if (Managers.Score.totalRounds > 5)
        {
            Managers.UI.inGameUI.stopButton.gameObject.SetActive(true);
        }
    }

    public override void OnDeactivate()
    {
        Debug.Log("<color=red>KickOff State</color> OnDeactivate");
    }

    public override void OnUpdate()
    {
        //   Debug.Log("<color=yellow>KickOff State</color> OnUpdate");
    }

    #endregion


    public void CountDown()
    {
        Managers.UI.inGameUI.PlayButton.gameObject.SetActive(false);
        Managers.UI.inGameUI.info.enabled = true;
        Managers.UI.inGameUI.info.color = Managers.UI.inGameUI.infoInitColor;
        Color initColor = Managers.UI.inGameUI.infoInitColor;
        Managers.UI.inGameUI.score.enabled = false;
        DOTween
            .To(
                () => initColor,
                x => Managers.UI.inGameUI.info.color = x,
                new Color(initColor.r, initColor.g, initColor.b, 0),
                1f
            )
            .SetLoops(4)
            .OnStepComplete(() =>
            {
                if (countdown > 1)
                {
                    GameObject countdownAudio = GameObject.Find("countdown");
                    if (countdownAudio != null && !countdownAudio.GetComponent<AudioSource>().isPlaying)
                    {
                        countdownAudio.GetComponent<AudioSource>().Play();
                    }
                }
                else
                {
                    GameObject goalAudio = GameObject.Find("goal");
                    if (goalAudio != null && !goalAudio.GetComponent<AudioSource>().isPlaying)
                    {
                        goalAudio.GetComponent<AudioSource>().Play();
                    }
                }
                countdown--;

                Managers.Audio.PlayCollisionSound();
                Managers.UI.inGameUI.SetInfoText(countdown.ToString(), true);
            })
            .OnComplete(() =>
            {
                Managers.UI.inGameUI.SetInfoText("", false);
                KickOff();
                Managers.Audio.PlayClickSound();
                Managers.UI.inGameUI.score.enabled = true;
                Managers.PowUps.canSpawnPowerup = true;
                Managers.Match.ball.ballBody.linearVelocity = _ballVelocity;
                StartCoroutine(Managers.PowUps.SpawnPowerup());
            });
    }

    public void KickOff()
    {
        Managers.UI.inGameUI.info.enabled = false;
        Managers.UI.inGameUI.gameBackButton.gameObject.SetActive(true);
        Managers.Input.isActive = true;
        Managers.Match.ball.KickOffBall();
        Managers.Game.SetState(typeof(GamePlayState));
    }
}

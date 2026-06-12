//  /*********************************************************************************
//   *********************************************************************************
//   *********************************************************************************
//   * Produced by Skard Games										                 *
//   * Facebook: https://goo.gl/5YSrKw											     *
//   * Contact me: https://goo.gl/y5awt4								             *
//   * Developed by Cavit Baturalp Gürdin: https://tr.linkedin.com/in/baturalpgurdin *
//   *********************************************************************************
//   *********************************************************************************
//   *********************************************************************************/

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : PersistentSingleton<MainMenu>
{
    public GameObject menuButtons;
    public GameObject settingsMenu;
    public GameObject credits;
    public GameObject showAdsRewarded;
    public GameObject showAdsDefault;
    public GameObject continueButton;
    public TextMeshProUGUI pongLogoText;

    void Start()
    {
        PlayerPrefs.SetInt("Input", 1);
        Managers.Input.inputType = (InputMethod)PlayerPrefs.GetInt("Input");
        Managers.Audio.PlayClickSound();
    }

    void OnEnable()
    {
        pongLogoText.enabled = true;
        menuButtons.SetActive(true);
    }

    void OnDisable()
    {
        pongLogoText.enabled = false;
        menuButtons.SetActive(false);
    }

    public void Continue()
    {
        Managers.Audio.PlayClickSound();
        Managers.Match.RetrieveSavedMatch();
        Managers.Game.SetState(typeof(KickOffState));
        Managers.UI.ActivateUI(Menus.INGAME);
    }

    public void NewGame()
    {
        StartCoroutine(loadNewGame());
    }

    IEnumerator loadNewGame()
    {
        GetComponent<Animator>().Play("NewGameClick");
        yield return new WaitForSeconds(2f);

        Managers.Match.isAiMatch = false;
        Managers.Audio.PlayClickSound();
        Managers.Match.ResetSavedGame();
        GameObject.Find("intro").GetComponent<AudioSource>().Stop();

        Managers.Game.SetState(typeof(KickOffState));
        Managers.UI.ActivateUI(Menus.INGAME);
        GameObject.Find("UI").GetComponent<Animator>().Play("InGameStart");
    }

    IEnumerator loadNewAiGame()
    {
        GetComponent<Animator>().Play("NewGameClick");
        yield return new WaitForSeconds(2f);

        Managers.Match.isAiMatch = true;
        Managers.Audio.PlayClickSound();
        Managers.Match.ResetSavedGame();
        Managers.Audio.StopGameMusic();
        GameObject.Find("intro").GetComponent<AudioSource>().Stop();
        Managers.UI.inGameUI.isReadyPlayerName2 = true;
        PlayerPrefs.SetString("player2", "Ali Bot");
        //adjust speed

        GameObject.Find("Player2").GetComponent<Paddle>().speed = Constants.PADDLE_SPEED_FOR_AI;
        ;
        //adjust flag button to not able to click

        Managers.UI.inGameUI.PlayerInputBtn2.gameObject.SetActive(false);
        
        PlayerInput p2Input = Managers.UI.inGameUI.PlayerInputBtn2.GetComponent<PlayerInput>();
        if (p2Input != null && p2Input.PlayerNameBtn != null)
        {
            p2Input.PlayerNameBtn.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            p2Input.textNameDisplay.text = "Ali Bot";
        }

        GameObject.Find("Player2").GetComponent<Paddle>().owner = PaddleOwner.AI;
        Managers.Game.SetState(typeof(KickOffState));
        Managers.UI.ActivateUI(Menus.INGAME);
        GameObject.Find("UI").GetComponent<Animator>().Play("InGameStartAi");
    }

    public void NewGameAI()
    {
        StartCoroutine(loadNewAiGame());
    }

    public void Settings()
    {
        Managers.Audio.PlayClickSound();
        DisableMenuButtons();
        settingsMenu.SetActive(true);
    }

    public void Credits()
    {
        Managers.Audio.PlayClickSound();
        DisableMenuButtons();
        credits.SetActive(true);
    }

    public void DisableMenuButtons()
    {
        menuButtons.SetActive(false);
    }

    IEnumerator quit()
    {
        transform.GetComponent<Animator>().Play("exitMainMenu");
        yield return new WaitForSeconds(1.2f);

        Application.Quit();
    }

    public void QuitApp()
    {
        StartCoroutine(quit());
    }
}

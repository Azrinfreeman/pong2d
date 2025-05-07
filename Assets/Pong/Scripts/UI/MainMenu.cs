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
        Managers.Audio.PlayClickSound();
        Managers.Match.ResetSavedGame();
        Managers.Game.SetState(typeof(KickOffState));
        Managers.UI.ActivateUI(Menus.INGAME);
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

    public void QuitApp()
    {
        Application.Quit();
    }
}

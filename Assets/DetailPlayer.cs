using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailPlayer : MonoBehaviour
{
    public TransformController2 transformController;
    public int no;
    public TextMeshProUGUI noText;
    public TextMeshProUGUI names;
    public Button button;
    public Transform playerDelete;

    private int tempno;

    // Start is called before the first frame update
    void Start()
    {
        transformController = transform.parent.GetComponent<TransformController2>();
        if (!transformController.isRank)
        {
            playerDelete = transform
                .parent.transform.parent.transform.parent.transform.parent.transform.GetChild(2)
                .transform;
        }
        no = transform.GetSiblingIndex();
        noText = transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        names = transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        button = transform.GetChild(0).transform.GetChild(2).GetComponent<Button>();

        if (!transformController.isRank)
        {
            Invoke("ApplyAgain", 0.2f);
        }
    }

    private void OnEnable()
    {
        no = transform.GetSiblingIndex();
        noText = transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        names = transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        button = transform.GetChild(0).transform.GetChild(2).GetComponent<Button>();
        //ApplyAgain();
    }

    public void ApplyAgain()
    {
        button.onClick.RemoveAllListeners();
        button.transform.gameObject.SetActive(true);
        //Debug.Log());
        names.text = PlayerPrefs.GetString(
            transformController.userCollection[transform.GetSiblingIndex()]
        );

        if (PlayerPrefs.GetInt("PlayerTotal") > 1)
        {
            if (
                PlayerPrefs.GetString(
                    transformController.userCollection[transform.GetSiblingIndex()]
                ) == PlayerPrefs.GetString("CurrentPlayer_")
            )
            {
                if (!transformController.isPlay)
                {
                    transform.GetChild(0).GetComponent<Image>().color = new Color32(
                        144,
                        0,
                        255,
                        255
                    );
                    button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "DELETE";
                    button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color =
                        Color.yellow;
                    button.onClick.AddListener(() => DisplayDelete());
                }
                else
                {
                    transform.GetChild(0).GetComponent<Image>().color = new Color32(
                        144,
                        0,
                        255,
                        255
                    );
                    button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PILIH";
                    button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color =
                        Color.yellow;

                    button.onClick.AddListener(() => PilihPlayer());
                    if (PlayerNameController.instance.names[0].text.Equals(names.text))
                    {
                        button.gameObject.SetActive(false);
                        Debug.Log("sama");
                    }

                    if (PlayerNameController.instance.names[1].text.Equals(names.text))
                    {
                        button.gameObject.SetActive(false);
                        Debug.Log("sama");
                    }
                }
            }
            else
            {
                if (!transformController.isPlay)
                {
                    transform.GetChild(0).GetComponent<Image>().color = new Color32(
                        144,
                        0,
                        255,
                        255
                    );
                    button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "DELETE";
                    button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color =
                        Color.yellow;
                    button.onClick.AddListener(() => DisplayDelete());
                }
                else
                {
                    transform.GetChild(0).GetComponent<Image>().color = new Color32(
                        144,
                        0,
                        255,
                        255
                    );
                    button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PILIH";
                    button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color =
                        Color.yellow;

                    button.onClick.AddListener(() => PilihPlayer());
                    if (PlayerNameController.instance.names[0].text.Equals(names.text))
                    {
                        button.gameObject.SetActive(false);

                        Debug.Log("sama");
                    }

                    if (PlayerNameController.instance.names[1].text.Equals(names.text))
                    {
                        button.gameObject.SetActive(false);
                        Debug.Log("sama");
                    }
                }
                /*
                button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "TUKAR";
                button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
                button.onClick.AddListener(() => TukarPlayer());
                */
            }
        }
        else
        {
            string tempNom = transformController.userCollection[0];
            string nom = tempNom.Substring(tempNom.Length - 1);
            if (Int32.Parse(nom) == PlayerPrefs.GetInt("CurrentPlayerNo_"))
            {
                button.transform.gameObject.SetActive(false);
                //button.onClick.AddListener(() => DisplayDelete());
            }
        }
    }

    public void PilihPlayer()
    {
        PlayerPrefs.SetString(
            "player" + (transformController.playerInput.playerInt + 1),
            names.text
        );
        transformController.playerInput.textNameDisplay.text = names.text;
        PlayerNameController.instance.names[transformController.playerInput.playerInt].text =
            names.text;
        /*PlayerNameBtn.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);*/

        if (transformController.playerInput.playerInt == 0)
        {
            Managers.UI.inGameUI.isReadyPlayerName1 = true;
            //disable the score if the player is an ai
            if (GameObject.Find("Player2").GetComponent<Paddle>().owner == PaddleOwner.AI)
            {
                GameObject.Find("ScoreP2").transform.gameObject.SetActive(false);
            }
        }
        else if (transformController.playerInput.playerInt == 1)
        {
            Managers.UI.inGameUI.isReadyPlayerName2 = true;
        }

        if (Managers.UI.inGameUI.isReadyPlayerName1 && Managers.UI.inGameUI.isReadyPlayerName2)
        {
            Managers.UI.inGameUI.levelChoices.transform.gameObject.SetActive(true);
            Managers.UI.inGameUI.PlayerInputBtn1.transform.gameObject.SetActive(false);
            Managers.UI.inGameUI.PlayerInputBtn2.transform.gameObject.SetActive(false);
        }

        if (transformController.playerInput.playerInt == 0)
        {
            if (
                PlayerNameController
                    .instance.transformController[1]
                    .GetComponent<TransformController2>()
                    .playerInput.gameObject.activeSelf
            )
            {
                PlayerNameController
                    .instance.transformController[1]
                    .GetComponent<TransformController2>()
                    .RefreshItem();
            }
            Debug.Log("refresh item0000");
            transformController.ClearChildrenAndDismiss();
        }
        else
        {
            if (
                PlayerNameController
                    .instance.transformController[0]
                    .GetComponent<TransformController2>()
                    .playerInput.gameObject.activeSelf
            )
            {
                PlayerNameController
                    .instance.transformController[0]
                    .GetComponent<TransformController2>()
                    .RefreshItem();
            }

            Debug.Log("refresh item111");
            transformController.ClearChildrenAndDismiss();
        }

        //transformController.playerInput.transform.gameObject.SetActive(false);
    }

    public void DisplayDelete()
    {
        playerDelete.gameObject.SetActive(true);
    }

    public void TukarPlayer()
    {
        /*
        GameObject.Find("canvs").GetComponent<GameStartupController>().ApplyScoreAgain();
        string tempNom = transformController.userCollection[transform.GetSiblingIndex()];
        string nom = tempNom.Substring(tempNom.Length - 1);
        PlayerPrefs.SetString("CurrentPlayer_", PlayerPrefs.GetString(tempNom));
        PlayerPrefs.SetInt("CurrentPlayerNo_", Int32.Parse(nom));
        PlayerPrefs.SetString(
            "CurrentPlayerid_",
            PlayerPrefs.GetString(transformController.userIdCollection[transform.GetSiblingIndex()])
        );

        //CurrentPlayerName.instance.ApplyName();
        transformController.DismissPlayerSelect();
        */
    }

    // Update is called once per frame
    void Update() { }
}

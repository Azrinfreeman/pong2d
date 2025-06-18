using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinnerController : MonoBehaviour
{
    public int player1;
    public int player2;

    public int noPlayerP1;
    public int noPlayerP2;

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
            winnerNameText.text = "Player 1: " + PlayerPrefs.GetString("player1");
            flagPlayer.sprite = GameObject
                .Find("FlagP1")
                .transform.GetChild(0)
                .transform.GetChild(0)
                .GetComponent<Image>()
                .sprite;
        }
        else if (player2 > player1)
        {
            winnerNameText.text = "Player 2: " + PlayerPrefs.GetString("player2");
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

    public void submitWinner()
    {
        StartCoroutine(submitWin());
    }

    IEnumerator submitWin()
    {
        if (GameObject.Find("Player2").GetComponent<Paddle>().owner != PaddleOwner.AI)
        {
            string str = "";
            //disable the score if the player is an ai

            //choose which score is higher
            if (player1 > player2)
            {
                //for first player
                for (int i = 0; i < 100; i++)
                {
                    if (PlayerPrefs.GetString("Player_" + i) == PlayerPrefs.GetString("player1"))
                    {
                        str = PlayerPrefs.GetString("Player_" + i);
                        noPlayerP1 = i;
                        player1 += PlayerPrefs.GetInt("PlayerScore_" + noPlayerP1);
                        PlayerPrefs.SetInt("PlayerScore_" + noPlayerP1, player1);
                    }
                }

                //for second player
                for (int i = 0; i < 100; i++)
                {
                    if (PlayerPrefs.GetString("Player_" + i) == PlayerPrefs.GetString("player2"))
                    {
                        str = PlayerPrefs.GetString("Player_" + i);
                        noPlayerP2 = i;
                        player2 += PlayerPrefs.GetInt("PlayerScore_" + noPlayerP2);
                        PlayerPrefs.SetInt("PlayerScore_" + noPlayerP2, player2);
                    }
                }
            }
            else
            {
                //for second player
                for (int i = 0; i < 100; i++)
                {
                    if (PlayerPrefs.GetString("Player_" + i) == PlayerPrefs.GetString("player2"))
                    {
                        str = PlayerPrefs.GetString("Player_" + i);
                        noPlayerP2 = i;
                        player2 += PlayerPrefs.GetInt("PlayerScore_" + noPlayerP2);
                        PlayerPrefs.SetInt("PlayerScore_" + noPlayerP2, player2);
                    }
                }
                //for first player
                for (int i = 0; i < 100; i++)
                {
                    if (PlayerPrefs.GetString("Player_" + i) == PlayerPrefs.GetString("player1"))
                    {
                        str = PlayerPrefs.GetString("Player_" + i);
                        noPlayerP1 = i;
                        player1 += PlayerPrefs.GetInt("PlayerScore_" + noPlayerP1);
                        PlayerPrefs.SetInt("PlayerScore_" + noPlayerP1, player1);
                    }
                }
            }
            Debug.Log(noPlayerP1);
            Debug.Log(player1);
            //pick the name and put the score in the local storage

            //insert the score in database with latest score data

            if (!string.IsNullOrEmpty(str))
            {
                WWWForm form = new WWWForm();

                form.AddField("PlayerName", PlayerPrefs.GetString("Player_" + noPlayerP1));
                form.AddField("Playerid", PlayerPrefs.GetString("Playerid_" + noPlayerP1));

                form.AddField("totalStars", PlayerPrefs.GetInt("PlayerScore_" + noPlayerP1));
                form.AddField(
                    "totalRounds",
                    PlayerPrefs.GetInt(
                        "CURRENT_TROPHY_PLAYER_" + PlayerPrefs.GetInt("CurrentPlayerNo_")
                    )
                );
                form.AddField("deviceName", SystemInfo.deviceName);

                using (
                    UnityWebRequest www = UnityWebRequest.Post(
                        "https://app-hanana.com/pong2d/insertmark.php",
                        form
                    )
                )
                {
                    yield return www.SendWebRequest();
                    if (
                        www.result == UnityWebRequest.Result.ConnectionError
                        || www.result == UnityWebRequest.Result.ProtocolError
                    )
                    {
                        Debug.Log(www.error);
                        Debug.Log("error server");
                    }
                    else
                    {
                        string retrieveData = www.downloadHandler.text.Trim();
                        if (retrieveData.Equals("Insert Successful"))
                        {
                            Debug.Log(retrieveData);
                            Debug.Log("First time user");
                        }
                        else if (retrieveData.Equals("Update Successful"))
                        {
                            Debug.Log(retrieveData);
                            Debug.Log("Existing user");
                        }
                        else
                        {
                            Debug.Log("Something else");
                            Debug.Log(retrieveData);
                        }
                    }
                }

                //// second player
                ///
                ///
                ///
                WWWForm form2 = new WWWForm();

                form2.AddField("PlayerName", PlayerPrefs.GetString("Player_" + noPlayerP2));
                form2.AddField("Playerid", PlayerPrefs.GetString("Playerid_" + noPlayerP2));

                form2.AddField("totalStars", PlayerPrefs.GetInt("PlayerScore_" + noPlayerP2));
                form2.AddField(
                    "totalRounds",
                    PlayerPrefs.GetInt(
                        "CURRENT_TROPHY_PLAYER_" + PlayerPrefs.GetInt("CurrentPlayerNo_")
                    )
                );
                form2.AddField("deviceName", SystemInfo.deviceName);

                using (
                    UnityWebRequest www = UnityWebRequest.Post(
                        "https://app-hanana.com/pong2d/insertmark.php",
                        form2
                    )
                )
                {
                    yield return www.SendWebRequest();
                    if (
                        www.result == UnityWebRequest.Result.ConnectionError
                        || www.result == UnityWebRequest.Result.ProtocolError
                    )
                    {
                        Debug.Log(www.error);
                        Debug.Log("error server");
                    }
                    else
                    {
                        string retrieveData = www.downloadHandler.text.Trim();
                        if (retrieveData.Equals("Insert Successful"))
                        {
                            Debug.Log(retrieveData);
                            Debug.Log("First time user");
                        }
                        else if (retrieveData.Equals("Update Successful"))
                        {
                            Debug.Log(retrieveData);
                            Debug.Log("Existing user");
                        }
                        else
                        {
                            Debug.Log("Something else");
                            Debug.Log(retrieveData);
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(1.5f);
        Destroy(GameObject.Find("Managers"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update() { }
}

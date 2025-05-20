using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public System.Random rnd = new System.Random();

    public System.Random rnd2 = new System.Random();

    public System.Random rnd3 = new System.Random();
    public List<int> randomAnswer;

    public TextMeshProUGUI questionTransform;
    public string questions;
    public int questionCount = 0;
    public int maxQuestionCount;
    int questionsNum;
    public int answers;
    public int playerInt = 0;
    public Button[] ansButton = new Button[2];
    public Button soundBtn;

    // Start is called before the first frame update
    void Start()
    {
        questionCount = 0;
        questionsNum = rnd3.Next(0, QuestionController.instance.questionsList.Count);
        Invoke("startControlling", 0.12f);
    }

    void startControlling()
    {
        //Time.timeScale = 1;
        if (transform.GetChild(0).transform.GetChild(0).transform.gameObject.activeSelf)
        {
            playerInt = 0;
            soundBtn = transform
                .GetChild(0)
                .transform.GetChild(0)
                .transform.GetChild(0)
                .transform.GetChild(1)
                .transform.GetChild(0)
                .transform.GetChild(2)
                .transform.GetComponent<Button>();
            ShowQuestion(playerInt);
            Debug.Log("player1 is active");
        }
        else if (transform.GetChild(0).transform.GetChild(1).transform.gameObject.activeSelf)
        {
            playerInt = 1;
            soundBtn = transform
                .GetChild(0)
                .transform.GetChild(1)
                .transform.GetChild(0)
                .transform.GetChild(1)
                .transform.GetChild(0)
                .transform.GetChild(2)
                .transform.GetComponent<Button>();
            ShowQuestion(playerInt);
            Debug.Log("player2 is active");
        }
    }

    void OnEnable()
    {
        Invoke("startControlling", 0.12f);
    }

    public void ShowQuestion(int playerInt)
    {
        StartCoroutine(showQuest(playerInt));
    }

    IEnumerator showQuest(int playerInt)
    {
        //Timing.gameObject.SetActive(true);

        //if else level

        questionsNum = rnd3.Next(0, QuestionController.instance.questionsList.Count);
        answers = questionsNum;

        questions = "PILIH HURUF...";
        questionTransform = transform
            .GetChild(0)
            .transform.GetChild(playerInt)
            .transform.GetChild(0)
            .transform.GetChild(1)
            .transform.GetChild(0)
            .transform.GetChild(0)
            .GetComponent<TextMeshProUGUI>();
        Debug.Log(questionTransform);
        questionTransform.text =
            questions + " " + QuestionController.instance.questionsList[answers].name;

        //assign buttons in gameobject

        ansButton[0] = transform
            .GetChild(0)
            .transform.GetChild(playerInt)
            .transform.GetChild(0)
            .transform.GetChild(1)
            .transform.GetChild(0)
            .transform.GetChild(1)
            .transform.GetChild(0) //answer btn 1
            .GetComponent<Button>();
        ansButton[1] = transform
            .GetChild(0)
            .transform.GetChild(playerInt)
            .transform.GetChild(0)
            .transform.GetChild(1)
            .transform.GetChild(0)
            .transform.GetChild(1)
            .transform.GetChild(1) //answer btn 2
            .GetComponent<Button>();

        //assign random answer to random buttons
        //enable the component on buttons;
        EnableAllButtons();

        //choose which button to put answer
        int buttonNum = rnd.Next(2);
        //add function to button
        ansButton[buttonNum].onClick.RemoveAllListeners();
        ansButton[buttonNum].onClick.AddListener(() => CorrectButtonFunction());

        //add play sound button to soundbtn
        soundBtn.onClick.RemoveAllListeners();
        soundBtn.onClick.AddListener(() => PlayCorrectAnswerSound(answers));
        //add something to button child
        ansButton[buttonNum].GetComponent<Image>().sprite = QuestionController
            .instance.questionsList[answers]
            .GetComponent<Image>()
            .sprite;

        int length;
        length = 2;
        for (int i = 0; i < length; i++)
        {
            if (i != buttonNum)
            {
                /*
                if (questionCount >= 0 && questionCount <= 20) // not more than 10
                {
                    randomAnswer = rnd.Next(2, 11);
                    while (randomAnswer == answers)
                    {
                        randomAnswer = rnd.Next(2, 11);
                    }
                    //                            Debug.Log("answer not more than 10 ");
                }
                
                */

                //chose random number and check if any same number already in the list
                int chooseRandom = rnd.Next(0, QuestionController.instance.questionsList.Count);

                //if list is empty (first time)
                if (randomAnswer.Count == 0)
                {
                    while (chooseRandom == questionsNum)
                    {
                        chooseRandom = rnd.Next(0, QuestionController.instance.questionsList.Count);
                    }
                    randomAnswer.Add(chooseRandom);
                }
                else
                {
                    // check if randomnumbers are duplicated in list
                    for (int l = 0; l < randomAnswer.Count; l++)
                    {
                        Debug.Log("Check");
                        while (randomAnswer[l] == chooseRandom)
                        {
                            Debug.Log("Duplicated");
                            //generate new number if chooseRandom already in the list
                            chooseRandom = rnd.Next(
                                0,
                                QuestionController.instance.questionsList.Count
                            );
                        }
                    }
                    //check if answers is already selected from the list
                    while (chooseRandom == answers)
                    {
                        Debug.Log("Duplicated");
                        //generate new number if chooseRandom already in the list
                        chooseRandom = rnd.Next(0, QuestionController.instance.questionsList.Count);
                    }

                    //

                    randomAnswer.Add(chooseRandom);
                }
                ansButton[i].onClick.RemoveAllListeners();
                ansButton[i].onClick.AddListener(() => buttonFunction());
                ansButton[i].GetComponent<Image>().sprite = QuestionController
                    .instance.questionsList[chooseRandom]
                    .GetComponent<Image>()
                    .sprite;
            }
        }
        if (questionCount == 0)
        {
            //play notidication sound
            if (!GameObject.Find("notification").GetComponent<AudioSource>().isPlaying)
            {
                GameObject.Find("notification").GetComponent<AudioSource>().Play();
            }
        }

        //play answer sound
        yield return new WaitForSeconds(1.1f);
        if (
            !QuestionController
                .instance.questionsList[answers]
                .GetComponent<AudioSource>()
                .isPlaying
        )
        {
            QuestionController.instance.questionsList[answers].GetComponent<AudioSource>().Play();
        }

        //activate background and show question also off center;
        // transform.GetChild(0).transform.gameObject.SetActive(true);
        //  transform.GetChild(2).transform.gameObject.SetActive(false);
        // transform.GetChild(3).transform.gameObject.SetActive(true);

        //Time.timeScale = 0;
    }

    IEnumerator incorrectAnswer()
    {
        DisableAllButtons();
        //add count to question
        questionCount++;
        Debug.Log("Incorrect");
        yield return new WaitForSeconds(0.45f);

        transform
            .GetChild(0)
            .transform.GetChild(playerInt)
            .transform.GetChild(0)
            .transform.GetChild(1)
            .transform.GetChild(1)
            .GetComponent<TextMeshProUGUI>()
            .text = "<color=red>SALAH!!</color>";

        yield return new WaitForSeconds(1f);
        transform
            .GetChild(0)
            .transform.GetChild(playerInt)
            .transform.GetChild(0)
            .transform.GetChild(1)
            .transform.GetChild(1)
            .GetComponent<TextMeshProUGUI>()
            .text = "<color=red></color>";

        if (questionCount > maxQuestionCount)
        {
            Managers.Game.SetState(typeof(KickOffState));
            transform.gameObject.SetActive(false);
            Managers.UI.inGameUI.gameBackButton.gameObject.SetActive(true);
        }
        else
        {
            ShowQuestion(playerInt);
        }
    }

    public void buttonFunction()
    {
        StartCoroutine(incorrectAnswer());
    }

    public void PlayCorrectAnswerSound(int answers)
    {
        Debug.Log("play sound");
        if (
            !QuestionController
                .instance.questionsList[answers]
                .GetComponent<AudioSource>()
                .isPlaying
        )
        {
            QuestionController.instance.questionsList[answers].GetComponent<AudioSource>().Play();
        }
    }

    public void CorrectButtonFunction()
    {
        StartCoroutine(roundCollected());
    }

    void DisableAllButtons()
    {
        for (int i = 0; i < ansButton.Length; i++)
        {
            ansButton[i].GetComponent<Button>().enabled = false;
        }
    }

    void EnableAllButtons()
    {
        for (int i = 0; i < ansButton.Length; i++)
        {
            ansButton[i].GetComponent<Button>().enabled = true;
        }
    }

    IEnumerator roundCollected()
    {
        DisableAllButtons();
        //disable buttons

        //add count to question
        questionCount++;
        //play correct sound
        if (GameObject.Find("rewarded").GetComponent<AudioSource>().isPlaying)
        {
            GameObject.Find("rewarded").GetComponent<AudioSource>().Stop();
        }
        if (!GameObject.Find("rewarded").GetComponent<AudioSource>().isPlaying)
        {
            GameObject.Find("rewarded").GetComponent<AudioSource>().Play();
        }
        transform
            .GetChild(0)
            .transform.GetChild(playerInt)
            .transform.GetChild(0)
            .transform.GetChild(1)
            .transform.GetChild(1)
            .GetComponent<TextMeshProUGUI>()
            .text = "<color=green>BETUL!</color>";
        //collect stars and timer
        /*
                Debug.Log("stars2");
                if (TimeToAnswer.instance.maxTime > 8)
                {
                    CollectionController.instance.addStars(10);
                }
                else if (TimeToAnswer.instance.maxTime > 4 && TimeToAnswer.instance.maxTime <= 8)
                {
                    int t3;
                    t3 = (int)TimeToAnswer.instance.maxTime;
                    CollectionController.instance.addStars(t3);
                }
                else if (TimeToAnswer.instance.maxTime > 0 && TimeToAnswer.instance.maxTime <= 4)
                {
                    CollectionController.instance.addStars(3);
                }
        */
        //Collect the round count
        CollectionController.instance.addStars(5, playerInt);
        //CollectionController.instance.addRounds(playerInt, 1);
        //roundTransform.GetComponent<Animator>().Play("collected");

        yield return new WaitForSeconds(0.45f);
        //play sound
        /*        if (!GameObject.Find("collected").GetComponent<AudioSource>().isPlaying)
                {
                    GameObject.Find("collected").GetComponent<AudioSource>().Play();
                }
          */yield return new WaitForSeconds(0.25f);
        //roundTransform.GetComponent<Animator>().Play("afterCollected");

        //destroy and remove the questions from the list
        QuestionController.instance.questionsList.RemoveAt(answers);
        Destroy(
            GameObject
                .Find("QuestionList-" + QuestionController.instance.level)
                .transform.GetChild(answers)
                .gameObject
        );
        //Destroy(gameObject);



        yield return new WaitForSeconds(1f);
        //erased text again
        transform
            .GetChild(0)
            .transform.GetChild(playerInt)
            .transform.GetChild(0)
            .transform.GetChild(1)
            .transform.GetChild(1)
            .GetComponent<TextMeshProUGUI>()
            .text = "<color=green></color>";
        if (questionCount > maxQuestionCount)
        {
            Managers.Game.SetState(typeof(KickOffState));
            transform.gameObject.SetActive(false);
            Managers.UI.inGameUI.gameBackButton.gameObject.SetActive(true);
        }
        else
        {
            ShowQuestion(playerInt);
        }
    }

    // Update is called once per frame
    void Update() { }
}

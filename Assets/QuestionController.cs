using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    public static QuestionController instance;
    public int level;

    public List<Transform> questionsList;

    void Awake()
    {
        instance = this;
    }

    public void setlevel(int levels)
    {
        level = levels;
        if (level == 1)
        {
            for (int i = 0; i < GameObject.Find("QuestionList-" + level).transform.childCount; i++)
            {
                questionsList.Add(GameObject.Find("QuestionList-" + level).transform.GetChild(i));
            }
            Managers.UI.inGameUI.fences[0].transform.gameObject.SetActive(true);
            Managers.UI.inGameUI.fences[1].transform.gameObject.SetActive(false);
            Managers.UI.inGameUI.fences[2].transform.gameObject.SetActive(false);
        }
        else if (level == 2)
        {
            for (int i = 0; i < GameObject.Find("QuestionList-" + level).transform.childCount; i++)
            {
                questionsList.Add(GameObject.Find("QuestionList-" + level).transform.GetChild(i));
            }

            Managers.UI.inGameUI.fences[0].transform.gameObject.SetActive(false);
            Managers.UI.inGameUI.fences[1].transform.gameObject.SetActive(true);
            Managers.UI.inGameUI.fences[2].transform.gameObject.SetActive(false);
        }
        else if (level == 3)
        {
            for (int i = 0; i < GameObject.Find("QuestionList-" + level).transform.childCount; i++)
            {
                questionsList.Add(GameObject.Find("QuestionList-" + level).transform.GetChild(i));
            }

            Managers.UI.inGameUI.fences[0].transform.gameObject.SetActive(false);
            Managers.UI.inGameUI.fences[1].transform.gameObject.SetActive(false);
            Managers.UI.inGameUI.fences[2].transform.gameObject.SetActive(true);
        }
        else if (level == 4)
        {
            for (int i = 0; i < GameObject.Find("QuestionList-" + level).transform.childCount; i++)
            {
                questionsList.Add(GameObject.Find("QuestionList-" + level).transform.GetChild(i));
            }

            Managers.UI.inGameUI.fences[0].transform.gameObject.SetActive(false);
            Managers.UI.inGameUI.fences[1].transform.gameObject.SetActive(false);
            Managers.UI.inGameUI.fences[2].transform.gameObject.SetActive(true);
        }

        Managers.UI.inGameUI.levelChoices.transform.gameObject.SetActive(false);
        Managers.UI.inGameUI.PlayButton.GetComponent<Transform>().gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionController : MonoBehaviour
{
    public static QuestionController instance;
    public int level;
    public List<Transform> questionsList;

    void Awake()
    {
        instance = this;
        if (level == 1)
        {
            for (int i = 0; i < GameObject.Find("QuestionList-" + level).transform.childCount; i++)
            {
                questionsList.Add(GameObject.Find("QuestionList-" + level).transform.GetChild(i));
            }
        }
        else if (level == 2)
        {
            for (int i = 0; i < GameObject.Find("QuestionList-" + level).transform.childCount; i++)
            {
                questionsList.Add(GameObject.Find("QuestionList-" + level).transform.GetChild(i));
            }
        }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}

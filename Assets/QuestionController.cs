using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionController : MonoBehaviour
{
    public static QuestionController instance;

    public List<Transform> questionsList;

    void Awake()
    {
        instance = this;
        for (int i = 0; i < GameObject.Find("QuestionList").transform.childCount; i++)
        {
            questionsList.Add(GameObject.Find("QuestionList").transform.GetChild(i));
        }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}

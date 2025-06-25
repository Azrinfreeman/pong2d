using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmController : MonoBehaviour
{
    public List<Transform> alarms;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            alarms.Add(transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update() { }
}

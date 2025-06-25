using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheersController : MonoBehaviour
{
    public Transform InGameUI;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (InGameUI.gameObject.activeSelf)
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
}

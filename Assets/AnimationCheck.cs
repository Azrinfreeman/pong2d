using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    public void DisableAnimator()
    {
        GameObject.Find("UI").GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update() { }
}

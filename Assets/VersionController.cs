using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VersionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = Application.version;
    }

    // Update is called once per frame
    void Update() { }
}

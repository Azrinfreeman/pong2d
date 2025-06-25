using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameController : MonoBehaviour
{
    public static PlayerNameController instance;

    void Awake()
    {
        instance = this;
    }

    public List<TextMeshProUGUI> names;
    public List<Transform> transformController;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}

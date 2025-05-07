using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagController : MonoBehaviour
{
    public static FlagController instance;

    void Awake()
    {
        instance = this;
    }

    //public List<Flags> flags = new List<Flags>();

    [SerializeField]
    public List<Transform> flagImages;

    [System.Serializable]
    public class Flags
    {
        public Image image;
        public string name;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            flagImages.Add(transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update() { }
}

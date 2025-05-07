using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchChecker : MonoBehaviour
{
    public static TouchChecker instance;

    void Awake()
    {
        instance = this;
    }

    public bool isPlayer1;
    public bool isPlayer2;

    public int player1Turn;
    public int player2Turn;

    // Start is called before the first frame update
    void Start()
    {
        player1Turn = 0;
        player2Turn = 0;
    }

    // Update is called once per frame
    void Update() { }
}

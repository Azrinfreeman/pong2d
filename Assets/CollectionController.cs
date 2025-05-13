using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionController : MonoBehaviour
{
    public static CollectionController instance;
    public int starsTotal;
    public int roundsTotal;
    public int stars;
    public int rounds;

    void Awake()
    {
        instance = this;
    }

    public string playerString;
    public int playerInt;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void addRounds(int round, int playerInt)
    {
        roundsTotal += round;
        if (playerInt == 0)
        {
            PlayerPrefs.SetInt("scorePlayer1", roundsTotal);
        }
        else if (playerInt == 1)
        {
            PlayerPrefs.SetInt("scorePlayer2", roundsTotal);
        }
    }

    public void addStars(int star, int playerInt)
    {
        stars += star;
        if (playerInt == 0)
        {
            int tempStar = PlayerPrefs.GetInt("scorePlayer1");

            tempStar += stars;
            PlayerPrefs.SetInt("scorePlayer1", tempStar);
        }
        else if (playerInt == 1)
        {
            int tempStar = PlayerPrefs.GetInt("scorePlayer2");
            tempStar += stars;

            PlayerPrefs.SetInt("scorePlayer2", tempStar);
        }
    }
}

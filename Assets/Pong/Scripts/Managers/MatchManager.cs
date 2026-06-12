//  /*********************************************************************************
//   *********************************************************************************
//   *********************************************************************************
//   * Produced by Skard Games										                  *
//   * Facebook: https://goo.gl/5YSrKw											      *
//   * Contact me: https://goo.gl/y5awt4								              *
//   * Developed by Cavit Baturalp Gürdin: https://tr.linkedin.com/in/baturalpgurdin *
//   *********************************************************************************
//   *********************************************************************************
//   *********************************************************************************/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour
{
    public Ball ball;
    public GameObject aiPaddle;
    public GameObject playerPaddle;
    public SavedGame savedGame;

    public bool isAiMatch;

    [Header("UI Alignment")]
    public Transform topWall;
    public Transform bottomWall;
    public float paddleHeightOffset = 0.6f;

    public void Reset()
    {
        ball.ResetBall();

        float topLimit = 2.36f;
        float bottomLimit = -2.36f;

        if (Camera.main != null)
        {
            if (topWall != null && bottomWall != null)
            {
                float y1 = topWall.position.y;
                float y2 = bottomWall.position.y;
                topLimit = Mathf.Max(y1, y2) - paddleHeightOffset;
                bottomLimit = Mathf.Min(y1, y2) + paddleHeightOffset;
                
                if (topLimit < bottomLimit)
                {
                    float mid = (topLimit + bottomLimit) / 2f;
                    topLimit = mid;
                    bottomLimit = mid;
                }
            }
            else
            {
                if (topWall != null) topLimit = topWall.position.y - paddleHeightOffset;
                if (bottomWall != null) bottomLimit = bottomWall.position.y + paddleHeightOffset;
            }
        }

        playerPaddle.transform.position = new Vector2(playerPaddle.transform.position.x, Constants.PLAYER.y);
        aiPaddle.transform.position = new Vector2(aiPaddle.transform.position.x, Constants.AI.y);
        playerPaddle.transform.localScale = Constants.PADDLE_SCALE;
        aiPaddle.transform.localScale = Constants.PADDLE_SCALE;

        Paddle pPaddle = playerPaddle.GetComponent<Paddle>();
        Paddle aPaddle = aiPaddle.GetComponent<Paddle>();

        pPaddle.topLimit = topLimit;
        pPaddle.bottomLimit = bottomLimit;
        if (aPaddle != null)
        {
            aPaddle.topLimit = topLimit;
            aPaddle.bottomLimit = bottomLimit;
        }

        pPaddle.speed = Constants.PADDLE_SPEED;
        if (isAiMatch && aPaddle != null)
        {
            aPaddle.speed = Constants.PADDLE_SPEED_FOR_AI;
        }
        else if (aPaddle != null)
        {
            aPaddle.speed = Constants.PADDLE_SPEED;
        }
    }

    public void RetrieveSavedMatch()
    {
        ball.transform.position = savedGame.ballPosition;
        playerPaddle.transform.position = savedGame.playerPosition;
        aiPaddle.transform.position = savedGame.aiPosition;
        playerPaddle.transform.localScale = savedGame.playerScale;
        aiPaddle.transform.localScale = savedGame.aiScale;

        ball.ballBody.linearVelocity = savedGame.ballVelocity;
        playerPaddle.GetComponent<Paddle>().speed = savedGame.playerSpeed;
        aiPaddle.GetComponent<Paddle>().speed = savedGame.aiSpeed;

        Managers.Score.aiScore = savedGame.aiScore;
        Managers.Score.playerScore = savedGame.playerScore;
    }

    public void SaveMatch()
    {
        savedGame.ballPosition = ball.transform.position;
        savedGame.playerPosition = playerPaddle.transform.position;
        savedGame.aiPosition = aiPaddle.transform.position;
        savedGame.playerScale = playerPaddle.transform.localScale;
        savedGame.aiScale = aiPaddle.transform.localScale;

        savedGame.ballVelocity = ball.ballBody.linearVelocity;
        savedGame.playerSpeed = playerPaddle.GetComponent<Paddle>().speed;
        savedGame.aiSpeed = aiPaddle.GetComponent<Paddle>().speed;

        savedGame.aiScore = Managers.Score.aiScore;
        savedGame.playerScore = Managers.Score.playerScore;

        Reset();
    }

    public void ResetSavedGame()
    {
        savedGame.ballVelocity = Vector2.zero;
        savedGame.playerPosition = new Vector2(playerPaddle.transform.position.x, Constants.PLAYER.y);
        savedGame.aiPosition = new Vector2(aiPaddle.transform.position.x, Constants.AI.y);
        savedGame.aiScore = 0;
        savedGame.playerScore = 0;
    }
}

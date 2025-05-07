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

public class ScoreManager : MonoBehaviour
{
    public int playerScore;
    public int aiScore;
    public int scoreLimit;

    public PaddleOwner Winner
    {
        get { return (aiScore > playerScore) ? PaddleOwner.PLAYER : PaddleOwner.PLAYER2; }
    }

    public void OnScore(PaddleOwner scorer)
    {
        if (scorer == PaddleOwner.PLAYER)
        {
            playerScore++;
        }
        else if (scorer == PaddleOwner.PLAYER2)
        {
            aiScore++;
        }

        Managers.UI.inGameUI.UpdateScore();

        if (playerScore == scoreLimit || aiScore == scoreLimit)
            Managers.Game.SetState(typeof(GameOverState));
        else
            Managers.Game.SetState(typeof(GoalState));
    }
}

using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball instance;

    public float speed;
    public float speedMultiplier;
    public ParticleSystem particle;
    public ParticleSystem hitParticle;

    [HideInInspector]
    public Rigidbody2D ballBody;

    [HideInInspector]
    public Paddle lastTouchedPaddle;

    public int paddle;

    void Awake()
    {
        instance = this;
        ballBody = GetComponent<Rigidbody2D>();
        ResetBall();
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("PADDLE"))
        {
            other.gameObject.GetComponent<Paddle>().ResetPuddleAnim1();
        }
        else if (other.gameObject.tag.Equals("PADDLE2"))
        {
            other.gameObject.GetComponent<Paddle>().ResetPuddleAnim2();
        }
    }

    IEnumerator blinking()
    {
        // Note: You may want to rename "goalUp" and "goalDown" in your Unity scene
        // to "goalLeft" and "goalRight", and update them here as well!
        if (paddle == 1)
        {
            yield return new WaitForSeconds(0.5f);
            AlarmController alarm = GameObject
                .Find("goalUp")
                .transform.GetChild(1)
                .GetComponent<AlarmController>();

            bool toggle = false;
            for (int i = 0; i < 6; i++)
            {
                if (!toggle)
                {
                    alarm.alarms[0].gameObject.SetActive(false);
                    alarm.alarms[1].gameObject.SetActive(true);
                    toggle = true;
                }
                else
                {
                    alarm.alarms[0].gameObject.SetActive(true);
                    alarm.alarms[1].gameObject.SetActive(false);
                    toggle = false;
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            AlarmController alarm = GameObject
                .Find("goalDown")
                .transform.GetChild(1)
                .GetComponent<AlarmController>();

            bool toggle = false;
            for (int i = 0; i < 6; i++)
            {
                if (!toggle)
                {
                    alarm.alarms[0].gameObject.SetActive(false);
                    alarm.alarms[1].gameObject.SetActive(true);
                    toggle = true;
                }
                else
                {
                    alarm.alarms[0].gameObject.SetActive(true);
                    alarm.alarms[1].gameObject.SetActive(false);
                    toggle = false;
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        hitParticle.Play();
        Managers.Audio.PlayCollisionSound();
        StartCoroutine(Managers.Cam.shaker.Shake());

        // Note: If you renamed your walls in Unity to LeftWall and RightWall,
        // change the names here to match!
        if (other.gameObject.name.Equals("BottomWall") || other.gameObject.name.Equals("LeftWall"))
        {
            paddle = 1;
            Managers.Score.OnScore(PaddleOwner.PLAYER);
            StartCoroutine(blinking());
        }
        else if (
            other.gameObject.name.Equals("TopWall") || other.gameObject.name.Equals("RightWall")
        )
        {
            paddle = 2;
            Managers.Score.OnScore(PaddleOwner.PLAYER2);
            StartCoroutine(blinking());
        }
        else if (other.gameObject.CompareTag("PADDLE"))
        {
            other.gameObject.GetComponent<Animator>().Play("pud_touch1");
            Vector2 velocity = ballBody.linearVelocity;

            // CHANGED FOR HORIZONTAL: Measure Y distance using the paddle's height
            float y = HitFactor(
                transform.position,
                other.transform.position,
                other.collider.bounds.size.y
            );

            // CHANGED FOR HORIZONTAL: Bounce left (-1) or right (1) depending on paddle's X position
            int temp = (other.transform.position.x > 0) ? -1 : 1;

            // Apply bounce direction (X is left/right power, Y is the angle off the paddle)
            Vector2 dir = new Vector2(temp, y).normalized;
            ballBody.linearVelocity = dir * velocity.magnitude * speedMultiplier;
            lastTouchedPaddle = other.gameObject.GetComponent<Paddle>();
        }
        else if (other.gameObject.CompareTag("PADDLE2"))
        {
            other.gameObject.GetComponent<Animator>().Play("pud_touch");
            Vector2 velocity = ballBody.linearVelocity;

            // CHANGED FOR HORIZONTAL
            float y = HitFactor(
                transform.position,
                other.transform.position,
                other.collider.bounds.size.y
            );

            // CHANGED FOR HORIZONTAL
            int temp = (other.transform.position.x > 0) ? -1 : 1;
            Vector2 dir = new Vector2(temp, y).normalized;
            ballBody.linearVelocity = dir * velocity.magnitude * speedMultiplier;
            lastTouchedPaddle = other.gameObject.GetComponent<Paddle>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (lastTouchedPaddle != null)
            other.gameObject.GetComponent<Powerup>().TriggerPowerup(lastTouchedPaddle);
    }

    public void KickOffBall()
    {
        GetComponent<Animator>().Play("ball_moving");
        ballBody.angularVelocity = 0.0f;

        // CHANGED FOR HORIZONTAL: Give it a random slight angle on Y, but shoot it strongly Left or Right (X)
        float randomY = Random.Range(-0.5f, 0.5f);
        Vector2 _direction =
            (Random.value >= 0.5f) ? new Vector2(1, randomY) : new Vector2(-1, randomY);

        ballBody.AddForce(_direction.normalized * speed);
        particle.gameObject.SetActive(true);
    }

    public void ResetBall()
    {
        GetComponent<Animator>().Play("idle");
        ballBody.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;
        particle.gameObject.SetActive(false);
    }

    // CHANGED FOR HORIZONTAL: Evaluates difference on the Y axis instead of X
    float HitFactor(Vector2 ballPosition, Vector2 paddlePosition, float paddleHeight)
    {
        return (ballPosition.y - paddlePosition.y) / paddleHeight;
    }

    public void ParticleRotation()
    {
        Vector3 directionOfMotion = new Vector3(
            0,
            ballBody.linearVelocity.y,
            ballBody.linearVelocity.x
        );
        Quaternion rotation = Quaternion.LookRotation(directionOfMotion);
        particle.transform.localRotation = rotation;
    }
}

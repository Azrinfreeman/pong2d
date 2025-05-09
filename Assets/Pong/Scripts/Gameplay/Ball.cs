﻿//  /*********************************************************************************
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

public class Ball : MonoBehaviour
{
    public float speed;
    public float speedMultiplier;
    public ParticleSystem particle;
    public ParticleSystem hitParticle;

    [HideInInspector]
    public Rigidbody2D ballBody;

    [HideInInspector]
    public Paddle lastTouchedPaddle;

    void Awake()
    {
        ballBody = GetComponent<Rigidbody2D>();
        ResetBall();
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("PADDLE"))
        {
            Debug.Log("out of paddle");
            other.gameObject.GetComponent<Paddle>().ResetPuddleAnim1();
            //other.gameObject.GetComponent<Animator>().Play("pud_idle");
        }
        else if (other.gameObject.tag.Equals("PADDLE2"))
        {
            Debug.Log("out of paddle");
            other.gameObject.GetComponent<Paddle>().ResetPuddleAnim2();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        hitParticle.Play();
        Managers.Audio.PlayCollisionSound();
        StartCoroutine(Managers.Cam.shaker.Shake());

        if (other.gameObject.name.Equals("BottomWall"))
        {
            Managers.Score.OnScore(PaddleOwner.PLAYER2);
        }
        else if (other.gameObject.name.Equals("TopWall"))
        {
            Managers.Score.OnScore(PaddleOwner.PLAYER);
        }
        else if (other.gameObject.CompareTag("PADDLE"))
        {
            other.gameObject.GetComponent<Animator>().Play("pud_touch1");
            Vector2 velocity = ballBody.velocity;

            float x = HitFactor(
                transform.position,
                other.transform.position,
                other.collider.bounds.size.x
            );
            int temp = 0;
            temp = (other.transform.position.y > 1) ? -1 : 1;
            Vector2 dir = new Vector2(x, temp).normalized;
            ballBody.velocity = dir * velocity.magnitude * speedMultiplier;
            lastTouchedPaddle = other.gameObject.GetComponent<Paddle>();
        }
        else if (other.gameObject.CompareTag("PADDLE2"))
        {
            other.gameObject.GetComponent<Animator>().Play("pud_touch");
            Vector2 velocity = ballBody.velocity;

            float x = HitFactor(
                transform.position,
                other.transform.position,
                other.collider.bounds.size.x
            );
            int temp = 0;
            temp = (other.transform.position.y > 1) ? -1 : 1;
            Vector2 dir = new Vector2(x, temp).normalized;
            ballBody.velocity = dir * velocity.magnitude * speedMultiplier;
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

        float r = Random.value;
        Vector2 _direction = (r >= 0.5f) ? new Vector2(r, 1) : new Vector2(r, -1);

        ballBody.AddForce(_direction * speed);
        particle.gameObject.SetActive(true);
    }

    public void ResetBall()
    {
        GetComponent<Animator>().Play("idle");
        ballBody.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        particle.gameObject.SetActive(false);
    }

    float HitFactor(Vector2 ballPosition, Vector2 paddlePosition, float paddleWidth)
    {
        return (ballPosition.x - paddlePosition.x) / paddleWidth;
    }

    public void ParticleRotation()
    {
        Vector3 directionOfMotion = new Vector3(0, ballBody.velocity.y, ballBody.velocity.x);
        Quaternion rotation = Quaternion.LookRotation(directionOfMotion);
        particle.transform.localRotation = rotation;
    }
}

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
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum PaddleOwner
{
    PLAYER,
    PLAYER2,
    AI,
}

public class Paddle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float speed;
    public PaddleOwner owner;

    public bool isClick;

    public Text countTouch;

    public Text p1;
    public Text p2;

    public string currentTouchGameObject;

    [HideInInspector]
    public Vector2 scale;
    int cnt = 0;
    #region Private Vars
    private Vector3 screenPoint;
    private Ball _ball;
    private Rigidbody2D _rigidBody;
    #endregion

    void Start()
    {
        AddPhysics2DRaycaster();
        _ball = Managers.Match.ball;
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void AddPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClick = true;
        // Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);

        if (owner == PaddleOwner.PLAYER)
        {
            currentTouchGameObject = eventData.pointerCurrentRaycast.gameObject.name;
            //  Debug.Log("paddle");
            TouchChecker.instance.isPlayer1 = true;
            if (TouchChecker.instance.player1Turn == 0 && TouchChecker.instance.player2Turn == 0)
            {
                TouchChecker.instance.player1Turn = 1;
            }
            else if (TouchChecker.instance.player2Turn == 1)
            {
                TouchChecker.instance.player1Turn = 2;
            }
        }
        if (owner == PaddleOwner.PLAYER2)
        {
            currentTouchGameObject = eventData.pointerCurrentRaycast.gameObject.name;
            //            Debug.Log("paddle2");
            TouchChecker.instance.isPlayer2 = true;
            if (TouchChecker.instance.player1Turn == 0 && TouchChecker.instance.player2Turn == 0)
            {
                TouchChecker.instance.player2Turn = 1;
            }
            else if (TouchChecker.instance.player1Turn == 1)
            {
                TouchChecker.instance.player2Turn = 2;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        currentTouchGameObject = "";
        isClick = false;
        if (owner == PaddleOwner.PLAYER)
        {
            Debug.Log("paddleDown");
            TouchChecker.instance.isPlayer1 = false;
            if (TouchChecker.instance.player1Turn > 0 && TouchChecker.instance.player2Turn > 0)
            {
                TouchChecker.instance.player1Turn = 0;
                TouchChecker.instance.player2Turn = 0;
            }
            else if (TouchChecker.instance.player1Turn > 0)
            {
                TouchChecker.instance.player1Turn = 0;
            }
        }
        if (owner == PaddleOwner.PLAYER2)
        {
            Debug.Log("paddleDown2");
            TouchChecker.instance.isPlayer2 = false;
            if (TouchChecker.instance.player1Turn > 0 && TouchChecker.instance.player2Turn > 0)
            {
                TouchChecker.instance.player1Turn = 0;
                TouchChecker.instance.player2Turn = 0;
            }
            else if (TouchChecker.instance.player2Turn > 0)
            {
                TouchChecker.instance.player2Turn = 0;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }
        p1.text = TouchChecker.instance.player1Turn.ToString();
        p2.text = TouchChecker.instance.player2Turn.ToString();
        //countTouch.text = Input.touchCount.ToString();
        countTouch.text = Input.touchCount.ToString();
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began) { }

            if (Input.GetTouch(0).phase == TouchPhase.Moved) { }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                //Debug.Log("touch endede");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cnt++;
            ScreenCapture.CaptureScreenshot(cnt.ToString() + ".png");
        }
        if (owner == PaddleOwner.PLAYER)
        {
            if (Managers.Input.isActive)
            {
                DragInput();
            }
        }
        else if (owner == PaddleOwner.PLAYER2)
        {
            if (Managers.Input.isActive)
            {
                DragInput2();
            }
        }
        else if (owner == PaddleOwner.AI)
        {
            AIControl();
        }
    }

    void KeyboardInput()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        CheckMovementBlock(direction);
    }

    void TouchLRInput()
    {
        float direction = 0;

        if (Input.GetMouseButton(0))
        {
            direction = (Input.mousePosition.x > Screen.width / 2) ? 1 : -1;
        }
        CheckMovementBlock(direction);
    }

    void DragInput()
    {
        /*
        if (TouchChecker.instance.isPlayer1)
        {
            Vector3 curScreenPoint = new Vector3(
                Input.GetTouch(0).position.x,
                Input.GetTouch(0).position.y,
                0
            );
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            curPosition.y = -4;
            curPosition.z = 0;
            // if (Mathf.Abs(curPosition.x) > 2.06f)
            //      return;
            transform.position = curPosition;
        }
        if (TouchChecker.instance.isPlayer2)
        {
            Vector3 curScreenPoint = new Vector3(
                Input.GetTouch(1).position.x,
                Input.GetTouch(1).position.y,
                0
            );
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            curPosition.y = -4;
            curPosition.z = 0;
            //if (Mathf.Abs(curPosition.x) > 2.06f)
            //  return;
            transform.position = curPosition;
        }
        */
        if (TouchChecker.instance.player1Turn == 1)
        {
            if (TouchChecker.instance.isPlayer1)
            {
                Vector3 curScreenPoint = new Vector3(
                    Input.GetTouch(0).position.x,
                    Input.GetTouch(0).position.y,
                    0
                );
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
                curPosition.y = -4;
                curPosition.z = 0;
                // if (Mathf.Abs(curPosition.x) > 2.06f)
                //      return;
                transform.position = curPosition;
            }
        }
        else if (TouchChecker.instance.player1Turn == 2)
        {
            if (TouchChecker.instance.isPlayer1 && TouchChecker.instance.isPlayer2)
            {
                Vector3 curScreenPoint = new Vector3(
                    Input.GetTouch(1).position.x,
                    Input.GetTouch(1).position.y,
                    0
                );
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
                curPosition.y = -4;
                curPosition.z = 0;
                // if (Mathf.Abs(curPosition.x) > 2.06f)
                //      return;
                transform.position = curPosition;
            }
        }
    }

    void DragInput2()
    {
        if (TouchChecker.instance.player2Turn == 1)
        {
            if (TouchChecker.instance.isPlayer2)
            {
                Vector3 curScreenPoint = new Vector3(
                    Input.GetTouch(0).position.x,
                    Input.GetTouch(0).position.y,
                    0
                );
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
                curPosition.y = 4;
                curPosition.z = 0;
                //if (Mathf.Abs(curPosition.x) > 2.06f)
                //  return;
                transform.position = curPosition;
            }
        }
        else if (TouchChecker.instance.player2Turn == 2)
        {
            Vector3 curScreenPoint = new Vector3(
                Input.GetTouch(1).position.x,
                Input.GetTouch(1).position.y,
                0
            );
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            curPosition.y = 4;
            curPosition.z = 0;
            //if (Mathf.Abs(curPosition.x) > 2.06f)
            //  return;
            transform.position = curPosition;
        }
        else if (TouchChecker.instance.player2Turn == 1 && TouchChecker.instance.player1Turn == 2)
        {
            Debug.Log("player 2 touch while 1 "); //then use tgouch 1
            if (TouchChecker.instance.isPlayer1)
            {
                Vector3 curScreenPoint = new Vector3(
                    Input.GetTouch(1).position.x,
                    Input.GetTouch(1).position.y,
                    0
                );
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
                curPosition.y = 4;
                curPosition.z = 0;
                // if (Mathf.Abs(curPosition.x) > 2.06f)
                //    return;
                transform.position = curPosition;
            }

            if (TouchChecker.instance.isPlayer2)
            {
                Vector3 curScreenPoint = new Vector3(
                    Input.GetTouch(0).position.x,
                    Input.GetTouch(0).position.y,
                    0
                );
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
                curPosition.y = 4;
                curPosition.z = 0;
                // if (Mathf.Abs(curPosition.x) > 2.06f)
                //     return;
                transform.position = curPosition;
            }
        }
    }

    void CheckMovementBlock(float dir)
    {
        float nextFramePosX = Mathf.Abs(
            (new Vector2(dir, 0) * speed * Time.deltaTime).x + transform.position.x
        );

        if (nextFramePosX < 2.36)
        {
            transform.Translate(new Vector2(dir, 0) * speed * Time.deltaTime);
        }
    }

    IEnumerator resetAnim1()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<Animator>().Play("pud_idle1");
    }

    IEnumerator resetAnim2()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<Animator>().Play("pud_idle");
    }

    public void ResetPuddleAnim1()
    {
        StartCoroutine(resetAnim1());
    }

    public void ResetPuddleAnim2()
    {
        StartCoroutine(resetAnim2());
    }

    void AIControl()
    {
        if (Mathf.Sign(transform.position.y) == Mathf.Sign(_ball.ballBody.velocity.y))
        {
            if (_ball.transform.position.x > transform.position.x + 0.410f)
            {
                if (_rigidBody.velocity.x < 0)
                    _rigidBody.velocity = Vector2.zero;

                _rigidBody.velocity = Vector2.right * speed;
            }
            else if (_ball.transform.position.x < transform.position.x - 0.410f)
            {
                if (_rigidBody.velocity.x > 0)
                    _rigidBody.velocity = Vector2.zero;

                _rigidBody.velocity = Vector2.left * speed;
            }
            else
            {
                _rigidBody.velocity = Vector2.zero;
            }
        }
        else
            _rigidBody.velocity = Vector2.zero;
    }
}

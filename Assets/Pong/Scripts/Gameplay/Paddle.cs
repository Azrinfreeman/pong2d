//  /*********************************************************************************
//   *********************************************************************************
//   *********************************************************************************
//   * Produced by Skard Games                                                        *
//   * Facebook: https://goo.gl/5YSrKw                                                *
//   * Contact me: https://goo.gl/y5awt4                                              *
//   * Developed by Cavit Baturalp Gürdin: https://tr.linkedin.com/in/baturalpgurdin  *
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

    [Header("Movement Limits")]
    public float topLimit = 2.36f;
    public float bottomLimit = -2.36f;

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

        if (owner == PaddleOwner.PLAYER)
        {
            currentTouchGameObject = eventData.pointerCurrentRaycast.gameObject.name;
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

        if (TouchChecker.instance.touchSwitch)
        {
            TouchChecker.instance.touchSwitch = false;
            if (TouchChecker.instance.player1Turn == 1 && TouchChecker.instance.player2Turn == 2)
            {
                Debug.Log("reset touch");
                TouchChecker.instance.player1Turn = 2;
                TouchChecker.instance.player2Turn = 1;
            }
            else if (
                TouchChecker.instance.player1Turn == 2
                && TouchChecker.instance.player2Turn == 1
            )
            {
                Debug.Log("reset touch");
                TouchChecker.instance.player1Turn = 1;
                TouchChecker.instance.player2Turn = 2;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Input.touchCount == 1)
        {
            if (owner == PaddleOwner.PLAYER)
            {
                Debug.Log("Player1Up");
                currentTouchGameObject = "";
                isClick = false;

                TouchChecker.instance.isPlayer1 = false;
                if (
                    TouchChecker.instance.player1Turn == 0
                    && TouchChecker.instance.player2Turn == 0
                )
                {
                    TouchChecker.instance.player1Turn = 0;
                    TouchChecker.instance.player2Turn = 0;
                }
                else if (
                    TouchChecker.instance.player1Turn == 1
                    && TouchChecker.instance.player2Turn == 0
                )
                {
                    TouchChecker.instance.player1Turn = 0;
                    TouchChecker.instance.player2Turn = 0;
                }
                else if (
                    TouchChecker.instance.player1Turn == 0
                    && TouchChecker.instance.player2Turn == 1
                )
                {
                    TouchChecker.instance.player1Turn = 0;
                    TouchChecker.instance.player2Turn = 0;
                }
                else if (
                    TouchChecker.instance.player1Turn == 1
                    && TouchChecker.instance.player2Turn == 2
                )
                {
                    TouchChecker.instance.player1Turn = 0;
                    TouchChecker.instance.player2Turn = 0;
                }
                else if (
                    TouchChecker.instance.player1Turn == 2
                    && TouchChecker.instance.player2Turn == 1
                )
                {
                    TouchChecker.instance.player1Turn = 1;
                    TouchChecker.instance.player2Turn = 2;
                }
                else if (TouchChecker.instance.player1Turn == 1)
                {
                    TouchChecker.instance.player2Turn = 0;
                }
                else if (TouchChecker.instance.player2Turn == 1)
                {
                    TouchChecker.instance.player1Turn = 0;
                }
            }

            if (owner == PaddleOwner.PLAYER2)
            {
                Debug.Log("player2Up");
                TouchChecker.instance.isPlayer2 = false;
                if (
                    TouchChecker.instance.player1Turn == 0
                    && TouchChecker.instance.player2Turn == 1
                )
                {
                    TouchChecker.instance.player1Turn = 0;
                    TouchChecker.instance.player2Turn = 0;
                }
                else if (
                    TouchChecker.instance.player1Turn == 2
                    && TouchChecker.instance.player2Turn == 1
                )
                {
                    TouchChecker.instance.player1Turn = 0;
                    TouchChecker.instance.player2Turn = 0;
                }
                else if (
                    TouchChecker.instance.player1Turn == 1
                    && TouchChecker.instance.player2Turn == 2
                )
                {
                    TouchChecker.instance.player1Turn = 2;
                    TouchChecker.instance.player2Turn = 1;
                }
                else if (TouchChecker.instance.player1Turn == 1)
                {
                    TouchChecker.instance.player2Turn = 0;
                }
                else if (TouchChecker.instance.player2Turn == 1)
                {
                    TouchChecker.instance.player1Turn = 0;
                }
            }
        }
        else if (Input.touchCount == 2)
        {
            if (owner == PaddleOwner.PLAYER)
            {
                if (
                    TouchChecker.instance.player1Turn == 1
                    && TouchChecker.instance.player2Turn == 2
                )
                {
                    TouchChecker.instance.player1Turn = 2;
                    TouchChecker.instance.player2Turn = 1;
                    TouchChecker.instance.touchSwitch = true;
                }
            }

            if (owner == PaddleOwner.PLAYER2)
            {
                if (
                    TouchChecker.instance.player1Turn == 2
                    && TouchChecker.instance.player2Turn == 1
                )
                {
                    TouchChecker.instance.player1Turn = 1;
                    TouchChecker.instance.player2Turn = 2;
                    TouchChecker.instance.touchSwitch = true;
                }
            }
        }
        else if (Input.touchCount == 0)
        {
            if (owner == PaddleOwner.PLAYER)
            {
                if (
                    TouchChecker.instance.player1Turn == 1
                    && TouchChecker.instance.player2Turn == 2
                )
                {
                    TouchChecker.instance.player1Turn = 0;
                    TouchChecker.instance.player2Turn = 0;
                }
            }

            if (owner == PaddleOwner.PLAYER2)
            {
                if (
                    TouchChecker.instance.player1Turn == 2
                    && TouchChecker.instance.player2Turn == 1
                )
                {
                    TouchChecker.instance.player1Turn = 0;
                    TouchChecker.instance.player2Turn = 0;
                }
            }
        }
    }

    public void simpoleControl()
    {
        currentTouchGameObject = "";
        isClick = false;
        if (owner == PaddleOwner.PLAYER)
        {
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

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began) { }
            if (Input.GetTouch(0).phase == TouchPhase.Moved) { }
            if (Input.GetTouch(0).phase == TouchPhase.Ended) { }
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
        float direction = Input.GetAxisRaw("Vertical"); // Changed to Vertical for UP/DOWN
        CheckMovementBlock(direction);
    }

    void TouchLRInput()
    {
        float direction = 0;

        if (Input.GetMouseButton(0))
        {
            direction = (Input.mousePosition.y > Screen.height / 2) ? 1 : -1; // Adjusted for Up/Down halves
        }
        CheckMovementBlock(direction);
    }

    Vector3 GetInputPositionSafe(int touchIndex)
    {
        if (Input.touchCount > touchIndex)
        {
            return new Vector3(Input.GetTouch(touchIndex).position.x, Input.GetTouch(touchIndex).position.y, 0);
        }
        return new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }

    void DragInput()
    {
        if (TouchChecker.instance.touchSwitch)
        {
            if (TouchChecker.instance.player1Turn == 1 && TouchChecker.instance.player1Turn == 1)
            {
                if (TouchChecker.instance.isPlayer1)
                {
                    Vector3 curScreenPoint = GetInputPositionSafe(0);
                    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

                    curPosition.x = transform.position.x;
                    curPosition.z = 0;
                    _rigidBody.MovePosition(curPosition); // Fixed here
                }
            }
        }
        else if (TouchChecker.instance.player1Turn == 1)
        {
            if (TouchChecker.instance.isPlayer1)
            {
                Vector3 curScreenPoint = GetInputPositionSafe(0);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

                curPosition.x = transform.position.x;
                curPosition.y = Mathf.Clamp(curPosition.y, bottomLimit, topLimit);
                curPosition.z = 0;
                _rigidBody.MovePosition(curPosition); // Fixed here
            }
        }
        else if (TouchChecker.instance.player1Turn == 2)
        {
            if (TouchChecker.instance.isPlayer1 && TouchChecker.instance.isPlayer2)
            {
                Vector3 curScreenPoint = GetInputPositionSafe(1);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

                curPosition.x = transform.position.x;
                curPosition.y = Mathf.Clamp(curPosition.y, bottomLimit, topLimit);
                curPosition.z = 0;
                _rigidBody.MovePosition(curPosition); // Fixed here
            }
        }
    }

    void DragInput2()
    {
        if (TouchChecker.instance.player2Turn == 1)
        {
            if (TouchChecker.instance.isPlayer2)
            {
                Vector3 curScreenPoint = GetInputPositionSafe(0);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

                curPosition.x = transform.position.x;
                curPosition.y = Mathf.Clamp(curPosition.y, bottomLimit, topLimit);
                curPosition.z = 0;
                _rigidBody.MovePosition(curPosition); // Fixed here
            }
        }
        else if (TouchChecker.instance.player2Turn == 2)
        {
            Vector3 curScreenPoint = GetInputPositionSafe(1);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

            curPosition.x = transform.position.x;
            curPosition.z = 0;
            _rigidBody.MovePosition(curPosition); // Fixed here
        }
        else if (TouchChecker.instance.player2Turn == 1 && TouchChecker.instance.player1Turn == 2)
        {
            if (TouchChecker.instance.isPlayer1)
            {
                Vector3 curScreenPoint = GetInputPositionSafe(1);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

                curPosition.x = transform.position.x;
                curPosition.y = Mathf.Clamp(curPosition.y, bottomLimit, topLimit);
                curPosition.z = 0;
                _rigidBody.MovePosition(curPosition); // Fixed here
            }

            if (TouchChecker.instance.isPlayer2)
            {
                Vector3 curScreenPoint = GetInputPositionSafe(0);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

                curPosition.x = transform.position.x;
                curPosition.y = Mathf.Clamp(curPosition.y, bottomLimit, topLimit);
                curPosition.z = 0;
                _rigidBody.MovePosition(curPosition); // Fixed here
            }
        }
    }

    void CheckMovementBlock(float dir)
    {
        float nextFramePosY = (new Vector2(0, dir) * speed * Time.deltaTime).y + transform.position.y;

        if (nextFramePosY < topLimit && nextFramePosY > bottomLimit)
        {
            transform.Translate(new Vector2(0, dir) * speed * Time.deltaTime);
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
        // Clamp position if it exceeds limits
        if (transform.position.y > topLimit)
        {
            transform.position = new Vector3(transform.position.x, topLimit, transform.position.z);
            _rigidBody.linearVelocity = Vector2.zero;
        }
        else if (transform.position.y < bottomLimit)
        {
            transform.position = new Vector3(transform.position.x, bottomLimit, transform.position.z);
            _rigidBody.linearVelocity = Vector2.zero;
        }

        // Ensure AI only tracks when ball is moving towards its side (evaluating X instead of Y)
        if (Mathf.Sign(transform.position.x) == Mathf.Sign(_ball.ballBody.linearVelocity.x))
        {
            // Track the ball's Y position to move up and down
            if (_ball.transform.position.y > transform.position.y + 0.410f)
            {
                if (_rigidBody.linearVelocity.y < 0)
                    _rigidBody.linearVelocity = Vector2.zero;

                if (transform.position.y < topLimit)
                    _rigidBody.linearVelocity = Vector2.up * speed;
                else
                    _rigidBody.linearVelocity = Vector2.zero;
            }
            else if (_ball.transform.position.y < transform.position.y - 0.410f)
            {
                if (_rigidBody.linearVelocity.y > 0)
                    _rigidBody.linearVelocity = Vector2.zero;

                if (transform.position.y > bottomLimit)
                    _rigidBody.linearVelocity = Vector2.down * speed;
                else
                    _rigidBody.linearVelocity = Vector2.zero;
            }
            else
            {
                _rigidBody.linearVelocity = Vector2.zero;
            }
        }
        else
            _rigidBody.linearVelocity = Vector2.zero;
    }
}

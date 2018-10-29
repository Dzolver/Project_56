using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public bool grounded;
    public LayerMask whatIsGround;
    public float moveSpeed;
    public float jumpForce;

    private Collider2D RunnerCollider;
    private Rigidbody2D RunnerRigidBody;
    private Animator RunnerAnimator;
    private Vector2 m_FirstPressPos;
    private Vector2 m_SecondPressPos;
    private Vector2 m_CurrentSwipe;
    private int m_MoveDirection;

    public float SwipeDetectionSensitivity;

    // Use this for initialization
    private void Start()
    {
        //initialize the component variables by searching for all needed components using GetComponent
        RunnerCollider = GetComponent<Collider2D>();
        RunnerRigidBody = GetComponent<Rigidbody2D>();
        RunnerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        MouseSwipe();
        TouchSwipe();
        //returns true or false whether the collider is touching another collider containing the layer called 'Ground'
        grounded = Physics2D.IsTouchingLayers(RunnerCollider, whatIsGround);
        //Character will move in a direction with each frame
        RunnerRigidBody.velocity = new Vector2(moveSpeed, RunnerRigidBody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            RunnerRigidBody.velocity = new Vector2(RunnerRigidBody.velocity.x, jumpForce);
        }

        RunnerAnimator.SetFloat("Speed", RunnerRigidBody.velocity.x);
        RunnerAnimator.SetBool("Grounded", grounded);
    }

    private void MouseSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_FirstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
        {
            m_SecondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            MovePlayer();
        }
    }

    private void TouchSwipe()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                m_FirstPressPos = touch.position;
            }
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Ended)
            {
                m_SecondPressPos = touch.position;
                MovePlayer();
                m_FirstPressPos = touch.position;
            }
        }
    }

    public void MovePlayer()
    {
        //create vector from the two points
        m_CurrentSwipe = new Vector2(m_SecondPressPos.x - m_FirstPressPos.x, m_SecondPressPos.y - m_FirstPressPos.y);

        if (Mathf.Abs(m_CurrentSwipe.x) <= SwipeDetectionSensitivity)
        {
            return;
        }

        //normalize the 2d vector
        m_CurrentSwipe.Normalize();

        if (m_CurrentSwipe.x < 0)
        {
            RunnerRigidBody.velocity = Vector2.zero;
            moveSpeed = -Mathf.Abs(moveSpeed);
        }
        else if (m_CurrentSwipe.x > 0/* && (currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)*/)
        {
            moveSpeed = Mathf.Abs(moveSpeed);
        }
    }
}
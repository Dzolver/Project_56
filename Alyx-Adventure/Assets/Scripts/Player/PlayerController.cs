using System;
using System.Collections;
using UnityEngine;

namespace AlyxAdventure
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        public bool grounded;
        public float moveSpeed = 5;
        public float jumpForce = 17;
        public float fallGravity = 15;
        public float maxSpeed = 15f;
        public float speedIncreaseRate;
        public float slidingInterval = .5f;
        public LayerMask whatIsGround;

        private bool coolDown;
        private bool sliding;
        private float CoolDownTime = 3f;
        private float gravity;
        private int m_MoveDirection;
        private Collider2D RunnerCollider;
        private Rigidbody2D RunnerRigidBody;
        private Animator RunnerAnimator;
        private Animator RunnerWeaponAnimator;
        private Vector2 m_FirstPressPos;
        private Vector2 m_SecondPressPos;
        private Vector2 m_CurrentSwipe;
        private float HorizontalSwipeDetectionSensitivity;
        private float VerticalSwipeDetectionSensitivity;

        //Attack variable
        public float swingCoolDown = 1; //player can only swing once once per second
        private float lastSwing;
        private Player player; //to know if playerAttacked

        private float BaseMovementSpeed;


        #region Life Cycle
        private void OnEnable()
        {
            MyEventManager.Instance.OnJumpClicked.AddListener(Jump);
            MyEventManager.Instance.OnFallOrSlideClicked.AddListener(OnFallOrSlideClicked);
            MyEventManager.Instance.OnAttackClicked.AddListener(OnAttackClicked);
            MyEventManager.Instance.OnSecondPassed.AddListener(IncreaseSpeed);
            MyEventManager.Instance.OnPowerupCollected.AddListener(OnPowerupCollected);
            MyEventManager.Instance.OnPowerupExhausted.AddListener(OnPowerupExhausted);
        }


        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.OnJumpClicked.RemoveListener(Jump);
                MyEventManager.Instance.OnFallOrSlideClicked.RemoveListener(OnFallOrSlideClicked);
                MyEventManager.Instance.OnAttackClicked.RemoveListener(OnAttackClicked);
                MyEventManager.Instance.OnPowerupCollected.RemoveListener(OnPowerupCollected);
                MyEventManager.Instance.OnSecondPassed.RemoveListener(IncreaseSpeed);
                MyEventManager.Instance.OnPowerupExhausted.RemoveListener(OnPowerupExhausted);
            }
        }

        private void Start()
        {
            HorizontalSwipeDetectionSensitivity = Screen.width / 160f;
            VerticalSwipeDetectionSensitivity = Screen.width / 160f;
            BaseMovementSpeed = moveSpeed;
            coolDown = false;

            RunnerCollider = GetComponent<Collider2D>();
            RunnerRigidBody = GetComponent<Rigidbody2D>();
            RunnerAnimator = GetComponent<Animator>();

            player = GetComponent<Player>();
            gravity = RunnerRigidBody.gravityScale;
            MyEventManager.Instance.ChangeMoveDirection.Dispatch(Direction.Right);
            StartCoroutine(UpdateAnimFrameRate());
        }

        private IEnumerator UpdateAnimFrameRate()
        {
            RunnerAnimator.speed = moveSpeed / BaseMovementSpeed; 
            yield return new WaitForSeconds(10f);
            
        }

        private void Update()
        {
            MouseSwipe();
            TouchSwipe();
            //MouseAttack();
            //TouchAttack();

            //Character will move in a direction with each frame
            RunnerRigidBody.velocity = new Vector2(moveSpeed * (int)GameData.Instance.direction, RunnerRigidBody.velocity.y);
            GameData.Instance.RunnerVelocity = RunnerRigidBody.velocity;

            if (sliding)
                return;
            RunnerAnimator.SetFloat("Speed", Mathf.Abs(RunnerRigidBody.velocity.x));
            RunnerAnimator.SetBool("Grounded", grounded);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GameStrings.Ground) || collision.gameObject.CompareTag(GameStrings.Platform))
            {
                grounded = true;
            }

        }
        #endregion

        #region SWIPE

        private void MouseSwipe()
        {
            //if (!coolDown)
            //{
                if (Input.GetMouseButtonDown(0) && Input.mousePosition.x < Screen.width/2)
                {
                Debug.Log("Detecting");
                    m_FirstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                }
                if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
                {
                    m_SecondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    SwipePlayer();
                }
            //}
        }

        private void TouchSwipe()
        {
            //if (!coolDown)
            //{
                if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began && touch.position.x < Screen.width / 2)
                    {
                        m_FirstPressPos = touch.position;
                    }
                    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Ended)
                    {
                        m_SecondPressPos = touch.position;
                        SwipePlayer();
                        m_FirstPressPos = touch.position;
                    }
                }
            //}
        }

        public void SwipePlayer()
        {
            //create vector from the two points
            m_CurrentSwipe = new Vector2(m_SecondPressPos.x - m_FirstPressPos.x, m_SecondPressPos.y - m_FirstPressPos.y);
            if (Mathf.Abs(m_CurrentSwipe.x) >= HorizontalSwipeDetectionSensitivity)
            {
                m_CurrentSwipe.Normalize();
                if (m_CurrentSwipe.x < 0 && !coolDown)
                {
                    MyEventManager.Instance.ChangeMoveDirection.Dispatch(Direction.Left);
                    RunnerRigidBody.velocity = Vector2.zero;
                    // moveSpeed = -Mathf.Abs(moveSpeed);
                    transform.localRotation = new Quaternion(0, 180, 0, transform.rotation.w);

                }
                else if (m_CurrentSwipe.x > 0 && !coolDown/* && (currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)*/)
                {
                    MyEventManager.Instance.ChangeMoveDirection.Dispatch(Direction.Right);
                    //moveSpeed = Mathf.Abs(moveSpeed);
                    transform.localRotation = Quaternion.identity;
                }
            }
            else if (Mathf.Abs(m_CurrentSwipe.y) >= VerticalSwipeDetectionSensitivity)
            {
                m_CurrentSwipe.Normalize();
                if (m_CurrentSwipe.y > 0)
                {
                    MyEventManager.Instance.OnJumpClicked.Dispatch();
                }
                else if (m_CurrentSwipe.y < 0)
                {
                    MyEventManager.Instance.OnFallOrSlideClicked.Dispatch();
                }

            }
            //normalize the 2d vector

           
           
            coolDown = true;
            StartCoroutine(WaitForCoolDown());
        }
        //private void MouseAttack()
        //{
        //    if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > Screen.width / 2)
        //    {
        //        Debug.Log("Detecting Attack");
        //        MyEventManager.Instance.OnAttackClicked.Dispatch();
        //    }
        //}

        //private void TouchAttack()
        //{
        //    if (Input.touchCount == 1)
        //    {
        //        Touch touch = Input.GetTouch(0);
        //        if (touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2)
        //        {
        //            Debug.Log("Detecting Attack");
        //            MyEventManager.Instance.OnAttackClicked.Dispatch();
        //        }
        //    }
        //}
      
        private IEnumerator WaitForCoolDown()
        {
            yield return new WaitForSeconds(CoolDownTime);
            coolDown = false;
        }

        #endregion

        #region Player Movements
        private void Jump()
        {
            if (grounded || sliding)
            {
                grounded = false;
                SetSlidingOff();
                if (RunnerRigidBody.gravityScale > gravity)
                    RunnerRigidBody.gravityScale = gravity;//resetting gravity

                RunnerRigidBody.velocity = new Vector2(RunnerRigidBody.velocity.x, jumpForce);
            }
        }


        private void FallOrSlide()
        {
            if (!grounded)
            {
                RunnerRigidBody.gravityScale = fallGravity;
            }
            if (grounded)
            {
                RunnerRigidBody.gravityScale = gravity;
                sliding = true;
                RunnerAnimator.SetBool("Sliding", sliding);
                Invoke("SetSlidingOff", slidingInterval);
            }
        }

        private void Attack()
        {
            if (Time.time - lastSwing >= swingCoolDown)
            {
                RunnerAnimator.SetTrigger("Attack");
                lastSwing = Time.time;
                player.attacked = true;
                Invoke("SetAttackOff", RunnerAnimator.GetCurrentAnimatorStateInfo(0).length);
            }

        }

        #endregion

        #region Listeners
        private void OnPowerupCollected(BasePowerup powerup)
        {
            if (powerup.GetPowerupType() == PowerupType.FastRunInvincibility)
            {
                moveSpeed += ((FastRunInvincibility)powerup).GetSpeed();
            }
        }


        private void OnPowerupExhausted(BasePowerup powerup)
        {
            if (powerup.GetPowerupType() == PowerupType.FastRunInvincibility)
            {
                moveSpeed -= ((FastRunInvincibility)powerup).GetSpeed();
            }
        }

        private void OnFallOrSlideClicked()
        {
            FallOrSlide();
        }

        public void OnAttackClicked()
        {
            Attack();
        }

        private void IncreaseSpeed(int totalSecPlayed)
        {
            if (Mathf.Abs(moveSpeed) < maxSpeed)
            {
                moveSpeed += speedIncreaseRate;
            }
        }

        #endregion

        #region Other Methods
        private void SetAttackOff()
        {
            player.attacked = false;
        }

        private void SetSlidingOff()
        {
            if (sliding)
            {
                sliding = false;
                RunnerAnimator.SetBool("Sliding", sliding);
            }

        }
        #endregion
    }
}
using System;
using System.Collections;
using UnityEngine;

namespace Project56
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        public bool grounded;
        public float speed, moveSpeed = 3;
        public float jumpForce = 17;
        public float fallGravity = 15;
        public float maxSpeed = 15f;
        public float speedIncreaseRate = 0.05f;
        public float slidingInterval = 0.5f;
        public float SwipeDetectionSensitivity;
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
        private Coroutine coroutine;

        //Attack variable
        public float swingCoolDown = 1; //player can only once per second
        private float lastSwing;
        private Player player; //to know if playerAttacked


        #region Life Cycle
        private void OnEnable()
        {
            MyEventManager.Instance.OnJumpClicked.AddListener(Jump);
            MyEventManager.Instance.OnFallOrSlideClicked.AddListener(OnFallOrSlideClicked);
            MyEventManager.Instance.OnAttackClicked.AddListener(OnAttackClicked);
            MyEventManager.Instance.IncreaseSpeed.AddListener(OnSpeedIncrease);
            MyEventManager.Instance.OnPowerupCollected.AddListener(OnPowerupCollected);
        }


        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.OnJumpClicked.RemoveListener(Jump);
                MyEventManager.Instance.OnFallOrSlideClicked.RemoveListener(OnFallOrSlideClicked);
                //MyEventManager.Instance.OnAttackClicked.RemoveListener(OnAttackClicked);
                MyEventManager.Instance.OnPowerupCollected.RemoveListener(OnPowerupCollected);
                MyEventManager.Instance.IncreaseSpeed.RemoveListener(OnSpeedIncrease);
            }
        }

        private void Start()
        {
            SwipeDetectionSensitivity = Screen.width / 30f;
            coolDown = false;
            //initialize the component variables by searching for all needed components using GetComponent
            RunnerCollider = GetComponent<Collider2D>();
            RunnerRigidBody = GetComponent<Rigidbody2D>();
            RunnerAnimator = GetComponent<Animator>();
            RunnerWeaponAnimator = transform.GetChild(0).GetComponent<Animator>();
            player = GetComponent<Player>();
            //cameraController = Camera.main.GetComponent<CameraController>();
            gravity = RunnerRigidBody.gravityScale;
            speed = moveSpeed;

            MyEventManager.Instance.ChangeMoveDirection.Dispatch(Direction.Right);
        }

        private void Update()
        {
            MouseSwipe();
            TouchSwipe();
            //returns true or false whether the collider is touching another collider containing the layer called 'Ground'
            //grounded = Physics2D.IsTouchingLayers(RunnerCollider, whatIsGround);
            //Character will move in a direction with each frame
            RunnerRigidBody.velocity = new Vector2(moveSpeed, RunnerRigidBody.velocity.y);
            GameData.Instance.RunnerVelocity = RunnerRigidBody.velocity;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (sliding)
                return;
            RunnerAnimator.SetFloat("Speed", RunnerRigidBody.velocity.x);
            RunnerAnimator.SetBool("Grounded", grounded);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GameStrings.Platform))
            {
                grounded = true;
            }
            
        }
        #endregion

        #region SWIPE

        private void MouseSwipe()
        {
            if (!coolDown)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    m_FirstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                }
                if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
                {
                    m_SecondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    SwipePlayer();
                }
            }
        }

        private void TouchSwipe()
        {
            if (!coolDown)
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

                        SwipePlayer();
                        m_FirstPressPos = touch.position;
                    }
                }
            }
        }

        public void SwipePlayer()
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
                transform.localRotation = new Quaternion(0, 180, 0, transform.rotation.w);
                MyEventManager.Instance.ChangeMoveDirection.Dispatch(Direction.Left);

            }
            else if (m_CurrentSwipe.x > 0/* && (currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)*/)
            {
                moveSpeed = Mathf.Abs(moveSpeed);
                transform.localRotation = Quaternion.identity;
                MyEventManager.Instance.ChangeMoveDirection.Dispatch(Direction.Right);
            }
            coolDown = true;
            StartCoroutine(WaitForCoolDown());
        }

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
                if (sliding)
                {
                    sliding = false;
                    RunnerAnimator.SetBool("Sliding", sliding);
                }
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
                RunnerWeaponAnimator.SetTrigger("Attack");
                lastSwing = Time.time;
                player.attacked = true;
                Invoke("SetAttackOff", RunnerWeaponAnimator.GetCurrentAnimatorStateInfo(0).length);

            }

        }

        #endregion

        #region Listeners
        private void OnPowerupCollected(BasePowerup powerup)
        {
            if (powerup.GetPowerupType() == PowerupType.FastRunInvincibility)
            {
                if (coroutine != null)
                    StopCoroutine(coroutine);
                ResetSpeed();
                moveSpeed += ((FastRunInvincibility)powerup).GetSpeed();
                coroutine = StartCoroutine(IEResetSpeed(powerup.GetPowerupDuration()));

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

        private void OnSpeedIncrease()
        {
            if (Mathf.Abs(moveSpeed) < maxSpeed)
            {
                if (moveSpeed >= 0)
                {
                    moveSpeed += speedIncreaseRate;
                }
                else
                {
                    moveSpeed -= speedIncreaseRate;
                }
                speed = moveSpeed;
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

        private IEnumerator IEResetSpeed(float duration)
        {
            yield return new WaitForSeconds(duration);
            ResetSpeed();
        }

        private void ResetSpeed()
        {
            moveSpeed = speed;
        }
        #endregion
    }
}
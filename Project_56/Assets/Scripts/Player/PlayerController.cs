using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public bool grounded;
	public LayerMask whatIsGround;
	public float moveSpeed;
	public float jumpForce;

	private Collider2D RunnerCollider;
	private Rigidbody2D RunnerRigidBody;
	private Animator RunnerAnimator;

	// Use this for initialization
	void Start () {
		//initialize the component variables by searching for all needed components using GetComponent
		RunnerCollider = GetComponent<Collider2D>();
		RunnerRigidBody = GetComponent<Rigidbody2D> ();	
		RunnerAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//returns true or false whether the collider is touching another collider containing the layer called 'Ground'
		grounded = Physics2D.IsTouchingLayers (RunnerCollider, whatIsGround);
		//Character will move in a direction with each frame
		RunnerRigidBody.velocity = new Vector2 (moveSpeed, RunnerRigidBody.velocity.y);

		if(Input.GetKeyDown(KeyCode.Space) && grounded == true)
		{
			RunnerRigidBody.velocity = new Vector2 (RunnerRigidBody.velocity.x, jumpForce);
		}

		RunnerAnimator.SetFloat ("Speed", RunnerRigidBody.velocity.x);
		RunnerAnimator.SetBool ("Grounded", grounded);
	}
}

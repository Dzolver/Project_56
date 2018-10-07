using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float moveSpeed;
	public float jumpForce;

	private Rigidbody2D RunnerRigidBody;

	// Use this for initialization
	void Start () {
		//initialize the component variables by searching for all needed components using GetComponent
		RunnerRigidBody = GetComponent<Rigidbody2D> ();	
	}
	
	// Update is called once per frame
	void Update () {
		//Character will move in a direction with each frame
		RunnerRigidBody.velocity = new Vector2 (moveSpeed, RunnerRigidBody.velocity.y);

		if(Input.GetKeyDown(KeyCode.Space) )
		{
			RunnerRigidBody.velocity = new Vector2 (RunnerRigidBody.velocity.x, jumpForce);
		}
	}
}

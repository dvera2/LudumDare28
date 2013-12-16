using UnityEngine;
using System.Collections;

public class DudeController : MonoBehaviour {

	DudeInputController controls;
	public AudioController audioController;
	GameController gameController;

	public float pushForce = 1f;
	public float jumpForce= 100f;
	public float maxJumpBoostForce = 250.0f;
	public float maxJumpHoldTime = 0.1f;

	public Transform headA;
	public Transform headB;
	public Transform foot;

	public float nextJump = 0;

	public bool canJump = true;
	public bool isGrounded = false;

	public Vector2 accelForce;

	public Vector2 prevVelocity;


	public float footResist = 0.25f;
	public float minJumpTime = 0.2f;
	public float jumpHold = 0;

	Vector3 startPosition;

	public bool shouldReset = true;
	public bool shouldJump = false;

	private float cachedJumpForce;

	// Use this for initialization
	void Start () {
		controls = GetComponent<DudeInputController>();
		controls.JumpPressed += HandleJumpPressed;
		controls.JumpReleased += HandleJumpReleased;

		gameController = GetComponent<GameController>();
		prevVelocity = Vector2.zero;
		startPosition = transform.position;
	}

	void HandleJumpPressed ()	{
		// Jump impulse
		if(canJump && Time.time >= nextJump) {
			canJump = false;
			nextJump = Time.time + minJumpTime;			
			audioController.PlayHop();

			// Save the jumping force for later
			cachedJumpForce = jumpForce;
			jumpHold = Time.time + maxJumpHoldTime;

			//Debug.Log ("Cached JumpForce:" + cachedJumpForce);
			shouldJump = true;
		}
	}

	void HandleJumpReleased () {
		shouldJump = false;
	}

	public void Reset() {
		transform.position = startPosition;
		transform.localRotation = Quaternion.identity;
		canJump = true;
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		// F = mA
		/*
		Vector2 accel = (rigidbody2D.velocity - prevVelocity) / Time.fixedDeltaTime;
		accelForce = rigidbody2D.mass * accel;
	
		if(rigidbody2D.velocity.y <= 0.01f) // grounded
			isGrounded = true;
		else
			isGrounded = false;
		*/

		// Tilt position
		rigidbody2D.AddForceAtPosition(pushForce * controls.forceInput * (headB.position - headA.position).normalized, headB.position);

		// Opposite push at feet from control
		rigidbody2D.AddForceAtPosition(pushForce * footResist * controls.forceInput * -Vector2.right, foot.position);

		if(shouldJump) {

			// Jump up towards head direction
			rigidbody2D.AddForce(cachedJumpForce * (transform.position - foot.position).normalized);

			cachedJumpForce = maxJumpBoostForce;

			// Reduce further cache jump calls
			//cachedJumpForce *= 0.5f;
			Debug.Log ("CachedJumpForce: " + cachedJumpForce);

			// Stop the jump after a certain period
			if(Time.time >= jumpHold)
				shouldJump = false;
		}

		// Add gravity
		//rigidbody2D.AddForce(gravity * rigidbody2D.mass);
		prevVelocity = rigidbody2D.velocity;
	}

	void OnCollisionEnter2D(Collision2D collision) {

		var dot = Vector2.Dot (headB.position - foot.position, Vector2.up);
		if(dot > 0.9f) {
			canJump = true;
		}
		else if(dot > 0.4f) {
			canJump = true;
			audioController.PlayKnock();
		}
		else {
			canJump = false;
			audioController.PlayFall();
		}
	}

	void Update() {
		if(shouldReset) {
			shouldReset = false;
			Reset ();
		}
	}
}

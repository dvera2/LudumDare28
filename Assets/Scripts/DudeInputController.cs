using UnityEngine;
using System.Collections;

public class DudeInputController : MonoBehaviour {

	public delegate void onJumpPress();
	public event onJumpPress JumpPressed;
	public event onJumpPress JumpReleased;

	public float forceInput;
	public bool resetPressed = false;

	public float jumpStrength = 0;
	public float maxHoldTime = 1.0f;

	private float jumpPressTime = 0;

	public bool jumpPressed = false;

	public GameController gameController;

	void Start() {
		if(!gameController) gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	void Update() {

		forceInput = 0;

		if(gameController.currentState == GameController.State.Playing) {

			float hor = Input.GetAxis("Horizontal");

			if(Input.GetKey (KeyCode.LeftArrow) || (hor < -0.01f)) {
				forceInput -= 1;
			}

			if(Input.GetKey(KeyCode.RightArrow) || (hor > 0.01f)) {
				forceInput += 1;
			}


			// Pressing jump button
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")) {
				jumpStrength = 0;
				jumpPressTime = Time.time;
				
				// Notify listeners
				if(JumpPressed != null) JumpPressed();
			}

			// Releasing jump button. Longer held, more strength <= maxHoldTime
			if(Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Jump")) {
				//float delta = Time.time - jumpPressTime;
				//jumpStrength = Mathf.Clamp(delta / maxHoldTime, 0.0f, 1.0f);

				Debug.Log ("Jump Released");

				// Notify listeners
				if(JumpReleased != null) JumpReleased();
			}
		}
	}
}

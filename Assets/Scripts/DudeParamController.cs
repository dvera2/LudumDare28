using UnityEngine;
using System.Collections;

public class DudeParamController : MonoBehaviour {

	public MinMaxSlider bounceSlider;
	public MinMaxSlider frictionSlider;
	public MinMaxSlider gravitySlider;
	public MinMaxSlider footForceSlider;
	public MinMaxSlider headForceSlider;
	public MinMaxSlider jumpForceSlider;
	public MinMaxSlider minJumpTimeSlider;	
	public MinMaxSlider maxJumpBoostSlider;

	PhysicsMaterial2D physMaterial2D;
	DudeController dude;
	float bounciness, friction;

	// Use this for initialization
	void Start () {
		physMaterial2D = GetComponent<BoxCollider2D>().sharedMaterial;
		bounciness = physMaterial2D.bounciness;
		friction = physMaterial2D.friction;

		UpdatePhysics();
		dude = GetComponent<DudeController>();

		if(bounceSlider) {
			bounceSlider.ValueChanged += (MinMaxSlider slider) => {
				bounciness = slider.value;
				UpdatePhysics();
			};
		}

		if(frictionSlider) {
			frictionSlider.ValueChanged += (MinMaxSlider slider) => {
				friction = slider.value;
				UpdatePhysics();
			};
		}

		if(gravitySlider) {
			gravitySlider.ValueChanged += (MinMaxSlider slider) => {
				rigidbody2D.gravityScale = slider.value;
			};
		}

		if(jumpForceSlider) {
			jumpForceSlider.ValueChanged += (MinMaxSlider slider) => {
				dude.jumpForce = slider.value;
			};
		}

		if(headForceSlider) {
			headForceSlider.ValueChanged += (MinMaxSlider slider) => {
				dude.pushForce = slider.value;
			};
		}

		if(minJumpTimeSlider) {
			minJumpTimeSlider.ValueChanged += (MinMaxSlider slider) => {
				dude.minJumpTime = slider.value;
			};
		}

		if(footForceSlider) {
			footForceSlider.ValueChanged += (MinMaxSlider slider) => {
				dude.footResist = slider.value;
			};
		}

		if(maxJumpBoostSlider) {
			maxJumpBoostSlider.ValueChanged += (MinMaxSlider slider) => {
				dude.maxJumpBoostForce = slider.value;
			};
		}
	}

	void UpdatePhysics() {

		physMaterial2D.bounciness = bounciness;
		physMaterial2D.friction = friction;
		var box2D = GetComponent<BoxCollider2D>();
		box2D.sharedMaterial = physMaterial2D;
		box2D.enabled = false;
		box2D.enabled = true;
	}
	
}
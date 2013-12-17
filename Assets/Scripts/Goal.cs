using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public delegate void onGoalHit();
	public event onGoalHit GoalHit;

	void OnTriggerEnter2D(Collider2D collider) {

		if(collider.tag == "Player") {
			collider.isTrigger = false;

			// Notify game that the goal was won
			if(GoalHit != null)	GoalHit();
		}
	}
}

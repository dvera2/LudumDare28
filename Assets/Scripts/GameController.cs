using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Goal goal;
	public DudeController dude;
	public GUIText uiText;
	public UIPanel settingsPanel;

	public AudioController audioController;

	public enum State {
		None,
		Intro,
		Playing,
		Won,
	}

	public State currentState;

	public bool canToggle = true;

	// Use this for initialization
	void Start () {
		currentState = State.None;
		settingsPanel.enabled = false;

		if(!dude) dude = GameObject.FindGameObjectWithTag("Player").GetComponent<DudeController>();

		SetState(State.Intro);

		goal.GoalHit += () => {

			uiText.text = "Goal Won!";
			SetState(State.Won);
		};
	}

	void SetState(State state) {
		if(currentState != state) {
			currentState = state;
			OnStateChanged();
		}
	}

	void OnStateChanged() {
		switch(currentState) {
		case State.Playing:
			dude.shouldReset = true;
			uiText.text = "";
			audioController.PlayStage();
			break;
		case State.Won:
			uiText.text = "You Win!";
			StartCoroutine(ResetGame());
			audioController.PlayWin();
			break;
		case State.Intro:
			uiText.text = "Press Enter to play";
			audioController.PlayIntro();
			break;
		default:
			break;
		}
	}

	void Update() {
		if(currentState == State.Intro) {
			if(Input.GetKey(KeyCode.Return) || Input.GetButton("Fire1")) {
				SetState (State.Playing);
			}
		}

		if(currentState == State.Playing) {
			if(Input.GetKey(KeyCode.R) || Input.GetButton("Fire1")) {
				dude.Reset ();
			}
		}

		if(Input.GetKey(KeyCode.O)) {
			if(canToggle) {				
				canToggle = false;
				settingsPanel.enabled = !settingsPanel.enabled;
				StartCoroutine(ResetToggle());
			}
		}
	}

	IEnumerator ResetToggle() {
		yield return new WaitForSeconds(0.5f);
		canToggle = true;
	}

	IEnumerator ResetGame() {
		yield return new WaitForSeconds(5.0f);
		SetState(State.Intro);
	}
}

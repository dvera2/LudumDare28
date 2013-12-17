using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Goal goal;
	public DudeController dude;
	public UIPanel settingsPanel;
	public UILabel uiText;
	public UILabel levelText;
	public GameObject uiRoot;

	public string levelName;

	public UIButton mainscreenButton;

	public Transform levelRoot;

	public AudioController audioController;

	public enum State {
		None,
		Seeking,
		Intro,
		Playing,
		Won,
	}

	int currentLevelIndex = 0;

	public State currentState;

	public bool canToggle = true;

	void Awake() {
		GameObject.DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start () {
		currentState = State.None;

		if(settingsPanel)
			settingsPanel.enabled = false;

		if(uiRoot) {
			uiRoot.SetActive(false);
		}
		audioController.PlayIntro();
	}

	public void Bind() {

		uiRoot.SetActive(true);
		currentState = State.Seeking;

		//uiText = GameObject.FindGameObjectWithTag("UI").GetComponent<UILabel>();
		LoadNextLevel();		

		Debug.Log ("Bound to Game");
	}

	public void Reload() {

		dude = GameObject.FindGameObjectWithTag("Player").GetComponent<DudeController>();
		dude.audioController = audioController;

		goal = GameObject.FindGameObjectWithTag("Goal").GetComponent<Goal>();

		Debug.Log ("Reloaded...");
		goal.GoalHit += () => {
			
			uiText.text = "Goal Won!";
			SetState(State.Won);
		};

		SetState(State.Intro);
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
			StartCoroutine(ToNextLevel());
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

		if(currentState == State.None)
			return;

		FindRoot();

		if(!levelRoot)
			Debug.Log("Can't find " + levelName);

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

	IEnumerator ToNextLevel() {
		yield return new WaitForSeconds(5.0f);
		LoadNextLevel();
	}

	void FindRoot() {

		if(levelRoot)
			return;

		if(Application.isLoadingLevel)
			return;
		
		levelRoot = GameObject.Find (levelName).transform; //.FindChild(levelName);
		if(!levelRoot) {
			uiRoot.SetActive(false);
		}
		else {
			levelText.text = levelName;
			levelRoot.position = Vector3.zero;
			levelRoot.localScale = Vector3.one;
			levelRoot.localRotation = Quaternion.identity;
			
			Reload();
		}
	}
		
	void LoadNextLevel() {

		currentLevelIndex++;
		if(currentLevelIndex > 10) {

			SetState(State.None);
			uiRoot.SetActive(false);
			Application.LoadLevel("Congrats");
		}
				
		if(levelRoot) {
			GameObject.Destroy(levelRoot.gameObject);
			levelRoot = null;
		}

		levelName = "Level" + string.Format("{0:00}", currentLevelIndex);

		try {
			Application.LoadLevelAdditive(levelName);
		}catch(System.Exception ex) {
		}
	}
}

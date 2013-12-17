using UnityEngine;
using System.Collections;

public class TitleScript : MonoBehaviour {

	public GameController controller;

	void Start() {
		controller.mainscreenButton = GameObject.FindWithTag("CreditsButton").GetComponent<UIButton>();
		EventDelegate.Add(controller.mainscreenButton.onClick, CreditsClicked);
	}

	public void PlayClicked() {
		Application.LoadLevel("Default");
//		Application.LoadLevelAdditive("Level01");

		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		controller.Bind ();
	}

	public void CreditsClicked() {
		Application.LoadLevel("Credits");
		//EventDelegate.Remove(controller.mainscreenButton.onClick, CreditsClicked);
		
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		//controller.mainscreenButton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<UIButton>();
		//EventDelegate.Add(controller.mainscreenButton.onClick, PlayClicked);
	}
}

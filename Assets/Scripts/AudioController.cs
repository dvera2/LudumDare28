using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	public AudioSource jumpPool;
	public AudioSource fallPool;
	public AudioSource musicPool;

	public AudioClip hopClip;
	public AudioClip fallClip;
	public AudioClip knockClip;
	public AudioClip winClip;

	public AudioClip titleMusic;
	public AudioClip stageMusic;
	

	public void PlayHop() {
		if(jumpPool) { 
			jumpPool.pitch = Random.Range(1.0f, 1.05f);
			jumpPool.PlayOneShot(hopClip, Random.Range (0.75f, 0.8f));
		}
	}

	public void PlayFall() {
		if(fallPool) {
			fallPool.pitch = Random.Range(1.0f, 1.05f);
			fallPool.PlayOneShot(fallClip, Random.Range (0.8f, 0.9f));
		}
	}

	public void PlayKnock() {
		if(fallPool) {
			fallPool.PlayOneShot(knockClip, Random.Range (0.8f, 0.9f));
		}
	}

	public void PlayIntro() {
		if(musicPool) {
			musicPool.Stop();
			musicPool.loop = true;
			musicPool.clip = titleMusic;
			musicPool.Play();
		}
	}

	public void PlayStage() {
		if(musicPool) {
			musicPool.Stop();			
			musicPool.loop = true;
			musicPool.clip = stageMusic;
            musicPool.Play();
        }
    }

	public void PlayWin() {
		if(musicPool) {
			musicPool.Stop();
			musicPool.loop = false;
			musicPool.clip = winClip;
			musicPool.Play();
        }
    }
}

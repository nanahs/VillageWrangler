using UnityEngine;
using System.Collections;

public class AudioArrays : MonoBehaviour 
{
	public AudioSource source;

	public AudioClip[] screamAudioClips;
	public AudioClip[] throwAudioClips;
	public AudioClip[] pickupAudioClips;
	public AudioClip speedBoost;
	public AudioClip strength;
	public AudioClip dizzy;
	public AudioClip intromusic;
	public AudioClip gamemusic;
	public AudioClip endclock;
	public AudioClip pushback;
	public AudioClip score;
	public AudioClip voicestart;
	public AudioClip bear;
	public AudioClip volcano;
	public AudioClip tartrap;

	void Start () {
		
	}
	

	void Update () {

		if(Input.GetKeyDown(KeyCode.Space)){
			playScore();
		}

	}

	public void playSpeedBoost(){
		playAudioClip (speedBoost);
	}

	public void playVolcano(){
		playAudioClip (volcano);
	}

	public void playTartrap(){
		playAudioClip (tartrap);
	}
		
	public void playBear(){
		playAudioClip (bear);
	}

	public void playPushBack(){
		playAudioClip (pushback);
	}

	public void playVoiceStart(){
		playAudioClip (voicestart);
	}

	public void playScore(){
		playAudioClip (score);
	}

	public void playEndClock(){
		playAudioClip (endclock);
	}

	public void playGameMusic(){
		playAudioClip (gamemusic);
	}

	public void playIntroMusic(){
		playAudioClip (intromusic);
	}

	public void playStrength(){
		playAudioClip (strength);
	}

	public void playDizzy(){
		playAudioClip (dizzy);
	}

	public void playPickup(){
		playAudioClip (pickupAudioClips[Random.Range(0, 2)]);
	}

	public void playThrow(){
		playAudioClip (throwAudioClips[Random.Range(0, 2)]);
	}

	public void playScream(){
		playAudioClip (screamAudioClips[Random.Range(0, 5)]);
	}

	public void playAudioClip(AudioClip ac){
		source.PlayOneShot (ac);
	}
}

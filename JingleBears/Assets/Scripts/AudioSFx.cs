using UnityEngine;
using System.Collections;

public class AudioSFx : MonoBehaviour {
	private static AudioSFx _instance;
	public AudioSource AudioEffects;
	public AudioClip sfxBeep;
	public AudioClip sfxClick;
	public AudioClip sfxBuzzer;
	public AudioClip sfxWin;
	public AudioClip sfxLose;

	void Awake() {
		Debug.Log ("AudioSFX - Awake() - " + gameObject.name);
		if(_instance == null) { 
			_instance = this;
		} else {
			Debug.Log ("Destroying self Instance: " + gameObject.name);
			Destroy (this);
		} 
	}

	void OnDestroy() { 
		if(_instance == this) { 
			_instance = null;
		}
	}
	
	public static void PlayAffirmativeBleep() { 
		_instance.AudioEffects.PlayOneShot(_instance.sfxBeep);
	}

	public static void PlayClick() { 
		_instance.AudioEffects.PlayOneShot(_instance.sfxClick);
	}

	public static void PlayBuzzer() {
		_instance.AudioEffects.PlayOneShot(_instance.sfxBuzzer);
	}

	public static void PlayWin() { 
		_instance.AudioEffects.PlayOneShot(_instance.sfxWin);
	}

	public static void PlayLose() { 
		_instance.AudioEffects.PlayOneShot(_instance.sfxLose);
	}

}

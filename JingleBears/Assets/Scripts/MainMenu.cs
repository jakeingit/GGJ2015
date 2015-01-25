using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public AudioSource AudioMusic;
	public HelpController TutorialHelp;

	public void ShowMenu() { 
		gameObject.SetActive(true);
		AudioMusic.Play();
	}

	public void HideMenu() { 
		AudioMusic.Stop();
		gameObject.SetActive(false);
	}

	public void OnPlaySong1() {
		AudioSFx.PlayClick();
		Controller.Instance.LoadSong1();
		HideMenu();
		Controller.Instance.Play();
	}

	public void OnPlaySong2() {
		AudioSFx.PlayClick();
		Controller.Instance.LoadSong2();
		HideMenu();
		Controller.Instance.Play();
	}

	public void OnHelp() { 
		AudioSFx.PlayClick();
		TutorialHelp.Show();
	}

	public void OnExit() { 
		AudioSFx.PlayClick();
		Application.Quit(); //Exit the application
	}
}

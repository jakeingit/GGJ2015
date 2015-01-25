using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public void ShowMenu() { 
		gameObject.SetActive(true);
	}

	public void HideMenu() { 
		gameObject.SetActive(false);
	}

	public void OnPlaySong1() {
		Controller.Instance.LoadSong1();
		HideMenu();
		Controller.Instance.Play();
	}

	public void OnPlaySong2() {
		Controller.Instance.LoadSong2();
		HideMenu();
		Controller.Instance.Play();
	}

	public void OnHelp() { 
		//Show the help image
	}

	public void OnExit() { 
		Application.Quit(); //Exit the application
	}
}

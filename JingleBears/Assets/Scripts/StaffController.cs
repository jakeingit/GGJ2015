using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//This class will read a song and populate the staff with notes from the song
public class StaffController : MonoBehaviour {
	public RectTransform RectStartBar;
	public Canvas CanvasDynamicNotes; 

	public GameObject ParentNoteView;
	public GameObject PrefabNoteView;

	public RectTransform PlayingNote;

	private Vector3 v3TimeTranslate;

	private Song _curSong; 
	private List<NoteView> _Notes = new List<NoteView>();

	public static float kSecondWidth = 300f;
	public static float kHeightOffset = 16;
	
	//Every Frame we will move the 
	void Update() { 
		//We will move the staff for an amount every frame
		if(_curSong != null) { 
			v3TimeTranslate.x = -Time.deltaTime;
			ParentNoteView.transform.Translate(v3TimeTranslate);
		}
	}

	public void LoadSong(Song songToLoad) { 
		//We will clear all of our current note views, and then we will read the song data to populate all of the notes that we need to
		_Notes.Clear();
		//Create enough note views for each fo the notes that we need to display 
		foreach(Note toLoad in songToLoad.Notes) { 
			GameObject newObj = Instantiate(PrefabNoteView) as GameObject;
			NoteView newView = newObj.GetComponent<NoteView>();
			newView.transform.SetParent(ParentNoteView.transform);
			newView.LoadNote(toLoad);
		}

		_curSong = songToLoad;
	}


	public void ShowPlayingNote(int noteID) { 
		//Based on the note ID, we will display the activation note
		PlayingNote.gameObject.SetActive(true);
		PlayingNote.transform.localPosition = new Vector3(0, StaffController.kHeightOffset * (noteID / 2));
	}


	public void HidePlayingNote() { 
		PlayingNote.gameObject.SetActive(false);
	}


	public void ResetSong() { 
		//Reset the note mechanic
		ParentNoteView.transform.localPosition = new Vector3(RectStartBar.rect.width, 0);
	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This class will read a song and populate the staff with notes from the song
public class StaffController : MonoBehaviour  {
	public RectTransform RectStartBar;
	public Canvas CanvasDynamicNotes; 

	public GameObject ParentNoteView;
	public GameObject PrefabNoteView;

	public PlayingNote PlayingNote;

	private Vector3 v3TimeTranslate;

	private Song _curSong; 
	private List<NoteView> _Notes = new List<NoteView>();

	public static float kSecondWidth = 300f;
	
	//Every Frame we will move the 
	public void UpdateStaff() { 
		//We will move the staff for an amount every frame
		if(_curSong != null) { 
			v3TimeTranslate.x = -kSecondWidth * Time.deltaTime * transform.localScale.x;
			ParentNoteView.transform.Translate(v3TimeTranslate);
		}
	}

	public void LoadSong(Song songToLoad) { 
		ResetSong();

		foreach(NoteView toDestroy in _Notes) { 
			Destroy(toDestroy.gameObject);
		}

		//We will clear all of our current note views, and then we will read the song data to populate all of the notes that we need to
		_Notes.Clear();
		//Create enough note views for each fo the notes that we need to display 
		foreach(Note toLoad in songToLoad.Notes) { 
			GameObject newObj = Instantiate(PrefabNoteView) as GameObject;
			NoteView newView = newObj.GetComponent<NoteView>();
			newView.transform.SetParent(ParentNoteView.transform, true);
			newView.LoadNote(toLoad);
			_Notes.Add(newView);
		}

		_curSong = songToLoad;
	}

	public void ResetSong() { 
		//Reset the note mechanic
		ParentNoteView.GetComponent<RectTransform>().anchoredPosition = new Vector2(264, 0);
	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Song  {
	public List<Note> Notes = new List<Note>(); 
	public int BPM;

	private int _curNoteOffsetIndex; //This is the index that we offset our
	private HashSet<Note> _curNotes = new HashSet<Note>();

	//This will get a collection of all of the notes that are currently playing or null if nothing
	public HashSet<Note> CurrentNotes { 
		get { return _curNotes; }
	}

	public void ResetSongProgression() { 
		_curNoteOffsetIndex = 0; //Start at the beginning when searching
	}

	//This is the song's update mechanic allowing us to update the song's notes to be set to a current time
	public void UpdateSongTime(float curSongTime) { 
		//Update the currently playing notes, based on this value's start time
		Note curNote = null;

		//Clear any active notes that are no longer playing
		_curNotes.RemoveWhere(item => { 
			if(item.StartTime + item.Duration < curSongTime) { 
				Debug.Log("Removing Note: " + item);
				return true;
			}
			return false;
		});

		//Add any new notes that may have become active
		for(int i = _curNoteOffsetIndex; i < Notes.Count; i++) { 
			curNote = Notes[i];
			if(curNote.StartTime < curSongTime) { 
				if(curNote.StartTime + curNote.Duration > curSongTime) { 
					Debug.Log ("Adding Note: " + curNote);
					_curNotes.Add(curNote);
				}
				_curNoteOffsetIndex = i + 1; //This is now the index that we are offset by
			}
		}
	}

	//Factory method that is used to create the test song that we will be usign for testing purposes
	public static Song CreateTestSong() { 
		Song retVal = new Song();
		retVal.Notes.Clear();
		for(int i = 0; i < 100; i++) { 
			retVal.Notes.Add(new Note(8, i * 2f + 0.5f, 1.5f));
		}
//		retVal.Notes.Add(new Note(0, 2f, 0.25f));
//		retVal.Notes.Add(new Note(2, 2.25f, 0.25f));
//		retVal.Notes.Add(new Note(4, 2.5f, 0.25f));
//		retVal.Notes.Add(new Note(6, 2.75f, 0.25f));
//		retVal.Notes.Add(new Note(8, 3f, 1.0f));
		retVal.Notes.Sort();
		retVal.BPM = 80;
		retVal._curNoteOffsetIndex = 0;
		return retVal;
	}
}

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
				return true;
			}
			return false;
		});

		//Add any new notes that may have become active
		for(int i = _curNoteOffsetIndex; i < Notes.Count; i++) { 
			curNote = Notes[i];
			if(curNote.StartTime < curSongTime) { 
				if(curNote.StartTime + curNote.Duration > curSongTime) { 
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
//		for(int i = 0; i < 100; i++) { 
//			retVal.Notes.Add(new Note(8 + (i % 2) * 8, i * 5f + 0.5f, 1.5f));
//		}

		//C-0
		//D-2
		//E-4
		//F-6
		//G-8
		//A-10
		//B-12
		//C-14
		//D-16
		//E-18
		//F-20
		//G-22
		//A-24

		retVal.Notes.Add(new Note(18, 0f, 1.13f));
		retVal.Notes.Add(new Note(8, 1.33f, 1.133f));
		retVal.Notes.Add(new Note(14, 2.66f, 0.466f));
		retVal.Notes.Add(new Note(12, 3.33f, 0.466f));
		retVal.Notes.Add(new Note(4, 4f, 0.466f));
		retVal.Notes.Add(new Note(8, 4.666f, 0.466f));
//		retVal.Notes.Add(new Note(4, 6.21f, 0.04f));
//		retVal.Notes.Add(new Note(2, 7.08f, 0.04f));
//		retVal.Notes.Add(new Note(4, 7.12f, 0.04f));
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

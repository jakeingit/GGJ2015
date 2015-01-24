using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Song  {
	public List<Note> Notes = new List<Note>(); 
	public int BPM;

	//Factory method that is used to create the test song that we will be usign for testing purposes
	public static Song CreateTestSong() { 
		Song retVal = new Song();
		retVal.Notes.Clear();
		retVal.Notes.Add(new Note(8, 0.5f, 1.5f));
		retVal.Notes.Add(new Note(0, 2f, 0.25f));
		retVal.Notes.Add(new Note(2, 2.25f, 0.25f));
		retVal.Notes.Add(new Note(4, 2.5f, 0.25f));
		retVal.Notes.Add(new Note(6, 2.75f, 0.25f));
		retVal.Notes.Add(new Note(8, 3f, 1.0f));
		retVal.BPM = 80;
		return retVal;
	}
}

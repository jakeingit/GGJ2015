using UnityEngine;
using System.Collections;

public class Note : System.IComparable<Note> {
	public int NoteID; //This is an int step between letters on the keyboard. A->B = 2, B->C = 2. All sharps and flats are 1. 
	public float StartTime; //This is the start time of the note as it it supposed to play
	public float Duration; //This is the duration of the note in seconds 

	public Note(int newNote, float newStartTime, float newDuration) { 
		NoteID = newNote;
		StartTime = newStartTime;
		Duration = newDuration;
	}

	public override string ToString ()
	{
		return string.Format("[Note ID={0} StartTime={1} Duration={2}]", NoteID, StartTime, Duration);
	}

	//When we are sorting ourselves in a list, we will compare our start times to determine the order in which the notes should appear
	public int CompareTo(Note other) { 
		return StartTime.CompareTo(other.StartTime);
	}
}

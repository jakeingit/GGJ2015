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

	public static string ConvertNoteIDToName(int noteID) { 
		switch(noteID) { 
		case 0:
			return "C";
		case 2:
			return "D";
		case 4:
			return "E";
		case 6:
			return "F";
		case 8:
			return "G";
		case 10:
			return "A";
		case 12:
			return "B";
		case 14:
			return "C";
		case 16:
			return "D";
		case 18:
			return "E";
		case 20:
			return "F";
		case 22:
			return "G";
		case 24:
			return "A";
		default:
			return string.Empty;
		}
	}

}

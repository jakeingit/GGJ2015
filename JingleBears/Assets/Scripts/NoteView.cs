﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NoteView : MonoBehaviour {
	private Note _model;
	private RectTransform _rectTrans;
	public Image ImageBG;
	public Text TxtLeft;
	public Text TxtRight;

	public static float kOctaveOffset = 50f;

	public Note Model { 
		get { return _model; }
	}

	void Awake() { 
		_rectTrans = GetComponent<RectTransform>();
	}

	//Based on the duration we will load the bar as appropriate
	public void LoadNote(Note noteToLoad) { 
		_model = noteToLoad;
		//Based on the duration we will load the distance, and position
		_rectTrans.localScale = Vector3.one;
		_rectTrans.localPosition = new Vector3(StaffController.kSecondWidth * noteToLoad.StartTime, noteToLoad.NoteID >= 14 ? kOctaveOffset * 4 : kOctaveOffset);
		_rectTrans.sizeDelta = new Vector2(StaffController.kSecondWidth * noteToLoad.Duration, _rectTrans.sizeDelta.y);
		TxtLeft.text = TxtRight.text = Note.ConvertNoteIDToName(noteToLoad.NoteID);
	}
}

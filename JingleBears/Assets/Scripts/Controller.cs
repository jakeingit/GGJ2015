using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Controller : MonoBehaviour  {
	private static Controller _instance; 
	public static Controller Instance { 
		get { return _instance; }
	}

	public StaffController Staff;

	private Song _curSong; 
	public Song CurSong { 
		get { return _curSong; }
	}

	private float _curEnergy; 
	private float _lastEnergy;
	public float CurrentEnergy { 
		get { return _curEnergy; }
	}

	private float _startStartTimeStamp;
	private float _songTime;
	public float SongPlayTime { 
		get { return _songTime; }
	}

	private bool _songPlaying = false;
	public bool IsPlaying { 
		get { return _songPlaying; }
	}
	
	private const float kBaseScoreMultiplier = .05f; //100 pts per second of accurate song playing

	//User Input
	private int _userNoteID; //This is the note that the user is currently using
	private int _newUserNote;
	private int _minNote = 0;
	private int _maxNote = 24;


	public int CurPoints; //How many points has the user accumulated so far
	public UnityEngine.UI.Slider SliderEnergy; //This is the UI reference to the points that the player currently has

	public PlayingNote UserNote;
	private bool _isUserPlayingNote = false;

	void Awake() { 
		//Singleton Check
		if(_instance != null && _instance != this) { 
			Destroy(this);
		} if(_instance == null) { 
			_instance = this; 
		} 
	}

	void Start() { 
		_curEnergy = _lastEnergy = 0.5f;
		UserNote.gameObject.SetActive(false);
		LoadTestSong();
	}
	
	// Update is called once per frame
	void Update () {
		if(_curSong != null && _songPlaying) { 
			HandleInput();	
			ScorePoints();
		}
	}

	private void HandleInput() { 
		//Handle the keyboard input so that we can move up and down the staff
		_newUserNote = _userNoteID;
		if(Input.GetButtonDown("NoteDown")) { 
			//We move up or down the note hierarchy
			_newUserNote -= 2;
		} else if(Input.GetButtonDown("NoteUp")) { 
			_newUserNote += 2;
		}

		//Bounds the user note
		_newUserNote = Mathf.Clamp(_newUserNote, _minNote, _maxNote);

		//Display the user's input note currently
		if(_newUserNote != _userNoteID) { 
			Debug.Log ("Showing Note: " + _newUserNote);
			UserNote.ShowNote(_newUserNote);
			_userNoteID = _newUserNote;
		}

		_isUserPlayingNote = Input.GetButton("NoteActivate");
	}

	//Score points based on the values that are enabled
	private void ScorePoints() { 
		//Based on the current play mask determine if we are playing the current note or not
		_songTime = Time.time - _startStartTimeStamp;
		_curSong.UpdateSongTime(_songTime);
		//Now we will get the current notes that are playing and determine how many bit flags match
		uint curFlag;
		int numFailNotes = 0;
		foreach(Note toCheck in _curSong.CurrentNotes) { 
			if(toCheck.NoteID == _userNoteID && _isUserPlayingNote) { 
				_curEnergy += Time.deltaTime * kBaseScoreMultiplier * 2f;
			} else { 
				numFailNotes++;
			}
		}

		if(_curSong.CurrentNotes.Count == 0 && _isUserPlayingNote) { 
			numFailNotes++;
		}

		_curEnergy += Time.deltaTime * -kBaseScoreMultiplier * Mathf.Min(numFailNotes, 1);
		_curEnergy = Mathf.Clamp01(_curEnergy);


		if(_isUserPlayingNote) { 
			UserNote.ShowPlaying(numFailNotes == 0);
		} else { 
			UserNote.StopPlaying();
		}

		//For every note that we are still playing, we will determine what we gain/lost
		UpdateEnergy();
	}

	private void UpdateEnergy() { 
		//Only update if we have a change 
		if((int)_curEnergy != _lastEnergy) {
			_lastEnergy = _curEnergy;
			SliderEnergy.value = _curEnergy;
		}
	}

	private void LoadTestSong() { 
		//Create a temporary song
		_curSong = Song.CreateTestSong();
		Staff.LoadSong(_curSong);
		_startStartTimeStamp = Time.time;
		_songPlaying = true;
	}
}

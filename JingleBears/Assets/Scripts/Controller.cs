using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {
	public static Controller _instance; 

	public StaffController Staff;

	private Song _curSong; 
	public Song CurSong { 
		get { return _curSong; }
	}

	private float _curEnergy; 
	private float _lastEnergy;
	public float CurrentEnergy { 
		get { return (int)_curEnergy; }
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

	private uint _playBitMask; //This is an unsigned int that we can use to track the play bit mask

	public int CurPoints; //How many points has the user accumulated so far
	public UnityEngine.UI.Slider SliderEnergy; //This is the UI reference to the points that the player currently has

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
		Staff.HidePlayingNote();
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
		//Check for Octave Higher
		int curKey = -1;
		_playBitMask = 0;
		if(Input.GetButton("C")) { 
			_playBitMask = _playBitMask | 1 << 0;
			curKey = 0;
		}
		if(Input.GetButton("D")) { 
			_playBitMask = _playBitMask | 1 << 2;
			curKey = 2;
		} 
		if(Input.GetButton("E")) { 
			_playBitMask = _playBitMask | 1 << 4;
			curKey = 4;
		}
		if(Input.GetButton("F")) { 
			_playBitMask = _playBitMask | 1 << 6;
			curKey = 6;
		}
		if(Input.GetButton("G")) { 
			_playBitMask = _playBitMask | 1 << 8;
			curKey = 8;
		} if(Input.GetButton("A")) { 
			_playBitMask = _playBitMask | 1 << 10;
			curKey = 10;
		} 
		if(Input.GetButton("B")) { 
			_playBitMask = _playBitMask | 1 << 12;
			curKey = 12;
		}

		if(curKey >= 0 && Input.GetButton("OctaveHigh")) { 
			curKey += 16;
			_playBitMask = _playBitMask ^ 1 << 12;
		} 

		if(curKey >= 0) { 
			Staff.ShowPlayingNote(curKey);
		} else { 
			Staff.HidePlayingNote();
		}
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
			curFlag = (uint)(1 << toCheck.NoteID);
			if((curFlag & _playBitMask) != 0) { 
				_playBitMask = _playBitMask ^ curFlag;
				_curEnergy += Time.deltaTime * kBaseScoreMultiplier * 2f;
			} else { 
				numFailNotes++;
			}
		}

		//Determine if we're losing any energy
		for(int i = 0; i < 32; i++) { 
			curFlag = (uint)(1 << i); 
			if((curFlag & _playBitMask) != 0) {
				numFailNotes++;
			}
		}

		_curEnergy += Time.deltaTime * -kBaseScoreMultiplier * Mathf.Min(numFailNotes, 1) * 0.5f;
		_curEnergy = Mathf.Clamp01(_curEnergy);

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

using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public static Controller _instance; 

	public StaffController Staff;

	private Song _curSong; 
	public Song CurSong { 
		get { return _curSong; }
	}

	public int CurPoints; //How many points has the user accumulated so far

	void Awake() { 
		//Singleton Check
		if(_instance != null && _instance != this) { 
			Destroy(this);
		} if(_instance == null) { 
			_instance = this; 
		} 
	}

	void Start() { 
		Staff.HidePlayingNote();
		LoadTestSong();
	}
	
	// Update is called once per frame
	void Update () {
		if(_curSong != null) { 
			HandleInput();	
		}
	}

	private void HandleInput() { 
		//Check for Octave Higher
		int curKey = -1;

		if(Input.GetButton("C")) { 
			curKey = 0;
		} else if(Input.GetButton("D")) { 
			curKey = 2;
		} else if(Input.GetButton("E")) { 
			curKey = 4;
		} else if(Input.GetButton("F")) { 
			curKey = 6;
		} else if(Input.GetButton("G")) { 
			curKey = 8;
		} else if(Input.GetButton("A")) { 
			curKey = 10;
		} else if(Input.GetButton("B")) { 
			curKey = 12;
		}

		if(curKey >= 0 && Input.GetButton("OctaveHigh")) { 
			curKey += 16;
		} 

		if(curKey >= 0) { 
			Staff.ShowPlayingNote(curKey);
		} else { 
			Staff.HidePlayingNote();
		}
	}


	private void LoadTestSong() { 
		//Create a temporary song
		_curSong = Song.CreateTestSong();
		Staff.LoadSong(_curSong);
	}
}

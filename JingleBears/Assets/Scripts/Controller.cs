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
		LoadTestSong();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	private void LoadTestSong() { 
		//Create a temporary song
		_curSong = Song.CreateTestSong();
		Staff.LoadSong(_curSong);
	}
}

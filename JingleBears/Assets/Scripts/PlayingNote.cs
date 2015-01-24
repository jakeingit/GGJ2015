using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayingNote : MonoBehaviour {
	public Image ImageNote;
	public Image ImageBar;

	private RectTransform _ourTrans;

	private int OffsetY = 16;

	void Awake() { 
		_ourTrans = GetComponent<RectTransform>();
	}

	public void ShowNote(int noteID) { 
		gameObject.SetActive(true);
		ImageBar.gameObject.SetActive(noteID % 4 == 0);
		_ourTrans.anchoredPosition = new Vector3(-80, 30 + OffsetY * noteID / 2);
		//TODO: Flip the note on higher notes
	}

	public void ShowPlaying(bool isCorrect) { 
		ImageBar.color = ImageNote.color = isCorrect ? Color.green : Color.red;
	}

	public void StopPlaying() { 
		ImageBar.color = ImageNote.color = Color.black;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;


//We will hande the mouse input so that we can pick the correct note
public class MouseArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	//We will use some hard-coded logic to help get the position of the mouse in this area so that we can notify the controller that this is going to be the user's suggested note

	public bool MouseOver = false;
	private Vector3 _newPosition;
	public Vector3 _prevPosition;

	public void OnPointerEnter(PointerEventData pe) {
		//Debug.Log("OnPointerEnter");
		MouseOver = true;
	}

	public void OnPointerExit(PointerEventData pe) { 
		//Debug.Log ("OnPointerExit");
		MouseOver = false;
	}

	void Update() { 
		if(MouseOver) { 
			//Determine which note we're at based on the mouse's position
			_newPosition = Input.mousePosition;
			if(_newPosition != _prevPosition) { 
				Debug.Log ("New Mouse Over Position: " + Input.mousePosition);
				float mouseY = Input.mousePosition.y;
				if(mouseY < 40f) { 
					Controller.Instance.SetUserNote(0);
				} else if(mouseY > 260) { 
					Controller.Instance.SetUserNote(24);
				} else { 
					int middleNoteID = Mathf.RoundToInt((mouseY - 30f) / 19f) * 2;
					Controller.Instance.SetUserNote(middleNoteID);
				} 
				_prevPosition = _newPosition;
			}

			//Controller.Instance.SetActivateNote(Input.GetMouseButton(0));
		}
	}
}


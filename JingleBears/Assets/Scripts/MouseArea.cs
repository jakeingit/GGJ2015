using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;


//We will hande the mouse input so that we can pick the correct note
public class MouseArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	//We will use some hard-coded logic to help get the position of the mouse in this area so that we can notify the controller that this is going to be the user's suggested note
	public Camera camUI;
	public UnityEngine.UI.Image ImageArea;
	public UnityEngine.Canvas CanvasParent;
	public bool MouseOver = false;
	private Vector3 _newPosition;
	public Vector3 _prevPosition;

	private const float kWorldTop = -23;
	private const float kWorldBot = -54;

	private RectTransform rectImageArea;

	private Bounds _areaBounds;
	private Vector2 _v2MousePos;

	void Start() { 
		rectImageArea = ImageArea.GetComponent<RectTransform>();
	}

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
			_v2MousePos.Set(Input.mousePosition.x, Input.mousePosition.y);
			//Determine which note we're at based on the mouse's position
			RectTransformUtility.ScreenPointToWorldPointInRectangle(rectImageArea, _v2MousePos, camUI, out _newPosition);
			//Debug.Log("New Mouse Position X: " + _newPosition.x + " Y: " + _newPosition.y + " Z: " + _newPosition.z);
			if(_newPosition != _prevPosition) { 
				//Debug.Log ("New Mouse Over Position: " + Input.mousePosition);
				float mouseY = _newPosition.y;
				if(mouseY < kWorldBot) { 
					Controller.Instance.SetUserNote(Controller.MinNoteID);
				} else if(mouseY > kWorldTop) { 
					Controller.Instance.SetUserNote(Controller.MaxNoteID);
				} else { 
					int middleNoteID = Controller.MaxNoteID + Mathf.RoundToInt((mouseY - kWorldTop) / 2.5f) * 2;
					Controller.Instance.SetUserNote(middleNoteID);
				} 
				_prevPosition = _newPosition;
			}

			//Controller.Instance.SetActivateNote(Input.GetMouseButton(0));
		}
	}
}


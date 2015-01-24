using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;


//We will hande the mouse input so that we can pick the correct note
public class InputArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


	public void OnPointerEnter(PointerEventData pe) {
		Debug.Log ("OnPointerEnter");
	}

	public void OnPointerExit(PointerEventData pe) { 
		Debug.Log ("OnPointerExit");
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndGameDialog : MonoBehaviour, IPointerClickHandler {
	public Animator Anim; 
	public Text TxtTitle;

	private bool _isHiding = false;

	public void ShowDialog(string DialogTitle) { 
		if(gameObject.activeInHierarchy == false) {
			_isHiding = false;
			gameObject.SetActive(true);
			TxtTitle.text = DialogTitle;
			Anim.SetTrigger("Show");
		}
	}

	public void OnPointerClick(PointerEventData eventData) { 
		return; //Handle this event
	}

	public void HideDialog() { 
		if(!_isHiding && gameObject.activeInHierarchy) { 
			_isHiding = true;
			Anim.SetTrigger("Hide");
			StartCoroutine(CoroutineWaitAndHide());
		}
	}

	private IEnumerator CoroutineWaitAndHide() { 
		yield return new WaitForSeconds(0.5f);
		gameObject.SetActive(false);
	}
}

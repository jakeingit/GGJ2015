using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class HelpController : MonoBehaviour {
	public Animator Anim;
	public RawImage TexTutorial;
	public Button ButtonNext;
	public Button ButtonPrev;

	private int _curPage;

	private const int kMinPageID = 1;
	private const int kMaxPageID = 5;

	public void Show() { 
		gameObject.SetActive(true);
		_curPage = kMinPageID;
		LoadPage(_curPage);
		UpdateArrows();
	}

	public void Hide() { 
		gameObject.SetActive(false);
	}

	public void NextPage() { 
		_curPage = Mathf.Min(kMaxPageID, ++_curPage);
		UpdateArrows();
		LoadPage(_curPage);
	}

	public void PrevPage() { 
		_curPage = Mathf.Max(kMinPageID, --_curPage);
		UpdateArrows();
		LoadPage(_curPage);
	}

	private void UpdateArrows() { 
		ButtonNext.gameObject.SetActive(true);
		ButtonPrev.gameObject.SetActive(true);
		if(_curPage == kMaxPageID) { 
			ButtonNext.gameObject.SetActive(false);
		} else if(_curPage == kMinPageID) { 
			ButtonPrev.gameObject.SetActive(false);
		}
	}

	private void LoadPage(int pageToLoad) { 
		Texture curTex = TexTutorial.texture;
		//Resources.UnloadAsset(curTex);
		TexTutorial.texture = Resources.Load<Texture>(string.Format("Tutorial/tutorial{0}", _curPage));
		Debug.Log("Loading Tutorial: " + string.Format("Tutorial/tutorial{0}", _curPage));
	}
}

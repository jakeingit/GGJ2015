using UnityEngine;
using System.Collections;

public class Dancer : MonoBehaviour {
	public Animator anim;

	public enum DancerStates {
		Dead,
		Bad,
		Neutral,
		Good
	}
	private DancerStates _curState;
	private DancerStates _newState;
	public float BadCutoff;
	public float GoodCutoff;


	void Start() { 
		StartCoroutine(CoroutineGoRandom());
	}

	// Update is called once per frame
	void Update () {
		_newState = GetStateForEnergy(Controller.Instance.CurrentEnergy);
		if(_newState != _curState) { 
			switch(_newState) { 
			case DancerStates.Bad:
				anim.SetTrigger("GoNegative");
				break;
			case DancerStates.Neutral:
				anim.SetTrigger("GoNeutral");
				break;
			case DancerStates.Good:
				anim.SetTrigger("GoPositive");
				break;
			case DancerStates.Dead:
				anim.SetTrigger("GoNeutral");
				break;
			}
			_curState = _newState;
		}
	}

	private DancerStates GetStateForEnergy(float curEnergy) { 
		if(curEnergy < BadCutoff) { 
			return DancerStates.Bad;
		} if(curEnergy > GoodCutoff) { 
			return DancerStates.Good;
		} else { 
			return DancerStates.Neutral;
		}
	}

	private IEnumerator CoroutineGoRandom() { 
		while(true) { 
			anim.ResetTrigger("Trigger1");
			anim.ResetTrigger("Trigger2");
			if(Random.Range(0, 100) % 2 == 0) { 
				anim.SetTrigger("Trigger1");
			} else { 
				anim.SetTrigger("Trigger2");
			}
			yield return new WaitForSeconds(Random.Range(2, 5));
		}
	}
}

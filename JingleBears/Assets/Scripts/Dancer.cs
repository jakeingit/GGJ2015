using UnityEngine;
using System.Collections;

public class Dancer : MonoBehaviour {
	public Animator anim;
	
	void Start() { 
		StartCoroutine(CoroutineGoRandom());
	}

	// Update is called once per frame
	void Update () {
		anim.SetFloat("Energy", Controller.Instance.CurrentEnergy);
	}


	private IEnumerator CoroutineGoRandom() { 
		while(true) { 
			anim.SetTrigger("GoRandom");
			yield return new WaitForSeconds(Random.Range(10, 30));
		}
	}
}

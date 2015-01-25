using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Scripte to manage the environment based on the energy of the game
public class EnvironmentEnergy : MonoBehaviour {
	public List<ParticleSystem> Positive;
	public List<ParticleSystem> Negative;
	
	private Controller _gameController;

	private const float kNeutral = 0.5f;
	private const float kEnergyStepValue = 0.2f;

	private float _lastEnergyCheck = -1f;
	private int _curEnvironmentValue; 
	private int _newEnvironmentValue;

	// Use this for initialization
	void Start () {
		_gameController = Controller.Instance;
		StopAll();
		Debug.Log("Start");
	}
	
	// Update is called once per frame
	void Update () {
		//We will constantly be checking energy to see if we need to switch up the particles
		if(_gameController.CurrentEnergy == _lastEnergyCheck) { return; }
		_newEnvironmentValue = DetermineEnvironmentEnergy(_gameController.CurrentEnergy);
		if(_newEnvironmentValue != _curEnvironmentValue) { 
			Debug.Log ("New Environment Value: " + _newEnvironmentValue);
			_curEnvironmentValue = _newEnvironmentValue;
			if(_curEnvironmentValue < 0) { 
				SetParticleActive(Positive, 0); //Stop positive effects
				SetParticleActive(Negative, Mathf.Abs(_curEnvironmentValue)); //Show some negative particles
			} else if(_curEnvironmentValue > 0) { 
				SetParticleActive(Positive, Mathf.Abs(_curEnvironmentValue));
				SetParticleActive(Negative, 0);
			} else { 
				StopAll();
			}
		}
	}

	public void ResetEnergy() { 
		StopAll(true);
		_curEnvironmentValue = 0;
	}

	private void StopAll(bool clearParticles = false) { 
		for(int i = 0; i < Positive.Count; i++) {
			Positive[i].enableEmission = false;
			if(clearParticles) { 
				Positive[i].Clear();
			}
		}

		for(int i = 0; i < Negative.Count; i++) { 
			Negative[i].enableEmission = false;
			if(clearParticles) { 
				Negative[i].Clear();
			}
		}
	}


	private int DetermineEnvironmentEnergy(float curEnergy) { 
		float energyDiff = curEnergy - kNeutral;
		if(Mathf.Abs(energyDiff) < kEnergyStepValue) {
			return 0;
		} else {
			return Mathf.RoundToInt(energyDiff / kEnergyStepValue);
		}
	}

	private void SetParticleActive(List<ParticleSystem> particles, int newActive) {
		//Find out how many are active, and determine if we need to add an active or not
		int currentActiveCount = particles.Count(pSys => { 
			return pSys.enableEmission;
		});
		
		int diff = newActive - currentActiveCount;
		Debug.Log ("Setting NumParticles Active: " + currentActiveCount + " NewActive: " + newActive);
		//If we have a net increase in particle requests, and we have enough particle systems to activate, pick one at random and activate it. 
		if(diff > 0 && particles.Count > currentActiveCount) { 
			List<ParticleSystem> inactiveParticles = particles.FindAll(pSys => { 
				return pSys.enableEmission == false;
			});
			
			SetEmissionCountOnParticlePool(inactiveParticles, Mathf.Abs(diff), true);
		} 
		//If we have a net decrease in particle requests, pick a random positive effect and deactivate them
		else if(diff < 0) { 
			List<ParticleSystem> activeParticles = particles.FindAll(pSys => { 
				return pSys.enableEmission == true;
			});
			
			SetEmissionCountOnParticlePool(activeParticles, Mathf.Abs(diff), false);
		}
	}
	
	private void SetEmissionCountOnParticlePool(List<ParticleSystem> particlePool, int numToPick, bool setEmission) { 
		if(particlePool.Count == 0) { 
			return;
		}
		
		int randIndex;
		for(int i = 0; i < numToPick && particlePool.Count > 0; i++) {
			randIndex = Random.Range(0, particlePool.Count);
			particlePool[randIndex].enableEmission = setEmission;
			particlePool.RemoveAt(randIndex);
		}
	}

}

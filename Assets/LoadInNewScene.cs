using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInNewScene : MonoBehaviour {

	static LoadInNewScene instance;

	void Awake(){
		if (instance) {
			Destroy (gameObject);
			return;
		}
		instance = this;

		DontDestroyOnLoad (this);
	}
}

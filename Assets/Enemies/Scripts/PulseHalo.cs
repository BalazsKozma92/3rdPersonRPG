using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseHalo : MonoBehaviour {

	Light halo;
	float originalRange;
	float duration = 1.5f;

	// Use this for initialization
	void Start () {
		halo = GetComponent<Light> ();
		originalRange = halo.range;
	}
	
	// Update is called once per frame
	void Update () {
		float amplitude = Mathf.PingPong (Time.time, duration);
		amplitude = amplitude / duration * 0.5f + 0.5f;
		halo.range = originalRange * amplitude;
	}
}

// 0 (0 amp)   0

// 0.56 (0.2 amp) 0,168

// 0,76 (0.8 amp) 0,224

// 1 (1.5 amp) 0,3

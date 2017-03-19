using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMe : MonoBehaviour {

	[SerializeField] float zRotationsPerMinute = 1f;

	// DegreesPerFrame = Time.deltaTime, 60, 360, RotationPerMinute
	// degrees frame^-1 = seconds frame^-1, seconds minute^-1, degrees rotation^-1, rotation minute^-1
	// degrees frame^-1 = seconds frame^-1 / seconds minute^-1, degrees rotation^-1, rotation minute^-1
	// degrees frame^-1 = frame^-1 minute, degrees rotation^-1 * rotation minute^-1
	// degrees frame^-1 = frame^-1 minute * degrees minute^-1
	// degrees frame^-1 = frame^-1 * degrees

	void Update () {
		float zDegreesPerFrame = Time.deltaTime / 60 * 360 * zRotationsPerMinute;
        transform.RotateAround (transform.position, transform.forward, zDegreesPerFrame);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskRotation : MonoBehaviour {

	RectTransform globeMask;

	void Start () {
		globeMask = GetComponent<RectTransform> ();
	}
	
	void Update () {
		globeMask.Rotate (Vector3.back, Time.deltaTime * 12f);
	}
}

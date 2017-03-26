using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSec : MonoBehaviour {

	[SerializeField] float sec = 0;
	Color color;

	void Start () {
		color = transform.GetComponentInChildren<MeshRenderer> ().material.color;
		Invoke ("DestroySelf", sec);
	}

	void Update(){
		if (color.a > 0) {
			color.a -= .2f * Time.deltaTime;
		}
	}

	void DestroySelf(){
		Destroy (gameObject);
	}
}
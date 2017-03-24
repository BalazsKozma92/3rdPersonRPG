using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float projectileSpeed;
	[HideInInspector] public float damageCaused;

	void Start(){
		Invoke ("DestroySelf", 5f);
	}

	void DestroySelf(){
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider collider){
		Component damagable = collider.gameObject.GetComponent (typeof(IDamagable));
		if (damagable && collider.gameObject.tag == "Player") {
			DestroySelf ();
			(damagable as IDamagable).TakeDamage (damageCaused);
		}
	}
}

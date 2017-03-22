using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float projectileSpeed;
	[HideInInspector] public float damageCaused;

	void OnTriggerEnter(Collider collider){
		Component damagable = collider.gameObject.GetComponent (typeof(IDamagable));
		if (damagable && collider.gameObject.tag == "Player") {
			(damagable as IDamagable).TakeDamage (damageCaused);
		}
	}
}

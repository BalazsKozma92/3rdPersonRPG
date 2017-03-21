using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float projectileSpeed;
	public float damageCaused;

	void OnTriggerEnter(Collider collider){
		Component damagable = collider.gameObject.GetComponent (typeof(IDamagable));
		if (damagable) {
			(damagable as IDamagable).TakeDamage (damageCaused);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] float damageCaused = 12.2f;

	void OnTriggerEnter(Collider collider){
		Component damagable = collider.gameObject.GetComponent (typeof(IDamagable));
		if (damagable && collider.gameObject.tag == "Enemy") {
			(damagable as IDamagable).TakeDamage (damageCaused);
		}
	}
}

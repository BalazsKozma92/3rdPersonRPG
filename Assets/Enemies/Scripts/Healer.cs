using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

public class Healer : MonoBehaviour {

	[SerializeField] float damagePerHeal = 8.3f;
	[SerializeField] float secondsBetweenHeals = 0.5f;

	bool isHealing = false;

	GameObject player = null;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update(){

		if (!isHealing) {
			isHealing = true;
			InvokeRepeating ("HealPlayer", 0.002f, secondsBetweenHeals);
		}
	}

	void HealPlayer(){
		player.GetComponent<IDamagable> ().TakeDamage (damagePerHeal);
	}
}

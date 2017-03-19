using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour {

	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] float attackRadius = 5f;

	float currentHealthPoints = 100f;
	AICharacterControl aiCharControl= null;
	GameObject player = null;
	GameObject basePoint;
	GameObject thisBasePoint;

	public float healthAsPercentage{
		get{
			return currentHealthPoints / maxHealthPoints; 
		}
	}

	void Start(){
		basePoint = GameObject.Find("EnemyBasePoint");
		thisBasePoint = Instantiate (basePoint, transform.position, transform.rotation, transform.parent);

		player = GameObject.FindGameObjectWithTag ("Player");
		aiCharControl = GetComponent<AICharacterControl> ();
	}

	void Update(){
		float distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);
		if (distanceToPlayer <= attackRadius) {
			aiCharControl.SetTarget (player.transform);
		} else {
			aiCharControl.SetTarget (thisBasePoint.transform);
		}
	}
}

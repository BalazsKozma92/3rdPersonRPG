using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable {

	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] float attackRadius = 5f;

	float currentHealthPoints = 100f;
	AICharacterControl aiCharControl= null;
	GameObject player = null;
	[SerializeField] GameObject basePoint;
	GameObject thisBasePoint;
	//Animator anim;
//	NavMeshAgent navMeshAgent;

	public float healthAsPercentage{
		get{ return currentHealthPoints / maxHealthPoints; }
	}

	void IDamagable.TakeDamage(float damage){
		currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);
		if (currentHealthPoints <= 0) {
			Destroy (gameObject);
			Destroy (thisBasePoint.gameObject);
		}
	}

	void Start(){
//		navMeshAgent = GetComponent<NavMeshAgent> ();

//		anim = GetComponent<Animator> ();
//		anim.SetBool ("isIdle", true);
//		anim.SetBool ("isRunning", false);
//		anim.SetBool ("isAttacking", false);

		thisBasePoint = Instantiate (basePoint, transform.position, transform.rotation, transform.parent);

		player = GameObject.FindGameObjectWithTag ("Player");
		aiCharControl = GetComponent<AICharacterControl> ();

	}

	void Update(){
		float distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);
//		float distanceToBasePoint = Vector3.Distance (thisBasePoint.transform.position, transform.position);
		if (distanceToPlayer <= attackRadius) {
//			anim.SetBool ("isIdle", false);
//			anim.SetBool ("isRunning", true);
			aiCharControl.SetTarget (player.transform);
//			if (distanceToPlayer <= navMeshAgent.stoppingDistance) {
//				anim.SetBool ("isRunning", false);
//				anim.SetBool ("isAttacking", true);
//			}
		} else {
//			anim.SetBool ("isRunning", true);
//			anim.SetBool ("isAttacking", false);
			aiCharControl.SetTarget (thisBasePoint.transform);
//			if (distanceToBasePoint <= navMeshAgent.stoppingDistance) {
//				anim.SetBool ("isIdle", true);
//				anim.SetBool ("isRunning", false);
//			}
		}
	}
}

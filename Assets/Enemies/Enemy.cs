using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable {

	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] float moveRadius = 3f;
	[SerializeField] float attackRadius = 6f;
	[SerializeField] float damagePerShot = 8.3f;
	[SerializeField] Projectile projectileToUse;
	[SerializeField] GameObject projectileSocket;

	float currentHealthPoints = 100f;
	AICharacterControl aiCharControl= null;
	GameObject player = null;
	[SerializeField] GameObject basePoint;
	GameObject thisBasePoint;


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
		thisBasePoint = Instantiate (basePoint, transform.position, transform.rotation, transform.parent);

		player = GameObject.FindGameObjectWithTag ("Player");
		aiCharControl = GetComponent<AICharacterControl> ();

	}

	void Update(){
		float distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);

		if (distanceToPlayer <= attackRadius) {
			SpawnProjectile ();
		}

		if (distanceToPlayer <= moveRadius) {
			aiCharControl.SetTarget (player.transform);
		} else {
			aiCharControl.SetTarget (thisBasePoint.transform);
		}
	}

	void SpawnProjectile(){
		Projectile newProjectile = Instantiate (projectileToUse, projectileSocket.transform.position, Quaternion.identity);
		newProjectile.damageCaused = damagePerShot;
		Vector3 unitVectorToPlayer = (player.transform.position - projectileSocket.transform.position).normalized;
		newProjectile.GetComponent<Rigidbody> ().velocity = unitVectorToPlayer * newProjectile.projectileSpeed;
	}

	void OnDrawGizmos(){
		Gizmos.color = new Color (255f, 0f, 0f, 0.5f);
		Gizmos.DrawWireSphere (transform.position, attackRadius);
		Gizmos.color = new Color (0f, 255f, 0f, 0.5f);
		Gizmos.DrawWireSphere (transform.position, moveRadius);
	}
}

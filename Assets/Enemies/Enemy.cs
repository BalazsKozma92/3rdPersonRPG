using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable {

	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] float moveRadius = 3f;
	float initialMoveRadius;
	[SerializeField] float attackRadius = 6f;
	[SerializeField] float damagePerShot = 8.3f;
	[SerializeField] float secondsBetweenShots = 0.5f;
	[SerializeField] Projectile projectileToUse;
	[SerializeField] GameObject projectileSocket;
	[SerializeField] Vector3 aimOffset = new Vector3(0, 1f, 0);

	bool isAttacking = false;
	bool isChasing = false;
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
		currentHealthPoints = maxHealthPoints;
		thisBasePoint = Instantiate (basePoint, transform.position, transform.rotation, transform.parent);

		initialMoveRadius = moveRadius;

		player = GameObject.FindGameObjectWithTag ("Player");
		aiCharControl = GetComponent<AICharacterControl> ();

	}

	void Update(){
		float distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);
		float playerDistanceToBasePoint = Vector3.Distance (thisBasePoint.transform.position, player.transform.position);

		if (distanceToPlayer <= attackRadius && !isAttacking) {
			isAttacking = true;
			InvokeRepeating ("SpawnProjectile", 0.001f, secondsBetweenShots);
		}
		if (distanceToPlayer > attackRadius){
			isAttacking = false;
			CancelInvoke ("SpawnProjectile");
		}

		if (distanceToPlayer <= moveRadius && playerDistanceToBasePoint <= initialMoveRadius * 3.5f) {
			if (!isChasing) {
				isChasing = true;
				moveRadius *= 2f;
			}
			aiCharControl.SetTarget (player.transform);
		} else {
			if (isChasing) {
				isChasing = false;
				moveRadius = initialMoveRadius;
			}
			aiCharControl.SetTarget (thisBasePoint.transform);
		}
	}

	void SpawnProjectile(){
		Projectile newProjectile = Instantiate (projectileToUse, projectileSocket.transform.position, projectileSocket.transform.rotation);
		newProjectile.damageCaused = damagePerShot;
		Vector3 unitVectorToPlayer = (player.transform.position - projectileSocket.transform.position + aimOffset).normalized;
		newProjectile.GetComponent<Rigidbody> ().velocity = unitVectorToPlayer * newProjectile.projectileSpeed;
	}

	void OnDrawGizmos(){
		Gizmos.color = new Color (255f, 0f, 0f, 0.5f);
		Gizmos.DrawWireSphere (transform.position, attackRadius);
		Gizmos.color = new Color (0f, 255f, 0f, 0.5f);
		Gizmos.DrawWireSphere (transform.position, moveRadius);
		Gizmos.color = new Color (0f, 0f, 255f, 0.5f);
		if (thisBasePoint) {
			Gizmos.DrawWireSphere (thisBasePoint.transform.position, initialMoveRadius * 3.5f);
		}
	}
}

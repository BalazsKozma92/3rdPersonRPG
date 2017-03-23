using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable {

	GameObject currentTarget;
	CameraRaycaster cameraRaycaster;

	public int playerLevel = 1;
	[SerializeField][HideInInspector] int enemyLayerNumber = 9;
	[SerializeField] float maxHealthPoints = 34f;
	[SerializeField] float maxManaPoints = 100f;
	[SerializeField] float damagePerClick = 6.73f;
	[SerializeField] float maxAttackRange = 1.5f;
	[SerializeField] float timeBetweenHits = .7f;

	float currentHealthPoints;
	float currentManaPoints = 100f;
	float lastHitTime = 0f;

	void Start(){
		OnLevelUp ();
		currentHealthPoints = maxHealthPoints;
		cameraRaycaster = FindObjectOfType<CameraRaycaster> ();
		cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
	}

	void OnLevelUp(){
		if (playerLevel > 1) {
			for (int i = 0; i < playerLevel-1; i++) {
				maxHealthPoints *= 1.2f;
			}
		}
		if (playerLevel > 1) {
			for (int i = 0; i < playerLevel-1; i++) {
				damagePerClick *= 1.15f;
			}
		}
	}

	void OnMouseClick(RaycastHit raycastHit, int layerHit){
		if (layerHit == enemyLayerNumber) {
			var enemy = raycastHit.collider.gameObject;

			float enemyToPlayerDistance = (enemy.transform.position - transform.position).magnitude;
			Vector3 faceTheEnemy = (enemy.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (faceTheEnemy), 0.2f);

			if (Time.time - lastHitTime > timeBetweenHits && enemyToPlayerDistance < maxAttackRange) {
				currentTarget = enemy;
				lastHitTime = Time.time;
				enemy.GetComponent<IDamagable> ().TakeDamage (damagePerClick);
			}
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.F)) {
			currentHealthPoints += 20f;
		}
	}

	public float healthAsPercentage{
		get{ return currentHealthPoints / maxHealthPoints; }
	}

	public float manaAsPercentage{
		get{ return currentManaPoints / maxManaPoints; }
	}

	void IDamagable.TakeDamage(float damage){
		currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);
	}
}

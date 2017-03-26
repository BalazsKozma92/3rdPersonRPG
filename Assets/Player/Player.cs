using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable {

	//GameObject currentTarget;
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

	public float GetCurrentHealth() { return currentHealthPoints; }
	public float GetMaxHealth() { return maxHealthPoints; }
	public float GetCurrentMana() { return currentManaPoints; }
	public float GetMaxMana() { return maxManaPoints; }

	public delegate void LeveledUp(int newLevel);
	public event LeveledUp notifyOnLevelingUpObservers;

	void Start(){
		currentHealthPoints = maxHealthPoints;
		currentManaPoints = maxManaPoints;
		cameraRaycaster = FindObjectOfType<CameraRaycaster> ();
		cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
	}

	void OnLevelUp(){
		playerLevel += 1;
		if (playerLevel > 1) {
				float tempHealth = (maxHealthPoints *= 1.22f);
				maxHealthPoints = tempHealth - ((tempHealth / 700f) * 25f);
		}
		if (playerLevel > 1) {
				damagePerClick *= 1.15f;
		}

		notifyOnLevelingUpObservers (playerLevel);
	}

	void OnMouseClick(RaycastHit raycastHit, int layerHit){
		if (layerHit == enemyLayerNumber) {
			var enemy = raycastHit.collider.gameObject;

			float enemyToPlayerDistance = (enemy.transform.position - transform.position).magnitude;
			Vector3 faceTheEnemy = (enemy.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (faceTheEnemy), 0.2f);

			if (Time.time - lastHitTime > timeBetweenHits && enemyToPlayerDistance < maxAttackRange) {
				//currentTarget = enemy;
				lastHitTime = Time.time;
				enemy.GetComponent<IDamagable> ().TakeDamage (damagePerClick);
			}
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.F)) {
			currentHealthPoints += 20f;
		}
		if (Input.GetKeyDown (KeyCode.G)) {
			OnLevelUp ();
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

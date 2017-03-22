using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable {

	GameObject currentTarget;

	CameraRaycaster cameraRaycaster;

	[SerializeField] int enemyLayerNumber = 9;
	[SerializeField] float maxHealthPoints = 100f;
	float currentHealthPoints = 100f;
	[SerializeField] float maxManaPoints = 100f;
	float currentManaPoints = 100f;

	[SerializeField] float damagePerClick = 6.73f;

	void Start(){
		cameraRaycaster = FindObjectOfType<CameraRaycaster> ();
		cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
	}

	void OnMouseClick(RaycastHit raycastHit, int layerHit){
		if (layerHit == enemyLayerNumber) {
			var enemy = raycastHit.collider.gameObject;
			currentTarget = enemy;
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				enemy.GetComponent<IDamagable> ().TakeDamage (damagePerClick);
			}
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

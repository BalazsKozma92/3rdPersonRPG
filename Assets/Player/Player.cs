﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable {

	[SerializeField] float maxHealthPoints = 100f;
	float currentHealthPoints = 100f;
	[SerializeField] float maxManaPoints = 100f;
	float currentManaPoints = 100f;

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

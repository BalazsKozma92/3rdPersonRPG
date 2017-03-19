using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerManaBar : MonoBehaviour
{
	Player player;
	Image manaImage;
	float manaPercentage;

	void Start()
	{
		manaImage = GetComponent<Image> ();
		player = FindObjectOfType<Player>();
	}

	void Update()
	{
		manaImage.fillAmount = player.manaAsPercentage;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerManaBar : MonoBehaviour
{
	Player player;
	float manaPercentage;
	Image manaImage;

	void Start()
	{
		manaImage = GetComponent<Image> ();
		player = FindObjectOfType<Player>();
	}

	void Update()
	{
		manaImage.fillAmount = player.manaAsPercentage;
		//rectTransform.localScale = new Vector2(healthPercentage, healthPercentage);
		//rectTransform.Rotate (Vector3.back, Time.deltaTime * 25f);
	}
}

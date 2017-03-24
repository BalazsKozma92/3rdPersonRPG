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
	[SerializeField] Text manaText = null;
	string currentMana;
	string maxMana;

	void Start()
	{
		manaImage = GetComponent<Image> ();
		player = FindObjectOfType<Player>();
	}

	void Update()
	{
		currentMana = player.GetCurrentMana ().ToString("F0");
		maxMana = player.GetMaxMana ().ToString ("F0");
		manaText.text = currentMana + " " + maxMana;

		manaImage.fillAmount = player.manaAsPercentage;
	}
}

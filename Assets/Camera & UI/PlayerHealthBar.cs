using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerHealthBar : MonoBehaviour
{
	Player player;
	Image healthImage;
	float healthPercentage;
	[SerializeField] Text healthText = null;
	string currentHealth;
	string maxHealth;

    void Start()
    {
		healthImage = GetComponent<Image> ();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
		currentHealth = player.GetCurrentHealth ().ToString("F0");
		maxHealth = player.GetMaxHealth ().ToString ("F0");
		healthText.text = currentHealth + " " + maxHealth;
	
		healthImage.fillAmount = player.healthAsPercentage;
    }
}

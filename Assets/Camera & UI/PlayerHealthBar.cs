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

    void Start()
    {
		healthImage = GetComponent<Image> ();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
		healthImage.fillAmount = player.healthAsPercentage;
    }
}

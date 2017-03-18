using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerHealthBar : MonoBehaviour
{
    Player player;
	float healthPercentage;
	Image healthImage;

    void Start()
    {
		healthImage = GetComponent<Image> ();
        player = FindObjectOfType<Player>();
    }
		
    void Update()
    {
		healthImage.fillAmount = player.healthAsPercentage;
		//rectTransform.localScale = new Vector2(healthPercentage, healthPercentage);
		//rectTransform.Rotate (Vector3.back, Time.deltaTime * 25f);

    }
}

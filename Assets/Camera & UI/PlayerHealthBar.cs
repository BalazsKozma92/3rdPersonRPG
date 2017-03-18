using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class PlayerHealthBar : MonoBehaviour
{

//    RawImage healthBarRawImage;
    Player player;
	float healthPercentage;
	RectTransform rectTransform;

    // Use this for initialization
    void Start()
    {
		rectTransform = GetComponent<RectTransform> ();
        player = FindObjectOfType<Player>();
     //   healthBarRawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
		healthPercentage = player.healthAsPercentage;
		rectTransform.localScale = new Vector2(healthPercentage, healthPercentage);
		rectTransform.Rotate (Vector3.back, Time.deltaTime * 25f);
        //float xValue = -(player.healthAsPercentage / 2f) - 0.5f;
        //healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }
}

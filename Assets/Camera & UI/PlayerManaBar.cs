using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class PlayerManaBar : MonoBehaviour
{
    Player player;
	float manaPercentage;
	RectTransform rectTransform;

    void Start()
    {
		rectTransform = GetComponent<RectTransform> ();
        player = FindObjectOfType<Player>();
    }
		
    void Update()
    {
		manaPercentage = player.manaAsPercentage;
		rectTransform.localScale = new Vector2(manaPercentage, manaPercentage);
		rectTransform.Rotate (Vector3.back, Time.deltaTime * 40f);
    }
}

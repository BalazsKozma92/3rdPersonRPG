using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerLevel : MonoBehaviour {

	Player player;
	Text levelText;

	// Use this for initialization
	void Start () {
		levelText = GetComponent<Text> ();
		player = FindObjectOfType<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		levelText.text = player.playerLevel.ToString ();
	}
}

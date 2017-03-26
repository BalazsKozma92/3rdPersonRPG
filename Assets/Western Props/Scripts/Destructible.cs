using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

	public GameObject destroyedVersion;
	Player player;

	void Start(){
		player = FindObjectOfType<Player> ();
	}

	void OnMouseDown ()
	{
		float playerToObjectDistance = (player.transform.position - transform.position).magnitude;
		if (playerToObjectDistance < 2f) {
			Instantiate(destroyedVersion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}

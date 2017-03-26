using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterScene : MonoBehaviour {

	[SerializeField] int sceneIndex = 0;

	void OnMouseDown(){
		SceneManager.LoadScene (sceneIndex);
	}
}

using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (AICharacterControl))]
[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{

	[SerializeField] const int walkableLayerNumber = 8;
	[SerializeField] const int enemyLayerNumber = 9;

	ThirdPersonCharacter thirdPersonCharacter = null;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster = null;
	Vector3 currentDestination, clickPoint;
	AICharacterControl aiCharControl = null;
	GameObject walkTarget = null;

	bool isInDirectMode = false;
	
    void Start()
    {
		cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
		aiCharControl = GetComponent<AICharacterControl> ();
		walkTarget = new GameObject ("WalkTarget");

		cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;
	}

	void ProcessMouseClick(RaycastHit raycastHit, int layerHit){
		switch (layerHit) {
		case enemyLayerNumber:
			// navigate to enemy
			GameObject enemy = raycastHit.collider.gameObject;
			aiCharControl.SetTarget (enemy.transform);
			break;
		case walkableLayerNumber:
			// navigate on ground
			walkTarget.transform.position = raycastHit.point;
			aiCharControl.SetTarget (walkTarget.transform);
			break;
		default:
			Debug.LogWarning ("Don't know how to handle mouse click for player movement.");
			return;
		}
	}

	// TODO make this get called again
	void ProcessDirectMovement(){
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		// calculate camera relative direction to move
		Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		Vector3 movement = v*cameraForward + h*Camera.main.transform.right;

		thirdPersonCharacter.Move (movement, false, false);
	}
}


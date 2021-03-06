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
	[SerializeField] GameObject walkTarget = null;

	ThirdPersonCharacter thirdPersonCharacter = null;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster = null;
	AICharacterControl aiCharControl = null;
	NavMeshAgent navMeshAgent;
	
    void Start()
    {
		navMeshAgent = GetComponent<NavMeshAgent> ();
		cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
		aiCharControl = GetComponent<AICharacterControl> ();

		cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;
	}

	void ProcessMouseClick(RaycastHit raycastHit, int layerHit){
		switch (layerHit) {
		case enemyLayerNumber:
			// navigate to enemy
			navMeshAgent.stoppingDistance = 1.5f;
			GameObject enemy = raycastHit.collider.gameObject;
			aiCharControl.SetTarget (enemy.transform);
			break;
		case walkableLayerNumber:
			// navigate on ground
			navMeshAgent.stoppingDistance = .2f;
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


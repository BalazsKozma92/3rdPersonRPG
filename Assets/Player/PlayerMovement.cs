using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float walkMoveStopRadius = 0.2f;
	[SerializeField] float attackMoveStopRadius = 5f;

	ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
	Vector3 currentDestination, clickPoint;

	bool isInDirectMode = false;
	
    private void Start()
    {
		cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }

	void ProcessDirectMovement(){
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		// calculate camera relative direction to move
		Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		Vector3 movement = v*cameraForward + h*Camera.main.transform.right;

		thirdPersonCharacter.Move (movement, false, false);
	}

//	void ProcessIndirectMovement(){
//		if (Input.GetMouseButton(0))
//		{
//			switch (cameraRaycaster.currentLayerHit) {
//			case Layer.Walkable:
//				currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);  // So not set in default case
//				break;
//			case Layer.Enemy:
//				currentDestination = ShortDestination(clickPoint, attackMoveStopRadius);  // So not set in default case
//				break;
//			default:
//				print ("Unexpected layer found");
//				return;
//			}
//		}
//		WalkToDestination ();
//	}

	void WalkToDestination(){
		var playerToClickPoint = currentDestination - transform.position;
		if (playerToClickPoint.magnitude >= 0.2f) {
			thirdPersonCharacter.Move (playerToClickPoint, false, false);
		} else {
			thirdPersonCharacter.Move (Vector3.zero, false, false);
		}
	}

	Vector3 ShortDestination(Vector3 destination, float shortening){
		Vector3 reductionVector = (destination - transform.position).normalized * shortening;
		return destination - reductionVector;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.black;
		Gizmos.DrawLine (transform.position, clickPoint);
		Gizmos.DrawSphere (currentDestination, 0.1f);
		Gizmos.DrawSphere (clickPoint, 0.15f);

		Gizmos.color = new Color (0f, 0f, 0f, 0.5f);
		Gizmos.DrawWireSphere (transform.position, attackMoveStopRadius);
	}
}


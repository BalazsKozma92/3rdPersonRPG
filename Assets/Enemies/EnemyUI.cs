using UnityEngine;
using UnityEngine.UI;

// Add a UI Socket transform to your enemy
// Attack this script to the socket
// Link to a canvas prefab that contains NPC UI
public class EnemyUI : MonoBehaviour {

    // Works around Unity 5.5's lack of nested prefabs
    [Tooltip("The UI canvas prefab")]
    [SerializeField] GameObject enemyCanvasPrefab = null;
	GameObject enemyParent;

    Camera cameraToLookAt;
	Vector3 sizeDifference;

    // Use this for initialization 
    void Start()
    {
		enemyParent = GetComponentInParent<Enemy> ().gameObject;
		cameraToLookAt = Camera.main;
		Instantiate(enemyCanvasPrefab, transform.position, transform.rotation, transform);
		sizeDifference = new Vector3 (1f / enemyParent.transform.localScale.x, 1f / enemyParent.transform.localScale.y, 1f / enemyParent.transform.localScale.z);
		transform.localScale = new Vector3 (sizeDifference.x * transform.localScale.x, sizeDifference.y * transform.localScale.y, sizeDifference.z * transform.localScale.z);
    }

    // Update is called once per frame 
    void LateUpdate()
    {
		transform.LookAt(cameraToLookAt.transform);
		transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }
}
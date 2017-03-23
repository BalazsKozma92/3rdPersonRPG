using UnityEngine;
using UnityEngine.UI;

// Add a UI Socket transform to your enemy
// Attack this script to the socket
// Link to a canvas prefab that contains NPC UI
public class EnemyUI : MonoBehaviour {

    // Works around Unity 5.5's lack of nested prefabs
    [Tooltip("The UI canvas prefab")]
    [SerializeField] GameObject enemyCanvasPrefab = null;
	Vector3 enemyParent;

	Player player;
    Camera cameraToLookAt;
	Vector3 sizeDifference;
	GameObject enemyUICanvas;
	GameObject enemy;
	Text LevelText;

    // Use this for initialization 
    void Start()
    {
		player = FindObjectOfType<Player> ();
		enemyParent = GetComponentInParent<Enemy> ().gameObject.transform.localScale;
		cameraToLookAt = Camera.main;
		enemyUICanvas = Instantiate(enemyCanvasPrefab, transform.position, transform.rotation, transform);
		transform.localScale = new Vector3 (1f / enemyParent.x, 1f / enemyParent.y, 1f / enemyParent.z);
		LevelText = GetComponentInChildren<Text> ();

		LevelText.text = GetComponentInParent<Enemy> ().enemyLevel.ToString ();
		int enemyLevel = GetComponentInParent<Enemy> ().enemyLevel;

		Color orange = new Color (191f/255f, 112f/255f, 11f/255f);

		if (enemyLevel > player.playerLevel + 2 && enemyLevel < player.playerLevel + 5) {
			LevelText.color = orange;
		} else if (enemyLevel > player.playerLevel + 4) {
			LevelText.color = Color.red;
		} else if (enemyLevel < player.playerLevel - 2 && enemyLevel > player.playerLevel - 5) {
			LevelText.color = Color.green;
		} else if (enemyLevel < player.playerLevel - 4) {
			LevelText.color = Color.gray;
		} else {
			LevelText.color = Color.yellow;
		}
    }

    // Update is called once per frame 
    void LateUpdate()
    {
		transform.LookAt(cameraToLookAt.transform);
		transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }
}
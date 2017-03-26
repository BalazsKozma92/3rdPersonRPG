using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour {

    [Tooltip("The UI canvas prefab")]
    [SerializeField] GameObject enemyCanvasPrefab = null;
	Vector3 enemyParent;
	int enemyLevel;
	Player player;
    Camera cameraToLookAt;
	Vector3 sizeDifference;
	GameObject enemy;
	Text LevelText;
	Color orange;

    void Start()
	{
		player = FindObjectOfType<Player> ();
		player.notifyOnLevelingUpObservers += UpdateColors;
		enemyParent = GetComponentInParent<Enemy> ().gameObject.transform.localScale;
		cameraToLookAt = Camera.main;
		Instantiate(enemyCanvasPrefab, transform.position, transform.rotation, transform);
		transform.localScale = new Vector3 (1f / enemyParent.x, 1f / enemyParent.y, 1f / enemyParent.z);
		LevelText = GetComponentInChildren<Text> ();
		orange = new Color (191f/255f, 112f/255f, 11f/255f);
		enemyLevel = GetComponentInParent<Enemy> ().enemyLevel;
		UpdateColors (1);
    }

	void UpdateColors(int playerLevel){
		if (gameObject != null) {
			LevelText.text = GetComponentInParent<Enemy> ().enemyLevel.ToString ();
			
			if (enemyLevel > playerLevel + 2 && enemyLevel < playerLevel + 5) {
				LevelText.color = orange;
			} else if (enemyLevel > playerLevel + 4) {
				LevelText.color = Color.red;
			} else if (enemyLevel < playerLevel - 2 && enemyLevel > playerLevel - 5) {
				LevelText.color = Color.green;
			} else if (enemyLevel < playerLevel - 4) {
				LevelText.color = Color.gray;
			} else {
				LevelText.color = Color.yellow;
			}
		}
	}

	public void UnsubscribeFromDelegate(){
		player.notifyOnLevelingUpObservers -= UpdateColors;
	}

    void LateUpdate()
    {
		transform.LookAt(cameraToLookAt.transform);
		transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }
}
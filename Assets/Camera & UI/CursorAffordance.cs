using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

	[SerializeField] Texture2D walkCursor = null;
	[SerializeField] Texture2D attackCursor = null;
	[SerializeField] Texture2D nullCursor = null;
	[SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);

	CameraRaycaster cameraRaycaster;

	void Start () {
		cameraRaycaster = GetComponent<CameraRaycaster> ();
		cameraRaycaster.onLayerChange += OnLayerChanged;
	}
	
	void OnLayerChanged (Layer newLayer) {
		switch (newLayer) {
		case Layer.Walkable:
			Cursor.SetCursor (walkCursor, cursorHotspot, CursorMode.Auto);
			break;
		case Layer.Enemy:
			Cursor.SetCursor (attackCursor, cursorHotspot, CursorMode.Auto);
			break;
		case Layer.RaycastEndStop:
			Cursor.SetCursor (nullCursor, cursorHotspot, CursorMode.Auto);
			break;
		default:
			Debug.LogError ("Don't know what cursor to show.");
			return;
		}
	}
}

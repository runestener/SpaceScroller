using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	protected static MainCamera _instance = null;
	public static MainCamera instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType(typeof(MainCamera)) as MainCamera;
				if (_instance == null) {
					_instance = Instantiate(Resources.Load("MainCamera", typeof(MainCamera))) as MainCamera;
					_instance.Init();
				}
			}
			return _instance;
		}
	}

	protected void Init() {

	}

	// Get the camera's field of view width translated to world cords
	// x-value = left side / minimum value 
	// y-value = right side / maximum value
	public Vector2 WidthBounds() {
		return new Vector2 (this.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0,0,0)).x, this.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x );
	}

	// Get the camera's field of view height translated to world cords
	// x-value = bottom side / minimum value 
	// y-value = top side / maximum value
	public Vector2 HeightBounds() {
		return new Vector2 (this.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0,0,0)).y, this.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0,Screen.height,0)).y );
	}

	public bool IsPositionWithinBounds(Vector2 position) {
		bool b = true;
		Vector2 x_axis = WidthBounds ();
		b = position.x > x_axis.x && position.x < x_axis.y;
		if (!b) {
			return b;
		}
		Vector2 y_axis = HeightBounds ();
		b = position.y > y_axis.x && position.y < y_axis.y;
		return b;
	}

	public bool IsPositionOutsideBottomOfCamera(float y_position) {
		return y_position < HeightBounds ().x;
	}

	public Vector2 GetWorldPositionToScreenPosition(Vector2 worldPosition) {
		return this.GetComponent<Camera>().WorldToScreenPoint(worldPosition);
	}

	public Vector2 GetScreenPositionToWorldPosition(Vector2 screenPosition) {
		return this.GetComponent<Camera>().ScreenToWorldPoint (screenPosition);
	}

}

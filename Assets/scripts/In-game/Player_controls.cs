using UnityEngine;
using System.Collections;

public class Player_controls : SpaceShip {

	protected static Player_controls _instance = null;
	public static Player_controls instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType(typeof(Player_controls)) as Player_controls;
				if (_instance == null) {
					GameObject go = Instantiate(Resources.Load("Player_controls", typeof(GameObject))) as GameObject;
					_instance = go.GetComponent<Player_controls>();
					_instance.Init();
				}
			}
			return _instance;
		}
	}

	protected void Init() {

	}

	protected override Vector3 ClampBoundaries() {
		Vector2 bounds = ShipTextureDimensions ();
		
		// get width bounds and clamp within value, taking into account the ship's texture width size (half since it's centered)
		Vector2 widthBounds = MainCamera.instance.WidthBounds ();
		float x_clamp = Mathf.Clamp (this.transform.position.x, widthBounds.x + bounds.x/2, widthBounds.y - bounds.x/2);
		
		// get height bounds and clamp within value, taking into account the ship's texture height size (half since it's centered)
		Vector2 heightBounds = MainCamera.instance.HeightBounds ();
		float y_clamp = Mathf.Clamp (this.transform.position.y, heightBounds.x + bounds.y/2, heightBounds.y - bounds.y/2);
		
		// return the clamped value for the space ship
		Vector3 dir_clamp = new Vector3 (x_clamp, y_clamp, this.transform.position.z);
		return dir_clamp;
		
	}

	protected override void Collision (Collidable c) {
		base.Collision (c);
		switch(c.collidableType) {
		case CollidableType.enemy:
			c.GetComponent<EnemyShip>().SubstractHealthPoints(collisionDamage);
			break;
		}
	}



	protected override void MoveUpdate () {
		if (this.isEnabled) {
			KeyboardUpdate ();
		}
	}

	protected void KeyboardUpdate() {
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			MoveLeft();
		}

		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			MoveRight();
		}

		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			MoveForward();
		}

		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			MoveBackward();
		}

		if (Input.GetKey (KeyCode.Space)) {
			Fire();
		}
	}

}

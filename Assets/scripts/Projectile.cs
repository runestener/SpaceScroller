using UnityEngine;
using System.Collections;

public class Projectile : Movable {

	protected float damage;


	public void InstantiateProjectil(float damage) {
		this.collidable.collisionEvent += ProjectileCollision;
		this.disabledPosition = new Vector2 (-100, -100);
		this.damage = damage;
		ResetProjectile ();
	}

	public void InitiateProjectile(Vector2 initPosition) {
		this.isEnabled = true;
		this.spriteRenderer.enabled = this.isEnabled;
		this.collidable.SetEnabled (this.isEnabled);
		this.transform.position = new Vector3 (initPosition.x, initPosition.y, this.transform.position.z);


		StartCoroutine (MoveRoutine() );
	}

	public void ResetProjectile() {
		//Debug.Log (string.Format("Resetting {0}", this.gameObject.name));
		this.transform.position = disabledPosition;
		this.isEnabled = false;
		this.spriteRenderer.enabled = this.isEnabled;
		this.collidable.SetEnabled (this.isEnabled);

		StopAllCoroutines ();
	}

	protected virtual IEnumerator MoveRoutine () {
		while(this.isEnabled && MainCamera.instance.IsPositionWithinBounds(this.transform.position)) {
			MoveForward();
			yield return null;
		}
		ResetProjectile ();
	}

	protected void ProjectileCollision(Collidable c) {
		switch (c.collidableType) {
		case CollidableType.enemy:
			//Debug.Log("projectile Collision with enemy");
			c.GetComponent<EnemyShip>().SubstractHealthPoints(this.damage);
			ResetProjectile();
			break;
		}
	}

	protected Collidable _collidable = null;
	protected Collidable collidable {
		get {
			if (this._collidable == null) {
				this._collidable = this.GetComponent<Collidable>();
			}
			return this._collidable;
		}
		set {
			if (this._collidable == null) {
				this._collidable = this.GetComponent<Collidable>();
			}
			this._collidable = value;
		}
	}

}

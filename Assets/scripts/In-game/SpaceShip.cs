using UnityEngine;
using System.Collections;

public class SpaceShip : WorldObject {


	[SerializeField]
	protected Transform shootTransform;

	protected Vector2 textureBounds = Vector2.zero;
	[SerializeField]
	protected float collisionDamage;
	[SerializeField]
	public int killPoints = 10;

	void Start() {
		if (this.weapon) {
			this.weapon.SetupWeapon (shootTransform);
		}
	}

	protected override void Velocity () {
		base.Velocity ();
		if (velocity.x == 0) {
			if (this.animator != null) {
				this.animator.SetTrigger ("neutral");
			}
		}
	}

	public virtual void SetupSpaceShip() {
		this.collidable.SetEnabled (true);
	}


	public override void InitiateRenderable (Vector2 initPosition) {
		base.InitiateRenderable (initPosition);
		this.collidable.SetEnabled (true);
	}

	public override void ResetRenderable ()	{
		base.ResetRenderable ();
		this.collidable.SetEnabled (false);
	}

	public override void SubscribeToEvents() {
		base.SubscribeToEvents ();
		this.collidable.collisionEvent += Collision;
	}
	
	public override void UnSubscribeToEvents() {
		base.UnSubscribeToEvents ();
		this.collidable.collisionEvent -= Collision;
	}

	protected virtual void Fire() {
		this.weapon.Fire ();
	}

	protected override void MoveLeft ()	{
		base.MoveLeft ();
		if (this.animator != null) {
			this.animator.SetTrigger("turnLeft");
		}
	}

	protected override void MoveRight () {
		base.MoveRight ();
		if (this.animator != null) {
			this.animator.SetTrigger("turnRight");
		}
	}

	// Get the texture's size
	protected Vector2 ShipTextureDimensions() {
		Vector2 bounds = new Vector2 (this.spriteRenderer.bounds.size.x, this.spriteRenderer.bounds.size.y);
		return bounds;
	}

	protected virtual void Collision(Collidable c) {

	}

	public void SetShipInvunerable(float time) {
		StartCoroutine (InvunerableRoutine(time) );
	}

	protected IEnumerator InvunerableRoutine(float time) {
		float t = Time.time + time;
		Color normalColor = new Color(this.spriteRenderer.color.a, this.spriteRenderer.color.g, this.spriteRenderer.color.b, this.spriteRenderer.color.a);
		this.collidable.SetEnabled(false);
		while(t >= Time.time) {
			Color color = (t - Time.time)*10 % 5f <= 2.5f ? Color.clear : normalColor;
			this.spriteRenderer.color = color;
			yield return null;
		} 
		this.spriteRenderer.color = normalColor;
		this.collidable.SetEnabled(true);

		yield return null;
	}


	protected Weapon _weapon = null;
	public Weapon weapon {
		get { 
			if (this._weapon == null) {
				this._weapon = this.GetComponent<Weapon>();
			}
			return this._weapon;
		}
		set {
			if (this._weapon == null) {
				this._weapon = this.GetComponent<Weapon>();
			}
			this._weapon = value;
		}
	}

	protected Collidable _collidable = null;
	public Collidable collidable {
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

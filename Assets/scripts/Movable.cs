using UnityEngine;
using System.Collections;
using System;

public class Movable : Renderable {

	protected bool _pause { get; set; }
	protected void SetPause(bool b) {
		this._pause = b;
	}

	protected Vector3 _velocity = new Vector3(0,0,0);
	public Vector3 velocity {
		get { return this._velocity; }
		protected set { this._velocity = value; }
	}

	public float velocityDecrease; 
	public float velocity_max;
	public float velocity_speed = 1f;


	void Update() {
		if (this.isEnabled && !_pause) {
			MoveUpdate ();
			Velocity();
			/*if (velocity != Vector3.zero) {
				this.transform.position += velocity * Time.deltaTime * velocity_speed;
				this.transform.position = ClampBoundaries();
				NormalizeVelocity ();
			} else if (velocity.x == 0) {
				if (this.animator != null) {
					this.animator.SetTrigger("neutral");
				}
			}*/
		}
	}

	protected virtual void Velocity() {
		if (velocity != Vector3.zero) {
			this.transform.position += velocity * Time.deltaTime * velocity_speed;
			this.transform.position = ClampBoundaries();
			NormalizeVelocity ();
		}
	}

	public override void SetupRenderable (Vector3 disabledPosition) {
		base.SetupRenderable (disabledPosition);
		
		SubscribeToEvents ();
	}

	public virtual void SubscribeToEvents() {
		GameManager.instance.pauseEvent += SetPause;
	}

	public virtual void UnSubscribeToEvents() {
		GameManager.instance.pauseEvent -= SetPause;
	}

	// move velocity towards 0
	protected void NormalizeVelocity() {
		float x = Mathf.MoveTowards (velocity.x, 0, velocityDecrease * Time.deltaTime);
		float y = Mathf.MoveTowards (velocity.y, 0, velocityDecrease * Time.deltaTime);
		velocity = new Vector3(x, y, velocity.z);
	}

	// Clamps the position of a movable character to stay within the screen
	// probably want to move this to the player ship, as it is the only character where it's relevant
	protected virtual Vector3 ClampBoundaries() {
		return this.transform.position;
	}

	// override method to add a move routine OR input to a movable in the update loop
	protected virtual void MoveUpdate() {

	}

	protected virtual void MoveBackward() {
		MoveBackward(1f);
	}

	protected virtual void MoveBackward(float factor) {
		AddVelocity (-Vector2.up * factor);
	}


	protected virtual void MoveForward() {
		MoveForward(1f);
	}

	protected virtual void MoveForward(float factor) {
		AddVelocity (Vector2.up * factor);
	}

	protected virtual void MoveLeft() {
		MoveLeft(1f);
	}

	protected virtual void MoveLeft(float factor) {
		AddVelocity (-Vector2.right * factor);
	}

	protected virtual void MoveRight() {
		MoveRight(1f);
	}

	protected virtual void MoveRight(float factor) {
		AddVelocity (Vector2.right * factor);
	}

	protected void AddVelocity(Vector3 direction) {
		velocity += direction;
		float x = Mathf.Clamp (velocity.x, -velocity_max, velocity_max);
		float y = Mathf.Clamp (velocity.y, -velocity_max, velocity_max);
		velocity = new Vector3 (x, y);
	}

	protected Animator _animator = null;
	protected Animator animator {
		get {
			if (this._animator == null) {
				this._animator = this.GetComponent<Animator>();
			}
			return this._animator;
		}
	}
}

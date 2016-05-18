using UnityEngine;
using System.Collections;
using System;

public class WorldObject : Movable {

	public Action<WorldObject> destroyEvent;

	[SerializeField]
	protected float _healthPoints;
	[SerializeField]
	protected float _maxHealthPoints;

	public float healthPoints { get; protected set; }

	// ALWAYS remove healthpoints with this method to check if a spaceship's hp stays above zero
	public bool SubstractHealthPoints(float val) {
		if (this.healthPoints > 0) {
			this.healthPoints -= val;
			if (this.healthPoints <= 0) {
				//Debug.Log(string.Format("{0} has perished" + this.gameObject.name));
				if (this.destroyEvent != null) {
					this.destroyEvent(this);
				}
				return true;
			}
		}
		return false;
	}
	
	// ALWAYS add healthpoints with this method to ensure that hp wont rise to infinity - but still making sure that they will rise to MAX
	public void AddHealthPoints(float val) {
		this.healthPoints = Mathf.Clamp (this.healthPoints + val, 0, this._maxHealthPoints ); 
	}

	public virtual void ResetStartWorldObject() {
		this.transform.position = this.disabledPosition;
		ResetRenderable ();
	}

	public override void ResetRenderable ()	{
		base.ResetRenderable ();
		this.healthPoints = this._healthPoints;
	}

}

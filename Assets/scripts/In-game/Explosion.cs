using UnityEngine;
using System.Collections;
using System;

public class Explosion : WorldObject {

	public Action<Explosion> finishedAnimationEvent;


	public void InitiateExplosion(WorldObject worldObject) {
		InitiateExplosion(worldObject, "explosion_large_rainbow");
	}

	public override void ResetStartWorldObject () {
		base.ResetStartWorldObject ();
		animator.SetTrigger ("neutral");
	}

	/*public void InitiateExplosion(WorldObject worldObject, string explosionAnimName) {
		InitiateExplosion(worldObject.transform.position, worldObject.velocity, worldObject.velocityDecrease*2f, explosionAnimName);
	}*/

	public void InitiateExplosion(Movable movable, string explosionAnimName) {
		//Debug.Log ("initiate explosion " + movable.gameObject.name + " " + Time.frameCount);
		InitiateExplosion(movable.transform.position, movable.velocity, movable.velocityDecrease*2f, explosionAnimName);
	}

	public void InitiateExplosion(Vector2 position, Vector2 velocity, float velocityDecrease, string explosionAnimName) {
//		Debug.Log (position);
		InitiateRenderable (position);
		this.velocity = velocity;
		this.velocityDecrease = 0;
		this.animator.SetTrigger (explosionAnimName);
	}

	protected void FinishedAnimation() {
		ResetRenderable ();
		if (this.finishedAnimationEvent != null) {
			this.finishedAnimationEvent(this);
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum CollidableType { 
	player = 0, enemy = 1, 
	playerProjectile = 10, enemy_projectile = 11
}

[RequireComponent(typeof(Collider2D))]
public class Collidable : MonoBehaviour {

	// subscribe to event to handle collision
	public Action<Collidable> collisionEvent = null;

	public CollidableType collidableType;

	public bool isEnabled { get; protected set; }

	public void SetEnabled(bool b) {
		this.isEnabled = b;
		this.GetComponent<Collider2D>().enabled = b;
	}

	private int triggerFrameCount = 0;
	// unity physics method to check collision
	// calls 'Collided' on object's collidable class
	void OnTriggerEnter2D(Collider2D other) {
		if (this.enabled) {
			Collidable c = other.GetComponent<Collidable> ();
			if (c && c.isEnabled) {
				//Debug.Log(this.gameObject.name);
				c.Collided(this);
			}
		}
	}

	public virtual void Collided(Collidable c) {
		//Debug.Log (string.Format("{0} called 'Collided' on {1} - frame {2}", c.gameObject.name, this.gameObject.name, Time.frameCount));
		if (collisionEvent != null) {
			collisionEvent(c);
		}
	}
}

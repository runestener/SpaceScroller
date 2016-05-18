using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class WorldObjectManager : MonoBehaviour {

	protected bool _pauseWorldObject = false;
	protected void SetPause(bool b) {
		this._pauseWorldObject = b;
	}

	public GameObject worldObjectPrefab;
	public GameObject parentObject;
	protected List<WorldObject> _worldObjects = new List<WorldObject>();
	public List<WorldObject> worldObjects {
		get {
			if (this._worldObjects == null) {
				this._worldObjects = new List<WorldObject>();
			}
			return this._worldObjects;
		}
		set {
			if (this._worldObjects == null) {
				this._worldObjects = new List<WorldObject>();
			}
			this._worldObjects = value;
		}
	}

	public abstract void SetupWorldObjectManager (int i);

	public abstract void ResetStartManager (int i);

	protected void SubscribeToObjectEvents() {
		for (int i = 0; i < this.worldObjects.Count; i++) {
			this.worldObjects[i].destroyEvent += DestroyObject;
		}
		GameManager.instance.pauseEvent += SetPause;
	}

	protected void UnSubscribeToObjectEvents() {
		for (int i = 0; i < this.worldObjects.Count; i++) {
			this.worldObjects[i].destroyEvent -= DestroyObject;
		}
		GameManager.instance.pauseEvent -= SetPause;
	}

	protected virtual void DestroyObject(WorldObject wo) {
		// Explosions!!
	}

	protected void ManageWorldObject(WorldObject wo) {
		if (this.parentObject) {
			wo.transform.parent = parentObject.transform;
			this.worldObjects.Add (wo);
		}
	}

	protected IEnumerator ResetWait(WorldObject wo) {
		yield return StartCoroutine ( ResetWait(wo, null) );
	}

	protected IEnumerator ResetWait(WorldObject wo, Action callback) {
		yield return new WaitForEndOfFrame();
		wo.ResetRenderable ();
		yield return null;
		if (callback != null) {
			callback();
		}

	}
}

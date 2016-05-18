using UnityEngine;
using System.Collections;
using System;


[ RequireComponent(typeof(SpriteRenderer)) ]
public class Renderable : MonoBehaviour {

	public bool isEnabled { get; set; }
	
	protected Vector3 disabledPosition { get; set; }

	protected Action spriteUpdate;

	protected Vector2 spriteExtends;

	public virtual void SetupRenderable(Vector3 disabledPosition) {
		this.disabledPosition = disabledPosition;
		ResetRenderable ();
		this.spriteUpdate += UpdateSpriteExtends;
	}

	public virtual void InitiateRenderable() {
		InitiateRenderable (this.disabledPosition);
	}
	
	public virtual void InitiateRenderable(Vector2 initPosition) {
		this.isEnabled = true;
		this.spriteRenderer.enabled = this.isEnabled;
		this.transform.position = initPosition;
	}
	
	public virtual void ResetRenderable() {
		//Debug.Log (string.Format("Resetting {0}", this.gameObject.name));
		this.transform.position = disabledPosition;
		this.isEnabled = false;
		this.spriteRenderer.enabled = this.isEnabled;
	}

	protected void UpdateSpriteExtends() {
		spriteExtends = new Vector2 (this.spriteRenderer.sprite.bounds.extents.x, this.spriteRenderer.sprite.bounds.extents.y);
	}

	protected SpriteRenderer _spriteRenderer = null;
	protected SpriteRenderer spriteRenderer {
		get {
			if (this._spriteRenderer == null) {
				this._spriteRenderer = this.GetComponent<SpriteRenderer>();
			}
			return this._spriteRenderer;
		}
		set {
			if (this._spriteRenderer == null) {
				this._spriteRenderer = this.GetComponent<SpriteRenderer>();
			}
			if (this.spriteUpdate != null) {
				this.spriteUpdate();
			}
			this._spriteRenderer = value;
		}
	}
}

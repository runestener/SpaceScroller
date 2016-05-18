using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundObject : Renderable {

	[SerializeField]
	protected float parallexSpeedDeviationPercentage;

	[SerializeField]
	protected float scaleDeviationPercentage;

	[SerializeField]
	protected List<Sprite> objectSprites = new List<Sprite>();

	protected Vector3 initialScale { get; set; }
	protected float initialScrollingSpeed { get; set; }

	void Update() {
		if (this.transform.position.y <= -7f) {
			InitiateRenderable(new Vector2(Random.Range(-5f, 5f), 30f) );
		}
	}

	public override void SetupRenderable (Vector3 disabledPosition)	{
		base.SetupRenderable (disabledPosition);
		this.initialScale = new Vector3 (this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
		this.initialScrollingSpeed = this.parallex.scrollingSpeed;
	}

	public override void InitiateRenderable(Vector2 position) {
		base.InitiateRenderable (position);
		if (this.objectSprites.Count >= 1) {
			this.spriteRenderer.sprite = this.objectSprites[Random.Range(0, this.objectSprites.Count)];
		}

		float newScrollingSpeed =  this.parallex.scrollingSpeed * (Random.Range(0f, parallexSpeedDeviationPercentage) / 100);
		this.parallex.scrollingSpeed = initialScrollingSpeed + (Random.Range (0, 2) == 1 ? -newScrollingSpeed : newScrollingSpeed);

		this.transform.localScale = initialScale * ( 1 + Random.Range(0f, scaleDeviationPercentage) / 100);

		this.transform.position =  new Vector3(position.x, position.y, this.transform.position.z);
	}



	protected Parallex _parallex = null;
	protected Parallex parallex {
		get {
			if (this._parallex == null) {
				this._parallex = this.GetComponent<Parallex>();
			}
			return this._parallex;
		}
		set {
			if (this._parallex == null) {
				this._parallex = this.GetComponent<Parallex>();
			}
			this._parallex = value;
		}
	}
}

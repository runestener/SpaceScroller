using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundController : MonoBehaviour {

	protected static BackgroundController _instance = null;
	public static BackgroundController instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType(typeof(BackgroundController)) as BackgroundController;
				if (_instance == null) {
					GameObject go = Instantiate(Resources.Load("BackgroundController", typeof(GameObject))) as GameObject;
					_instance = go.GetComponent<BackgroundController>();
					_instance.Init();
				}
			}
			return _instance;
		}
	}

	protected bool _finishedSetup = false;

	[SerializeField]
	protected List<SpriteRenderer> starBackgrounds = new List<SpriteRenderer>();
	[SerializeField]
	protected GameObject backgroundObjectPrefab;

	protected List<BackgroundObject> backgroundObjects = new List<BackgroundObject>();

	protected float y_screen_position;
	protected Vector2 extendsPosition;

	protected void Init() {

	}

	void Awake() {
		if (this != instance) {
			Destroy(this.gameObject);
			return;
		}
		SetupBackgroundController ();
	}

	public void SetupBackgroundController () {
		if (!this._finishedSetup) {
			this._finishedSetup = true;
			SetStarBackgroundStartPosition ();
			SetupBackgroundObjects ();
		}
	}

	void Update () {
		if (starBackgrounds.Count >= 1) {
			for (int i = 0; i < starBackgrounds.Count; i++) {
				SpriteRenderer sr = starBackgrounds[i];
				if (!CheckSpriteInsideCamera(sr)) {
					sr.transform.position = new Vector3(sr.transform.position.x, GetSpritesTopYPosition(starBackgrounds[(i+1)%2]) + sr.bounds.extents.y -0.1f, sr.transform.position.z);
				}
			}
		}
	}

	protected void SetupBackgroundObjects() {
		for (int i = 0; i < 10; i++) {
			GameObject go = Instantiate(backgroundObjectPrefab) as GameObject;
			go.transform.parent = this.transform;
			BackgroundObject bo = go.GetComponent<BackgroundObject>();
			bo.SetupRenderable(new Vector3(Random.Range(-5f, 5f),6f + Random.Range(3.7f,4f)*i,0));
			bo.InitiateRenderable();
			backgroundObjects.Add(bo);
		}
	}

	public void ResetStartBackgroundObjects() {
		for (int i = 0; i < 10; i++) {
			backgroundObjects[i].ResetRenderable();
			backgroundObjects[i].InitiateRenderable();
		}
	}

	protected bool CheckSpriteInsideCamera(SpriteRenderer sr) {
		if (sr) {
			Vector2 extendsPosition = new Vector2(sr.transform.position.x + sr.sprite.bounds.max.x,
			                                      sr.transform.position.y + sr.sprite.bounds.max.y * sr.transform.localScale.y);
			y_screen_position = MainCamera.instance.GetWorldPositionToScreenPosition(extendsPosition).y;
			if (y_screen_position <= 0) {
				return false;
			}
		}
		return true;
	}

	protected void SetStarBackgroundStartPosition() {
		SpriteRenderer sr = starBackgrounds [0];
		Vector2 cameraBottomPosition = MainCamera.instance.GetComponent<Camera>().ScreenToWorldPoint (Vector2.zero);
		float sr_y_position = sr.bounds.extents.y - (Mathf.Abs (cameraBottomPosition.y) + Mathf.Abs (sr.transform.position.y));
		sr.transform.position += new Vector3 (0, sr_y_position, 0);

		SpriteRenderer sr_2 = starBackgrounds [1];
		float sr_2_y_position = GetSpritesTopYPosition (sr);
		sr_2.transform.position = new Vector3 (sr_2.transform.position.x, sr_2_y_position + sr_2.bounds.extents.y -0.1f, sr_2.transform.position.z);
	}

	protected float GetSpritesTopYPosition(SpriteRenderer sr) {
		return sr.transform.position.y + sr.bounds.extents.y;
	}

}

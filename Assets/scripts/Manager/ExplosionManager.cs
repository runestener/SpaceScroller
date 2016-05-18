using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionManager : WorldObjectManager {

	[SerializeField]
	protected List<string> explosionNames;

	public override void SetupWorldObjectManager (int position)	{
		for (int i = 0; i < 10; i++) {
			Vector3 setupPosition = new Vector3(100+position, 100+i, 0);
			GameObject go = Instantiate(worldObjectPrefab, setupPosition, Quaternion.identity ) as GameObject;
			Explosion e = go.GetComponent<Explosion>();
			e.SetupRenderable (setupPosition);
			ManageWorldObject(e);
		}
	}

	public override void ResetStartManager (int position) {
		for (int i = 0; i < 10 ; i++) {
			worldObjects[i].ResetStartWorldObject();
		}
	}

	public void StartExplosionOfMovable(Movable m) {

		string explosionName = this.explosionNames[ Random.Range(0, this.explosionNames.Count) ];
		Explosion explosion = this.worldObjects.Find (e => e.isEnabled == false).GetComponent<Explosion>();
		explosion.InitiateExplosion (m, explosionName);
	}

	public void StartExplosion(Vector2 position, Vector2 velocity, float decrease) {
		string explosionName = this.explosionNames[ Random.Range(0, this.explosionNames.Count) ];
		Explosion explosion = this.worldObjects.Find (e => e.isEnabled == false).GetComponent<Explosion>();

		Debug.Log (explosionName);
	}
}

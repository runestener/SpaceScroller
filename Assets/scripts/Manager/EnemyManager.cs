using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum enemyType { none = -1, spiteFighter = 0, starDestroyer = 1, warper = 2 }

public class EnemyManager : WorldObjectManager {

	public List<EnemyData> enemyData = new List<EnemyData> ();
	public List<EnemyWave> enemyWave = new List<EnemyWave> ();

	public override void SetupWorldObjectManager (int position)	{
		for (int i = 0; i < 60; i++) {
			Vector3 setupPosition = new Vector3(100+position, 100+i, 0);
			GameObject go = Instantiate(worldObjectPrefab, setupPosition, Quaternion.identity ) as GameObject;
			EnemyShip e = go.GetComponent<EnemyShip>();
			e.SetupRenderable(setupPosition);
			e.destroyEvent += ShipDestroyed;
			ManageWorldObject(e);
		}
		/*worldObjects [0].InitiateRenderable (new Vector2(0,4));
		worldObjects [0].GetComponent<EnemyShip>().StartMoveRoutine ();*/
	}

	public override void ResetStartManager (int position) {
		for (int i = 0; i < 60; i++) {
			worldObjects[i].ResetStartWorldObject();
		}

		//SetupWave (enemyWave[5]);
		StartCoroutine (ShipSpawnRoutine() );
		/*worldObjects [0].InitiateRenderable (new Vector2(0,4));
		EnemyShip e = worldObjects [0].GetComponent<EnemyShip> ();
		e.SetupEnemyShipData ( enemyData[Random.Range(0, enemyData.Count) ] );
		e.StartMoveRoutine ();*/

	}

	public void StopSpawnRoutine() {
		StopAllCoroutines ();
	}

	protected IEnumerator ShipSpawnRoutine() {
		SetupWave (enemyWave[0]);
		yield return new WaitForSeconds (6f);
		SetupWave (enemyWave[1]);
		yield return new WaitForSeconds (8f);
		SetupWave (enemyWave[2]);
		yield return new WaitForSeconds (8f);
		SetupWave (enemyWave[3]);
		yield return new WaitForSeconds (8f);
		SetupWave (enemyWave[4]);
		yield return new WaitForSeconds (8f);
		while (GameManager.instance.playerManager.playerLives >= 1) {
			SetupWave( enemyWave[Random.Range(0, enemyWave.Count)], true );
			yield return new WaitForSeconds(Random.Range(2f, 6f));
		}
		yield return null;
	}

	protected void ShipDestroyed(WorldObject wo) {
		GameManager.instance.explosionManager.StartExplosionOfMovable (wo);
		//wo.ResetRenderable ();
		StartCoroutine ( ResetWait(wo) );
		GameManager.instance.playerManager.AddScore (wo.GetComponent<SpaceShip> ().killPoints);
	}

	public void SetupWave(EnemyWave ew) {
		SetupWave (ew, false);
	}

	public void SetupWave(EnemyWave ew, bool randomEnemy) {
		Vector2 startPosition = Vector2.zero;
		for (int y = 0; y < ew.numberRows; y++) {
			EnemyData ed = randomEnemy ? this.enemyData[Random.Range(0, enemyData.Count)] : this.enemyData[0];
			for (int x = 0; x < ew.numberPerRow; x++) {
				float x_pos = MainCamera.instance.GetScreenPositionToWorldPosition(new Vector2((float)Screen.width/(ew.numberPerRow/(x+1f))-  Screen.width/(ew.numberPerRow*2), 0)).x;
				startPosition = new Vector2(x_pos, 6f + y*2f);

				SetupEnemy(ed, startPosition);
			}
		}
	}

	public void SetupEnemy(EnemyData ed, Vector2 startPosition) {
		WorldObject wo = worldObjects.Find(w => w.isEnabled == false);
		if (wo != null) {
			EnemyShip es = wo.GetComponent<EnemyShip>(); 
			es.InitiateRenderable (startPosition);
			es.SetupEnemyShipData (ed);
			es.StartMoveRoutine ();
		}
	}
}

[System.Serializable]
public class EnemyWave {
	public int numberRows = 1;
	public int numberPerRow = 1;
}

[System.Serializable]
public class EnemyData {
	public string name;
	public enemyType enemyType;
	public Sprite sprite;
	public RuntimeAnimatorController animatorController;
	public float healthPoints;
	public float collisionDamage;
	public int killPoints;
	public float velocity_max;
	public float velocity_decrease;

}
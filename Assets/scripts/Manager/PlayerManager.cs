using UnityEngine;
using System.Collections;

public class PlayerManager : WorldObjectManager {


	public int playerLives { get; protected set; }
	public int playerScore { get; protected set; }

	protected Player_controls _player { 
		get { return Player_controls.instance; } 
		//set { Player_controls.instance = value; }
	}

	public override void SetupWorldObjectManager (int i) {
		//this.playerLives = 3;
		//this.playerScore = 0;

		Vector3 setupPosition = new Vector3(0, -4, 0);
		//GameObject go = Instantiate(worldObjectPrefab, setupPosition, Quaternion.identity ) as GameObject;
		this._player.transform.position = setupPosition;

		//Player_controls p = go.GetComponent<Player_controls>();
		this._player.SetupRenderable (setupPosition);
		this._player.destroyEvent += ShipDestroyed;
		//p.SetupRenderable (setupPosition);
		//p.destroyEvent += ShipDestroyed;
		//this._player = p;

		ManageWorldObject(this._player);
		//ManageWorldObject(p);
		//p.InitiateRenderable (setupPosition);
		
		//p.isEnabled = false;
	}

	public override void ResetStartManager (int i )	{
		this.playerLives = 3;
		this.playerScore = 0;

		this._player.InitiateRenderable (new Vector3(0, -4, 0));
		
		this._player.isEnabled = false;
	}


	public void StartLevel() {
		this._player.isEnabled = true;
	}

	protected void ShipDestroyed(WorldObject wo) {
		GameManager.instance.explosionManager.StartExplosionOfMovable (wo);
		//wo.ResetRenderable ();
		StartCoroutine ( ResetWait(wo, SubstractLife) );

	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Y)) {
			SubstractLife();
		}
	}

	protected void SubstractLife() {
		this.playerLives -= 1;
		if (this.playerLives <= 0) {
			GameManager.instance.GameOver(this.playerScore);
		} else {
			// respawn player and make him invunerable for a period
			this._player.ResetRenderable();
			this._player.InitiateRenderable();
			this._player.SetShipInvunerable(4f);
		}
	}

	public void AddScore(int score) {
		this.playerScore += score;
	}
}

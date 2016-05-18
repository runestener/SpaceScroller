using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

	protected static GameManager _instance = null;
	public static GameManager instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType(typeof(GameManager)) as GameManager;
				if (_instance == null) {
					GameObject go = Instantiate(Resources.Load("GameManager", typeof(GameObject))) as GameObject;
					_instance = go.GetComponent<GameManager>();
					_instance.Init();
				}
			}
			return _instance;
		}
	}

	void Awake() {
		if (this != instance) {
			Destroy(this);
			return;
		}
		SetupLevel ();
	}

	protected void Init() {

	}

	public Action<bool> pauseEvent;
	protected bool _pause = false;
	public bool pause {
		get { return this._pause; }
		protected set {
			if (this.pauseEvent != null) {
				this.pauseEvent(value);
			}
			this._pause = value;
		}
	}

	[SerializeField]
	protected string gameName = "futu_carrot";
	protected int _highScore = 0;
	public int highScore {
		get {
			if (PlayerPrefs.HasKey(gameName) && PlayerPrefs.GetInt(gameName) > this._highScore) {
				this._highScore = PlayerPrefs.GetInt(gameName, 0);
			}
			return this._highScore;
		}
		set {
			if (value > PlayerPrefs.GetInt(gameName, 0)) {
				PlayerPrefs.SetInt(gameName, value);
				this._highScore = value;
			}
		}
	}

	public void SetupStartScreen() {
		//this.playerManager.SetupWorldObjectManager (0);
	}

	public void SetupLevel() {
		this.playerManager.SetupWorldObjectManager (0);
		//this.playerManager.StartLevel ();
		this.enemyManager.SetupWorldObjectManager (1);
		this.explosionManager.SetupWorldObjectManager (2);
		BackgroundController.instance.SetupBackgroundController ();
	}

	public void ResetStartLevel() {
		this.playerManager.ResetStartManager (0);
		this.playerManager.StartLevel ();
		this.enemyManager.ResetStartManager (1);
		this.explosionManager.ResetStartManager (2);
		BackgroundController.instance.ResetStartBackgroundObjects ();
	}

	public void GameOver(int score) {
		this.highScore = score;
		GameGUI.instance.ChangeGUI(GameGUI.gameGuiType.gameOver);
		this.enemyManager.StopSpawnRoutine ();
	}

	public void PauseGame(bool b) {
		this.pause = b;
	}


	protected EnemyManager _enemyManager = null;
	public EnemyManager enemyManager {
		get {
			if (this._enemyManager == null) {
				this._enemyManager = this.GetComponent<EnemyManager>();
			}
			return this._enemyManager;
		}
	}

	protected ExplosionManager _explosionManager = null;
	public ExplosionManager explosionManager {
		get {
			if (this._explosionManager == null) {
				this._explosionManager = this.GetComponent<ExplosionManager>();
			}
			return this._explosionManager;
		}
	}

	protected PlayerManager _playerManager = null;
	public PlayerManager playerManager {
		get {
			if (this._playerManager == null) {
				this._playerManager = this.GetComponent<PlayerManager>();
			}
			return this._playerManager;
		}
	}
}

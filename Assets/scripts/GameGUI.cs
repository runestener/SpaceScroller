using UnityEngine;
using System.Collections;
using System;

public class GameGUI : MonoBehaviour {

	public enum gameGuiType { mainScreen = 0, inGame = 1, gameOver = 2 }

	protected static GameGUI _instance = null;
	public static GameGUI instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType(typeof(GameGUI)) as GameGUI;
				if (_instance == null) {
					GameObject go = Instantiate(Resources.Load("GameGUI", typeof(GameObject))) as GameObject;
					_instance = go.GetComponent<GameGUI>();
					_instance.Init();
				}
			}
			return _instance;
		}
	}

	protected Action GUIMethod;

	protected void Init() {

	}

	void Awake() {
		ChangeGUI (gameGuiType.mainScreen);
		//GUIMethod = DrawPreGameGUI;
	}

	[SerializeField]
	protected GUIStyle fontStyle, largeFontStyle;

	[SerializeField]
	protected Texture2D livesTexture;

	void Update() {
		InputKeys ();
	}

	protected void InputKeys() {
		if (GUIMethod == DrawGameGUI){
			if (Input.GetKeyDown (KeyCode.P)) {
				GameManager.instance.PauseGame (!GameManager.instance.pause);
			}
		}
		else if (GUIMethod == DrawPreGameGUI || GUIMethod == DrawGameOverGUI) {
			if (Input.GetKeyUp(KeyCode.N)) {
				PressedStartLevel();
			}
			if (GUIMethod == DrawGameOverGUI) {
				if (Input.GetKeyUp(KeyCode.M)) {
					Application.LoadLevel(0);
				}
			}
		}
		if (Input.GetKeyUp(KeyCode.Escape)) {
			PressedEscape();
		}
	}

	protected gameGuiType _gameGuiType;

	protected void PressedStartLevel() {
		//Debug.Log("pressed startbutton");
		ChangeGUI (gameGuiType.inGame );
		//GameManager.instance.SetupLevel ();
		GameManager.instance.ResetStartLevel ();
	}

	protected void PressedEscape() {
		switch (this._gameGuiType) {
		case gameGuiType.mainScreen:
			Application.Quit();
			break;
		case gameGuiType.inGame:
			if (!GameManager.instance.pause) {
				GameManager.instance.PauseGame( !GameManager.instance.pause );
			}
			else {
				Application.LoadLevel(0);
			}
			break;
		case gameGuiType.gameOver:
			Application.LoadLevel(0);
			break;
		}
	}

	public void ChangeGUI (gameGuiType gameGui) {
		switch (gameGui) {
		case gameGuiType.mainScreen:
			GUIMethod = DrawPreGameGUI;
			break;
		case gameGuiType.inGame:
			GUIMethod = DrawGameGUI;
			break;
		case gameGuiType.gameOver:
			GUIMethod = DrawGameOverGUI;
			break;
		}
		this._gameGuiType = gameGui;
	}

	void OnGUI () {

		GUILayout.BeginArea(new Rect(0,0, Screen.width, Screen.height)); {

			if (GUIMethod != null) {
				GUIMethod();
			}

		} GUILayout.EndArea ();
	}

	protected void DrawGameGUI() {
		GUILayout.BeginVertical(); {
			
			GUILayout.BeginHorizontal(); {
				
				GUILayout.Space(5f);
				GUILayout.Label(string.Format("SCORE: {0}", GameManager.instance.playerManager.playerScore), fontStyle);
					
				GUILayout.FlexibleSpace();
				GUILayout.Label(string.Format("HIGH SCORE: {0}", GameManager.instance.highScore), fontStyle);
				GUILayout.Space(5f);
				
			} GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal(); {
				
				GUILayout.Space(7f);
				GUILayout.Label("LIVES:", fontStyle);
				GUILayout.Space(3f);
				for (int i = 0; i < GameManager.instance.playerManager.playerLives; i++) {
					GUILayout.Label(livesTexture, GUIStyle.none);
					GUILayout.Space(3f);
				}
				GUILayout.FlexibleSpace();
			} GUILayout.EndHorizontal();
			
			GUILayout.FlexibleSpace();
			if (GameManager.instance.pause) {
				GUILayout.Label("pause", largeFontStyle);
				GUILayout.Label("Press 'esc' to leave game", fontStyle);
			}
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(); {
				
				GUILayout.FlexibleSpace();
				GUILayout.Label("press 'p' for pause", fontStyle);
				GUILayout.Space(5f);
				
			} GUILayout.EndHorizontal();
			
		} GUILayout.EndVertical();
	}


	protected void DrawPreGameGUI() {
		GUILayout.BeginVertical (); {

			GUILayout.Space(70f);
			GUILayout.BeginHorizontal ();
			{

				GUILayout.FlexibleSpace ();
				GUILayout.Label ("futu-carrot", largeFontStyle);
				GUILayout.FlexibleSpace ();

			}
			GUILayout.EndHorizontal ();

			GUILayout.FlexibleSpace ();
			GUILayout.BeginHorizontal ();
			{
				
				GUILayout.FlexibleSpace ();
				if (GUILayout.Button ("press here or\n'N' to begin", fontStyle)) {
					PressedStartLevel();
				}

				GUILayout.FlexibleSpace ();
				
			}
			GUILayout.EndHorizontal ();


			//GUILayout.FlexibleSpace();
			GUILayout.Space(100f);
			GUILayout.Label(string.Format("High Score: {0}", GameManager.instance.highScore), fontStyle );
			/*GUILayout.BeginHorizontal ();
			{
				
				GUILayout.FlexibleSpace ();
				if (((int)Time.time) % 2 == 0) {
					GUILayout.Label("Insert coin", fontStyle);
				}
				GUILayout.FlexibleSpace ();
			} GUILayout.EndHorizontal();*/
			GUILayout.FlexibleSpace();
			GUILayout.Label("Press 'esc' to quit", fontStyle);

		} GUILayout.EndVertical ();

	}

	protected void DrawGameOverGUI () {
		GUILayout.BeginVertical (); {

			GUILayout.Space(80f);
			GUILayout.BeginHorizontal ();
			{
				
				GUILayout.FlexibleSpace ();
				GUILayout.Label ("GAME OVER !!", largeFontStyle);
				GUILayout.FlexibleSpace ();
				
			}
			GUILayout.EndHorizontal ();

			GUILayout.Space(120f);
			GUILayout.BeginHorizontal ();
			{
				
				GUILayout.FlexibleSpace ();
				GUILayout.Label (string.Format("SCORE: {0}", GameManager.instance.playerManager.playerScore), fontStyle);
				GUILayout.FlexibleSpace ();
				
			}
			GUILayout.EndHorizontal ();

			GUILayout.Space(60f);
			GUILayout.BeginHorizontal ();
			{
				
				GUILayout.FlexibleSpace ();
				if (GUILayout.Button ("press here or\n'N' to play again", fontStyle)) {
					PressedStartLevel();
				}
				GUILayout.FlexibleSpace ();
				
			}
			GUILayout.EndHorizontal ();
			GUILayout.Space(20f);
			GUILayout.Label("press 'm' for Main Menu", fontStyle);

			GUILayout.FlexibleSpace();


		} GUILayout.EndVertical ();
	}
}

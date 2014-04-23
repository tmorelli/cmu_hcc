using UnityEngine;
using System.Collections;

public class gameController : MonoBehaviour
{
		public static int level = 0;
		public static int shotsComplete = 0;
		public static bool sequenceInitialized = false;
		public static int[] shotsPerRound = {16,8,14,26};
//		public static int[] shotsPerRound = {1,1,1,26};
		public static float lastSelection;
		public static float MAX_SCORE = 20.0f;
		public static float totalScore = 0.0f;
		public static GUIText scoreText = null;
		public static GUIText infoText = null;
		public static int round = 0;
		public static int THUMBPAD = 0;
		public static int THUMBBUTTON = 1;
		public static int FINGERPAD = 2;
		public static int FINGERBUTTON = 3;
		
		public static int [] scene1Sequence = {1,5,13,0,7,6,4,3,8,9,2,11,14,10,12,1};
		public static int [] scene2Sequence = {1,2,5,3,4,0,6,1};
		public static int [] scene3Sequence = {11,4,10,3,9,2,8,1,7,0,6,12,5,11};
		public static int [] scene4Sequence = {0,11,1,12,2,13,3,14,4,15,5,16,6,17,7,18,8,19,9,20,10,21,11,22,12,0};
		
		
		
		
		public static bool mouseButton = false;
		public static int[] controlSequence = {
				THUMBPAD,
				THUMBBUTTON,
				FINGERPAD,
				FINGERBUTTON
		};
		public static string[] controlInfo = {"Move with left thumb and select by clicking touchpad",
						"Move with left thumb and select by pressing X",
						"Move with index finger and select by clicking touchpad",
						"Move with index finger and select by pressing X"};
		public  gameManager gm;
		public static gameController gc;
		//this for initialization

		public static int targetHit ()
		{
				gc.gm.playSelectSound();
				int retVal = 0; //nothing left
				//return 1 if the scene is to choose another random target
				shotsComplete ++;
				float score = MAX_SCORE - (Time.time - lastSelection);
				if (score < 0.0f) {
						score = 0.0f;
				}
				totalScore += score;
				if (scoreText == null)
						createScoreText ();
				scoreText.text = "Score: " + ((int)(totalScore * 100)).ToString ();
				Debug.Log (totalScore);
				lastSelection = Time.time;

				Debug.Log (shotsComplete);
				if (shotsComplete == shotsPerRound [level]) {
						level++;
						if (level < shotsPerRound.Length) {
								gc.gm.logEvent ("Change scenes!");
								if (level == 1) {
									Application.LoadLevel ("scene2");
								}
								else if (level==2){
									Application.LoadLevel ("scene3");
								}
								else{
									Application.LoadLevel ("scene4");
								}
									
								shotsComplete = 0;
						} else {
								if (round < controlInfo.Length - 1) {
										gc.gm.logEvent ("Change rounds!");
										round++;
										level = 0;
										Application.LoadLevel ("scene1");
										updateInfoText();
										shotsComplete = 0;
								} else {
										gc.gm.logEvent("Game Over!!");
										gc.gm.gameOver();
								}
		
						}
				} else {
						int nextTarget = 0;
						if (level == 0){
							nextTarget = scene1Sequence[shotsComplete];
						}
						else if (level == 1){
							nextTarget = scene2Sequence[shotsComplete];
						}
						else if (level == 2){
							nextTarget = scene3Sequence[shotsComplete];
						}
						else if (level == 3){
							nextTarget = scene4Sequence[shotsComplete];
						}
						gc.gm.displayNextSprite (nextTarget);
						retVal = 1;
						//chooseNextTarget
				}
				return retVal;
		}
		static void updateInfoText(){
			if (infoText == null){
				createInfoText();
			}
			else{
				infoText.text = controlInfo [controlSequence [round]];
			}
			
			if (controlSequence[round] % 2 == 0){
					mouseButton = true;
			}
			else{
				mouseButton = false;
			}

		}
		static public bool useMouseButton(){
			return mouseButton;
		}
		static void createInfoText ()
		{
				if (infoText == null) {
						GameObject go = new GameObject ("infoText");
						infoText = (GUIText)go.AddComponent (typeof(GUIText));
		
						//scoreText = new GUIText ();
						infoText.material.color = new Color (0.0f, 0.0f, 0.0f);
						infoText.transform.position = new Vector3 (0.25f, .95f, 1.0f);
						infoText.text = controlInfo [controlSequence [round]];
				}
		}
	
		static void createScoreText ()
		{
				GameObject go = new GameObject ("GameObjecttext");
				scoreText = (GUIText)go.AddComponent (typeof(GUIText));
		
				//scoreText = new GUIText ();
				scoreText.material.color = new Color (0.0f, 0.0f, 0.0f);
				scoreText.transform.position = new Vector3 (0.5f, 1.0f, 1.0f);
				scoreText.text = "0.0";

		}

		void initializePlaySequence ()
		{
				sequenceInitialized = true;
				//public static gameType[] controlSequence = {thumbPad,thumbButton,fingerPad,fingerButton};
				for (int x = 0; x < 4; x++) {
						int s = Random.Range (0, 3);
						int g = controlSequence [x];
						controlSequence [x] = controlSequence [s];
						controlSequence [s] = g;
				}
				for (int x = 0; x< 4; x++) {
						gc.gm.logEvent ("Seq," +x+","+ controlSequence [x]);
				}
				
				if (controlSequence[0] % 2 == 0){
					mouseButton = true;
				}
				else{
					mouseButton = false;
				}
		}

		void Start ()
		{

				gc = this;

				lastSelection = Time.time;
				GameObject go = GameObject.Find ("main");
				if (go == null) {
						Debug.Log ("Could not find background");
				}
				gm = (gameManager)go.GetComponent (typeof(gameManager));
				if (gm == null) {
						Debug.Log ("Could not find gamemgr");
				}
				gc.gm.logEvent ("Starting gamecontroller");
				
				
				if (sequenceInitialized == false){
					initializePlaySequence ();
				}
				if (scoreText == null)
						createScoreText ();
				if (infoText == null) {
						createInfoText ();
				}


		}
		// Update is called once per frame
		void Update ()
		{
	
		}
		
}

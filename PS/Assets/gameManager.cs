using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class gameManager : MonoBehaviour {
	public GameObject [] sprites;
	public int [] chosen;
	public int visibleSprite = 0;
	public AudioClip selectSound;
	public static StreamWriter sw = null;
	// Use this for initialization
	void Start () {
		chosen = new int[sprites.Length];
		for (int x = 0; x< sprites.Length; x++) {
			SpriteRenderer sr = sprites[x].GetComponent<SpriteRenderer> ();
			sr.sortingOrder = -2;
			chosen[x] = 0;
		}
//		visibleSprite = chooseNextSprite ();
		SpriteRenderer s = sprites[visibleSprite].GetComponent<SpriteRenderer> ();
		s.sortingOrder = 2;

//		SpriteRenderer sr = sprite1.GetComponent<SpriteRenderer> ();
//		sr.sortingOrder = 2;
	}

	public void playSelectSound(){
		audio.PlayOneShot(selectSound);
	}
	
	public void displayNextSprite(int _sprite){
		visibleSprite = _sprite;//chooseNextSprite ();
		logEvent("NextSprite,"+visibleSprite.ToString());
		SpriteRenderer s = sprites[visibleSprite].GetComponent<SpriteRenderer> ();
		s.sortingOrder = 2;
	}
	int chooseNextSprite(){
		bool found = false;
		int nextSprite = 0;
		while (found == false){
			nextSprite = UnityEngine.Random.Range(0,sprites.Length-1);
			//Debug.Log ("NextSprite: "+ nextSprite);
			if (chosen[nextSprite] == 0){
				chosen[nextSprite]=1;
				break;
			}
		}
		return nextSprite;
	}
	public void gameOver(){
		sw.Close();
	}
	public 	void logEvent(string _event){
		if (sw == null){
			DateTime dtCurTime = DateTime.Now; 
			DateTime dtEpochStartTime = Convert.ToDateTime("1/1/1970 8:00:00 AM"); 
			TimeSpan ts = dtCurTime.Subtract(dtEpochStartTime); 

			long epochtime; 
			epochtime = ((((((ts.Days * 24) + ts.Hours) * 60) + ts.Minutes) * 60) + ts.Seconds); 
			sw = new StreamWriter(epochtime.ToString()+".txt");
		}
		Debug.Log("GMLOG,"+_event);
		sw.WriteLine("GMLOG,"+_event);
	}
	// Update is called once per frame
	void Update () {
	
	}
}

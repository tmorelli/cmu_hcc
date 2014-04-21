using UnityEngine;
using System.Collections;

public class MoveReel : MonoBehaviour
{

	public Sprite[] symbols;
	public bool spinning = false;
	public bool stopping = false;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
/*
		if (transform.name == "reel1"){
			Debug.Log(transform.name);
		}
*/
		if (globals.isSpinning(transform.name) > 0) {
			MoveSymbols();
			spinning = true;
			stopping = false;
		}
		else if (spinning ){						
			this.audio.Stop();

			spinning = false;
			stopping = true;
			MoveSymbols();
		}
		else if (stopping){
			MoveSymbols();
		}		
	
	}
	void MoveSymbols(){
		Transform[] allChildren = GetComponentsInChildren<Transform> ();
		SpriteRenderer[] childrenRenderer = GetComponentsInChildren<SpriteRenderer> ();
		
		int idx = 0;
		foreach (Transform child in allChildren) {
			// do whatever with child transform here
			Vector3 v = child.position;
			v.y -= .36f; //.18
			if (v.y < -2.0f && idx <= 4 && idx > 0) {
				v.y = 3.6f;
				//childrenRenderer [idx - 1].sprite = symbols [(int)Random.Range (0, 3)];

				int nextSymbol = globals.getNextSymbolIndex(transform.name);
				if(transform.name == "reel1")
					Debug.Log("Next Symbol: "+nextSymbol + "CenterStop: " + globals.getSpinningCenterStop(transform.name));
				childrenRenderer [idx - 1].sprite = symbols [nextSymbol];

				if (stopping){
//					if(transform.name == "reel1")
//						Debug.Log ("CenterStop: " + globals.getSpinningCenterStop(transform.name) + ",Stop:" +globals.getStop(transform.name));
					if (globals.getSpinningCenterStop(transform.name) == globals.getStop(transform.name)){
						stopping = false;
						this.audio.Play();
					}
				}
			}
			idx++;
			child.position = v;
		}
	}
}

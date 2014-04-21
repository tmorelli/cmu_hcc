using UnityEngine;
using System.Collections;

public class MoveSymbol : MonoBehaviour
{
	public float yValue = 0.0f;
	public Sprite[] symbols;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{

/*
		if (globals.spinning) {
			Vector3 v = transform.position;
			v.y -= .36f;
			yValue = v.y;
			if (v.y < -2.0f) {
				v.y = 3.6f;
				//also change sprite renderer
				SpriteRenderer renderer = transform.GetComponent<SpriteRenderer> ();
				renderer.sprite = symbols [(int)Random.Range (0, 3)];
			}
			transform.position = v;
		}
*/
	}
}

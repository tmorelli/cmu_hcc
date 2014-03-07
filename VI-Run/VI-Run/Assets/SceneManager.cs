using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	public GameObject player1;
	public GameObject player2;
	public static float distance = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		distance = player1.transform.position.z - player2.transform.position.z;
		Debug.Log(distance);
	}
}

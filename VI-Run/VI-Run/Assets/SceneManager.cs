using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System;


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
		int rumbleVal = (int)Math.Floor(255.0/Math.Abs(distance));
		
		
		StreamWriter sw = new StreamWriter("commands.txt");
        sw.WriteLine(rumbleVal);
		sw.Close();
	}
}

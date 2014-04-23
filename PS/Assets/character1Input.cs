using UnityEngine;
using System.Collections;

public class character1Input : MonoBehaviour {

	// Use this for initialization
	//public gameController gc;

	Animator anim;
	bool touched = false;

	void Start () {
		//anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
//		if (touched == true){
//			return;
//		}
		if (gameController.useMouseButton() == true && Input.GetMouseButtonDown(0)){
			SpriteRenderer sr = GetComponent<SpriteRenderer> ();
			if (sr.sortingOrder<0){
				return;   //not visible!!
			}
			Debug.Log("Button Click!!");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
			if(hit.collider != null && hit.collider.transform == this.transform)
			{
				touched = true;
				gameController.targetHit();
				gameController.gc.gm.logEvent ("Selected,"+Time.time+","+Input.mousePosition.x+","+Input.mousePosition.y);

				//Debug.Log ("Selected,"+Time.time+","+Input.mousePosition.x+","+Input.mousePosition.y);
			//	Debug.Log ("Hit!");
				//anim.SetBool("selected", true);
				Invoke ("DestroySelf", 0.2f);
				// raycast hit this gameobject
				
			}
			else{
				gameController.gc.gm.logEvent ("Notvalid,"+Time.time +","+Input.mousePosition.x+","+Input.mousePosition.y);
			}
		}
		else if (gameController.useMouseButton() == false && Input.GetButtonDown("Jump")){

			SpriteRenderer sr = GetComponent<SpriteRenderer> ();
			if (sr.sortingOrder<0){
				return;   //not visible!!
			}

			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
			if(hit.collider != null && hit.collider.transform == this.transform)
			{
				touched = true;
				gameController.targetHit();
				gameController.gc.gm.logEvent ("Selected,"+Time.time+","+Input.mousePosition.x+","+Input.mousePosition.y);
				
			//	Debug.Log ("Hit!");
				//anim.SetBool("selected", true);
				Invoke ("DestroySelf", 0.2f);
				// raycast hit this gameobject
				
			}
			else{
				gameController.gc.gm.logEvent ("Notvalid,"+Time.time +","+Input.mousePosition.x+","+Input.mousePosition.y);
			}

		}
	}
	void DestroySelf(){
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		sr.sortingOrder = -2;
		
//		Destroy (gameObject);
	}
}

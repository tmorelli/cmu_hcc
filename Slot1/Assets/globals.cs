using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class globals : MonoBehaviour {
	public static int spinning = 0 ;
	public static int totalStops = 22;
	public static int totalVirtualStops = 64;
	public static bool canPlay = true;

	public static int numSymbols = 7;

	public static int denom = 25;
	
	public static StreamWriter sw = null;
	
	

	static int bl = 3;
	static int r7 = 0;
	static int h7 = 1;
	static int b7 = 2;

	static int rb = 4;
	static int hb = 5;
	static int bb = 6;
	
	public AudioSource winSound;

	public static int []stopIndex = {21,21,21};
	public static int []stops = {0,0,0};
	public static int [] virtualStops = {0,0,0};
	public static int []reel1Stops = {bl,b7,bl,hb,bl,bb,bl,r7,bl,bb,bl,hb,bl,b7,bl,hb,bl,bb,bl,h7,bl,rb};
	public static int []reel2Stops = {bl,b7,bl,rb,bl,bb,bl,h7,bl,bb,bl,rb,bl,b7,bl,rb,bl,bb,bl,r7,bl,hb};
	public static int []reel3Stops = {bl,h7,bl,rb,bl,hb,bl,b7,bl,hb,bl,rb,bl,h7,bl,rb,bl,hb,bl,r7,bl,bb};
	
	public static int [] reel1Weights = {4,4,2,4,1,4,4,2,4,4,1,4,1,4,1,4,1,4,1,2,4,4};
	public static int [] reel2Weights = {4,1,3,4,1,4,4,2,4,4,1,4,2,1,2,4,1,4,2,4,4,4};
	public static int [] reel3Weights = {4,3,2,4,1,3,4,1,4,3,1,4,1,2,1,4,1,3,3,1,4,4};
	
	

	public static int credits = 2000;
	public static int bet = 25;
	public static int winAmount = 0;

	public  GUIText creditText;
	public  GUIText winText;
	//symbol order - r7,w7,b7,bb,1b,2b,3b
	public static int [] paytable = {100,500,1000,100,25,100,500};

	public static int [,] pays = new int [,] {{r7,h7,b7,400},
									{r7,r7,r7,250},
									{h7,h7,h7,200},
									{b7,b7,b7,150},
//									{a7,a7,a7,80},
									{rb,hb,bb,50},
									{bb,bb,bb,40},
									{hb,hb,hb,25},
//									{ar,ah,ab,20},
									{rb,rb,rb,10},
//									{ab,ab,ab,5},
//									{ar,ar,ar,2},
//									{ah,ah,ah,2},
//									{ab,ab,ab,2},
									{bl,bl,bl,1}};
									
	public static int totalPays = 9;

	// Use this for initialization
	void Start () {
	
	}



	// Update is called once per frame
	void Update () {

//		string winT = "$" + System.String.Format("{0:.##}", (float)((float)winAmount / 100.00f));
//		string creditT = "$"+System.String.Format("{0:.##}",(float)((float)credits / 100.00f));

		string creditT = System.String.Format("{0:C2}",(float)((float)credits/100.0f));
		string winT = System.String.Format("{0:C2}",(float)((float)winAmount/100.0f));

		winText.text = winT;
		creditText.text = creditT;
		
		
		
		//winText.text = "$"+(float)((float)winAmount / 100.00f);
		//creditText.text = "$"+(float)((float)credits / 100.00f);


		if (Input.GetButtonDown("Jump")){
			if (canPlay == true && spinning == 0 && credits > denom){
				spinning = 7;
				canPlay = false;
				generateStops();
				this.audio.Play();
				credits -= 25;
				creditText.text = "$"+(float)((float)credits / 100.00f);
				StartCoroutine(StopReels (2f));
			}
		}
	}

	public static int getStop(string reel){
		if (reel == "reel1"){
			return stops[0];                    
		}
		if (reel == "reel2"){
			return stops[1];                    
		}
		if (reel == "reel3"){
			return stops[2];                    
		}
		return -1;
	}

	public static int getSpinningCenterStop(string reel){
		if (reel == "reel1"){
			if (stopIndex[0]+2 > totalStops-1) 
				return stopIndex[0]+2-totalStops;
			return stopIndex[0]+2;                    
		}
		if (reel == "reel2"){
			if (stopIndex[1]+2 > totalStops-1) 
				return stopIndex[1]+2-totalStops;
			return stopIndex[1]+2;                    
		}
		if (reel == "reel3"){
			if (stopIndex[2]+2 > totalStops-1) 
				return stopIndex[2]+2-totalStops;
			return stopIndex[2]+2;                    
		}
		return -1;
	}

	public static int convertVirtualToPhysical(int reel){
		int virtualStop = virtualStops[reel];
		int physicalStop = -1;
		if (reel == 0){
			int total = 0;
			for (int x =0; x< totalStops; x++){
				total+= reel1Weights[x];
				if (virtualStop < total){
					physicalStop = x;
					break;
				}	
			}
		}
		if (reel == 1){
			int total = 0;
			for (int x =0; x< totalStops; x++){
				total+= reel2Weights[x];
				if (virtualStop < total){
					physicalStop = x;
					break;
				}	
			}
		}
		if (reel == 2){
			int total = 0;
			for (int x =0; x< totalStops; x++){
				total+= reel2Weights[x];
				if (virtualStop < total){
					physicalStop = x;
					break;
				}	
			}
		}


		if (physicalStop == -1){
			physicalStop = totalStops - 1;
		}
		return physicalStop;
	}


	public static void generateStops(){
		
		virtualStops[0] = (int)UnityEngine.Random.Range (0, totalVirtualStops);
		virtualStops[1] = (int)UnityEngine.Random.Range (0, totalVirtualStops);
		virtualStops[2] = (int)UnityEngine.Random.Range (0, totalVirtualStops);
		logEvent("vstops,"+virtualStops[0].ToString()+","+virtualStops[1].ToString()+","+virtualStops[2].ToString());

		stops[0] = convertVirtualToPhysical(0);
		stops[1] = convertVirtualToPhysical(1);
		stops[2] = convertVirtualToPhysical(2);
		

/*
		stops[0] = (int)Random.Range (0, totalStops);
		stops[1] = (int)Random.Range (0, totalStops);
		stops[2] = (int)Random.Range (0, totalStops);
		
*/

		logEvent("stops,"+stops[0].ToString()+","+stops[1].ToString()+","+stops[2].ToString());
		Debug.Log("Next Stops: " + stops[0] +","+stops[1]+","+stops[2]);
	}

	IEnumerator StopReels(float aWaitTime)
	{
		yield return new WaitForSeconds(aWaitTime);

		for (int x =0; x< 3; x++){
			if ((spinning & 1 << x) > 0){
				spinning &= ~(1<<x);
				if (x!=2)
					StartCoroutine(StopReels (.5f));
				break;
			}
		}
		if (spinning == 0){
			StartCoroutine(EvaluateWin (.5f));
//			EvaluateWin();
//			this.audio.Stop();
		}
	}

	public static bool isRed(int s){
		if (s== rb || s == r7){
			return true;
		}
		return false;
	}
	public static bool isWhite(int s){
		if (s== hb || s == h7){
			return true;
		}
		return false;
	}
	public static bool isBlue(int s){
		if (s== bb || s == b7){
			return true;
		}
		return false;
	}


	public static bool isSeven(int s){
		if (s== r7 || s== h7 || s== b7){
			return true;
		}
		return false;
	}

	public static bool isBar(int s){
		if (s== rb || s== hb || s== bb){
			return true;
		}
		return false;
	}

	public static int CheckForAnySeven(){
		if (isSeven(reel1Stops[stops[0]]) &&  isSeven(reel2Stops[stops[1]]) && isSeven(reel3Stops[stops[2]])){
			return 80;
		}
		return 0;
	}
	public static int CheckForAnyRWB(){
		if (isRed(reel1Stops[stops[0]]) &&  isWhite(reel2Stops[stops[1]]) && isBlue(reel3Stops[stops[2]])){
			return 20;
		}
		
		return 0;
		
	}
	public static int CheckForAnyBar(){
		if (isBar(reel1Stops[stops[0]]) &&  isBar(reel2Stops[stops[1]]) && isBar(reel3Stops[stops[2]])){
			return 5;
		}
		return 0;
	}
	public static int CheckForAnyRed(){
		if (isRed(reel1Stops[stops[0]]) &&  isRed(reel2Stops[stops[1]]) && isRed(reel3Stops[stops[2]])){
			return 2;
		}
		return 0;
	}
	public static int CheckForAnyWhite(){
		if (isWhite(reel1Stops[stops[0]]) &&  isWhite(reel2Stops[stops[1]]) && isWhite(reel3Stops[stops[2]])){
			return 2;
		}
		return 0;
	}
	public static int CheckForAnyBlue(){
		if (isBlue(reel1Stops[stops[0]]) &&  isBlue(reel2Stops[stops[1]]) && isBlue(reel3Stops[stops[2]])){
			return 2;
		}
		return 0;
	}

	
//	public static void EvaluateWin(){
	IEnumerator EvaluateWin( float waitTime){
		yield return new WaitForSeconds(waitTime);
		winAmount  = 0;
		for (int x = 0; x < totalPays; x++){
			if (pays[x,0] == reel1Stops[stops[0]] && 
			    pays[x,1] == reel2Stops[stops[1]] &&
			    pays[x,2] == reel3Stops[stops[2]]){
			    winAmount = pays[x,3] * denom;
			    break;
			    
			}
		} 		
		int t = CheckForAnySeven() * denom;
		if (t > winAmount){
			winAmount = t;
		}
		t=CheckForAnyRWB() * denom;
		if (t > winAmount){
			winAmount = t;
		}
		t = CheckForAnyBar() * denom;		
		if (t > winAmount){
			winAmount = t;
		}
		t = CheckForAnyRed() * denom;		
		if (t > winAmount){
			winAmount = t;
		}
		t = CheckForAnyWhite() * denom;		
		if (t > winAmount){
			winAmount = t;
		}
		t = CheckForAnyBlue() * denom;		
		if (t > winAmount){
			winAmount = t;
		}

		this.audio.Stop();
		if (winAmount > 0){
			this.winSound.Play();
		}


		
/*		
		for (int x = 0; x< numSymbols; x++){
			if (reel1Stops[stops[0]] == x && reel2Stops[stops[1]] == x && reel3Stops[stops[2]] == x){
				winAmount = paytable[x];
				break;
			}
		}
		
*/
		credits += winAmount;
		Debug.Log("Win amount: " + winAmount);
		logEvent("winamount,"+winAmount.ToString()+","+credits);
		canPlay = true;

	}

	public static int getNextSymbolIndex(string reel){
		if (reel == "reel1"){
			stopIndex[0] --;
			if (stopIndex[0] < 0) {
				stopIndex[0] = totalStops-1;
			}
			Debug.Log("Globals Reel1 StopIndex: "+ stopIndex[0]);
			return reel1Stops[stopIndex[0]];                    
		}
		if (reel == "reel2"){
			stopIndex[1] --;
			if (stopIndex[1] < 0) {
				stopIndex[1] = totalStops-1;
			}
			return reel2Stops[stopIndex[1]];                    
		}
		if (reel == "reel3"){
			stopIndex[2] --;
			if (stopIndex[2] < 0) {
				stopIndex[2] = totalStops-1;
			}
			return reel3Stops[stopIndex[2]];                    
		}
		return 0;
	}

	public static int isSpinning(string reel){
		if (reel == "reel1"){  
			return spinning & 1<<0;
		}
		if (reel == "reel2"){  
			return spinning & 1<<1;
		}
		if (reel == "reel3"){  
			return spinning & 1<<2;
		}
		return 0;
	}

	void OnGUI() {
//		GUI.Label(new Rect(0, 0, windowRight-windowLeft, windowBottom - windowTop), text,style);
	}
	
	
	public  static void logEvent(string _event){
                if (sw == null){
                        DateTime dtCurTime = DateTime.Now;
                        DateTime dtEpochStartTime = Convert.ToDateTime("1/1/1970 8:00:00 AM");
                        TimeSpan ts = dtCurTime.Subtract(dtEpochStartTime);
 
                        long epochtime;
                        epochtime = ((((((ts.Days * 24) + ts.Hours) * 60) + ts.Minutes) * 60) + ts.Seconds);
                        sw = new StreamWriter(epochtime.ToString()+".txt");
                }
                DateTime curTime = DateTime.Now;
                DateTime epochStartTime = Convert.ToDateTime("1/1/1970 8:00:00 AM");
                TimeSpan t = curTime.Subtract(epochStartTime);
                long ep;
            	ep = ((((((t.Days * 24) + t.Hours) * 60) + t.Minutes) * 60) + t.Seconds);
                sw.WriteLine("SLOT,"+ep.ToString()+","+_event);
                sw.Flush();
        }
	
}

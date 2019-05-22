// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
//timer script - can count or countdown (originaly used as countdown)


	public bool  run = false;
	public bool  end = true;
	public bool  showTimeLeft = false;
	public float timeAvailable = 60; 
	public bool  timeEnd;

	public float startTime;
	public float endTime;
	public float curTime;
	public string curStrTime;
	public bool  pause = false;

	public GameObject guiTimer;

	void  Awake (){
		guiTimer = GameObject.Find("GUI/Timer");
	}

	void  PauseTimer ( bool ispaused  ){
		pause = ispaused;
	}

	public void  EndTimer (){
		if (end) return;
		run = false;
		end = true;
		endTime = Time.time;

		curTime = endTime - startTime;

		int minutes = (int) (curTime / 60);
		int seconds = (int) (curTime % 60);
		//int fraction = (curTime * 100) % 100;

		curStrTime = System.String.Format ("{0:00}:{1:00}", minutes, seconds); 

	}

	public void  RunTimer (){
		run = true;
		end = false;
		startTime = Time.time;
	}

	void  Update (){

		if (pause) {
			startTime = startTime + (Time.deltaTime);
			return;
		}	

		if (run) {
			curTime = Time.time - startTime;
		} else {
			curTime = endTime - startTime;
		}

		float showTime= curTime;

		if (showTimeLeft) {
			showTime = timeAvailable - curTime;
			if (showTime<=0) {
				timeEnd = true;
				showTime = 0;
				GetComponent<EndLevelScript>().done = true;
			}
  
		}

		int minutes = (int)(showTime / 60);
		int seconds = (int)(showTime % 60);
		//int fraction = (showTime * 100) % 100;

		curStrTime = System.String.Format ("{0:00}:{1:00}", minutes, seconds); 
   
		guiTimer.GetComponent<EasyFontTextMesh>().Text = curStrTime;    


	}
}
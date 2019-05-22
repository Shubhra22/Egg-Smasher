using UnityEngine;
using System.Collections;

public class PrepareLevel : MonoBehaviour {
//show couting down before game starts


	public GameObject GetReady;
	public GameObject GO;
	public bool  started;
	public AudioClip playIt;

	void  Awake (){
		AudioListener.pause=false;
	
		//Set game time
		//GetComponent<Timer>().timeAvailable = SharedSettings.ConfigTime;
		if(Application.loadedLevelName=="Arcade")
		{
			GetComponent<Timer>().timeAvailable = 60;
		}
		
	}

	IEnumerator  PrepareRoutine (){
		yield return new WaitForSeconds(1.0f);
		GetReady.SetActive(true);;
		yield return new WaitForSeconds(2.0f);
		GetReady.SetActive(false);;
		GO.SetActive(true);
/*if(Application.loadedLevelName=="Arcade")
		{
		GetComponent<Timer>().RunTimer();
		}*/
		started = true;
		yield return new WaitForSeconds(1.0f);
		GO.SetActive(false);;
		if(Application.loadedLevelName=="Arcade")
		{
			GetComponent<Timer>().RunTimer();
		}
	}

	void  Start (){

		//GameObject.Find("GUI/LevelName").GetComponent<GUIText>().text = SharedSettings.LevelName[SharedSettings.LoadLevel-1]; 
		GetComponent<AudioSource>().PlayOneShot(playIt,1.0f);
		StartCoroutine(PrepareRoutine());
	}



}
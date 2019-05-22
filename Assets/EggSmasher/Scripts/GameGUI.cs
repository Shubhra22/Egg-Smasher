// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {
	
public GUISkin skin;
private bool  pause = false;
public GameObject pauseinfo;
public GameObject buttonsOnPause;
RaycastHit hit;
public ButtonId buttonpressed;
//private MouseControl mcontrl; 
void  Start (){
	if(StartScreen.isactive==true)
		{
			 AudioListener.pause=true;
		}
}

void  FixedUpdate (){
	if (!GameObject.Find("MainScripts").GetComponent<PrepareLevel>().started)
		pauseinfo.SetActive(false); 
	else
	 	pauseinfo.SetActive(true); 
}
void  Update (){
	
	
	if(Input.GetMouseButtonDown(0))
	{
		Ray ray;
		ray=Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray,out hit))
		{
			if(hit.collider.gameObject.GetComponent<ButtonId>())
			{
				buttonpressed=hit.collider.gameObject.GetComponent<ButtonId>();
				if(buttonpressed.id=="pause")
				{
					//GameObject.Find("FruitDispenser").GetComponent<BoxCollider>().active=false;
					//pause=!pause;
				    Time.timeScale=0;
				    AudioListener.pause=true;
				   //Pause();
				   buttonsOnPause.SetActive(true);
				   pauseinfo.SetActive(false);
				   
				}
				if(buttonpressed.id=="play")
				{
				
					if(GameObject.Find("MainScripts").GetComponent<MouseControl>().isfrezed==true)
				   		 Time.timeScale=0.5f;
				   	else
				   		Time.timeScale=1;
				    if(StartScreen.isactive==true)
						{
			 				AudioListener.pause=true;
						}	
				     else
				        AudioListener.pause=false;
				   //Pause();
				   buttonsOnPause.SetActive(false);
				   pauseinfo.SetActive(true);
				   
				}
				else if(buttonpressed.id=="replay")
				{
					//pause=!pause;
				    Application.LoadLevel(Application.loadedLevel);
				    Time.timeScale=1;
				    //AudioListener.pause=false;
				   
				}
				if(buttonpressed.id=="home")
				{
					if(StartScreen.isactive==true)
						{
			 				AudioListener.pause=true;
						}	
				     else
				        AudioListener.pause=false;
					Application.LoadLevel("Start");
				   	Time.timeScale=1;
				}
				
				
			}
		}
	}
	if (Input.GetKeyDown(KeyCode.Escape)) 
	{
		
		//pause=!pause;
				    Time.timeScale=0;
				    AudioListener.pause=true;
				   //Pause();
				   buttonsOnPause.SetActive(true);
				   pauseinfo.SetActive(false);
	}
}

}
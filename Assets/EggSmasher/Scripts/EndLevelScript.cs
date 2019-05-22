using UnityEngine;
using System.Collections;

public class EndLevelScript : MonoBehaviour
{

	public GameObject scores;
	public GameObject HighScores;
	public bool  done = false;
	public bool  completed = false;
	public GameObject finishedGUI;
	public GameObject guiPoints;
	public MouseControl mouseControl;
	public GameObject Retry;
	public int best=0;
	RaycastHit hit;
	public ButtonId buttonpressed;
	public GameObject frenzy;
	public GameObject fruit;
	
	void  Awake (){
		
		guiPoints = GameObject.Find("GUI/Point");	
		mouseControl = GetComponent<MouseControl>();
	}

	void  LateUpdate (){
		guiPoints.GetComponent<EasyFontTextMesh>().Text = mouseControl.points.ToString();
			
		if (done && !completed) 
		{
			//PlayerPrefs.SetInt("highScore", 0);
			//Debug.Log(best);
			//Debug.Log(mouseControl.points);
			//Debug.Log("The value of key 'highscoreCurrentRounds' is: " + PlayerPrefs.GetInt("highscoreCurrent"));
			GameObject.Find("Trail").SetActive(false);
			GameObject.Find("FruitDispenser").GetComponent<FruitDispenser>().pause = true;
			
			//GetComponent<Frenzy>().pause=true;
			completed = true;
			if(Application.loadedLevelName=="Arcade")
			{
				PlayerPrefs.SetInt("arcadeHighScore", MouseControl.arcadeHighScore);
				GameObject.Find("MainScripts").GetComponent<Timer>().EndTimer();
				scores.GetComponent<EasyFontTextMesh>().Text = guiPoints.GetComponent<EasyFontTextMesh>().Text;
				HighScores.GetComponent<EasyFontTextMesh>().Text = MouseControl.arcadeHighScore.ToString();
				
			}
			else if(Application.loadedLevelName=="Classic")
			{
				PlayerPrefs.SetInt("classicHighScore", MouseControl.classicHighScore);
				scores.GetComponent<EasyFontTextMesh>().Text = guiPoints.GetComponent<EasyFontTextMesh>().Text;
				HighScores.GetComponent<EasyFontTextMesh>().Text = MouseControl.classicHighScore.ToString();
			}
			GameObject.Find("GUI").SetActive(false);
			//finishedGUI.SetActive(true);
			
			StartCoroutine(PrepareRoutine());
			GetComponent<MouseControl>().enabled = false;
			if(frenzy.activeSelf==true)
				{
				frenzy.SetActive(false);
				fruit.SetActive(false);
				//GameObject.Find("FruitDispenser").GetComponent<FruitDispenser>().pause = true;
				}
			//GetComponent<Frenzy>().enabled = false;
			//GameObject.Find("FinishedGUI/Timer").GetComponent<GUIText>().text = "Points : "+mouseControl.points.ToString();
			
		}
		else if(completed==true)
		{
			if(Input.GetMouseButtonDown(0))
			{
				Ray ray;
				ray=Camera.main.ScreenPointToRay(Input.mousePosition);
				if(Physics.Raycast(ray,out hit))
				{
					if(hit.collider.gameObject.GetComponent<ButtonId>())
					{
						buttonpressed=hit.collider.gameObject.GetComponent<ButtonId>();
					}
					if(buttonpressed.id=="retry")
					{
							//pause=!pause;
							Application.LoadLevel(Application.loadedLevel);
							Time.timeScale=1;
							//AudioListener.pause=false;
						   
					}
					if(buttonpressed.id=="exit")
					{
							//pause=!pause;
							Application.LoadLevel("Start");
							//Time.timeScale=1;
							//AudioListener.pause=false;
						   
					}
				}
			}
		}
	}

	IEnumerator PrepareRoutine ()
	{
		//yield return new WaitForSeconds(1.0f);
		finishedGUI.SetActive(true);
		yield return new WaitForSeconds(4.0f);
		finishedGUI.SetActive(false);;
		Retry.SetActive(true);
	}	
}
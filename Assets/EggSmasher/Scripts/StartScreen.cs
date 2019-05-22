// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	RaycastHit hit;
	ButtonId buttonpressed;
	public GameObject gameOptions;
	public GameObject messageBox;
	public GameObject cross;
	public GameObject about;

	public GameObject newgame;
	public GameObject aboutgame;
	public GameObject QuitGame;
	public GameObject ClassicMood;
	public GameObject ArcadeMood;
	public GameObject exitmood;
	public GameObject MainScreen;
public static bool  isactive ;
void  Start (){
	if(isactive==true)
		cross.SetActive(isactive);
}
IEnumerator  NewGame (){
	yield return new WaitForSeconds(0.5f);
	gameOptions.SetActive(true);
	MainScreen.SetActive(false);
}
IEnumerator  About (){
	yield return new WaitForSeconds(0.5f);
	about.SetActive(true);
	//MainScreen.SetActive(false);
}

void  Quit (){
	messageBox.SetActive(true);
	newgame.tag="destroyed";
	aboutgame.tag="destroyed";
	QuitGame.tag="destroyed";
}
IEnumerator  Classic (){
	yield return new WaitForSeconds(0.7f);
	Application.LoadLevel("Classic");
}

IEnumerator  Arcade (){
	yield return new WaitForSeconds(0.7f);
	Application.LoadLevel("Arcade");
}

IEnumerator  Exit (){
	yield return new WaitForSeconds(0.5f);
	Application.LoadLevel("Start");
}
void  Update (){

if(newgame.activeSelf==false)
	//gameOptions.SetActive(true);
	{
		StartCoroutine(NewGame());
	}
else if(aboutgame.activeSelf==false)
	{
		StartCoroutine(About());
	}
else if(QuitGame.activeSelf==false)
	{
		
		Quit();
	}	

if(ClassicMood.activeSelf==false)
	{
		StartCoroutine(Classic());
	}
else if(ArcadeMood.activeSelf==false)
	{
		StartCoroutine(Arcade());
	}
else if(exitmood.activeSelf==false)
	{
		StartCoroutine(Exit());
	}

if(Input.GetMouseButtonDown(0))
	{
		Ray ray;
		ray=Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray,out hit))
		{
			if(hit.collider.gameObject.GetComponent<ButtonId>())
			{
				buttonpressed=hit.collider.gameObject.GetComponent<ButtonId>();
//				if(buttonpressed.id=="newgame")
//				{
//					gameOptions.SetActive(true);
//				}
//				if(buttonpressed.id=="quit")
//				{
//					messageBox.SetActive(true);
//					
//				}
//				if(buttonpressed.id=="about")
//				{
//					about.SetActive(true);
//				}
				if(buttonpressed.id=="backhome")
				{
					about.SetActive(false);
					MainScreen.SetActive(true);
					
				}
//				if(buttonpressed.id=="classic")
//				{
//				 	Application.LoadLevel("Classic");
//				}
//				else if(buttonpressed.id=="arcade")
//				{
//				 	Application.LoadLevel("Arcade");
//				}
				if(buttonpressed.id=="yes")
					{
						Application.Quit(); 
					}
				else if(buttonpressed.id=="no")
				{
						messageBox.SetActive(false);
						newgame.tag="freze";
						aboutgame.tag="frenzy";
						QuitGame.tag="bomb";
				}
				
				if(buttonpressed.id=="facebook")
				{
					Application.OpenURL("https://www.facebook.com/multichoiceapps");
				}
				if(buttonpressed.id=="twitter")
				{
					Application.OpenURL("https://twitter.com/multichoiceapps");
				}
				if(buttonpressed.id=="sound")
				{
					//Application.OpenURL("https://www.facebook.com/multichoiceapps");
					isactive = !isactive;
					cross.SetActive(isactive);
					AudioListener.pause=isactive;
				}
			}
		}
	}
	if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			messageBox.SetActive(true);
			
		}

}
}
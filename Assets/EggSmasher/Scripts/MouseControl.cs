// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour {
//control by mouse - can be easly translate to control by touch etc

//
//Vector3 originPosition;
//Quaternion originRotation;
//float shake_decay;
//float shake_intensity;;
//private Atest textmeshnew;

public GameObject Frenzy;
public bool  isfrenzy =false;

public bool  isfrezed =false;
public GameObject freeze;

public int num=1;

public int life=3;
public GameObject[] lifeArray;
public int count;
public float timertest=0;
public float t=0;
//TextMesh scoretext;
public GameObject scoretext;
public GameObject high;
public int raycastCount = 10;
	
public bool  started = false;
public Vector3 start;
public Vector3 end;

public int mouseMode = 0;

private float cSize;
Vector2 screenInp;

public bool  fire = false;
public bool  fire_prev = false;
public bool  fire_down = false;
public bool  fire_up = false;

public LineRenderer trail;

public float trial_alpha = 0.0f;

public AudioClip[] splatSfx;
public GameObject[] splashPrefab;
public GameObject[] splashFlatPrefab;
public GameObject[] feedBack;
public AudioClip swingSfx;

public int linePart = 0;
public float lineTimer = 0.0f;

public Vector3[] trailPositions = new Vector3[10];
public bool  blockSfx = false;

public int points = 0;
public static int classicHighScore = 0;
public static int arcadeHighScore =0;
void  Awake (){
 		//textmeshnew=this.GetComponent<"EasyFontTextMesh">();
 	//cSize = Screen.width * 0.01f;
}
void  Start (){
	classicHighScore = PlayerPrefs.GetInt("classicHighScore", 0);
	arcadeHighScore = PlayerPrefs.GetInt("arcadeHighScore", 0);
	
	//PlayerPrefs.SetInt("highscoreCurrent", best);
	//best=PlayerPrefs.GetInt("highscoreCurrent");
	//print (Application.loadedLevelName);
}

IEnumerator  PrepareFrenzy (){
						GameObject.Find("FruitDispenser").GetComponent<FruitDispenser>().pause = true;
						Frenzy.SetActive(true);
						isfrenzy=true;
						yield return new WaitForSeconds(10.0f);
						isfrenzy=false;
						Frenzy.SetActive(false);
						GameObject.Find("FruitDispenser").GetComponent<FruitDispenser>().pause = false;
}
IEnumerator  PrepareFreze (){
						Time.timeScale=0.5f;
						num=10;
						isfrezed=true;
						freeze.SetActive(true);
						yield return new WaitForSeconds(10.0f);
						freeze.SetActive(false);
						Time.timeScale=1;
						num=5;
						isfrezed=false;
}


//explode object
void  BlowObject (RaycastHit hit ){
			
			
			if (hit.collider.gameObject.tag != "destroyed")
				{
					count++;
					 t=timertest;
					
					timertest=Time.time;
					//if(timertest>0.5f)
					Vector3 splashZ= hit.point;
					
					hit.collider.gameObject.GetComponent<CreateOnDestroy>().Kill();
					Destroy(hit.collider.gameObject);
					//count=0;
					GetComponent<AudioSource>().PlayOneShot(splatSfx[Random.Range(0,splatSfx.Length)],1.0f);
					
					int index= 0;
					if (hit.collider.tag=="red") index = 0;
					if (hit.collider.tag=="yellow") index = 1;
					if (hit.collider.tag=="green") index = 2;
					
					splashZ.z = 4; //front
					GameObject.Instantiate(splashPrefab[index],splashZ,Quaternion.identity);
						
					splashZ.z = 10;	//back
					 GameObject.Instantiate(splashFlatPrefab[index],splashZ,Quaternion.identity);
					

//**********************************************************************************************************************************************8					
					if(hit.collider.gameObject.tag=="freze")
					{
						StartCoroutine(PrepareFreze());
					}
					if(hit.collider.gameObject.tag=="frenzy")
					{
						StartCoroutine(PrepareFrenzy());
					}

//***********************************************************************************************************************************************
					//if bomb inc points
					if (hit.collider.gameObject.tag !="bomb") 
						{
							points++;
							if(timertest-t<0.08f && !isfrenzy) 
//							if(timertest-t<0.08f) 	
								{
									splashZ.z=6;
									GameObject insFeedback=GameObject.Instantiate(feedBack[0],splashZ,Quaternion.identity);
									Destroy(insFeedback,1);
									points+=20;
								}
							//count = 0;
						} 
					
						else 
						{
							if(Application.loadedLevelName=="Arcade")
								{
									points-=10;
									if (points<0) points = 0;
								}
							else if(Application.loadedLevelName=="Classic")
								{
									
									if(life<=1)
									GetComponent<EndLevelScript>().done = true;
									lifeArray[life-1].SetActive(false);
									//lifeArray[]
									life--;
									
//									Shake();
									print("Enrty");
								}
							
						}
						
						
					
					
				}
				if(points > classicHighScore && Application.loadedLevelName=="Classic") 
				{
					classicHighScore = points;
				}
				else if(points > arcadeHighScore && Application.loadedLevelName=="Arcade")
				{
					arcadeHighScore = points;
				}
				//else count = 0;	
					hit.collider.gameObject.tag = "destroyed";
				
					

}

//send vertex position of trail into trail object
void  SendTrailPosition (){

int index= 0;
foreach(Vector3 v in trailPositions) 
	{
		trail.SetPosition(index,v);
		index++;
	}

}

//add vertex position of trail (array)
void  AddTrailPosition (){

if (linePart>9) {

for (int i=0;i<=8;i++) trailPositions[i] = trailPositions[i+1];
trailPositions[9] = Camera.main.ScreenToWorldPoint(new Vector3(start.x, start.y, 10));


} else {

for (int ii=linePart;ii<=9;ii++) 
				trailPositions[ii] = Camera.main.ScreenToWorldPoint(new Vector3(start.x, start.y, 10));		

}

}

//play sound of swing
IEnumerator  PlaySfx ()
{

	if (blockSfx) yield break;
	blockSfx = true;
	GetComponent<AudioSource>().PlayOneShot(swingSfx,1.0f);
	yield return new WaitForSeconds(swingSfx.length);
	blockSfx = false;

}

//control script
void  Control (){

		//first time DOWN button
		if (fire_down)
		{
			trial_alpha = 1.0f;
			linePart = 0;
			start = screenInp;
			end = screenInp;
			
			AddTrailPosition();
				
			started = true;
			lineTimer = 0.25f;
		}
		
		//continous hold
		if (fire && started)
		{
		
			start = screenInp;
		
			//distance on world space
			Vector3 a= Camera.main.ScreenToWorldPoint(new Vector3(start.x, start.y, 10));
			Vector3 b= Camera.main.ScreenToWorldPoint(new Vector3(end.x, end.y, 10));

			if (Vector3.Distance(a, b) > 0.4f)
			{
				StartCoroutine(PlaySfx());
			}
			
			if (Vector3.Distance(a,b)>0.1f) {
			
				lineTimer = 0.5f;
				AddTrailPosition();
				linePart++;
			}
		
			end = screenInp;
			
			trial_alpha = 0.75f;
		}
		
		//if trial alpha is more than 0.5f - perform raycast of cut
		if (trial_alpha>0.5f) 
		{

			for (int p= 0; p<8 ; p++) 
			{
				for (int i= 0; i < raycastCount; i++)
				{
					Ray ray = Camera.main.ScreenPointToRay(Vector3.Lerp(Camera.main.WorldToScreenPoint(trailPositions[p]),
									Camera.main.WorldToScreenPoint(trailPositions[p+1]), i * 1.0f / raycastCount * 1.0f));
					Debug.DrawLine(ray.origin,ray.direction * 10,Color.red,1.0f);
					
					RaycastHit hit;
					
					if (Physics.Raycast(ray, out hit,100,(1<<10)))//cast a ray only against the 10th layer and ignore all other colliders.
					{
						BlowObject(hit);
						
					}
					
					//Debug.Log(count);
				}
			}
			
		} 
		
		if (trial_alpha==0) linePart=0;
				
		lineTimer -= num*Time.deltaTime;
		//lineTimer -= num;
		
		if (lineTimer<=0.0f) {
			AddTrailPosition();
			linePart++;
			lineTimer = 0.01f;
		}
		
		
		if (fire_up && started) started = false;
		
		//copy array to trail
		SendTrailPosition();

}

void  Update (){
//		if(shake_intensity > 0)
//			{
//		      transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
//		      transform.rotation = Quaternion(
//		      originRotation.x + Random.Range(-shake_intensity,shake_intensity)*.2,
//		      originRotation.y + Random.Range(-shake_intensity,shake_intensity)*.2,
//		      originRotation.z + Random.Range(-shake_intensity,shake_intensity)*.2,
//		      originRotation.w + Random.Range(-shake_intensity,shake_intensity)*.2);
//		      shake_intensity -= shake_decay;
//   			}
		//guiText.text = "nHigh Score: " + highScore;
		//if(started ) count++;
		//else if(!started) count=0;
		//Counter();
		//Debug.Log("Timer: "+timertest+"n"+"T:"+t+" Diff: "+(timertest-t));	
		//scoretext.text=points.ToString() ;
		scoretext.GetComponent<EasyFontTextMesh>().Text=points.ToString();
	
		if(Application.loadedLevelName=="Arcade")
		{
			high.GetComponent<EasyFontTextMesh>().Text=arcadeHighScore.ToString();
		}
		else if(Application.loadedLevelName=="Classic")
		{
			high.GetComponent<EasyFontTextMesh>().Text=classicHighScore.ToString();
		}
		Vector2 Mouse;
		
		if (Time.timeScale<=0) return;//lineTimer=Time.deltaTime;
		
		//here you can add touch control
		
		Mouse.x = Mathf.Clamp((Input.mousePosition.x - (Screen.width/2)) / Screen.width*2,-1,1);
		Mouse.y = Mathf.Clamp(-(Input.mousePosition.y - (Screen.height/2)) / Screen.height*2,-1,1);		
		
		
		screenInp = Mouse;
		
		screenInp.x = (screenInp.x + 1) * 0.5f;
		screenInp.y = (-screenInp.y + 1) * 0.5f;

		screenInp.x *= Screen.width;
		screenInp.y *= Screen.height;
		
		fire_down = false;
		fire_up = false;
		
		fire = Input.GetMouseButton(0);
		
		if (fire && !fire_prev) fire_down = true;
		if (!fire && fire_prev) fire_up = true;
		fire_prev = fire;
		
		Control();
				
		Color c1=Color.cyan;
		Color c2=Color.white;
		trail.SetColors(c1,c2);		
		
		if (trial_alpha>0) trial_alpha -= 20*Time.deltaTime;
		

}

//void  Shake (){
//   originPosition = transform.position;
//   originRotation = transform.rotation;
//   shake_intensity = .3;
//   shake_decay = 0.002f;
//}
}
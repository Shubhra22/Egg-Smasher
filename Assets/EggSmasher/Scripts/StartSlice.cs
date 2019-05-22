// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class StartSlice : MonoBehaviour {
public int num=1;
//static bool  isdestroyed=false;
public int a =4;
public int b =10;
	public int raycastCount = 10;
	
	public bool  started = false;
	public Vector3 start;
	public Vector3 end;

	public int mouseMode = 0;

    private float cSize;
	public Vector2 screenInp;

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

void  Start (){
	
}

//explode object
IEnumerator  BlowObject ( RaycastHit hit  ){
			
			
			if (hit.collider.gameObject.tag != "destroyed")
				{
					Vector3 splashZ= hit.point;
					//hit.collider.gameObject.SetActive(false);
					hit.collider.gameObject.GetComponent<CreateOnDestroy>().Kill();
					//Destroy(hit.collider.gameObject);
					hit.collider.gameObject.SetActive(false);
					
					//count=0;
					GetComponent<AudioSource>().PlayOneShot(splatSfx[Random.Range(0,splatSfx.Length)],1.0f);
					
					int index= 0;
					if (hit.collider.tag=="red") index = 0;
					if (hit.collider.tag=="yellow") index = 1;
					if (hit.collider.tag=="green") index = 2;
					
					splashZ.z = a; //front
					//FIXME_VAR_TYPE ins= 
					GameObject.Instantiate(splashPrefab[index],splashZ,Quaternion.identity);
						
					splashZ.z = b;	//back
					//FIXME_VAR_TYPE ins2=
					 GameObject.Instantiate(splashFlatPrefab[index],splashZ,Quaternion.identity);
					 yield return  new WaitForSeconds (0.9f);
					 hit.collider.gameObject.SetActive(true);
					 //if(hit.collider.gameObject.tag == "bomb")
					 	
	}
					//if(isdestroyed)
					 //hit.collider.gameObject.tag = "destroyed";
				
					

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
IEnumerator  PlaySfx (){

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
					
					if (Physics.Raycast(ray,out hit,100,(1<<10)))//cast a ray only against the 10th layer and ignore all other colliders.
					{
						StartCoroutine(BlowObject(hit));
						
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
				
		//FIXME_VAR_TYPE c1= Color(1,1,0,trial_alpha);
		//FIXME_VAR_TYPE c2= Color(1,0,0,trial_alpha);
		Color c1=Color.cyan;
		Color c2=Color.white;
		trail.SetColors(c1,c2);		
		
		if (trial_alpha>0) trial_alpha -= 20*Time.deltaTime;
		

}
}
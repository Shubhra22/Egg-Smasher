// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class FruitDispenser : MonoBehaviour {
//fruit dispenser - dispense fruits with differend speed and time and gravitation


public GameObject[] fruits;
public GameObject bomb;
//float xd=99898979;
public float z;
public AudioClip sfx;
public bool  pause = false;
public float timer = 3.0f;
private bool  started = false;
private float powerMod;
public GameObject Frenzy;
public GameObject Freze;

public GameObject FrenzyEggs;

	void ChangeGravity(int val)
	{
		Vector3 gravity = Physics.gravity;
		gravity.y = val;
		Physics.gravity = gravity;
	}
void  Awake (){

	//Random.seed = xd;	
	ChangeGravity(-4);
	powerMod = 0.70f;
/*
if (SharedSettings.LoadLevel==1) {
	Random.seed = 356355;
	Physics.gravity.y = -2;
	powerMod = 1.0f;
}
if (SharedSettings.LoadLevel==2) {
	Random.seed = 12411245;
	Physics.gravity.y = -3;
	powerMod = 0.825f;
}
if (SharedSettings.LoadLevel==3) {
	Random.seed = 898979;
	Physics.gravity.y = -4;
	powerMod = 0.70f;

}
if (SharedSettings.LoadLevel==4) {
	Random.seed = 64223459;
	Physics.gravity.y = -5;
	powerMod = 0.625f; 
}*/

}

void  Update (){
//Debug.Log(Physics.gravity.y);

//if(FrenzyEggs.activeSelf==true) pause=true;
//else if(FrenzyEggs.activeSelf==false) pause=false;
if (pause) return;

timer -= Time.deltaTime;

if (timer<=0.0f && !started) {
timer = 0.0f;
started = true;
}

if (started) {

/*if (SharedSettings.LoadLevel==1) {
	if (timer<=0.0f) {
		FireUp();
		timer = 2.0f;
	}
}

if (SharedSettings.LoadLevel==2) {
	if (timer<=0.0f) {
		FireUp();
		timer = 1.75f;
	}
}
*/
if (SharedSettings.LoadLevel==3) {
	if (timer<=0.0f) {
		FireUp();
		timer = 0.9f;
	}
}
/*
if (SharedSettings.LoadLevel==4) {
	if (timer<=0.0f) {
		FireUp();
		timer = 1.0f;
	}
}
*/
}

}

void  Spawn ( string isbomb  ){

	float x = Random.Range(-4.5f,4.5f);
	float y =Random.Range(-3,3);
	z += 1;
	if (z>=5.0f) z = 0;
	GameObject ins;
	
	if (isbomb=="Fruit") 
		ins = GameObject.Instantiate(fruits[Random.Range(0,fruits.Length)],transform.position + new Vector3(x,0,z),Random.rotation);
	
	else if(isbomb=="Frenzy")
		ins = GameObject.Instantiate(Frenzy,transform.position + new Vector3(x,0,z),Random.rotation);
	
	else if(isbomb=="Freze")
	ins = GameObject.Instantiate(Freze,transform.position + new Vector3(x,0,z),Random.rotation);
	
	else
		ins = GameObject.Instantiate(bomb,transform.position + new Vector3(x,0,z),Random.rotation);

	float power= Random.Range(1.5f,1.8f) * -Physics.gravity.y * 1.5f * powerMod;

	
	Vector3 direction= new Vector3(-x * 0.05f * Random.Range(0.3f,0.8f),1,0);

	direction.z = 0.0f;

	ins.GetComponent<Rigidbody>().velocity =  direction * power;
	
	GetComponent<AudioSource>().PlayOneShot(sfx,1.0f);
	ins.GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere * 0.1f,ForceMode.Impulse);

}

void  FireUp (){

	if (pause) return;

	Spawn("Fruit");
	
	
	if (SharedSettings.LoadLevel==3 && Random.Range(0,10)<4) {
		Spawn("Fruit");
	}

	if (SharedSettings.LoadLevel==3 && Random.Range(0,700)<90) {
		Spawn("Bomb");
	}
	
	if (SharedSettings.LoadLevel==3 && Random.Range(0,350)<10) {
		Spawn("Frenzy");
	}
	
	if (SharedSettings.LoadLevel==3 && Random.Range(0,300)<10) {
		Spawn("Freze");
	}
}

    void  OnTriggerEnter ( Collider other  ){
        Destroy(other.gameObject);
    }
}
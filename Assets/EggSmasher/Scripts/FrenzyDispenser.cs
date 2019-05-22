using UnityEngine;
using System.Collections;

public class FrenzyDispenser : MonoBehaviour {

	public GameObject right;
	public GameObject left;
	public GameObject[] fruits;
	public float z;

	public float spawnrate = 1;
	private float nextSpawn;
	void  Start (){

	}

	void  Update (){
		float y =Random.Range(-1,0);
		z+=1;
		if(z>=5)
		{
			z=0;
		}
		if(Time.time>nextSpawn && GameObject.Find("MainScripts").GetComponent<EndLevelScript>().completed==false)
		{
			nextSpawn = Time.time+spawnrate;
			GameObject insRight= GameObject.Instantiate(fruits[Random.Range(0,fruits.Length)],right.transform.position +new Vector3(0,y,z),Random.rotation);
			GameObject insLeft= GameObject.Instantiate(fruits[Random.Range(0,fruits.Length)],left.transform.position +new Vector3(0,y,z),Random.rotation);
			
			float power= Random.Range(1.5f,1.8f) * Physics.gravity.y;
			Vector3 direction= new Vector3(1,-y* 0.05f * Random.Range(0.3f,0.8f) ,0);
			direction.z = 0.0f;

			insRight.GetComponent<Rigidbody>().velocity =  direction * power;
			//audio.PlayOneShot(sfx,1.0f);
			insRight.GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere*0.2f ,ForceMode.Impulse);	
			
			insLeft.GetComponent<Rigidbody>().velocity =  -direction * power;
			insLeft.GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere*0.2f ,ForceMode.Impulse);	
			
		}
	}
}
using UnityEngine;
using System.Collections;

public class CreateOnDestroy : MonoBehaviour 
{
	public bool  killed = false;
	public GameObject[] prefab;

	public void  Kill (){

		if (killed) return;

		killed = true;

		foreach(var p in prefab) 
		{
			GameObject ins= GameObject.Instantiate(p,transform.position,Random.rotation);
			if (ins.GetComponent<Rigidbody>()) {
				ins.GetComponent<Rigidbody>().velocity =  Random.onUnitSphere + Vector3.up;	
				//audio.PlayOneShot(sfx,1.0f);
				ins.GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere * 1,ForceMode.Impulse);
			}
		}

	}


}
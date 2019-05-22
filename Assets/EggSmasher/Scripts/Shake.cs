// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour 
{
	Vector3 originPosition;
	Quaternion originRotation;
	float shake_decay;
	float shake_intensity;
 
//	void  OnGUI (){
//		if (GUI.Button ( new Rect(20,40,80,20), "Shake")) {
//			Shake();
//		}
//	}
 
	void  Update ()
	{
		if(shake_intensity > 0)
		 {
			transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
			transform.rotation = new Quaternion(
				originRotation.x + Random.Range(-shake_intensity,shake_intensity)*.2f,
				originRotation.y + Random.Range(-shake_intensity,shake_intensity)*.2f,
				originRotation.z + Random.Range(-shake_intensity,shake_intensity)*.2f,
				originRotation.w + Random.Range(-shake_intensity,shake_intensity)*.2f);
			shake_intensity -= shake_decay;
		}
	}
 
//	void Shake (){
//		originPosition = transform.position;
//		originRotation = transform.rotation;
//		shake_intensity = .3f;
//		shake_decay = 0.002f;
//	}
}
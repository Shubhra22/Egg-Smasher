// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {


	public float a;
	public float b;
	public float c;

	void  Start (){

	}

	void  Update (){
		transform.Rotate(a,b,c);
	}
}
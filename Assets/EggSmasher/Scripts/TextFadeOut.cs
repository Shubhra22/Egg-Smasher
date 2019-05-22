// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class TextFadeOut : MonoBehaviour {
//fade out GUIText color


	public float speed = 1.0f;

	void  Update (){

		if (enabled)
		{
			Color c = GetComponent<GUIText>().material.color;
			c.a -= Time.deltaTime * speed;
			GetComponent<GUIText>().material.color = c;
		}
	}
}
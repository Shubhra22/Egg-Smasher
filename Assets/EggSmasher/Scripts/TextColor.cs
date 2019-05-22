// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class TextColor : MonoBehaviour {
//change GUIText color


	public Color color;

	void  Awake (){

		GetComponent<GUIText>().material.color = color;

	}
}
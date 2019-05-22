using UnityEngine;
using System.Collections;

public class TextGlow : MonoBehaviour {
// make text glow - example from pause menu



	void  Update ()
	{

		Color c = GetComponent<GUIText>().material.color;
		c.a = Mathf.PingPong(Time.time,0.5f)+0.5f;
		GetComponent<GUIText>().material.color = c;
	}
}
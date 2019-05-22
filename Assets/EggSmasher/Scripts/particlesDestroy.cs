﻿// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class particleDestroy : MonoBehaviour 
{
	//particle auto destroy for shrunken

	private ParticleSystem thisParticleSystem;

	void  Start (){
		thisParticleSystem = this.GetComponent<ParticleSystem>();
		if (!thisParticleSystem.loop) {
			Destroy(this.gameObject, thisParticleSystem.startLifetime);
		}
	}
}
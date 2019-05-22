using UnityEngine;
using System.Collections;

public class GUIRatioFix : MonoBehaviour {

	public float m_NativeRatio= 1.3333333333333f;
    
	void  Awake (){    
		float currentRatio= (Screen.width+0.0f) / Screen.height;
		Vector3 scale = transform.localScale;
		scale.x *= m_NativeRatio / currentRatio;
		transform.localScale = scale;
	}
}
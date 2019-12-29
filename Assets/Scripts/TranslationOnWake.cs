using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationOnWake : MonoBehaviour {
	public float rot_magnitude = 1f;
	public float translation_magnitude = 1f;
	// Use this for initialization
	void Start () {
		transform.Translate(new Vector3(Random.Range(0.0f, 1.0f)* translation_magnitude, 
			Random.Range(0.0f, 1.0f)* translation_magnitude,
			Random.Range(0.0f, 1.0f)* translation_magnitude));

		transform.Rotate(Random.Range(0.0f, 1.0f) * rot_magnitude,
						Random.Range(0.0f, 1.0f) * rot_magnitude,
						Random.Range(0.0f, 1.0f) * rot_magnitude);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

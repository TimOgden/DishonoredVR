using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectBlinkSpheres : MonoBehaviour {
	static LineRenderer lineR;
	public Transform top, bottom;
	private Renderer rend;
	// Use this for initialization
	void Start () {
		lineR = GetComponent<LineRenderer>();
		rend = bottom.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(lineR.enabled) {
			lineR.SetPosition(0,top.position);
			lineR.SetPosition(1,bottom.position);
		}
	}

	public static void SetActive(bool active) {
		lineR.enabled = active;
	}
}

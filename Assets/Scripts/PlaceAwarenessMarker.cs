using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceAwarenessMarker : MonoBehaviour {
	private Camera camera;
	private Transform markerLoc;
	private RectTransform rect;
	public AlertnessMeter alertness;

	// Use this for initialization
	void Start () {
		camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		markerLoc = alertness.entity.Find("AwarenessLocation");
		rect = alertness.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 heading = markerLoc.position - camera.transform.position;
		if(Vector3.Dot(camera.transform.forward, heading) > 0) {
			Vector3 placement = camera.WorldToScreenPoint(markerLoc.position);
			placement.x = Mathf.Clamp(placement.x,0,camera.pixelWidth);
			placement.y = Mathf.Clamp(placement.y,0,.96f*camera.pixelHeight);
			rect.anchoredPosition = placement;
		}
	}
}

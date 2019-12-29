using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GuardDetection : MonoBehaviour {
	public static float visualCheckFrequency = 2f; // How often should all the guards raycast out
	public float viewAngle = 100;
	public float alertnessLevel = 0f;
	public float alertnessSpeed = 1f;
	public float numTimesSightedMultiplier = 1f;
	public float closenessMultiplier = 1f;
	private Transform player;
	public Transform eyes;
	private Transform raycastTargets;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Corvo").transform;
		raycastTargets = player.Find("RaycastTargets");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider other) {
		if(other.tag=="Player") {
			Vector3 direction = other.transform.position - eyes.position;
			float angle = Vector3.Angle(eyes.forward, direction);
			if(angle <= viewAngle / 2f) {
				// Player is within eyesight
				RaycastHit hit;
				int numTargetsSighted = 0; //The number of targets (head, hands, feet) that the guard sees
				for(int i = 0; i<raycastTargets.childCount; i++) {
					Vector3 targetDirection = raycastTargets.GetChild(i).position - eyes.position;;
					if(Physics.Raycast(eyes.position, targetDirection, out hit)) {
						if(hit.collider.tag == "Player") {
							Debug.DrawRay(eyes.position, targetDirection, Color.green);
							numTargetsSighted++;
						}
						else
							Debug.DrawRay(eyes.position, targetDirection, Color.yellow);
					} else {
						Debug.DrawRay(eyes.position, targetDirection, Color.red);
					}
				}
				float dist = Vector3.Distance(other.transform.position, eyes.position);
				alertnessLevel += Time.deltaTime * alertnessSpeed
									+ numTargetsSighted * numTimesSightedMultiplier
									+ (1/dist) * closenessMultiplier;
				alertnessLevel = Mathf.Min(alertnessLevel,1);
				Debug.Log(alertnessLevel);
			}
		}
	}
}
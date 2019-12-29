using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Blink : Power {
	public Transform player;
	public float blinkTime;
	public static string powersButton = "Fire2";
	public float maxDistance;
	public Transform blink_top, blink_bottom;
	public float whereToClimb;
	public float pushAmount;
	public Material[] climbColors;

	private Renderer top_rend, bottom_rend;
	private CharacterController cc;
	private Image frozenTimePanel;
	int layer_mask = 1;
	// Use this for initialization
	void Start () {
		top_rend = blink_top.GetComponent<Renderer>();
		bottom_rend = blink_bottom.GetComponent<Renderer>();
		top_rend.enabled = false;
		bottom_rend.enabled = false;
		if(mana==null)
			mana = GameObject.Find("Canvas").GetComponent<ManaManager>();
		cc = transform.parent.GetComponent<CharacterController>();
		frozenTimePanel = GameObject.Find("FrozenTimeScreen").GetComponent<Image>();
		StartCoroutine("ShouldFreezeTime");
	}
	
	void LateUpdate () {
		mana.Regenerate(this);
		if(Input.GetButton(powersButton) && mana.CanUse(this)) {
			//Debug.Log("Yo");
			RaycastHit hit;
			if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),
					out hit, maxDistance, layer_mask)) {
				Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.green);
				blink_top.position = hit.point;
				if(isClimbable(out hit)) {
					if(hit.collider.tag == "Climbable") {
						bottom_rend.enabled = false;
						ConnectBlinkSpheres.SetActive(false);
						top_rend.material = climbColors[1];
					} else {
						top_rend.material = climbColors[0];

					}
				} else {
					// Calculate where to put bottom blink thing
					RaycastHit topHit;
					Physics.Raycast(blink_top.position, -Vector3.up, out topHit, maxDistance, layer_mask);
					blink_bottom.position = blink_top.position - Vector3.up*topHit.distance;
					//Debug.Log(topHit.collider.name);
					//Make them both visible
					top_rend.enabled = true;
					bottom_rend.enabled = true;
					ConnectBlinkSpheres.SetActive(true);
					top_rend.material = climbColors[0];

				}

			} else {
				Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.red);
				blink_top.position = transform.position + transform.forward * maxDistance;

				// Calculate where to put bottom blink thing
				RaycastHit topHit;
				Physics.Raycast(blink_top.position, -Vector3.up, out topHit, 1000, layer_mask);
				blink_bottom.position = blink_top.position - Vector3.up*topHit.distance;
				//blink_top.position = topHit.point;
				//Make them both visible
				top_rend.enabled = true;
				bottom_rend.enabled = true;
				ConnectBlinkSpheres.SetActive(true);
				top_rend.material = climbColors[0];
			}
			
		}

		if(Input.GetButtonUp(powersButton)) {
			cc.enabled = true;
			if(mana.CanUse(this)) {
				mana.Use(this);
				RaycastHit hit;
				if(isClimbable(out hit)) {
					blink_top.position = hit.point;
					float heightOfCollider = hit.transform.localScale.y * ((BoxCollider)hit.collider).size.y;
					float topMost = hit.transform.position.y + heightOfCollider / 2;
					Vector3 desired_position = 
						new Vector3(blink_top.position.x,topMost+1f,blink_top.position.z) + transform.forward * pushAmount;
					//transform.parent.position = desired_position;
					StartCoroutine(GoToBlink(desired_position, blinkTime));
				} else {
					//Get distance from ground
					RaycastHit aboveHit;
					Physics.Raycast(blink_top.position, -Vector3.up, out aboveHit, 1000, layer_mask);
					Vector3 blinkpos_ycorrected = new Vector3(blink_top.position.x, 
							aboveHit.point.y+1f, blink_top.position.z);
					if(hit.distance < 1) {
						//player.position = blinkpos_ycorrected;
						StartCoroutine(GoToBlink(blinkpos_ycorrected, blinkTime));
					} else {
						//player.position = blink_top.position;
						StartCoroutine(GoToBlink(blink_top.position, blinkTime));
					}
				}
				ConnectBlinkSpheres.SetActive(false);
				top_rend.enabled = false;
				bottom_rend.enabled = false;
			}
		}
	}

	private IEnumerator GoToBlink(Vector3 pos, float duration) {
		float elapsedTime = 0;
		Vector3 initial_pos = transform.position;
		cc.detectCollisions = false;
		while(elapsedTime < duration) {
			elapsedTime += Time.deltaTime;
			transform.parent.position = Vector3.Lerp(initial_pos, pos, elapsedTime / duration);
			yield return new WaitForEndOfFrame();
		}
		cc.detectCollisions = true;
		
	}

	private bool isClimbable(out RaycastHit hit) {
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),
					out hit, maxDistance, layer_mask)) {
			blink_top.position = hit.point;
			float heightOfCollider = hit.transform.localScale.y * hit.collider.bounds.size.y;
			float topMost = hit.transform.position.y + heightOfCollider / 2;
			return hit.transform.tag == "Climbable" && topMost - hit.point.y < whereToClimb
				&& Vector3.Dot(Vector3.up, hit.normal) < .5;
		} else {
			return false;
		}
	}

	private IEnumerator ShouldFreezeTime() {
		while(true) {
			if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A)
					&& !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)
					&& Input.GetButton(powersButton)) {
				cc.enabled = false;
				frozenTimePanel.enabled = true;
			} else {
				cc.enabled = true;
				frozenTimePanel.enabled = false;
			}
			yield return new WaitForSeconds(.1f);
		}
	}
}
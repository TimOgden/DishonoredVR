using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour {
	private float holdLength;
	public float howLongToHold;
	public float jumpForce;
	private CharacterController cc;
	private bool canDoubleJump = true;

	void Start() {
		cc = GetComponent<CharacterController>();
	}
	// Update is called once per frame
	void Update () {
		if(canDoubleJump) {
			/*
			if(Input.GetButton("Jump")) {
				holdLength += Time.deltaTime;
				if(holdLength > howLongToHold) {
					holdLength = 0;
					Jump();
					canDoubleJump = false;
				}
			}*/

			if(Input.GetButtonDown("Jump")) {
				RaycastHit hit;
				Physics.Raycast(transform.position, -Vector3.up, out hit, .5f);
				Debug.Log("Can Double Jump:" + canDoubleJump);
				holdLength = 0;
				if(!cc.isGrounded)
					canDoubleJump = false;
				Jump();

				
				
			}
			/*
			if(Input.GetButtonUp("Jump")) {
				holdLength = 0;
			}*/
		}
		if(cc.isGrounded)
			canDoubleJump = true;
	}

	void Jump() {
		cc.Move(Vector3.up * jumpForce);
	}
}

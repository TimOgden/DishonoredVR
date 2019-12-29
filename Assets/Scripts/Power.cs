using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour {
	public float manaConsumption;
	public float manaRegenerable;
	public ManaManager mana;
	// Use this for initialization
	void Start () {
		mana = GameObject.Find("Canvas").GetComponent<ManaManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}

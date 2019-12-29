using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManaManager : MonoBehaviour {
	[SerializeField]
	public Slider currentMana;
	[SerializeField]
	public Slider maxRegen;
	public float regenSpeed;
	public float cooldownLength;
	public float regencooldownLength;
	private bool canCast = true;
	private bool canRegen = true;
	public bool freezeRegen = false;
	// Use this for initialization
	void Start () {
		
	}

	void Update () {

	}

	
	public bool CanUse(Power power) {
		return currentMana.value >= power.manaConsumption && canCast;
	}

	// If you can regen mana (only based on time since last cast)
	public bool CanRegen() {
		return canRegen;
	}

	public void Use(Power power) {
		currentMana.value -= power.manaConsumption;
		maxRegen.value = currentMana.value + power.manaRegenerable;
		StartCoroutine("Cooldown");
		StartCoroutine("RegenCooldown");
	}

	public void Regenerate(Power power) {
		if(currentMana.value < maxRegen.value && canRegen && !freezeRegen) {
			currentMana.value += Time.deltaTime * regenSpeed;
		}
	}

	public IEnumerator Cooldown() {
		canCast = false;
		float initial_time = 0;
		while(initial_time < cooldownLength) {
			initial_time += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		canCast = true;
	}

	public IEnumerator RegenCooldown() {
		canRegen = false;
		float initial_time = 0;
		while(initial_time < regencooldownLength) {
			initial_time += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		canRegen = true;
	}
}

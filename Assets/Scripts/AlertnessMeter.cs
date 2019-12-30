using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AlertnessMeter : MonoBehaviour {
	private Image meterIcon;
	private Slider slider;
	private Image fillColor;
	private float alertness = 0f;
	public Transform entity;
	public Sprite regularMeter, activeMeter;

	// Use this for initialization
	void Awake () {
		meterIcon = GetComponent<Image>();
		slider = transform.Find("Slider").GetComponent<Slider>();
		fillColor = transform.Find("Slider").Find("Fill Area").GetChild(0).GetComponent<Image>();
	}

	public float GetAlertness() {
		return alertness;
	}

	public void SetAlertness(float alertness) {
		this.alertness = alertness;
	}

	public void AddAlertness(float amount) {
		alertness += amount;
		alertness = Mathf.Min(alertness,2.0f);
		if(this.alertness >= 1 && meterIcon.sprite == regularMeter) {
			slider.value = 0;
			meterIcon.sprite = activeMeter;
			fillColor.color = Color.red;
		}
		if(alertness <= 0f)
			Destroy(this.gameObject);
	}

	public void UpdateAlertness() {
		float prev_val = slider.value;
		float current_val = alertness;
		if(alertness >= 1)
			current_val = alertness - 1;
		slider.value = current_val;
		if(alertness > 0) {
			Color c = meterIcon.color;
			meterIcon.color = new Color(c.r,c.g,c.b,255);
		}
		//StartCoroutine(LerpTo(prev_val, current_val, GuardDetection.visualCheckFrequency));
	}

	IEnumerator UpdateValues() {
		while(true) {
			if(alertness>=1 && meterIcon.sprite == regularMeter) {
				slider.value = 0;
				meterIcon.sprite = activeMeter;
				fillColor.color = Color.red;
			}
			yield return new WaitForSeconds(.5f);
		}
	}

	IEnumerator LerpTo(float start, float end, float duration) {
		float startTime = 0;
		while(startTime < duration) {
			startTime += Time.deltaTime;
			float visibleAlertness = Mathf.Lerp(start, end, startTime/duration);
			slider.value = visibleAlertness;
			yield return null;
		}
	}
}

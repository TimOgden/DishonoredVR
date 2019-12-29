using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AlertnessMeter : MonoBehaviour {
	private Image meterIcon;
	private Slider slider;
	private Image fillColor;
	private bool alert = false;

	public Sprite regularMeter, activeMeter;
	// Use this for initialization
	void Start () {
		meterIcon = GetComponent<Image>();
		slider = transform.Find("Slider").GetComponent<Slider>();
		fillColor = transform.Find("Slider").Find("Fill Area").GetChild(0).GetComponent<Image>();
		StartCoroutine("UpdateValues");
	}

	public bool GetAlert() {
		return alert;
	}

	public void SetAlert(bool alert) {
		this.alert = alert;
	}
	
	IEnumerator UpdateValues() {
		while(true) {
			if(slider.value == 1) {
				slider.value = 0;
				meterIcon.sprite = activeMeter;
				fillColor.color = Color.red;
			}
			yield return new WaitForSeconds(.5f);
		}
	}
}

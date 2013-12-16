using UnityEngine;
using System.Collections;

public class MinMaxSlider : MonoBehaviour {

	public UILabel min;
	public UILabel max;
	public UILabel valueText;

	public delegate void sliderChanged(MinMaxSlider slider);
	public event sliderChanged ValueChanged;

	public float minValue = 1.0f;
	public float maxValue = 1.0f;
	public float value;

	UISlider slider;

	// Use this for initialization
	void Start () {
		slider = GetComponent<UISlider>();
		EventDelegate.Add(slider.onChange, this.onValueChanged);
	}

	void OnDestroy() {
		EventDelegate.Remove(slider.onChange, this.onValueChanged);
	}
	
	// Update is called once per frame
	void onValueChanged () {
		min.text = "" + minValue;
		max.text = "" + maxValue;
		value = minValue + slider.value * (maxValue - minValue);
		valueText.text = string.Format("{0:0.00}", value);

		if(ValueChanged != null) ValueChanged(this);
	}
}

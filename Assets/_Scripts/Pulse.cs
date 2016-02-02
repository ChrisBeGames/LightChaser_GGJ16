using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour {

	public float minIntensity, maxIntensity, rate;
	public Light theLight;
	float current = 0;

	void Update () {
		current += rate * Time.deltaTime;

		float pos = current % 2;
		if (pos > 1)
			pos = -pos + 2;

		theLight.intensity = Mathf.Lerp (minIntensity, maxIntensity, pos);
	}
}

using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

	public float rate;

	void Update () {
		transform.Rotate (new Vector3 (0, rate, 0));
	}
}

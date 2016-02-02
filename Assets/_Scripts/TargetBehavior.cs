using UnityEngine;
using System.Collections;

public class TargetBehavior : MonoBehaviour {

	[SerializeField]
	float _rotationSpeed;

	[SerializeField]
	Vector3 _rotationAxis;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Rotate (_rotationAxis * Time.deltaTime * _rotationSpeed);
	}
}

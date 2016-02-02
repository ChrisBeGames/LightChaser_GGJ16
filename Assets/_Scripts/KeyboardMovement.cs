using UnityEngine;
using System.Collections;

public class KeyboardMovement : MonoBehaviour {

	public Rigidbody player;

	public AudioSource oarLeft, oarRight;
	public float volumeLimit;

	private float volumeLeft, volumeRight;

	void Start () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<Rigidbody> ();
		}
	}
	
	void Update () {
		float left = Input.GetAxis ("Row Left");
		float right = Input.GetAxis ("Row Right");
		float intent = Input.GetAxis ("Intent");

		DoMovement (left, right, intent);
	}

	public void DoMovement(float left, float right, float intent = 1){
		Vector3 leftPos = transform.TransformPoint (new Vector3 (-1, 0, 0));
		Vector3 rightPos = transform.TransformPoint (new Vector3 (1, 0, 0));

		player.AddForceAtPosition (transform.forward * (intent * left), leftPos);
		player.AddForceAtPosition (transform.forward * (intent * right), rightPos);

		if (oarLeft && oarRight) {
			if (left != 0) 
				volumeLeft = volumeLeft + Time.deltaTime > volumeLimit ? volumeLimit : volumeLeft + Time.deltaTime;
			else
				volumeLeft = volumeLeft - Time.deltaTime < 0 ? 0 : volumeLeft - Time.deltaTime;

			if (right != 0) 
				volumeRight = volumeRight + Time.deltaTime > volumeLimit ? volumeLimit : volumeRight + Time.deltaTime;
			else
				volumeRight = volumeRight - Time.deltaTime < 0 ? 0 : volumeRight - Time.deltaTime;

			oarLeft.volume = volumeLeft;
			oarRight.volume = volumeRight;
		}
	}
}

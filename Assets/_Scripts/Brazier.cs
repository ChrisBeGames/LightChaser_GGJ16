using UnityEngine;
using System.Collections;

public class Brazier : MonoBehaviour {

	public bool isLit;
	public GameObject fire, targetPuzzle;

	void OnTriggerEnter (Collider other) {
		print (other.name + " entered.");
		if (other.tag == "Ignitor") {
			if (!isLit)
				FindObjectOfType<PixAudio> ().Success ();

			isLit = true;
			fire.SetActive (true);

			if (targetPuzzle)
				targetPuzzle.BroadcastMessage ("EnablePuzzle", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Update is called once per frame
	void Update () {
		if (fire.GetComponent<ParticleSystem>()) {
			if (isLit && !fire.activeSelf) {
				OnTriggerEnter (GameObject.FindGameObjectWithTag ("Ignitor").GetComponent<Collider> ());
			} else if (!isLit && fire.activeSelf) {
				fire.SetActive (false);
			}
		}
	}
}

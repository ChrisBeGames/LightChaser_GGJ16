using UnityEngine;
using System.Collections;

public class FinalTorchPuzzle : MonoBehaviour {

	void OnCollisionEnter(Collision coll){
		if (coll.collider.tag == "Ignitor") {
			GetComponentInParent<AudioSource> ().Play ();
			FindObjectOfType<GameEnd> ().torchPuzzle = true;
		}
	}
}

using UnityEngine;
using System.Collections;

public class RockButton : MonoBehaviour {

	void OnCollisionEnter(Collision coll){
		if (coll.collider.tag == "Ignitor") {

		}
	}
}

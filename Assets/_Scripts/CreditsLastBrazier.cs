using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreditsLastBrazier : MonoBehaviour {

	public List<Brazier> braziers = new List<Brazier>();

	void Update () {
		if (!braziers.Exists (obj => !obj.isLit))
			GetComponent<Brazier> ().isLit = true;
	}
}

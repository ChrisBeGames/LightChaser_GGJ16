using UnityEngine;
using System.Collections;

public class ToggleLightsInChildren : MonoBehaviour {

	public bool lightsOn { get; private set; }
	public bool starterPiece;

	[SerializeField]
	private bool puzzleEnabled;
	private bool puzzleComplete;


	void EnablePuzzle(){
		puzzleEnabled = true;

		if (starterPiece && !puzzleComplete)
			ToggleLights ();
	}

	void CompletePuzzle(){
		puzzleComplete = true;
		lightsOn = true;
		foreach (var t in GetComponentsInChildren<Light>()) {
			t.enabled = true;
			t.color = Color.green;
		}
	}

	public void ToggleLights()
	{
		//Toggle bool
		lightsOn = !lightsOn;

		//Switch the lights
		foreach (var t in GetComponentsInChildren<Light>()) {
			t.enabled = lightsOn;
		}
	}

	void OnCollisionEnter(Collision coll){
		if (puzzleEnabled && !puzzleComplete) {
			if (coll.collider.tag == "Ignitor")
				ToggleLights ();

			foreach (var t in GetComponent<Neighbors>().neighbors)
				t.SendMessage ("ToggleLights");
		}
	}
}

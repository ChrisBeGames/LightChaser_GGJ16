using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartBuoys : MonoBehaviour {

	List<ToggleLightsInChildren> toggles = new List<ToggleLightsInChildren>();

	public bool puzzleComplete;

	void EnablePuzzle(){
		if (!puzzleComplete)
			GetComponent<AudioSource> ().Play ();
	}

	void Update(){
		if (!toggles.Exists (obj => !obj.lightsOn) && !puzzleComplete) {
			foreach (var t in toggles)
				t.SendMessage ("CompletePuzzle");

			puzzleComplete = true;

			FindObjectOfType<GameEnd> ().lightsPuzzle = true;
		}
	}

	void Start(){
		toggles.AddRange (GetComponentsInChildren<ToggleLightsInChildren> ());
	}

}

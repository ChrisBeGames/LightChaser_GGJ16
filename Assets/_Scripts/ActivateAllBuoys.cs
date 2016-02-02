using UnityEngine;
using System.Collections;

public class ActivateAllBuoys : MonoBehaviour {

	public bool puzzleComplete;

	void EnablePuzzle(){
		if (!puzzleComplete)
		{
			foreach (var t in FindObjectsOfType<SpawnEdge>()) {
				foreach (var u in t.spawnedBuoys) {
					u.SendMessage ("EnablePuzzle");
				}
			}
			puzzleComplete = true;

			GetComponent<AudioSource> ().Play ();

			FindObjectOfType<GameEnd> ().buoyPuzzle = true;
		}
	}
}
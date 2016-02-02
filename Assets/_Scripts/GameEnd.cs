using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEnd : MonoBehaviour {

	public bool buoyPuzzle, lightsPuzzle, torchPuzzle;

	public bool gameOver { get; private set; }

	void Update(){
		if (buoyPuzzle && lightsPuzzle && torchPuzzle && !gameOver) {
			gameOver = true;
			print ("GAME OVER, YOU WIN. CONGRATS");

			transform.position = new Vector3 (0, 10, 0);
			GetComponent<AudioSource> ().volume = 1;
			GameObject.Find ("Music").GetComponent<AudioSource> ().volume = 0;
		}
	}
}

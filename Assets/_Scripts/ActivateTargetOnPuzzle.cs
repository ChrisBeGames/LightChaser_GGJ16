using UnityEngine;
using System.Collections;

public class ActivateTargetOnPuzzle : MonoBehaviour {

	public GameObject target;
	public bool toggle;

	void EnablePuzzle(){
		if (target) {
			if (toggle)
				target.SetActive (!target.activeSelf);
			else if (!target.activeSelf)
				target.SetActive (true);
		}
	}
}

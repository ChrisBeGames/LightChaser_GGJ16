using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RaiseRock : MonoBehaviour
{

	public List<Brazier> comboBraziers = new List<Brazier>();
	public Vector3 rockDestination;

	private bool puzzleEnabled, lerping;
	private float progress;

	void EnablePuzzle()
	{
		if (!comboBraziers.Exists(obj => !obj.isLit))
		{
			puzzleEnabled = true;
			lerping = true;

			foreach (var t in comboBraziers)
				t.GetComponentInChildren<AutoDisable>().doShutoff = false;

			GetComponent<AudioSource>().Play();
		}
	}

	void Update()
	{
		if (puzzleEnabled && lerping)
		{
			progress = Mathf.Clamp01(progress + Time.deltaTime / 100);

			transform.position = Vector3.Lerp(transform.position, rockDestination, progress);

			if (progress == 1)
				lerping = false;
		}
	}
}

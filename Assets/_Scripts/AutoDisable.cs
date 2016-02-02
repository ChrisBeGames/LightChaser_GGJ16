using UnityEngine;
using System.Collections;

public class AutoDisable : MonoBehaviour {

	public bool doShutoff = true, playAudioOnShutoff;
	public float time;
	public AudioSource src;

	void OnEnable(){
		Invoke ("Shutoff", time);
	}

	void Shutoff(){
		if (doShutoff) {
			if (playAudioOnShutoff) {
				if (src == null)
					src = GetComponent<AudioSource> ();

				src.Play ();
			}

			GetComponentInParent<Brazier> ().isLit = false;

			gameObject.SetActive(false);

			FindObjectOfType<PixAudio> ().Failure ();
		}
	}
}

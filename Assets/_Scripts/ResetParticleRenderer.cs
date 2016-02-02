using UnityEngine;
using System.Collections;

public class ResetParticleRenderer : MonoBehaviour {

	public ParticleSystemRenderer psr;

	void Update(){
		if (psr.enabled = false)
			psr.enabled = true;
	}
}

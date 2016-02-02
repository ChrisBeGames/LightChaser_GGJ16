using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PixAudio : MonoBehaviour
{

	public AudioClip c0, c1, c2, c3, c4, c5;
	private bool b0, b1, b2, b3, b4, b5, dni;
	//Starting clips and Do Not Interrupt
	public List<AudioClip> grunt = new List<AudioClip>();
	public List<AudioClip> fly = new List<AudioClip>();
	public List<AudioClip> success = new List<AudioClip>();
	public List<AudioClip> failure = new List<AudioClip>();
	public List<AudioClip> idle = new List<AudioClip>();

	AudioSource src;

	void OnCollisionEnter(Collision coll)
	{
		Grunt();
	}


	public void Grunt()
	{
		if (grunt.Count > 0)
			Play(grunt [Random.Range(0, grunt.Count)]);
	}

	public void Fly()
	{
		if (fly.Count > 0)
			Play(fly [Random.Range(0, fly.Count)]);
	}

	public void Success()
	{
		if (success.Count > 0)
			Play(success [Random.Range(0, success.Count)]);
	}

	public void Failure()
	{
		if (failure.Count > 0)
			Play(failure [Random.Range(0, failure.Count)]);
	}

	public void Idle()
	{
		if (idle.Count > 0)
			Play(idle [Random.Range(0, idle.Count)], false);
	}

	public void Play(AudioClip clip, bool stop = true)
	{
		if (!src.isPlaying || (!success.Contains(src.clip) && stop && !dni))
		{
			src.Stop();
			src.clip = clip;
			src.Play();
		}
	}

	void Update()
	{
		if (!src.isPlaying)
		{
			dni = false;
			if (!b0)
			{
				b0 = true;
				Play(c0);
			}
			else if (!b1)
			{
				b1 = true;
				Play(c1);
			}
			else if (!b2)
			{
				b2 = true;
				Play(c2);
			}
			else if (!b3)
			{
				b3 = true;
				Play(c3);
			}
			else if (!b4)
			{
				b4 = true;
				Play(c4);
			}
			else if (!b5)
			{
				b5 = true;
				Play(c5);
			}
				
		}
	}

	void Start()
	{
		src = GetComponent<AudioSource>();
		src.clip = c0;
		src.PlayDelayed(2);
		dni = true;
		b0 = true;
	}
}

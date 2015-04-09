using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationScript : MonoBehaviour {

	public Animator animationClip;
	public bool playOnAwake;
	// Use this for initialization
	void Start ()
	{
		if(playOnAwake)
		{
			PlayAnimation();
		}
		else
		{

		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void PlayAnimation()
	{
	}

	public void StopAnimation()
	{
	}
}

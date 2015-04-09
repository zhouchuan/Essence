using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlotModule {

	private PlotModule() { }

	private static PlotModule instance = null;
	public static PlotModule Instance()
	{
		if(instance == null)
		{
			instance = new PlotModule();
		}
		return instance;
	}

	private int captionIndex = 0;
	public int CaptionIndex
	{
		set { captionIndex = value; }
		get { return captionIndex; }
	}


	private AudioClipContainer audioContainer;
	public AudioClipContainer AudioContainer
	{
		set { audioContainer = value; }
		get { return audioContainer; }
	}

	private float currentAudioLength = 0f;
	public float CurrentAudioLength
	{
		set { currentAudioLength = value; }
		get { return currentAudioLength; }
	}

	// play plot in order
	public void SetPlotCaption(AudioSource playerAudio, UILabel captionLabel, List<string> plotList)
	{
		if(captionIndex < plotList.Count)
		{
			playerAudio.clip = audioContainer.audioPlotList[captionIndex];
			currentAudioLength = playerAudio.clip.length;
			if(!playerAudio.isPlaying)
			{
				playerAudio.Play();
			}
			captionLabel.text = plotList[captionIndex];
		}
		captionIndex++;
	}

	// Play Plot by index and show the relative caption
	public void SetCaptionByIndex(AudioSource playerAudio, UILabel captionLabel, List<string> plotList, int index)
	{
		if(index < plotList.Count)
		{
			playerAudio.clip = audioContainer.audioPlotList[index];
			currentAudioLength = playerAudio.clip.length;
			if(!playerAudio.isPlaying)
			{
				playerAudio.Play();
			}
			captionLabel.text = plotList[index];
		}
	}

	// Just Play Audio by index
	public void PlayAudioByIndex(AudioSource playerAudio, int index)
	{
		playerAudio.clip = audioContainer.audioPlotList[index];
		currentAudioLength = playerAudio.clip.length;
		if(!playerAudio.isPlaying)
		{
			playerAudio.Play();
		}
	}

	// Just Play Audio by name
	public void PlayAudioByName(AudioSource playerAudio, string audioName)
	{
		playerAudio.clip = audioContainer.AudioClipsDic[audioName];
		currentAudioLength = playerAudio.clip.length;
		if(!playerAudio.isPlaying)
		{
			playerAudio.Play();
		}
	}

	public void ResetCaptionIndex()
	{
		captionIndex = 0;
	}

	public bool IsPlotPlaying(AudioSource playerAudio)
	{
		return playerAudio.isPlaying;
	}

	public bool IsPlotFinished(List<string> plotList)
	{
		return captionIndex >= plotList.Count;
	}
}

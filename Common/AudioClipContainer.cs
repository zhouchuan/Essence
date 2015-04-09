using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioClipContainer : MonoBehaviour {

	public List<AudioClip> audioPlotList;
	public List<float> AudioLengthList {set; get;}
	public List<float> AudioTriggerList {set; get;}
	public Dictionary<string,AudioClip> AudioClipsDic { set; get; }
	void Awake()
	{
		AudioClipsDic = new Dictionary<string, AudioClip>();
		AudioLengthList = new List<float>();
		AudioTriggerList = new List<float>();
		GetAudioLength();
		GetAudioTriggerTime();
	}

	void GetAudioTriggerTime()
	{
		foreach(AudioClip clip in audioPlotList)
		{
			if(AudioTriggerList.Count > 0)
			{
				if(clip != null)
				{
					float triggerTime = AudioTriggerList[AudioTriggerList.Count - 1] + clip.length;
					AudioTriggerList.Add(triggerTime);
				}
				else
				{
					AudioTriggerList.Add(AudioTriggerList[AudioTriggerList.Count - 1]);
				}
			}
			else
			{
				AudioTriggerList.Add(clip.length);
			}
		}
	}

	void GetAudioLength()
	{
		foreach(AudioClip clip in audioPlotList)
		{
			if(clip != null)
			{
				AudioLengthList.Add(clip.length);
				AudioClipsDic.Add(clip.name, clip);
			}
			else if(AudioLengthList.Count > 0)
			{
				AudioLengthList.Add(0.0f);
			}
		}
	}

}
